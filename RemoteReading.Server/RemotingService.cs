using System;
using System.Collections.Generic;

using ESPlus.Rapid;
using JustLib.Records;
using RemoteReading.Core;
using ESPlus.Application.CustomizeInfo.Server;
using ESPlus.Application.CustomizeInfo;
using ESPlus.Application.Basic.Server;
using ESPlus.Application;
using ESFramework.Server.UserManagement;
using ESPlus.Rapid;
using ESPlus.Serialization;
using System.Drawing;
using ESFramework;
using ESBasic.Security;
namespace RemoteReading.Server
{
    public class RemotingService :MarshalByRefObject, IRemotingService
    {
        private GlobalCache globalCache;
        private IRapidServerEngine rapidServerEngine;
        public RemotingService(GlobalCache db ,IRapidServerEngine engine)
        {
            this.globalCache = db;
            this.rapidServerEngine = engine;
        }

        public List<Hospital> GetAllHospitals()
        {
            List<Hospital> list = new List<Hospital>();
            try
            {
                return list = this.globalCache.GetAllHospitals();
              
            }
            catch (Exception ee)
            {
                return null;
            }

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
            foreach (string servicer in servicers)
            {
                //可能要分块发送
                this.rapidServerEngine.CustomizeController.Send(servicer, InformationTypes.NewCheckUser, sendinfo, true, ActionTypeOnChannelIsBusy.Continue);
            }
            return true;
        }
        public RegisterResult Register(GGUser user)
        {
            try
            {
                if (this.globalCache.IsUserExist(user.UserID))
                {
                    return RegisterResult.Existed;
                }
                if (globalCache.IsCheckedCacheExist(user.UserID))
                {
                    return RegisterResult.Existed;
                }
                string useridbyemail;
                if (this.globalCache.IsExitEmail(user.UserContact.Email, out useridbyemail) || this.globalCache.IsExitEmailInCheckCache(user.UserContact.Email))
                {
                    return RegisterResult.EmailExisted;
                }
                if (this.globalCache.IsExitMobilePhone(user.UserContact.MobilePhone) || this.globalCache.IsExitMobilePhoneInCache(user.UserContact.MobilePhone))
                {
                    return RegisterResult.MobileExisted;
                }
                Hospital hospital = globalCache.GetHostpitalName(user.UserContact.HospitalID);
                user.Hospi = hospital;
                //这里由客服审核是否为专家，如果是专家，则添加至所有的客户好友列表，如果是客户，则添加所有的专家为好友。
                user.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//设置时间;
                user.IsChecking = true;//处于待审核状态;
                user.CheckType = CheckType.Register;//注册审核类型
                user.IsActivited = false;//默认是不激活的
                user.UserType = EUserType.NormalClient;
                if (this.globalCache.InsertCheckUserList(user))
                {
                    user.CheckType = CheckType.Register;
                    if (SendToServicesCheck(user)) //发送给所有在线的客服审核
                        return RegisterResult.Succeed;
                    else
                        return RegisterResult.Error;
                }
                else
                    return RegisterResult.Error;
            }
            catch (Exception ee)
            {
                return RegisterResult.Error;
            }
        }

        public List<GGUser> SearchUser(string idOrName)
        {
            return this.globalCache.SearchUser(idOrName);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public ChatRecordPage GetChatRecordPage(ChatRecordTimeScope timeScope, string senderID, string accepterID, int pageSize, int pageIndex)
        {
            return this.globalCache.GetChatRecordPage(timeScope ,senderID, accepterID, pageSize, pageIndex);
        }

        public ChatRecordPage GetGroupChatRecordPage(ChatRecordTimeScope timeScope, string groupID, int pageSize, int pageIndex)
        {
            ChatRecordPage page = this.globalCache.GetGroupChatRecordPage(timeScope ,groupID, pageSize, pageIndex);
            return page;
        }       
       
        public void InsertChatMessageRecord(ChatMessageRecord record)
        {
            //目前没有通过remoting插入数据库
        }
    }
}
