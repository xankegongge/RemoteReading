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

namespace GradeSystem.Server
{
    /// <summary>
    /// 自定义信息处理器。
    /// （1）当用户上线时，在其LocalTag挂接资料信息。
    /// </summary>
    internal class CustomizeHandler : ICustomizeHandler 
    {
        private GlobalCache globalCache;
        private IRapidServerEngine rapidServerEngine;

        public void Initialize(GlobalCache db, IRapidServerEngine engine)
        {
            this.globalCache = db;
            this.rapidServerEngine = engine;           
            

            this.rapidServerEngine.UserManager.SomeOneDisconnected += new ESBasic.CbGeneric<UserData, DisconnectedType>(UserManager_SomeOneDisconnected);
            this.rapidServerEngine.UserManager.SomeOneConnected += new ESBasic.CbGeneric<UserData>(UserManager_SomeOneconnected);
            //this.rapidServerEngine.MessageReceived += new ESBasic.CbGeneric<string, int, byte[], string>(rapidServerEngine_MessageReceived);
        }

        

       

        //void rapidServerEngine_MessageReceived(string sourceUserID, int informationType, byte[] info, string tag)
        //{
            
        //    if (informationType == InformationTypes.GetMDImages)
        //    {
        //        string gid = System.Text.Encoding.UTF8.GetString(info);//根据ID获取信息
        //        List<byte []> listMaps = this.globalCache.GetBitmapsByMedicalReadingGuid(gid);
        //        byte[] imagesinfo = CompactPropertySerializer.Default.Serialize<List<byte[]>>(listMaps);//序列化
        //     //   if (listMaps != null)
        //            this.rapidServerEngine.CustomizeController.SendBlob(sourceUserID, InformationTypes.GetMDImages, imagesinfo, 2048);
        //        //else
        //        //    this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.GetMDImages, null, null);
        //    }
        //    if (informationType == InformationTypes.Advice)
        //    {
        //        try
        //        {
        //            string userid = System.Text.Encoding.UTF8.GetString(info);//根据ID获取信息
        //            string advice = tag;
        //            if (this.globalCache.InsertAdvice(userid, advice))
        //            {
        //                this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.Advice, new byte[1] { 0 }, true, ActionTypeOnChannelIsBusy.Continue);
        //            }
        //            else
        //            {
        //                this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.Advice, new byte[1] { 0x05 }, true, ActionTypeOnChannelIsBusy.Continue);
        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }
        //    if (informationType == InformationTypes.GetMDSmallImages)
        //    {
        //        try
        //        {
        //            string gid = System.Text.Encoding.UTF8.GetString(info);//根据ID获取信息
        //            if (ValidUtils.IsGuidByReg(gid))
        //            {

        //                List<byte[]> listMaps = this.globalCache.GetSmallBitmapsByMedicalReadingGuid(gid);
        //                if (listMaps != null)
        //                {
        //                    byte[] imagesinfo = CompactPropertySerializer.Default.Serialize<List<byte[]>>(listMaps);//序列化
        //                    //   if (listMaps != null)
        //                    this.rapidServerEngine.CustomizeController.SendBlob(sourceUserID, InformationTypes.GetMDImages, imagesinfo, 1024);
        //                }
        //                else
        //                {
        //                    //服务器操作内部错误
        //                    this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.GetMDImages, new byte[1] { 1 }, true, ActionTypeOnChannelIsBusy.Continue);

        //                }
        //            }
        //            else//序列化问题
        //                this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.GetMDImages, new byte[1] { 2 }, true, ActionTypeOnChannelIsBusy.Continue);

        //        }
        //        catch (System.Exception ex)
        //        {//未知错误
        //            this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.GetMDImages, new byte[1]{3}, true,ActionTypeOnChannelIsBusy.Continue);
        //        }
               
        //    }
        //    if (informationType == InformationTypes.GetSingelImage)
        //    {
        //        try
        //        {
        //            string rpgid = System.Text.Encoding.UTF8.GetString(info);//根据ID获取信息
        //            if (ValidUtils.IsGuidByReg(rpgid))
        //            {
        //                byte[] singleImage = this.globalCache.GetSingleBitmapsByReadingPictureGuid(rpgid);
        //                if (singleImage != null && singleImage.Length > 0)
        //                {
        //                    byte[] imagesinfo = CompactPropertySerializer.Default.Serialize<byte[]>(singleImage);//序列化
        //                    //   if (listMaps != null)
        //                    this.rapidServerEngine.CustomizeController.SendBlob(sourceUserID, InformationTypes.GetSingelImage, imagesinfo, 2048);
        //                    //else
        //                }
        //                else
        //                    this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.GetSingelImage, new byte[1] { 1 }, true, ActionTypeOnChannelIsBusy.Continue);

        //            }
        //            else
        //                this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.GetSingelImage, new byte[1] { 2 }, true, ActionTypeOnChannelIsBusy.Continue);

        //        }
        //        catch (System.Exception ex)
        //        {
        //            this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.GetSingelImage, new byte[1] { 3 }, true, ActionTypeOnChannelIsBusy.Continue);
        //        }
        //        //    this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.GetMDImages, null, null);
        //    }
        //    if (informationType == InformationTypes.Chat)//聊天信息
        //    {             
        //        string destID = tag;
        //        if (this.rapidServerEngine.UserManager.IsUserOnLine(destID))
        //        {
        //           // this.rapidServerEngine.SendMessage(destID, informationType, info, sourceUserID, 2048);
        //            rapidServerEngine.CustomizeController.Send(destID, informationType, info);
        //        }
        //        else
        //        {
        //            OfflineMessage msg = new OfflineMessage(sourceUserID, destID, informationType, info);
        //            this.globalCache.StoreOfflineMessage(msg);//在内存缓存离线消息，并不是在数据库中缓存
        //        }
        //       // this.globalCache.StoreChatRecord(sourceUserID, destID, info);//暂时不插入服务器数据库
        //        return;
        //    }
        //    if (informationType == InformationTypes.SendMedicalReadingRejectedReason)//拒绝用户阅片请求
        //    {
        //        try
        //        {
        //            string []complex = tag.Split(';');
        //            if (complex.Length == 2)
        //            {
        //                string id = complex[1];
        //                string rejectedreason = complex[0];
        //                if (this.globalCache.UpdateMedicalReading(id, rejectedreason))
        //                {
        //                    MedicalReading mr = this.globalCache.GetMedicalReadingByGuid(id);
        //                    byte[] bupdatetime = System.Text.Encoding.UTF8.GetBytes(mr.CreatedTime);
        //                    //向客户端发送更新阅片消息，已拒绝
        //                    if (this.rapidServerEngine.UserManager.IsUserOnLine(mr.UserIDTo))
        //                    {
        //                        this.rapidServerEngine.SendMessage(mr.UserIDTo, InformationTypes.SendMedicalReadingRejectedReason, bupdatetime, tag);
        //                    }
        //                    else//在数据库中缓存离线阅片消息，当客户上线时转发给他；
        //                    {
        //                        this.globalCache.SoreOffMedicalReading(mr.UserIDTo, mr);
        //                    }
        //                    if (this.rapidServerEngine.UserManager.IsUserOnLine(mr.UserIDFrom))
        //                    {
        //                        this.rapidServerEngine.SendMessage(mr.UserIDFrom, InformationTypes.SendMedicalReadingRejectedReason, bupdatetime, tag);
        //                    }
        //                    else//在数据库中缓存离线阅片消息，当客户上线时转发给他；
        //                    {
        //                        this.globalCache.SoreOffMedicalReading(mr.UserIDFrom, mr);
        //                    }
	                      
        //               }
        //                else
        //                {
        //                    this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.SendMedicalReadingRejectedReason, new byte[1] { 1 }, null);
	
        //                }
        //            }
        //            else//序列化错误
        //            {
        //                this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.SendMedicalReadingRejectedReason, new byte[1] { 2 }, null);
	
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.SendMedicalReadingRejectedReason, new byte[1] { 3 }, null);
	
        //        }
        //    }
            
        //    if (informationType == InformationTypes.SendMedicalReadingReceived)//专家接收用户阅片请求
        //    {
        //        string id = tag;
        //        try
        //        {
        //            if (id!=null)
        //            {
        //                if (this.globalCache.UpdateMedicalReading(id))//通知客户端更新阅片状态
        //                {
        //                    //更新成功，向该客户发送更新状态消息；
        //                    MedicalReading mr = this.globalCache.GetMedicalReadingByGuid(id);
        //                    //向客户端发送更新阅片消息，正在处理
        //                    byte[] bupdatetime = System.Text.Encoding.UTF8.GetBytes(mr.CreatedTime);
        //                    //向客户端发送更新阅片消息，已拒绝
        //                    if (this.rapidServerEngine.UserManager.IsUserOnLine(mr.UserIDTo))
        //                    {
        //                        this.rapidServerEngine.SendMessage(mr.UserIDTo, InformationTypes.SendMedicalReadingReceived, bupdatetime, id);
        //                    }
        //                    else//在数据库中缓存离线阅片消息，当客户上线时转发给他；
        //                    {
        //                        this.globalCache.SoreOffMedicalReading(mr.UserIDTo, mr);
        //                    }
        //                    if (this.rapidServerEngine.UserManager.IsUserOnLine(mr.UserIDFrom))
        //                    {
        //                        this.rapidServerEngine.SendMessage(mr.UserIDFrom, InformationTypes.SendMedicalReadingReceived, bupdatetime, id);  //更新成功
        //                    }
        //                    else//在数据库中缓存离线阅片消息，当客户上线时转发给他；
        //                    {
        //                        this.globalCache.SoreOffMedicalReading(mr.UserIDFrom, mr);
        //                    }
	                  
	                        
        //                }
        //                else
        //                {
        //                    this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.SendMedicalReadingReceived, new byte[1] { 1 }, null);
	
        //                }
        //            }
        //            else//更新失败;
        //            {//byte  0x01 服务器数据库 操作失败， 0x02 序列化错误
        //                this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.SendMedicalReadingReceived, new byte[1] { 2 }, null);
	
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.SendMedicalReadingReceived, new byte[1] { 3 }, null);
        //        }
        //    }
        //    if (informationType == InformationTypes.SendUpdateMedicalReading)//更新阅片专家列表。专家提供
        //    {
        //        try
        //        {
        //            List<ReadingPicture> listpics = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<List<ReadingPicture>>(info, 0);
	               
        //            if (tag != null&&listpics!=null)
        //            {
        //                if (this.globalCache.UpdateMedicalReading(tag,listpics))
        //                {
        //                    //更新成功，向该客户发送更新状态消息；
        //                    MedicalReading mr = this.globalCache.GetMedicalReadingByGuid(tag);
        //                    string updatetime = mr.CreatedTime;
        //                   string  id = updatetime + ";" + tag;//更新时间与mdid;
        //                   byte [] listnewpics = ESPlus.Serialization.CompactPropertySerializer.Default.Serialize<List<ReadingPicture>>(mr.ListPics);
        //                    //向客户端发送更新阅片消息，已完成；实时更新
        //                   //向客户端发送更新阅片消息，已拒绝
        //                   if (this.rapidServerEngine.UserManager.IsUserOnLine(mr.UserIDTo))
        //                   {
        //                       this.rapidServerEngine.SendMessage(mr.UserIDTo, InformationTypes.SendUpdateMedicalReading, listnewpics, id);  //更新成功
        //                   }
        //                   else//在数据库中缓存离线阅片消息，当客户上线时转发给他；
        //                   {
        //                       this.globalCache.SoreOffMedicalReading(mr.UserIDTo, mr);
        //                   }
        //                   if (this.rapidServerEngine.UserManager.IsUserOnLine(mr.UserIDFrom))
        //                   {
	
        //                       this.rapidServerEngine.SendMessage(mr.UserIDFrom, InformationTypes.SendUpdateMedicalReading, listnewpics, id);  //更新成功
        //                   }
        //                   else//在数据库中缓存离线阅片消息，当客户上线时转发给他；
        //                   {
        //                       this.globalCache.SoreOffMedicalReading(mr.UserIDFrom, mr);
        //                   }
	                     
        //                }
        //                else//更新失败;
        //                {//byte  0x01 服务器数据库 操作失败， 0x02 序列化错误
        //                    this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.SendUpdateMedicalReading, new byte[1]{1}, null);  
	
        //                }
        //            }
        //            else//序列化有问题;
        //            {
        //                this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.SendUpdateMedicalReading, new byte[1] {2}, null);
	
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            this.rapidServerEngine.SendMessage(sourceUserID, InformationTypes.SendUpdateMedicalReading, new byte[1] { 3 }, null);
	
        //        }
        //    }
        //    if (informationType == InformationTypes.UpdateUserInfo)//更新用户信息
        //    {
        //        GGUser user = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<GGUser>(info, 0);
        //        GGUser old = this.globalCache.GetUser(user.UserID);
        //        if (old == null||user==null)
        //            return;
        //        this.globalCache.UpdateUser(user);
        //        List<string> friendIDs = this.globalCache.GetFriends(sourceUserID);
        //        byte[] subData = ESPlus.Serialization.CompactPropertySerializer.Default.Serialize<GGUser>(user.PartialCopy); //0922   
        //        foreach (string friendID in friendIDs)
        //        {
        //            if (friendID != sourceUserID)
        //            {
        //                //可能要分块发送
        //                if (this.rapidServerEngine.UserManager.IsUserOnLine(friendID))
        //                    this.rapidServerEngine.CustomizeController.Send(friendID, InformationTypes.UserInforChanged, subData, true, ActionTypeOnChannelIsBusy.Continue);
        //            }
        //        }
        //        return;
        //    }


        //    if (informationType == InformationTypes.UpdateCheckUserInfo)//更新用户信息
        //    {
        //        try
        //        {
        //            GGUser user = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<GGUser>(info, 0);
        //            if (this.globalCache.IsCheckUserExist(user.UserID))//已经提交过申请
        //            {
        //                this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.UpdateCheckUserInfo, new byte[1] { 4 }, true, ActionTypeOnChannelIsBusy.Continue);
        //                return;
        //            }
        //            user.CheckType = CheckType.Update;//更新类型;
        //            user.IsChecking = true;//处于待审核状态;
        //            if (user != null)
        //            {
        //                Hospital hospital = globalCache.GetHostpitalName(user.UserContact.HospitalID);//最好给他一个hos对象;
        //                user.Hospi = hospital;//设置医院;
        //                user.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//设置更新时间;  
        //                byte[] btime = System.Text.Encoding.UTF8.GetBytes(user.CreateTime);
        //                if (this.globalCache.InsertCheckUserList(user))//插入审核缓存列表，暂时不更新数据库；
        //                {
        //                    //通知发送方已经发送给客服审核了
        //                    if (this.rapidServerEngine.UserManager.IsUserOnLine(sourceUserID))
        //                        this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.UpdateCheckUserInfo, btime, true, ActionTypeOnChannelIsBusy.Continue);
        //                    SendToServicesCheck(user); //发送给所有在线的客服审核
	                        
	
	                        
        //                }
        //                else//插入失败，有可能是已经提交过了，有未审核的资料。
        //                {
        //                    //回送给发送方，插入失败;
        //                   // if (this.rapidServerEngine.UserManager.IsUserOnLine(sourceUserID))
        //                        this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.UpdateCheckUserInfo, new byte[1]{1}, true, ActionTypeOnChannelIsBusy.Continue);
        //                }
        //            }
        //            else//user序列化有问题
        //                this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.UpdateCheckUserInfo, new byte[1] { 2 }, true, ActionTypeOnChannelIsBusy.Continue);
	            
        //        }
        //        catch (System.Exception ex)
        //        {//未知错误
        //            this.rapidServerEngine.CustomizeController.Send(sourceUserID, InformationTypes.UpdateCheckUserInfo, new byte[1] { 3 }, true, ActionTypeOnChannelIsBusy.Continue);
        //        }
        // } 
        //    return;
        //}
     
       
   
        private void UserManager_SomeOneconnected(UserData data)
        {
            LoginUser user = this.globalCache.GetUser(data.UserID);
           
            if (user != null)
            {
                user.OnlineState = UserStatus.Online;
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
        void UserManager_SomeOneDisconnected(UserData data, DisconnectedType obj2)
        {
            LoginUser user = this.globalCache.GetUser(data.UserID);
           
            if (user != null)
            {
                user.OnlineState = UserStatus.OffLine;
            }
        }

        ////发送离线消息给客户端
        //public void SendOfflineMessage(string destUserID)
        //{
        //    List<OfflineMessage> list = this.globalCache.PickupOfflineMessage(destUserID);
        //    if (list != null && list.Count > 0)
        //    {
        //        foreach (OfflineMessage msg in list)
        //        {
        //            byte[] bMsg = CompactPropertySerializer.Default.Serialize<OfflineMessage>(msg);
        //            this.rapidServerEngine.CustomizeController.SendBlob(msg.DestUserID, InformationTypes.OfflineMessage, bMsg, 2048);
        //        }
        //    }

        //}
        

        /// <summary>
        /// 处理来自客户端的消息。
        /// </summary> 
        public void HandleInformation(string sourceUserID, int informationType, byte[] info)
        {
           
            

            //if (informationType == InformationTypes.GetOfflineMessage)//获取离线消息
            //{
            //    this.SendOfflineMessage(sourceUserID);//发送离线消息
                
            //    return;
            //}

             

            if (informationType == InformationTypes.ChangeStatus)//改变状态
            {
                LoginUser user = this.globalCache.GetUser(sourceUserID);

                int newStatus = BitConverter.ToInt32(info, 0);
                user.OnlineState = (UserStatus)newStatus;
                //GGUser expert = this.globalCache.GetExpert(sourceUserID); ;
                //if (expert != null)
                //{
                //    expert.UserStatus = (UserStatus)newStatus;
                //}

               // List<string> contacts = this.globalCache.GetAllContacts(sourceUserID);                          
                //UserStatusChangedContract contract = new UserStatusChangedContract(sourceUserID, newStatus);
               // byte[] msg = ESPlus.Serialization.CompactPropertySerializer.Default.Serialize(contract);
              //  foreach (string friendID in contacts)
                {
                    //this.rapidServerEngine.Send(friendID, InformationTypes.UserStatusChanged, msg);
                }                
                return;
            }
        }

        /// <summary>
        /// 处理来自客户端的同步调用请求。
        /// </summary>       
        public byte[] HandleQuery(string sourceUserID, int informationType, byte[] info)
        {
            //if (informationType == InformationTypes.GetFriendIDList)
            //{
            //    List<string> friendIDs = this.globalCache.GetFriends(sourceUserID);
            //    return CompactPropertySerializer.Default.Serialize<List<string>>(friendIDs);
            //}
            //if (informationType == InformationTypes.GetAllHospitals)
            //{
            //    List<Hospital> hospitals = this.globalCache.GetAllHospitals();
            //    return CompactPropertySerializer.Default.Serialize<List<Hospital>>(hospitals);
            //}
            //if (informationType == InformationTypes.AddFriend)
            //{
            //    AddFriendContract contract = CompactPropertySerializer.Default.Deserialize<AddFriendContract>(info ,0);
            //    bool isExist = this.globalCache.IsUserExist(contract.FriendID);
            //    if (!isExist)
            //    {
            //        return BitConverter.GetBytes((int)AddFriendResult.FriendNotExist);
            //    }
            //    this.globalCache.AddFriend(sourceUserID, contract.FriendID ,contract.CatalogName);

            //    //0922
            //    GGUser owner = this.globalCache.GetUser(sourceUserID);
            //    byte[] ownerBuff = CompactPropertySerializer.Default.Serialize<GGUser>(owner);

            //    //通知对方
            //    this.rapidServerEngine.CustomizeController.Send(contract.FriendID, InformationTypes.FriendAddedNotify, ownerBuff, true, ESFramework.ActionTypeOnChannelIsBusy.Continue);
            //    return BitConverter.GetBytes((int)AddFriendResult.Succeed);
            //}

            //if (informationType == InformationTypes.GetAllContacts)
            //{
            //    List<string> contacts = this.globalCache.GetAllContacts(sourceUserID);
            //    Dictionary<string, GGUser> contactDic = new Dictionary<string, GGUser>();
            //    foreach (string friendID in contacts)
            //    {
            //        if (!contactDic.ContainsKey(friendID))
            //        {
            //            GGUser friend = this.globalCache.GetUser(friendID);
            //            if (friend != null)
            //            {
            //                contactDic.Add(friendID, friend);
            //            }
            //        }
            //    }

            //    return CompactPropertySerializer.Default.Serialize<List<GGUser>>(new List<GGUser>(contactDic.Values));
            //}
            //if (informationType == InformationTypes.GetAllMedicalReading)//移动端分页获取阅片信息;
            //{
            //     List<MedicalReading> listMedical=null;
            //    if(info[0]==0x01)//专家

            //        listMedical = this.globalCache.GetMedicalReadingsByUserID(sourceUserID, false);
            //    else
            //        listMedical = this.globalCache.GetMedicalReadingsByUserID(sourceUserID, true);
            //    if (listMedical != null)

            //        return CompactPropertySerializer.Default.Serialize<List<MedicalReading>>(listMedical);
            //    else
            //        return null;
            //}
            //if (informationType == InformationTypes.GetAllMedicalReadings)//仅限PC端获取全部阅片信息;
            //{
            //    List<MedicalReading> listMedical = null;
            //    if (info[0] == 0x01)//专家
            //        listMedical = this.globalCache.GetAllMedicalReadings(sourceUserID, false);
            //    else
            //        listMedical = this.globalCache.GetAllMedicalReadings(sourceUserID, true);
            //    if (listMedical != null)

            //        return CompactPropertySerializer.Default.Serialize<List<MedicalReading>>(listMedical);
            //    else
            //        return null;
            //}
            
            //if (informationType == InformationTypes.GetSomeMedicalReading)
            //{
              
            //    List<string> listguids = CompactPropertySerializer.Default.Deserialize<List<string>>(info, 0);
            //    List<MedicalReading> listMDs = this.globalCache.GetSomeMedicalReading(listguids);

            //   return CompactPropertySerializer.Default.Serialize<List<MedicalReading>>(listMDs);
                    
               

            //}
            //if (informationType == InformationTypes.GetClientOrExpertMDGuids)
            //{
            //    if (info[0] == 0x01)//表示是专家;
            //    {
            //        List<string> listguids = this.globalCache.GetClientOrExpertMDGuids(sourceUserID, true);
            //        if (listguids != null)
            //            return CompactPropertySerializer.Default.Serialize<List<string>>(listguids);
            //        else
            //            return null;
            //    }
            //    else//是客户
            //    {
            //        List<string> listguids = this.globalCache.GetClientOrExpertMDGuids(sourceUserID, false);
            //        if (listguids != null)
            //            return CompactPropertySerializer.Default.Serialize<List<string>>(listguids);
            //        else
            //            return null;
            //    }

            //}
            //if (informationType == InformationTypes.GetAllExperts)//请求获取所有专家信息
            //{

            //    List<GGUser> listexperts = this.globalCache.GetAllExperts();

            //    return CompactPropertySerializer.Default.Serialize<List<GGUser>>(listexperts);
            //}
            
            if (informationType == InformationTypes.GetSomeUsers)//请求获取一些好友信息
            {
                List<string> friendIDs = CompactPropertySerializer.Default.Deserialize<List<string>>(info, 0);
                List<LoginUser> friends = new List<LoginUser>();
                foreach (string friendID in friendIDs)
                {
                    LoginUser friend = this.globalCache.GetUser(friendID);
                    if (friend != null)
                    {
                        friends.Add(friend);
                    }
                }

                return CompactPropertySerializer.Default.Serialize<List<LoginUser>>(friends);
            }

            

            if (informationType == InformationTypes.GetUserInfo)
            {
                string target = System.Text.Encoding.UTF8.GetString(info);//根据ID获取信息
                LoginUser user = this.globalCache.GetUser(target);
                if (user == null)
                {
                    return null;
                }
                if (sourceUserID != target)  //0922   
                {
                    user = user.PartialCopy;
                }
                return CompactPropertySerializer.Default.Serialize<LoginUser>(user);
            }     

           

            if (informationType == InformationTypes.ChangePassword)
            {
                ChangePasswordContract contract = CompactPropertySerializer.Default.Deserialize<ChangePasswordContract>(info, 0);
                ChangePasswordResult res = this.globalCache.ChangePassword(sourceUserID, contract.OldPasswordMD5, contract.NewPasswordMD5);
                return BitConverter.GetBytes((int)res);
            }
            
            
           
            //if (informationType == InformationTypes.GetMoreMedicalReadings)//上拉请求更多的阅片信息
            //{
            //    try
            //    {
            //        string tag = System.Text.Encoding.UTF8.GetString(info);//得到utf-8字符串，解析数据；
            //        string[] complex = tag.Split(';');
            //        if (complex.Length == 5)
            //        {
            //            string userid = complex[0];//用户ID
            //            bool isexpert = this.globalCache.GetUser(userid).UserType == EUserType.NormalClient ? false : true; ;//是否是专家
            //            int readingstatus = int.Parse(complex[1]);//阅片状态;
            //            DateTime foremosttime = DateTime.Parse(complex[2]);//列表中最下阅片时间;
            //            int currPage = int.Parse(complex[3]);
            //            int pageSize = int.Parse(complex[4]);//第5个参数
            //            List<MedicalReading> list = this.globalCache.GetMoreMedicalReadings(userid, isexpert, readingstatus, foremosttime,currPage,pageSize);
            //            return CompactPropertySerializer.Default.Serialize<List<MedicalReading>>(list);
                       
            //        }
            //        else//序列化错误
            //        {
            //            return new byte[1] { 2 };

            //        }
            //    }
            //    catch (System.Exception ex)
            //    {
            //        return new byte[1] { 3 };

            //    }
            //}
            //if (informationType == InformationTypes.GetNewMedicalReadings)//下拉请求最新的阅片信息
            //{
            //    try
            //    {
            //        string tag= System.Text.Encoding.UTF8.GetString(info);//得到utf-8字符串，解析数据；
            //        string[] complex = tag.Split(';');
            //        if (complex.Length == 3)
            //        {
            //            string userid = complex[0];//用户ID
            //            bool isexpert = this.globalCache.GetUser(userid).UserType == EUserType.NormalClient ? false : true; ;//是否是专家
            //            int readingstatus = int.Parse(complex[1]);//阅片状态;
            //            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            //            dtFormat.ShortDatePattern = "yyyy-MM-dd HH:mm:ss";
            //            DateTime lastupdatetime = Convert.ToDateTime(complex[2], dtFormat);//最后更新时间;
            //            List<MedicalReading> list = this.globalCache.GetNewMedicalReadings(userid, isexpert, readingstatus, lastupdatetime);
            //            return CompactPropertySerializer.Default.Serialize<List<MedicalReading>>(list);
                      
            //        }
            //        else//序列化错误
            //        {
            //           return  new byte[1] { 2 };

            //        }

            //    }
            //    catch (System.Exception ex)//服务器内部异常错误;
            //    {
            //        return new byte[1] { 3 };

            //    }
            //}
            return null;
        }

        public bool CanHandle(int informationType)
        {
            return InformationTypes.ContainsInformationType(informationType);
        }
       
    }
}
