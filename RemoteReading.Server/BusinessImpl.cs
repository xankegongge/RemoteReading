using System;
using System.Collections.Generic;
using System.Text;
using Thrift;
using Thrift.Collections;
using Thrift.Protocol;
using Thrift.Transport;
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
using RemoteReading.Core;
namespace RemoteReading.Server
{
    public class BusinessImpl : RegisterRPC.Iface
    {
        private GlobalCache globalCache;
        private IRapidServerEngine rapidServerEngine;
        public BusinessImpl()
        {

        }
        public BusinessImpl(GlobalCache globalCache,IRapidServerEngine engine)
        {
            this.globalCache = globalCache;
            this.rapidServerEngine = engine;
        }
        List<Hospital> RegisterRPC.Iface.GetAllHospitals()
        {
            return this.globalCache.GetAllHospitals();
        }
        bool RegisterRPC.Iface.ResetPassword(string email)
        {

            string userID; 
            try
            {
                this.globalCache.IsExitEmail(email, out userID);
	            if (userID == null)
	            {
	                return false;
	            }
                
            }
            catch (System.Exception ex)
            {
                return false;
            }
                MailSend ms = new MailSend(this.globalCache);
                return ms.MailSendResetPasswd(email, userID);//发送邮件;;
           
        }
        public bool SendToServicesCheck(GGUser sendUser)
        {
            //GGUser user = this.globalCache.GetUser(target);
            if (sendUser == null)
            {
                return false;
                   
            }
            byte[] sendinfo= CompactPropertySerializer.Default.Serialize<GGUser>(sendUser);
            //获取在线的客服列表；
            List<string> servicers=this.globalCache.GetServicersID();
            if (servicers.Count > 0)
            {
                foreach (string servicer in servicers)
                {
                    //可能要分块发送
                    this.rapidServerEngine.CustomizeController.Send(servicer, InformationTypes.NewCheckUser, sendinfo, true, ActionTypeOnChannelIsBusy.Continue);
                }
            }
            //当客服不在线
            else
            {
                
            }
            return true;
        }
       
        
        int RegisterRPC.Iface.Register(string userid, string pwd, string personname, string mobilephone, string email, int hospitalID, int title, int usertype)
        {
            try
            {
                EUserType userType = (EUserType)usertype;
                if (this.globalCache.IsUserExist(userid))
                {
                    return (int)RegisterResult.Existed;
                }
                if (globalCache.IsCheckedCacheExist(userid))
                {
                    return (int)RegisterResult.Existed;
                }
                string useridbyemail;
                if (this.globalCache.IsExitEmail(email,out useridbyemail)||this.globalCache.IsExitEmailInCheckCache(email))
                {
                    return (int)RegisterResult.EmailExisted;
                }
                if (this.globalCache.IsExitMobilePhone(mobilephone)||this.globalCache.IsExitMobilePhoneInCache(mobilephone))
                {
                    return (int)RegisterResult.MobileExisted;
                }
                Hospital hospital=globalCache.GetHostpitalName(hospitalID);

                UserContact us = new UserContact(personname, mobilephone, email, (EProfessionTitle)title, hospitalID);
                
                GGUser user = new GGUser(userid, pwd, "", "", "", 0, "",
                                     userType, false, us, null);
                user.Hospi = hospital;
                if (this.globalCache.IsUserExist(user.UserID))
                {
                    return (int)RegisterResult.Existed;
                }
                user.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//设置时间;
                user.IsChecking = true;
                user.CheckType = CheckType.Register;
                user.IsActivited = false;//
                user.UserType = EUserType.NormalClient;
               //这里由客服审核是否为专家，如果是专家，则添加至所有的客户好友列表，如果是客户，则添加所有的专家为好友。
                if (this.globalCache.InsertCheckUserList(user))
                {
                   if(SendToServicesCheck(user)) //发送给所有在线的客服审核
                        return (int)RegisterResult.Succeed;
                   else
                       return (int)RegisterResult.Error;
                }
                else
                    return (int)RegisterResult.Error;
            }
            catch (Exception ee)
            {
                return (int)RegisterResult.Error;
            }
        }
    }
}
