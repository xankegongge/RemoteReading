using System;
using System.Collections.Generic;
using System.Text;
using ESPlus.Application.CustomizeInfo.Server;
using ESPlus.Application.CustomizeInfo;
using ESPlus.Application.Basic.Server;
using ESPlus.Application;
using ESFramework.Server.UserManagement;
using ESPlus.Rapid;
using ESPlus.Serialization;
using System.Drawing;
using ESFramework;
using JustLib;
using System.Globalization;
using RemoteReading.Core;
namespace RemoteReading.Server
{
    /// <summary>
    /// 自定义信息处理器。
    /// （1）当用户上线时，在其LocalTag挂接资料信息。
    /// </summary>
    internal class CustomizeHandler : IIntegratedCustomizeHandler
    {
        private GlobalCache globalCache;
        private IRapidServerEngine rapidServerEngine;
        private OfflineFileController offlineFileController;      
        public void Initialize(GlobalCache db, IRapidServerEngine engine, OfflineFileController fileCtr)
        {
            this.globalCache = db;
            this.rapidServerEngine = engine;           
            this.offlineFileController = fileCtr;

            this.rapidServerEngine.UserManager.SomeOneDisconnected += new ESBasic.CbGeneric<UserData, ESFramework.Server.DisconnectedType>(UserManager_SomeOneDisconnected);
            this.rapidServerEngine.UserManager.SomeOneConnected += new ESBasic.CbGeneric<UserData>(UserManager_SomeOneconnected);
            this.rapidServerEngine.GroupController.BroadcastReceived += new ESBasic.CbGeneric<string, string, int, byte[]>(GroupController_BroadcastReceived);
            this.rapidServerEngine.MessageReceived += new ESBasic.CbGeneric<string, int, byte[], string>(rapidServerEngine_MessageReceived);
            this.rapidServerEngine.CustomizeController.TransmitFailed += new ESBasic.CbGeneric<Information>(CustomizeController_TransmitFailed);
        }

        

        void CustomizeController_TransmitFailed(Information information)
        {
            OfflineMessage msg = new OfflineMessage(information.SourceID, information.DestID, information.InformationType, information.Content);
            this.globalCache.StoreOfflineMessage(msg);
        }

        void rapidServerEngine_MessageReceived(string sourceUserID, int informationType, byte[] info, string tag)
        {
            if (informationType == InformationTypes.GetAllMedicalReading)
            {
                List<MedicalReading> listMedical = this.globalCache.GetMedicalReadingsByUserID(sourceUserID, true);
               byte []mes= ESPlus.Serialization.CompactPropertySerializer.Default.Serialize<List<MedicalReading>>(listMedical); //0922   
                if (listMedical != null)
                    this.rapidServerEngine.SendMessage(sourceUserID,InformationTypes.GetAllMedicalReading,mes,null); 
            }
            if (informationType == InformationTypes.GetMDImages)
            {
                string gid = System.Text.Encoding.UTF8.GetString(info);//根据ID获取信息
                List<byte []> listMaps = this.globalCache.GetBitmapsByMedicalReadingGuid(gid);
                byte[] imagesinfo = CompactPropertySerializer.Default.Serialize<List<byte[]>>(listMaps);//序列化
             //   if (listMaps != null)
                    this.rapidServerEngine.CustomizeController.SendBlob(sourceUserID, InformationTypes.GetMDImages, imagesinfo, 2048);
                //else
                //    this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.GetMDImages, null, null);
            }
            if (informationType == InformationTypes.Advice)
            {
                try
                {
                    string userid = System.Text.Encoding.UTF8.GetString(info);//根据ID获取信息
                    string advice = tag;
                    if (this.globalCache.InsertAdvice(userid, advice))
                    {
                        this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.Advice, new byte[1] { 0 }, true, ActionTypeOnChannelIsBusy.Continue);
                    }
                    else
                    {
                        this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.Advice, new byte[1] { 0x05 }, true, ActionTypeOnChannelIsBusy.Continue);
                    }

                }
                catch (Exception ex)
                {

                }
            }
            if (informationType == InformationTypes.GetMDSmallImages)
            {
                try
                {
                    string gid = System.Text.Encoding.UTF8.GetString(info);//根据ID获取信息
                    if (ValidUtils.IsGuidByReg(gid))
                    {

                        List<byte[]> listMaps = this.globalCache.GetSmallBitmapsByMedicalReadingGuid(gid);
                        if (listMaps != null)
                        {
                            byte[] imagesinfo = CompactPropertySerializer.Default.Serialize<List<byte[]>>(listMaps);//序列化
                            //   if (listMaps != null)
                            this.rapidServerEngine.CustomizeController.SendBlob(sourceUserID, InformationTypes.GetMDImages, imagesinfo, 1024);
                        }
                        else
                        {
                            //服务器操作内部错误
                            this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.GetMDImages, new byte[1] { 1 }, true, ActionTypeOnChannelIsBusy.Continue);

                        }
                    }
                    else//序列化问题
                        this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.GetMDImages, new byte[1] { 2 }, true, ActionTypeOnChannelIsBusy.Continue);

                }
                catch (System.Exception ex)
                {//未知错误
                    this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.GetMDImages, new byte[1]{3}, true,ActionTypeOnChannelIsBusy.Continue);
                }
               
            }
            if (informationType == InformationTypes.GetSingelImage)
            {
                try
                {
	                string rpgid = System.Text.Encoding.UTF8.GetString(info);//根据ID获取信息
                    if (ValidUtils.IsGuidByReg(rpgid))
                    {
                        byte[] singleImage = this.globalCache.GetSingleBitmapsByReadingPictureGuid(rpgid);
                        if (singleImage != null && singleImage.Length > 0)
                        {
                            byte[] imagesinfo = CompactPropertySerializer.Default.Serialize<byte[]>(singleImage);//序列化
                            //   if (listMaps != null)
                            this.rapidServerEngine.CustomizeController.SendBlob(sourceUserID, InformationTypes.GetSingelImage, imagesinfo, 2048);
                            //else
                        }
                        else
                            this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.GetSingelImage, new byte[1] { 1 }, true, ActionTypeOnChannelIsBusy.Continue);

                    }
                    else
                        this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.GetSingelImage, new byte[1] { 2 }, true, ActionTypeOnChannelIsBusy.Continue);

                }
                catch (System.Exception ex)
                {
                    this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.GetSingelImage, new byte[1] { 3 }, true, ActionTypeOnChannelIsBusy.Continue);
                }
                //    this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.GetMDImages, null, null);
            }
            if (informationType == InformationTypes.Chat)//聊天信息
            {             
                string destID = tag;
                if (this.rapidServerEngine.UserManager.IsUserOnLine(destID))
                {
                   // this.rapidServerEngine.SendMessage(destID, informationType, info, sourceUserID, 2048);
                    rapidServerEngine.CustomizeController.Send(destID, informationType, info);
                }
                else
                {
                    OfflineMessage msg = new OfflineMessage(sourceUserID, destID, informationType, info);
                    this.globalCache.StoreOfflineMessage(msg);//在内存缓存离线消息，并不是在数据库中缓存
                }
               // this.globalCache.StoreChatRecord(sourceUserID, destID, info);//暂时不插入服务器数据库
                return;
            }
            if (informationType == InformationTypes.SendMedicalReadingRejectedReason)//拒绝用户阅片请求
            {
                try
                {
	                string []complex = tag.Split(';');
	                if (complex.Length == 2)
	                {
	                    string id = complex[1];
	                    string rejectedreason = complex[0];
	                    if (this.globalCache.UpdateMedicalReading(id, rejectedreason))
	                    {
	                        MedicalReading mr = this.globalCache.GetMedicalReadingByGuid(id);
	                        byte[] bupdatetime = System.Text.Encoding.UTF8.GetBytes(mr.CreatedTime);
	                        //向客户端发送更新阅片消息，已拒绝
	                        if (this.rapidServerEngine.UserManager.IsUserOnLine(mr.UserIDTo))
	                        {
	                            this.rapidServerEngine.SendMessage(mr.UserIDTo, InformationTypes.SendMedicalReadingRejectedReason, bupdatetime, tag);
	                        }
	                        else//在数据库中缓存离线阅片消息，当客户上线时转发给他；
	                        {
	                            this.globalCache.SoreOffMedicalReading(mr.UserIDTo, mr);
	                        }
	                        if (this.rapidServerEngine.UserManager.IsUserOnLine(mr.UserIDFrom))
	                        {
	                            this.rapidServerEngine.SendMessage(mr.UserIDFrom, InformationTypes.SendMedicalReadingRejectedReason, bupdatetime, tag);
	                        }
	                        else//在数据库中缓存离线阅片消息，当客户上线时转发给他；
	                        {
	                            this.globalCache.SoreOffMedicalReading(mr.UserIDFrom, mr);
	                        }
	                      
	                   }
	                    else
	                    {
	                        this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.SendMedicalReadingRejectedReason, new byte[1] { 1 }, null);
	
	                    }
	                }
	                else//序列化错误
	                {
	                    this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.SendMedicalReadingRejectedReason, new byte[1] { 2 }, null);
	
	                }
                }
                catch (System.Exception ex)
                {
                    this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.SendMedicalReadingRejectedReason, new byte[1] { 3 }, null);
	
                }
            }
            
            if (informationType == InformationTypes.SendMedicalReadingReceived)//专家接收用户阅片请求
            {
                string id = tag;
                try
                {
	                if (id!=null)
	                {
	                    if (this.globalCache.UpdateMedicalReading(id))//通知客户端更新阅片状态
	                    {
	                        //更新成功，向该客户发送更新状态消息；
	                        MedicalReading mr = this.globalCache.GetMedicalReadingByGuid(id);
	                        //向客户端发送更新阅片消息，正在处理
	                        byte[] bupdatetime = System.Text.Encoding.UTF8.GetBytes(mr.CreatedTime);
	                        //向客户端发送更新阅片消息，已拒绝
	                        if (this.rapidServerEngine.UserManager.IsUserOnLine(mr.UserIDTo))
	                        {
	                            this.rapidServerEngine.SendMessage(mr.UserIDTo, InformationTypes.SendMedicalReadingReceived, bupdatetime, id);
	                        }
	                        else//在数据库中缓存离线阅片消息，当客户上线时转发给他；
	                        {
	                            this.globalCache.SoreOffMedicalReading(mr.UserIDTo, mr);
	                        }
	                        if (this.rapidServerEngine.UserManager.IsUserOnLine(mr.UserIDFrom))
	                        {
	                            this.rapidServerEngine.SendMessage(mr.UserIDFrom, InformationTypes.SendMedicalReadingReceived, bupdatetime, id);  //更新成功
	                        }
	                        else//在数据库中缓存离线阅片消息，当客户上线时转发给他；
	                        {
	                            this.globalCache.SoreOffMedicalReading(mr.UserIDFrom, mr);
	                        }
	                  
	                        
	                    }
	                    else
	                    {
	                        this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.SendMedicalReadingReceived, new byte[1] { 1 }, null);
	
	                    }
	                }
	                else//更新失败;
	                {//byte  0x01 服务器数据库 操作失败， 0x02 序列化错误
	                    this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.SendMedicalReadingReceived, new byte[1] { 2 }, null);
	
	                }
                }
                catch (System.Exception ex)
                {
                    this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.SendMedicalReadingReceived, new byte[1] { 3 }, null);
                }
            }
            if (informationType == InformationTypes.SendUpdateMedicalReading)//更新阅片专家列表。专家提供
            {
                try
                {
	                List<ReadingPicture> listpics = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<List<ReadingPicture>>(info, 0);
	               
	                if (tag != null&&listpics!=null)
	                {
	                    if (this.globalCache.UpdateMedicalReading(tag,listpics))
	                    {
	                        //更新成功，向该客户发送更新状态消息；
	                        MedicalReading mr = this.globalCache.GetMedicalReadingByGuid(tag);
	                        string updatetime = mr.CreatedTime;
	                       string  id = updatetime + ";" + tag;//更新时间与mdid;
	                       byte [] listnewpics = ESPlus.Serialization.CompactPropertySerializer.Default.Serialize<List<ReadingPicture>>(mr.ListPics);
	                        //向客户端发送更新阅片消息，已完成；实时更新
	                       //向客户端发送更新阅片消息，已拒绝
	                       if (this.rapidServerEngine.UserManager.IsUserOnLine(mr.UserIDTo))
	                       {
	                           this.rapidServerEngine.SendMessage(mr.UserIDTo, InformationTypes.SendUpdateMedicalReading, listnewpics, id);  //更新成功
	                       }
	                       else//在数据库中缓存离线阅片消息，当客户上线时转发给他；
	                       {
	                           this.globalCache.SoreOffMedicalReading(mr.UserIDTo, mr);
	                       }
	                       if (this.rapidServerEngine.UserManager.IsUserOnLine(mr.UserIDFrom))
	                       {
	
	                           this.rapidServerEngine.SendMessage(mr.UserIDFrom, InformationTypes.SendUpdateMedicalReading, listnewpics, id);  //更新成功
	                       }
	                       else//在数据库中缓存离线阅片消息，当客户上线时转发给他；
	                       {
	                           this.globalCache.SoreOffMedicalReading(mr.UserIDFrom, mr);
	                       }
	                     
	                    }
	                    else//更新失败;
	                    {//byte  0x01 服务器数据库 操作失败， 0x02 序列化错误
	                        this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.SendUpdateMedicalReading, new byte[1]{1}, null);  
	
	                    }
	                }
	                else//序列化有问题;
	                {
	                    this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.SendUpdateMedicalReading, new byte[1] {2}, null);
	
	                }
                }
                catch (System.Exception ex)
                {
                    this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.SendUpdateMedicalReading, new byte[1] { 3 }, null);
	
                }
            }
            if (informationType == InformationTypes.UpdateUserInfo)//更新用户信息
            {
                GGUser user;
                try
                {
                    user = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<GGUser>(info, 0);

                }
                catch (Exception ex)
                {
                    return;
                }
                 GGUser old = this.globalCache.GetUser(user.UserID);
                if (old == null||user==null)
                    return;
                this.globalCache.UpdateUser(user);
                List<string> friendIDs = this.globalCache.GetFriends(sourceUserID);
                byte[] subData = ESPlus.Serialization.CompactPropertySerializer.Default.Serialize<GGUser>(user.PartialCopy); //0922   
                foreach (string friendID in friendIDs)
                {
                    if (friendID != sourceUserID)
                    {
                        //可能要分块发送
                        if (this.rapidServerEngine.UserManager.IsUserOnLine(friendID))
                            this.rapidServerEngine.CustomizeController.Send(friendID, InformationTypes.UserInforChanged, subData, true, ActionTypeOnChannelIsBusy.Continue);
                    }
                }
                return;
            }


            if (informationType == InformationTypes.UpdateCheckUserInfo)//更新用户信息
            {
                try
                {
                    GGUser user = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<GGUser>(info, 0);
                    if (this.globalCache.IsCheckUserExist(user.UserID))//已经提交过申请
                    {
                        this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.UpdateCheckUserInfo, new byte[1] { 4 }, true, ActionTypeOnChannelIsBusy.Continue);
                        return;
                    }
                    user.CheckType = CheckType.Update;//更新类型;
                    user.IsChecking = true;//处于待审核状态;
	                if (user != null)
	                {
	                    Hospital hospital = globalCache.GetHostpitalName(user.UserContact.HospitalID);//最好给他一个hos对象;
	                    user.Hospi = hospital;//设置医院;
	                    user.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//设置更新时间;  
	                    byte[] btime = System.Text.Encoding.UTF8.GetBytes(user.CreateTime);
	                    if (this.globalCache.InsertCheckUserList(user))//插入审核缓存列表，暂时不更新数据库；
	                    {
	                        //通知发送方已经发送给客服审核了
	                        if (this.rapidServerEngine.UserManager.IsUserOnLine(sourceUserID))
	                            this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.UpdateCheckUserInfo, btime, true, ActionTypeOnChannelIsBusy.Continue);
                            SendToServicesCheck(user); //发送给所有在线的客服审核
	                        
	
	                        
	                    }
	                    else//插入失败，有可能是已经提交过了，有未审核的资料。
	                    {
	                        //回送给发送方，插入失败;
	                       // if (this.rapidServerEngine.UserManager.IsUserOnLine(sourceUserID))
	                            this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.UpdateCheckUserInfo, new byte[1]{1}, true, ActionTypeOnChannelIsBusy.Continue);
	                    }
	                }
	                else//user序列化有问题
	                    this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.UpdateCheckUserInfo, new byte[1] { 2 }, true, ActionTypeOnChannelIsBusy.Continue);
	            
                }
                catch (System.Exception ex)
                {//未知错误
                    this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.UpdateCheckUserInfo, new byte[1] { 3 }, true, ActionTypeOnChannelIsBusy.Continue);
                }
         } 
            return;
        }
        public bool SendToServicesCheck(GGUser sendUser)
        {
            //GGUser user = this.globalCache.GetUser(target);
            if (sendUser == null)
            {
                return false;

            }
            byte[] sendinfo = CompactPropertySerializer.Default.Serialize<GGUser>(sendUser);
            //获取在线的客服列表；
            List<string> servicers = this.globalCache.GetServicersID();
            if (servicers.Count > 0)
            {
                foreach (string servicer in servicers)
                {
                    //可能要分块发送
                    this.rapidServerEngine.CustomizeController.Send(servicer, InformationTypes.NewCheckUser, sendinfo, true, ActionTypeOnChannelIsBusy.Continue);
                }
            }
            else//这里暂时不保存离线客服消息
            {
                //this.globalCache.StoreOfflineServerMsg(sendUser);//保存离线
            }
            return true;
        }
       
        void GroupController_BroadcastReceived(string broadcasterID, string groupID, int broadcastType, byte[] broadcastContent )
        {
            if (broadcastType == BroadcastTypes.BroadcastChat)
            {
                this.globalCache.StoreGroupChatRecord(groupID, broadcasterID, broadcastContent);
            }
        }
        private void UserManager_SomeOneconnected(UserData data)
        {
            GGUser user = this.globalCache.GetUser(data.UserID);
            GGUser expert = this.globalCache.GetExpert(data.UserID);
            if (expert != null)
            {
                expert.UserStatus = UserStatus.Online;
            }
            if (user != null)
            {
                user.UserStatus = UserStatus.Online;
            }
            //重新登录就会发送离线消息;
            //SendOfflineMessage(user.UserID);//发送离线消息
            //SendOfflineMDMessage(user.UserID);//发送阅片离线消息：
            //List<string> contacts = this.globalCache.GetAllContacts(data.UserID);
            //UserStatusChangedContract contract = new UserStatusChangedContract(data.UserID, 2);
            //byte[] msg = ESPlus.Serialization.CompactPropertySerializer.Default.Serialize(contract);
            //foreach (string friendID in contacts)
            //{
            //    this.rapidServerEngine.CustomizeController.Send(friendID, InformationTypes.UserStatusChanged, msg);
            //}
            //return;
        }
        void UserManager_SomeOneDisconnected(UserData data, ESFramework.Server.DisconnectedType obj2)
        {
            GGUser user = this.globalCache.GetUser(data.UserID);
            GGUser expert = this.globalCache.GetExpert(data.UserID);
            if (expert != null)
            {
                expert.UserStatus = UserStatus.OffLine;
            }
            if (user != null)
            {
                user.UserStatus = UserStatus.OffLine;
            }
        }

        //发送离线消息给客户端
        public void SendOfflineMessage(string destUserID)
        {
            List<OfflineMessage> list = this.globalCache.PickupOfflineMessage(destUserID);
            if (list != null && list.Count > 0)
            {
                foreach (OfflineMessage msg in list)
                {
                    byte[] bMsg = CompactPropertySerializer.Default.Serialize<OfflineMessage>(msg);
                    this.rapidServerEngine.CustomizeController.SendBlob(msg.DestUserID, InformationTypes.OfflineMessage, bMsg, 2048);
                }
            }

        }
        //发送离线消息给客户端
        public void SendOfflineMDMessage(string destUserID)
        {
            List<MedicalReading> list = this.globalCache.PickupOfflineMDMessage(destUserID);
            if (list != null && list.Count > 0)
            {
                byte[] bMsg = CompactPropertySerializer.Default.Serialize<List<MedicalReading>>(list);//一次性发送
                    this.rapidServerEngine.SendMessage(destUserID, InformationTypes.OfflineMDMessage, bMsg, null);
            }

        }

        /// <summary>
        /// 处理来自客户端的消息。
        /// </summary> 
        public void HandleInformation(string sourceUserID, int informationType, byte[] info)
        {
           
            if (informationType == InformationTypes.SendMedicalReading)//处理医疗阅片信息
            {
                try
                {
                    MedicalReading md = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<MedicalReading>(info, 0);
                    string desTo = md.UserIDTo;
                    md.CreatedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    if (md.ListPics != null)//插入数据库；
                    {
                        MedicalReading mrnew= this.globalCache.InsertMedicalReading(md);//插入成功
                        if (mrnew.MedicalReadingID!= null)
                        {
                           // md.MedicalReadingID = mdid;
                            byte[] medical = ESPlus.Serialization.CompactPropertySerializer.Default.Serialize<MedicalReading>(mrnew);
                            //this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.MedicalInsertOKServerACK, medical, mrnew.MedicalReadingID);//向客户端发送插入成功消息
                            if (this.rapidServerEngine.UserManager.IsUserOnLine(sourceUserID))
                                this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.MedicalReadingAdd, medical, null, 1024);
                            else//在数据库中缓存离线阅片消息，当客户上线时转发给他；
                            {
                                this.globalCache.SoreOffMedicalReading(sourceUserID, mrnew);
                            }
                            //转发给另一个客户端，并保存;
                            if (this.rapidServerEngine.UserManager.IsUserOnLine(desTo))
                            {
                                this.rapidServerEngine.SendMessage(desTo, InformationTypes.MedicalReadingAdd, medical, null,1024);
                            }
                            else//在数据库中缓存离线阅片消息，当客户上线时转发给他；
                            {
                                this.globalCache.SoreOffMedicalReading(desTo, mrnew);
                                //OfflineMessage msg = new OfflineMessage(sourceUserID, destID, informationType, info);
                                //this.globalCache.StoreOfflineMessage(msg);

                            }
                        }
                    }
                    else
                  //  if (this.rapidServerEngine.UserManager.IsUserOnLine(sourceUserID))//插入失败;
                        this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.MedicalReadingAdd, null, "error", 1024);
                   
                    
                }
                catch (System.Exception ex)
                {
                    // System.Windows.Forms.MessageBox.Show(ex.ToString());
                 //   if (this.rapidServerEngine.UserManager.IsUserOnLine(sourceUserID))//插入失败;
                        this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.MedicalReadingAdd, null, "error", 1024);
                   
                }
                return;
            }
            if (informationType == InformationTypes.AddFriendCatalog)//添加好友目录
            {
                string catalogName = System.Text.Encoding.UTF8.GetString(info) ;
                this.globalCache.AddFriendCatalog(sourceUserID, catalogName);
                return;
            }
            
            if (informationType == InformationTypes.RemoveFriendCatalog)
            {
                string catalogName = System.Text.Encoding.UTF8.GetString(info);
                this.globalCache.RemoveFriendCatalog(sourceUserID, catalogName);
                return;
            }

            if (informationType == InformationTypes.ChangeFriendCatalogName)
            {
                ChangeCatalogContract contract = CompactPropertySerializer.Default.Deserialize<ChangeCatalogContract>(info, 0);
                this.globalCache.ChangeFriendCatalogName(sourceUserID, contract.OldName, contract.NewName);
                return;
            }

            if (informationType == InformationTypes.MoveFriendToOtherCatalog)//移动朋友去别的目录
            {
                MoveFriendToOtherCatalogContract contract = CompactPropertySerializer.Default.Deserialize<MoveFriendToOtherCatalogContract>(info, 0);
                this.globalCache.MoveFriend(sourceUserID,contract.FriendID, contract.OldCatalog, contract.NewCatalog);
                return;
            }

            if (informationType == InformationTypes.GetOfflineMessage)//获取离线消息
            {
                this.SendOfflineMessage(sourceUserID);//发送离线消息
                
                return;
            }
            if (informationType == InformationTypes.OfflineMDMessage)//获取离线消息
            {
               
                this.SendOfflineMDMessage(sourceUserID);//发送离线阅片
                return;
            }
            if (informationType == InformationTypes.GetOfflineFile)
            {
                this.offlineFileController.SendOfflineFile(sourceUserID);
                return;
            }

            if (informationType == InformationTypes.QuitGroup)//退出群组
            {
                string groupID = System.Text.Encoding.UTF8.GetString(info) ;
                this.globalCache.QuitGroup(sourceUserID, groupID);
                //通知其它组成员
                this.rapidServerEngine.GroupController.Broadcast(groupID, BroadcastTypes.SomeoneQuitGroup, System.Text.Encoding.UTF8.GetBytes(sourceUserID), ESFramework.ActionTypeOnChannelIsBusy.Continue);
              
                return;
            }

            if (informationType == InformationTypes.DeleteGroup)//删除群组
            {
                string groupID = System.Text.Encoding.UTF8.GetString(info);               
                //通知其它组成员
                this.rapidServerEngine.GroupController.Broadcast(groupID, BroadcastTypes.GroupDeleted, System.Text.Encoding.UTF8.GetBytes(sourceUserID), ESFramework.ActionTypeOnChannelIsBusy.Continue);
                this.globalCache.DeleteGroup(groupID);
                return;
            }

            if (informationType == InformationTypes.RemoveFriend)
            {
                string friendID = System.Text.Encoding.UTF8.GetString(info);
                this.globalCache.RemoveFriend(sourceUserID, friendID);
                //通知好友
                this.rapidServerEngine.CustomizeController.Send(friendID, InformationTypes.FriendRemovedNotify, System.Text.Encoding.UTF8.GetBytes(sourceUserID));
                return;
            }        

            if (informationType == InformationTypes.ChangeStatus)//改变状态
            {
                GGUser user = this.globalCache.GetUser(sourceUserID);

                int newStatus = BitConverter.ToInt32(info, 0);
                user.UserStatus = (UserStatus)newStatus;
                GGUser expert = this.globalCache.GetExpert(sourceUserID); ;
                if (expert != null)
                {
                    expert.UserStatus = (UserStatus)newStatus;
                }

                List<string> contacts = this.globalCache.GetAllContacts(sourceUserID);                          
                UserStatusChangedContract contract = new UserStatusChangedContract(sourceUserID, newStatus);
                byte[] msg = ESPlus.Serialization.CompactPropertySerializer.Default.Serialize(contract);
                foreach (string friendID in contacts)
                {
                    this.rapidServerEngine.CustomizeController.Send(friendID, InformationTypes.UserStatusChanged, msg);
                }                
                return;
            }
        }

        /// <summary>
        /// 处理来自客户端的同步调用请求。
        /// </summary>       
        public byte[] HandleQuery(string sourceUserID, int informationType, byte[] info)
        {
            if (informationType == InformationTypes.GetFriendIDList)
            {
                List<string> friendIDs = this.globalCache.GetFriends(sourceUserID);
                return CompactPropertySerializer.Default.Serialize<List<string>>(friendIDs);
            }
            if (informationType == InformationTypes.GetAllHospitals)
            {
                List<Hospital> hospitals = this.globalCache.GetAllHospitals();
                return CompactPropertySerializer.Default.Serialize<List<Hospital>>(hospitals);
            }
            if (informationType == InformationTypes.AddFriend)
            {
                AddFriendContract contract = CompactPropertySerializer.Default.Deserialize<AddFriendContract>(info ,0);
                bool isExist = this.globalCache.IsUserExist(contract.FriendID);
                if (!isExist)
                {
                    return BitConverter.GetBytes((int)AddFriendResult.FriendNotExist);
                }
                this.globalCache.AddFriend(sourceUserID, contract.FriendID ,contract.CatalogName);

                //0922
                GGUser owner = this.globalCache.GetUser(sourceUserID);
                byte[] ownerBuff = CompactPropertySerializer.Default.Serialize<GGUser>(owner);

                //通知对方
                this.rapidServerEngine.CustomizeController.Send(contract.FriendID, InformationTypes.FriendAddedNotify, ownerBuff, true, ESFramework.ActionTypeOnChannelIsBusy.Continue);
                return BitConverter.GetBytes((int)AddFriendResult.Succeed);
            }

            if (informationType == InformationTypes.GetAllContacts)
            {
                List<string> contacts = this.globalCache.GetAllContacts(sourceUserID);
                Dictionary<string, GGUser> contactDic = new Dictionary<string, GGUser>();
                foreach (string friendID in contacts)
                {
                    if (!contactDic.ContainsKey(friendID))
                    {
                        GGUser friend = this.globalCache.GetUser(friendID);
                        if (friend != null)
                        {
                            contactDic.Add(friendID, friend);
                        }
                    }
                }

                return CompactPropertySerializer.Default.Serialize<List<GGUser>>(new List<GGUser>(contactDic.Values));
            }
            if (informationType == InformationTypes.GetAllMedicalReading)//移动端分页获取阅片信息;
            {
                 List<MedicalReading> listMedical=null;
                if(info[0]==0x01)//专家

                    listMedical = this.globalCache.GetMedicalReadingsByUserID(sourceUserID, false);
                else
                    listMedical = this.globalCache.GetMedicalReadingsByUserID(sourceUserID, true);
                if (listMedical != null)

                    return CompactPropertySerializer.Default.Serialize<List<MedicalReading>>(listMedical);
                else
                    return null;
            }
            if (informationType == InformationTypes.GetAllMedicalReadings)//仅限PC端获取全部阅片信息;
            {
                List<MedicalReading> listMedical = null;
                if (info[0] == 0x01)//专家
                    listMedical = this.globalCache.GetAllMedicalReadings(sourceUserID, false);
                else
                    listMedical = this.globalCache.GetAllMedicalReadings(sourceUserID, true);
                if (listMedical != null)

                    return CompactPropertySerializer.Default.Serialize<List<MedicalReading>>(listMedical);
                else
                    return null;
            }
            
            if (informationType == InformationTypes.GetSomeMedicalReading)
            {
              
                List<string> listguids = CompactPropertySerializer.Default.Deserialize<List<string>>(info, 0);
                List<MedicalReading> listMDs = this.globalCache.GetSomeMedicalReading(listguids);

               return CompactPropertySerializer.Default.Serialize<List<MedicalReading>>(listMDs);
                    
               

            }
            if (informationType == InformationTypes.GetClientOrExpertMDGuids)
            {
                if (info[0] == 0x01)//表示是专家;
                {
                    List<string> listguids = this.globalCache.GetClientOrExpertMDGuids(sourceUserID, true);
                    if (listguids != null)
                        return CompactPropertySerializer.Default.Serialize<List<string>>(listguids);
                    else
                        return null;
                }
                else//是客户
                {
                    List<string> listguids = this.globalCache.GetClientOrExpertMDGuids(sourceUserID, false);
                    if (listguids != null)
                        return CompactPropertySerializer.Default.Serialize<List<string>>(listguids);
                    else
                        return null;
                }

            }
            if (informationType == InformationTypes.GetAllExperts)//请求获取所有专家信息
            {

                List<GGUser> listexperts = this.globalCache.GetAllExperts();

                return CompactPropertySerializer.Default.Serialize<List<GGUser>>(listexperts);
            }
            
            if (informationType == InformationTypes.GetSomeUsers)//请求获取一些好友信息
            {
                List<string> friendIDs = CompactPropertySerializer.Default.Deserialize<List<string>>(info, 0);
                List<GGUser> friends = new List<GGUser>();
                foreach (string friendID in friendIDs)
                {
                    GGUser friend = this.globalCache.GetUser(friendID);
                    if (friend != null)
                    {
                        friends.Add(friend);
                    }
                }

                return CompactPropertySerializer.Default.Serialize<List<GGUser>>(friends);
            }

            if (informationType == InformationTypes.GetContactsRTData)//请求联系人的数据
            {
                List<string> contacts = this.globalCache.GetAllContacts(sourceUserID);               
                Dictionary<string, UserRTData> dic = new Dictionary<string, UserRTData>();
                foreach (string friendID in contacts)
                {
                    if (!dic.ContainsKey(friendID))
                    {
                        GGUser data = this.globalCache.GetUser(friendID);
                        if (data != null)
                        {
                            UserRTData rtData = new UserRTData(data.UserStatus ,data.Version) ;
                            dic.Add(friendID, rtData);
                        }
                    }
                }     
                Dictionary<string, int> groupVerDic = this.globalCache.GetMyGroupVersions(sourceUserID);
                ContactsRTDataContract contract = new ContactsRTDataContract(dic, groupVerDic);
                return CompactPropertySerializer.Default.Serialize(contract);
            }

            if (informationType == InformationTypes.GetUserInfo)
            {
                string target = System.Text.Encoding.UTF8.GetString(info);//根据ID获取信息
                GGUser user = this.globalCache.GetUser(target);
                if (user == null)
                {
                    return null;
                }
                if (sourceUserID != target)  //0922   
                {
                    user = user.PartialCopy;
                }
                return CompactPropertySerializer.Default.Serialize<GGUser>(user);
            }     

            if (informationType == InformationTypes.GetMyGroups)//获取我的组
            {
                List<GGGroup> myGroups = this.globalCache.GetMyGroups(sourceUserID);
                return CompactPropertySerializer.Default.Serialize(myGroups);
            }

            if (informationType == InformationTypes.GetSomeGroups)//通过组号来获取组对象信息。
            {
                List<string> groups = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<List<string>>(info, 0);
                List<GGGroup> myGroups = new List<GGGroup>();
                foreach (string groupID in groups)
                {
                    GGGroup group = this.globalCache.GetGroup(groupID);
                    if (group != null)
                    {
                        myGroups.Add(group);
                    }
                }
                
                return CompactPropertySerializer.Default.Serialize(myGroups);
            }

            if (informationType == InformationTypes.JoinGroup)
            {
                string groupID = System.Text.Encoding.UTF8.GetString(info);
                JoinGroupResult res = this.globalCache.JoinGroup(sourceUserID, groupID);
                if (res == JoinGroupResult.Succeed)
                {
                    //通知其它组成员
                    this.rapidServerEngine.GroupController.Broadcast(groupID, BroadcastTypes.SomeoneJoinGroup, System.Text.Encoding.UTF8.GetBytes(sourceUserID), ESFramework.ActionTypeOnChannelIsBusy.Continue);
                }
                return BitConverter.GetBytes((int)res);
            }

            if (informationType == InformationTypes.CreateGroup)
            {
                CreateGroupContract contract = CompactPropertySerializer.Default.Deserialize<CreateGroupContract>(info, 0);
                CreateGroupResult res = this.globalCache.CreateGroup(sourceUserID, contract.ID, contract.Name, contract.Announce);               
                return BitConverter.GetBytes((int)res);
            }

            if (informationType == InformationTypes.GetGroup)
            {
                string groupID = System.Text.Encoding.UTF8.GetString(info);
                GGGroup group = this.globalCache.GetGroup(groupID);
                return CompactPropertySerializer.Default.Serialize(group);
            }

            if (informationType == InformationTypes.ChangePassword)
            {
                ChangePasswordContract contract = CompactPropertySerializer.Default.Deserialize<ChangePasswordContract>(info, 0);
                ChangePasswordResult res = this.globalCache.ChangePassword(sourceUserID, contract.OldPasswordMD5, contract.NewPasswordMD5);
                return BitConverter.GetBytes((int)res);
            }
            if (informationType == InformationTypes.GetAllCheckedUser)//获取该客服已经审核的人员;
            {
                return CompactPropertySerializer.Default.Serialize<List<GGUser>>(this.globalCache.GetAllCheckedUserByPage(sourceUserID));

            }
            if (informationType == InformationTypes.GetUnCheckedUser)//获取该客服未审核的人员;
            {
                return CompactPropertySerializer.Default.Serialize<List<GGUser>>(this.globalCache.GetAllUnCheckedUser());

            }
            if (informationType == InformationTypes.CheckUserResult)
            {
                int len = info.Length; bool isexpert = false;bool isPassed=false;
                byte[] userid = new byte[len - 2];
                try
                {
	                Array.Copy(info, userid, len - 2);//复制数组
	                string checkUserID = System.Text.Encoding.UTF8.GetString(userid);
	                if (info[len - 1] == 0x01)//审核通过
	                {
	                    isPassed=true;
	                    if (info[len - 2] == 0x01)//审核结果为专家
	                     {
	                         isexpert = true;
	                     }
	                    
	                }
	                else//审核不通过
	                {
	                   isPassed=false;
	                }
                    GGUser checkUser = this.globalCache.GetCheckUserCache().Get(checkUserID);
                   // GGUser localUser = this.globalCache.GetUser(checkUserID);
                    if (checkUser == null )
                    {
                        return new byte[1] { 0x02 };//缓存中无此用户;
                    }
                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//设置更新时间;
                    checkUser.CreateTime = time;
                    checkUser.IsActivited = true;//审核后肯定是激活的：
                    checkUser.activitedByUserID = sourceUserID;
                    checkUser.UserType = isexpert == true ? EUserType.Expert : EUserType.NormalClient;
                    checkUser.IsChecking = false;//更新完成；
                    checkUser.Version += 1;//自增1;更新
	                if(this.globalCache.CheckUserResult(isPassed, checkUser))
	                {
	                    //缓存更新没问题，将新消息发送给其他的客服人员，让其取消审核
	                   
	                    foreach (string servicerID in this.globalCache.GetServicersID())
	                    {
	                        if (servicerID != sourceUserID)
	                        {
                                if (this.rapidServerEngine.UserManager.IsUserOnLine(servicerID))
	                                 this.rapidServerEngine.CustomizeController.Send(servicerID, InformationTypes.CheckCancel, userid);
	                        }
	                    }
                     //审核完成，返回创建时间;
                           // string time = globalCache.GetUser(checkUserID).CreateTime;
                            byte[] b = System.Text.Encoding.UTF8.GetBytes(time);
                        
	                    return b;//成功返回更新时间
	                }  
	                else
	                    return new byte[1] { 0x01 };//服务器内部提交失败，最大可能是邮件发送失败返回
                }
                catch (System.Exception ex)
                {
                    return new byte[1] { 0x03 };//未知错误;
                }
                
            }
            if (informationType == InformationTypes.GetMoreMedicalReadings)//上拉请求更多的阅片信息
            {
                try
                {
                    string tag = System.Text.Encoding.UTF8.GetString(info);//得到utf-8字符串，解析数据；
                    string[] complex = tag.Split(';');
                    if (complex.Length == 5)
                    {
                        string userid = complex[0];//用户ID
                        bool isexpert = this.globalCache.GetUser(userid).UserType == EUserType.NormalClient ? false : true; ;//是否是专家
                        int readingstatus = int.Parse(complex[1]);//阅片状态;
                        DateTime foremosttime = DateTime.Parse(complex[2]);//列表中最下阅片时间;
                        int currPage = int.Parse(complex[3]);
                        int pageSize = int.Parse(complex[4]);//第5个参数
                        List<MedicalReading> list = this.globalCache.GetMoreMedicalReadings(userid, isexpert, readingstatus, foremosttime,currPage,pageSize);
                        return CompactPropertySerializer.Default.Serialize<List<MedicalReading>>(list);
                       
                    }
                    else//序列化错误
                    {
                        return new byte[1] { 2 };

                    }
                }
                catch (System.Exception ex)
                {
                    return new byte[1] { 3 };

                }
            }
            if (informationType == InformationTypes.GetNewMedicalReadings)//下拉请求最新的阅片信息
            {
                try
                {
                    string tag= System.Text.Encoding.UTF8.GetString(info);//得到utf-8字符串，解析数据；
                    string[] complex = tag.Split(';');
                    if (complex.Length == 3)
                    {
                        string userid = complex[0];//用户ID
                        bool isexpert = this.globalCache.GetUser(userid).UserType == EUserType.NormalClient ? false : true; ;//是否是专家
                        int readingstatus = int.Parse(complex[1]);//阅片状态;
                        DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
                        dtFormat.ShortDatePattern = "yyyy-MM-dd HH:mm:ss";
                        DateTime lastupdatetime = Convert.ToDateTime(complex[2], dtFormat);//最后更新时间;
                        List<MedicalReading> list = this.globalCache.GetNewMedicalReadings(userid, isexpert, readingstatus, lastupdatetime);
                        return CompactPropertySerializer.Default.Serialize<List<MedicalReading>>(list);
                      
                    }
                    else//序列化错误
                    {
                       return  new byte[1] { 2 };

                    }

                }
                catch (System.Exception ex)//服务器内部异常错误;
                {
                    return new byte[1] { 3 };

                }
            }
            if (informationType == InformationTypes.RemoveFriend)
            {
                try
                {
                    string friendID = System.Text.Encoding.UTF8.GetString(info);
                    this.globalCache.RemoveFriend(sourceUserID, friendID);
                    //通知好友
                    this.rapidServerEngine.CustomizeController.Send(friendID, InformationTypes.FriendRemovedNotify, System.Text.Encoding.UTF8.GetBytes(sourceUserID));
                }
                catch (Exception ex)
                {
                    return new byte[]{0x01};//删除有问题;
                }
               
                return new byte[]{0x00};
            }     
            if (informationType == InformationTypes.GetNewCheckUserInfo)//下拉请求最新的用户审核信息
            {
                try
                {
                    string tag = System.Text.Encoding.UTF8.GetString(info);//得到utf-8字符串，解析数据；
                    string[] complex = tag.Split(';');
                    if (complex.Length == 3)
                    {
                        string userid = complex[0];//用户ID
                        bool isChecked = complex[1]=="0" ? false : true; ;//是获取已审核还是未审核的列表;
                        DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
                        dtFormat.ShortDatePattern = "yyyy-MM-dd HH:mm:ss";
                        DateTime lastupdatetime = Convert.ToDateTime(complex[2], dtFormat);//最后更新时间;
                        List<GGUser> list = this.globalCache.GetNewCheckUserInfo(userid, isChecked, lastupdatetime);
                        return CompactPropertySerializer.Default.Serialize<List<GGUser>>(list);

                    }
                    else//序列化错误
                    {
                        return new byte[1] { 2 };

                    }

                }
                catch (System.Exception ex)//服务器内部异常错误;
                {
                    return new byte[1] { 3 };

                }
            }
            if (informationType == InformationTypes.GetMoreCheckUserInfo)//上拉请求更多的阅片信息
            {
                try
                {
                    string tag = System.Text.Encoding.UTF8.GetString(info);//得到utf-8字符串，解析数据；
                    string[] complex = tag.Split(';');
                    if (complex.Length == 5)
                    {
                        string userid = complex[0];//用户ID
                        bool isChecked = complex[1]=="0" ? false : true; ;//是获取已审核还是未审核的列表; 
                        DateTime foremosttime = DateTime.Parse(complex[2]);//列表中最下审核创建时间;
                        int currPage = int.Parse(complex[3]);
                        int pageSize = int.Parse(complex[4]);//第5个参数
                        List<GGUser> list = this.globalCache.GetMoreCheckUserInfo(userid, isChecked, foremosttime, currPage, pageSize);
                        return CompactPropertySerializer.Default.Serialize<List<GGUser>>(list);

                    }
                    else//序列化错误
                    {
                        return new byte[1] { 2 };

                    }
                }
                catch (System.Exception ex)
                {
                    return new byte[1] { 3 };

                }
            }
            return null;
        }

        public bool CanHandle(int informationType)
        {
            return InformationTypes.ContainsInformationType(informationType);
        }
       
    }
}
