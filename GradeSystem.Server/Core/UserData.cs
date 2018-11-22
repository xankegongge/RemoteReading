using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace GradeSystem.Server
{
    // 摘要:
    //     登陆用户的基础信息。可用于独立的应用服务器。
    [Serializable]
    public class UserData
    {
        public UserData()
        {
        }
        public UserData(ClientInfo info)
        {
            this.IPAddress = info.IpAddress;
            this.Port = info.Port;
            this.ConnID = info.ConnId;
        }
        public UserData(string _userID, string _ipaddress,
            ushort port,string serverID, ClientType _clientType, DateTime logon,
            IntPtr conid)
        {
            this.UserID = _userID;
            this.IPAddress = _ipaddress;
            this.Port = port;
            this.AppServerID = serverID;
            this.ClientType = _clientType;
            this.TimeLogon = logon;
            this.ConnID=conid;
        }
        // 摘要:
        //     服务器看到的客户端的地址（通常是经过NAT之后的地址）。
        public string  IPAddress { get; set; }
        public IntPtr ConnID { get; set; }
        public ushort Port { get; set; }
        //
        // 摘要:
        //     在ESPlatform群集环境中，目标用户所连接的应用服务器ID。
        public string AppServerID { get; set; }
        //
        // 摘要:
        //     客户端类型
        public ClientType ClientType { get; set; }
        //
        // 摘要:
        //     用户的携带数据（应用程序可以使用该属性保存与当前用户相关的其它信息），仅在本地使用。不会在ACMS和AS之间同步。
        public object LocalTag { get; set; }
       
        //
        // 摘要:
        //     用户的携带数据（应用程序可以使用该属性保存与当前用户相关的其它信息），该Tag会在ACMS和AS之间自动同步。该Tag指向的对象必须可序列化。
        public object Tag { get; set; }
        //
        // 摘要:
        //     上线时间。
        public DateTime TimeLogon { get; set; }
        public string UserID { get; set; }

      //  public void SetP2PAddress(P2PAddress addr);
        public override string ToString()
        {
            return UserID;
        }
    }
    public class ChangePasswordContract
    {
        public ChangePasswordContract() { }
        public ChangePasswordContract(string oldPasswordMD5, string newPasswordMD5)
        {
            this.OldPasswordMD5 = oldPasswordMD5;
            this.NewPasswordMD5 = newPasswordMD5;
        }

        public string OldPasswordMD5 { get; set; }

        public string NewPasswordMD5 { get; set; }
    }

    public class UserStatusChangedContract
    {
        public UserStatusChangedContract() { }
        public UserStatusChangedContract(string userID, int newStatus)
        {
            this.UserID = userID;
            this.NewStatus = newStatus;
        }

        public string UserID { get; set; }

        public int NewStatus { get; set; }
    }
}
