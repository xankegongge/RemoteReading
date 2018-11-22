using System;
using System.Collections.Generic;
using System.Linq;
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
using ESBasic.Security;
using System.Collections.Generic;
using System.Collections.Specialized;
using RemoteReading.Core;
namespace RemoteReading.Server
{
    public class MailSend
    {
        private GlobalCache globalCache;
        public MailSend(GlobalCache globalCache)
        {
            this.globalCache = globalCache;
        }
        public static string getRandomString(int count)
        {
            int number;
            string checkCode = String.Empty;     //存放随机码的字符串 

            System.Random random = new Random();

            for (int i = 0; i < count; i++) //产生4位校验码 
            {
                number = random.Next();
                number = number % 36;
                if (number < 10)
                {
                    number += 48;    //数字0-9编码在48-57 
                }
                else
                {
                    number += 55;    //字母A-Z编码在65-90 
                }

                checkCode += ((char)number).ToString();
            }
            return checkCode;
        }
        public string RepalceHTML(string userid,string content)
        {
            string templetpath = AppDomain.CurrentDomain.BaseDirectory + "mailtemplate\\emailTemplate.html";
            NameValueCollection myCol = new NameValueCollection();
            myCol.Add("ename", userid);
            myCol.Add("content", content);
            myCol.Add("link", "http://www.medicaldl.com/center.asp");
            myCol.Add("time", DateTime.Now.ToShortDateString());
            string mailBody = TemplateHelper.BulidByFile(templetpath, myCol);
            return mailBody;
        }
        public  bool MailSendResetPasswd(string email,string userid)
        {
             string newpasswd; string passMD5;
            try
            {
                newpasswd = getRandomString(6);
                MailHelper mail = new MailHelper();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" 您在" + DateTime.Now.ToShortDateString() + " 提交了找回密码请求，您的新密码为:");
                sb.AppendLine( newpasswd);
                sb.AppendLine(",建议您登录之后，立刻修改密码!");
                string htmlstr = RepalceHTML(userid, sb.ToString());
                mail.SendAsync("密码重置", htmlstr, email, emailCompleted);
                passMD5 = SecurityHelper.MD5String(newpasswd, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                return false;
            }

            return this.globalCache.UpdatePasswd(userid, passMD5);//更新数据库;
        }
        /// <summary>
        /// 发送一封审核不通过的邮件
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool MailSendOneCheckPass(GGUser user,string email,bool ispassed)
        {
            try
            {
                MailHelper mail = new MailHelper();
                StringBuilder sb = new StringBuilder();
                if (ispassed)
                {
                  
                    sb.AppendLine("您在远程阅片系统提交的审核请求已经审核通过！");
                    
                }
                else
                {
                    sb.AppendLine("尊敬的用户：");
                    
                }
                string htmlstr = RepalceHTML(user.UserID, sb.ToString());
                string title = user.CheckType == CheckType.Register ? "远程阅片系统注册审核结果" : "远程阅片系统信息更新审核结果";
                mail.SendAsync(title, htmlstr, email, emailCompleted);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        static void emailCompleted(string message)
        {
            if (message == "true")
            {
                System.Console.WriteLine("邮件发送成功\n");
            }

        }
    }
}
