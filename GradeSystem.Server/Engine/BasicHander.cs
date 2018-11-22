using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeSystem.Server
{
    /// <summary>
    /// 基础处理器，用于验证登陆的用户。
    /// </summary>
    internal class BasicHandler : IBasicHandler
    {
        private GlobalCache globalCache;
        public BasicHandler(GlobalCache db)
        {
            this.globalCache = db;
        }

        /// <summary>
        /// 此处验证用户的账号和密码。返回true表示通过验证。
        /// </summary>  
        public bool VerifyUser( string userID, string password, out string failureCause)
        {
            failureCause = "";
            LoginUser user = this.globalCache.GetUser(userID);
            if (user == null)
            {
                failureCause = "用户不存在！";
                return false;
            }

            if (user.PasswordMD5 != password)
            {
                failureCause = "密码错误！";
                return false;
            }
            if (!user.IsActivited)
            {
                failureCause = "账号没激活";
                return false;
            }
            if (user.UserType == EUserType.Administrator)
            {
                failureCause = "无此用户!";
                return false;
            }
            return true;
        }
    }
}
