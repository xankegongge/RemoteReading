using System;
using System.Collections.Generic;
using System.Text;
using CCWin.Win32;
using System.Windows.Forms;
using CCWin.SkinControl;
using ESBasic;
using ESPlus.Serialization;
using System.Runtime.InteropServices;
using OMCS.Passive;
using JustLib;
using JustLib.Controls;
using JustLib.Records;
using RemoteReading.Core;

namespace RemoteReading
{
    public partial class MainForm : ESPlus.Application.CustomizeInfo.IIntegratedCustomizeHandler
    {
       
        void rapidPassiveEngine_MessageReceived(string sourceUserID, int informationType, byte[] info, string tag)
        {
            if (!this.initialized)
            {
                return;
            }
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbGeneric<string, int, byte[], string>(this.rapidPassiveEngine_MessageReceived), sourceUserID, informationType, info, tag);
            }
            else
            {
                if (informationType == InformationTypes.SendMedicalReadingRejectedReason)//接收服务器发送过来的拒绝用户阅片请求
                {
                  //  if (this.globalUserCache.CurrentUser.UserType == EUserType.NormalClient)//客户
                    {
                        string updatetime = System.Text.Encoding.UTF8.GetString(info);
                        string[] complex = tag.Split(';');
                        if (complex.Length == 2)
                        {
                            string id = complex[1];
                            string rejectedreason = complex[0];
                            if (id != null)
                            {
                                this.UpdateMedicalReading(id, rejectedreason, updatetime);//主界面更新
                               
                            }
                        }
                    }
                }
                if (informationType == InformationTypes.GetMDImages)//这里接收到便是没有图像的消息
                {

                    isGetImagesOK = true;
                    this.LoadImages(null);
                    return;

                }
                if (informationType == InformationTypes.SendMedicalReadingReceived)//接收服务器发送过来的专家接收用户阅片请求
                {
                  //  if (this.globalUserCache.CurrentUser.UserType == EUserType.NormalClient)
                    {
                        string updatetime = System.Text.Encoding.UTF8.GetString(info);
                        string id = tag;
                        if (id != null)
                        {
                            if (id != null)
                            {
                                this.UpdateMedicalReading(id, updatetime);//主界面更新,接收更新

                            }
                        }
                    }
                }
                if (informationType == InformationTypes.SendUpdateMedicalReading)//接收服务器发送过来的更新阅片专家列表。专家提供
                {
                  //  if (this.globalUserCache.CurrentUser.UserType == EUserType.NormalClient)
                    {
                        string[] complex = tag.Split(';');
                        if (complex.Length != 2)
                        {
                            MessageBox.Show("完成阅片失败");
                        }
                        string id = complex[1];
                        string updatetime = complex[0];
                        List<ReadingPicture> listpics = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<List<ReadingPicture>>(info, 0);

                        if (id != null)
                        {
                            if (id != null)
                            {
                                this.UpdateMedicalReading(id, listpics, updatetime);//主界面更新
                                //FrmStatus fs = new FrmStatus();
                                //fs.Show("专家已处理一条阅片信息",true);
                            }
                        }
                    }
                }
                if (informationType == InformationTypes.MedicalReadingAdd)//阅片消息
                {
                    //if (this.globalUserCache.CurrentUser.UserType == EUserType.Expert)
                    {
                        if (tag != null && tag.Contains("error"))
                        {
                            MessageBox.Show("提交阅片失败");
                            return;
                        }
                        MedicalReading mr = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<MedicalReading>(info, 0);
                        string expertID = mr.UserIDTo;//专家ID
                        if (mr.UserFrom == null)
                        {
                            GGUser userfrom = this.globalUserCache.GetUser(mr.UserIDFrom);
                            mr.UserFrom = userfrom;
                        }
                        if (mr.UserTo == null)
                        {
                            GGUser userto = this.globalUserCache.GetUser(mr.UserIDTo);
                            mr.UserTo = userto;
                        }
                        this.globalUserCache.AddMedicalReading(mr);//自动触发界面更新
                        if (this.globalUserCache.CurrentUser.UserType == EUserType.NormalClient)
                        this.UpdateMREvent(mr);
                        if (this.globalUserCache.CurrentUser.UserType == EUserType.Expert)
                        this.notifyIcon.PushMedicalReadingMessage(mr.MedicalReadingID, informationType, info, expertID);
                    }
                    return;
                }
                 if (informationType == InformationTypes.OfflineMDMessage)//阅片离线消息
                        {
                            List<MedicalReading> msg = CompactPropertySerializer.Default.Deserialize<List<MedicalReading>>(info, 0);
                            foreach (MedicalReading mr in msg)
                            {
                                if (mr.UserFrom == null)
                                {
                                    GGUser userfrom = this.globalUserCache.GetUser(mr.UserIDFrom);
                                    mr.UserFrom = userfrom;
                                }
                                if (mr.UserTo == null)
                                {
                                    GGUser userto = this.globalUserCache.GetUser(mr.UserIDTo);
                                    mr.UserTo = userto;
                                }
                                this.globalUserCache.UpdateMedicalReading(mr);//自动触发界面更新
                                if(this.globalUserCache.CurrentUser.UserType==EUserType.NormalClient)
                                    this.notifyIcon.PushMedicalReadingMessage(mr.MedicalReadingID, informationType, info, mr.UserIDTo);
                                else
                                    this.notifyIcon.PushMedicalReadingMessage(mr.MedicalReadingID, informationType, info, mr.UserIDFrom);
                            }
                           
                               
                        }
                //if (informationType == InformationTypes.Chat)//聊天消息采用引擎的MessageReceived方法，而一些好友、群通知等采用Customize机制
                //{
                //    sourceUserID = tag;
                //    byte[] bChatBoxContent = info;
                //    if (bChatBoxContent != null)
                //    {
                //        ChatMessageRecord record = new ChatMessageRecord(sourceUserID, this.rapidPassiveEngine.CurrentUserID, bChatBoxContent, false);
                //        GlobalResourceManager.ChatMessageRecordPersister.InsertChatMessageRecord(record);
                //    }

                //    byte[] decrypted = info;
                //    if (GlobalResourceManager.Des3Encryption != null)
                //    {
                //        decrypted = GlobalResourceManager.Des3Encryption.Decrypt(info);
                //    }

                //    ChatBoxContent content = CompactPropertySerializer.Default.Deserialize<ChatBoxContent>(decrypted, 0);
                //    GGUser user = this.globalUserCache.GetUser(sourceUserID);
                //    this.notifyIcon.PushFriendMessage(sourceUserID, informationType, info, content);
                //    return;
                //}
            }
        }
       public List<byte[]> listimages = null;
        #region HandleInformation
        public void HandleInformation(string sourceUserID, int informationType, byte[] info)
        {
            if (!this.initialized)
            {
                return;
            }

         

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbGeneric<string, int, byte[]>(this.HandleInformation), sourceUserID, informationType, info);
            }
            else
            {
                try
                {
                    #region 需要twinkle的消息
                    if (informationType == InformationTypes.Chat || informationType == InformationTypes.OfflineMessage || informationType == InformationTypes.OfflineFileResultNotify
                                 || informationType == InformationTypes.Vibration || informationType == InformationTypes.VideoRequest || informationType == InformationTypes.AgreeVideo
                                 || informationType == InformationTypes.RejectVideo || informationType == InformationTypes.HungUpVideo || informationType == InformationTypes.DiskRequest
                                 || informationType == InformationTypes.AgreeDisk || informationType == InformationTypes.RejectDisk || informationType == InformationTypes.RemoteHelpRequest
                                 || informationType == InformationTypes.AgreeRemoteHelp || informationType == InformationTypes.RejectRemoteHelp || informationType == InformationTypes.CloseRemoteHelp
                                 || informationType == InformationTypes.TerminateRemoteHelp
                                 || informationType == InformationTypes.AudioRequest || informationType == InformationTypes.RejectAudio || informationType == InformationTypes.AgreeAudio
                                 || informationType == InformationTypes.HungupAudio
                                 || informationType == InformationTypes.FriendAddedNotify)
                    {
                        if (informationType == InformationTypes.FriendAddedNotify)
                        {
                            GGUser owner = CompactPropertySerializer.Default.Deserialize<GGUser>(info, 0); // 0922
                            this.globalUserCache.CurrentUser.AddFriend(owner.ID, this.globalUserCache.CurrentUser.DefaultFriendCatalog);
                            this.globalUserCache.OnFriendAdded(owner); //自然会添加 好友条目
                            sourceUserID = owner.UserID;
                        }

                       object tag = null;
                        if (informationType == InformationTypes.OfflineMessage)
                        {
                            byte[] bChatBoxContent = null;
                            OfflineMessage msg = CompactPropertySerializer.Default.Deserialize<OfflineMessage>(info, 0);
                            if (msg.InformationType == InformationTypes.Chat) //目前只处理离线的聊天消息
                            {
                                sourceUserID = msg.SourceUserID;
                                bChatBoxContent = msg.Information;
                                byte[] decrypted = bChatBoxContent;
                                if (GlobalResourceManager.Des3Encryption != null)
                                {
                                    decrypted = GlobalResourceManager.Des3Encryption.Decrypt(bChatBoxContent);
                                }

                                ChatMessageRecord record = new ChatMessageRecord(sourceUserID, this.rapidPassiveEngine.CurrentUserID, decrypted, false);
                                GlobalResourceManager.ChatMessageRecordPersister.InsertChatMessageRecord(record);
                                ChatBoxContent content = CompactPropertySerializer.Default.Deserialize<ChatBoxContent>(decrypted, 0);
                                tag = new Parameter<ChatBoxContent, string>(content, msg.Time);
                            }
                        }
                       
                        if (informationType == InformationTypes.Chat)//聊天消息采用引擎的MessageReceived方法，而一些好友、群通知等采用Customize机制
                        {
                            //sourceUserID = tag;
                            byte[] bChatBoxContent = info;
                            if (bChatBoxContent != null)
                            {
                                ChatMessageRecord record = new ChatMessageRecord(sourceUserID, this.rapidPassiveEngine.CurrentUserID, bChatBoxContent, false);
                                GlobalResourceManager.ChatMessageRecordPersister.InsertChatMessageRecord(record);
                            }

                            byte[] decrypted = info;
                            if (GlobalResourceManager.Des3Encryption != null)
                            {
                                decrypted = GlobalResourceManager.Des3Encryption.Decrypt(info);
                            }

                            ChatBoxContent content = CompactPropertySerializer.Default.Deserialize<ChatBoxContent>(decrypted, 0);
                            GGUser user = this.globalUserCache.GetUser(sourceUserID);
                            tag = content;
                          //  this.notifyIcon.PushFriendMessage(sourceUserID, informationType, info, content);
                           // return;
                        }
                        if (informationType == InformationTypes.OfflineFileResultNotify)
                        {
                            OfflineFileResultNotifyContract contract = CompactPropertySerializer.Default.Deserialize<OfflineFileResultNotifyContract>(info, 0);
                            sourceUserID = contract.AccepterID;
                        }

                        GGUser users = this.globalUserCache.GetUser(sourceUserID);
                        this.notifyIcon.PushFriendMessage(sourceUserID, informationType, info, tag);
                        return;
                    }
                    #endregion
                    if (informationType == InformationTypes.GetMDImages)
                    {

                        listimages = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize <List<byte[]>>(info, 0);
                        isGetImagesOK = true;
                        this.LoadImages(listimages);
                        return;

                    }
                    
                    if (informationType == InformationTypes.InputingNotify)
                    {
                        ChatForm form = this.chatFormManager.GetForm(sourceUserID);
                        if (form != null)
                        {
                            form.OnInptingNotify();
                        }
                        return;
                    }

                    if (informationType == InformationTypes.FriendRemovedNotify)
                    {
                        string friendID = System.Text.Encoding.UTF8.GetString(info);
                        this.globalUserCache.RemovedFriend(friendID);
                        return;
                    }

                    if (informationType == InformationTypes.UserInforChanged)
                    {
                        GGUser user = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<GGUser>(info, 0);
                        this.globalUserCache.AddOrUpdateUser(user);
                        return;
                    }

                    if (informationType == InformationTypes.UserStatusChanged)
                    {
                        UserStatusChangedContract contract = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<UserStatusChangedContract>(info, 0);
                        this.globalUserCache.ChangeUserStatus(contract.UserID, (UserStatus)contract.NewStatus);
                    }
                }
                catch (Exception ee)
                {
                    GlobalResourceManager.Logger.Log(ee, "MainForm.HandleInformation", ESBasic.Loggers.ErrorLevel.Standard);
                    MessageBox.Show(ee.Message);
                }
            }
        }

        #endregion

        public byte[] HandleQuery(string sourceUserID, int informationType, byte[] info)
        {
            return null;
        }

        public bool CanHandle(int informationType)
        {
            return InformationTypes.ContainsInformationType(informationType);
        }

        void GroupOutter_BroadcastReceived(string broadcasterID, string groupID, int broadcastType, byte[] content )
        {
            if (!this.initialized)
            {
                return;
            }

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbGeneric<string, string, int, byte[]>(this.GroupOutter_BroadcastReceived), broadcasterID, groupID, broadcastType, content);
            }
            else
            {
                try
                {
                    if (broadcastType == BroadcastTypes.BroadcastChat)
                    {
                        GGGroup group = this.globalUserCache.GetGroup(groupID);

                        byte[] decrypted = content;
                        if (GlobalResourceManager.Des3Encryption != null)
                        {
                            decrypted = GlobalResourceManager.Des3Encryption.Decrypt(content);
                        }

                        this.notifyIcon.PushGroupMessage(broadcasterID, groupID, broadcastType, decrypted);
                        ChatMessageRecord record = new ChatMessageRecord(broadcasterID, groupID, decrypted, true);
                        GlobalResourceManager.ChatMessageRecordPersister.InsertChatMessageRecord(record);
                        return;
                    }

                    if (broadcastType == BroadcastTypes.SomeoneJoinGroup)
                    {
                        string userID = System.Text.Encoding.UTF8.GetString(content);
                        this.globalUserCache.OnSomeoneJoinGroup(groupID, userID);
                        return;
                    }

                    if (broadcastType == BroadcastTypes.SomeoneQuitGroup)
                    {
                        string userID = System.Text.Encoding.UTF8.GetString(content);
                        this.globalUserCache.OnSomeoneQuitGroup(groupID, userID);
                        return;
                    }

                    if (broadcastType == BroadcastTypes.GroupDeleted)
                    {
                        string userID = System.Text.Encoding.UTF8.GetString(content);
                        this.globalUserCache.OnGroupDeleted(groupID, userID);
                        return;
                    }
                }
                catch (Exception ee)
                {
                    GlobalResourceManager.Logger.Log(ee, "MainForm.GroupOutter_BroadcastReceived", ESBasic.Loggers.ErrorLevel.Standard);
                    MessageBox.Show(ee.Message);
                }

            }
        }
    }
}
