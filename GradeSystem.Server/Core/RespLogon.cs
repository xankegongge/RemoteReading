using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeSystem.Server
{
    [Serializable]
    public partial class RespLogon
    {
        public LogonResult LoginResult{get;set;}
        public string Failure { get; set; }//失败理由;
        public RespLogon(LogonResult login, string fail)
        {
            LoginResult=login;
            Failure = fail;
        }
        public RespLogon()
        {
        }
        
    }
}
