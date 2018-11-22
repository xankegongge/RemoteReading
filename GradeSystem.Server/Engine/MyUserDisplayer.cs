using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeSystem.Server
{
    class MyUserDisplayer:IUserDisplayer
    {
        public MyUserDisplayer()
        {
        }
        public void AddUser(string userID, ClientType clientType, string userAddress)
        {
        }
        //
        // 摘要:
        //     清除所有。通常是通信引擎停止时被调用。
        public void ClearAll()
        {
        }
        public void OnMessageReceived(string userID, int messageType)
        {
        }
        public void OnMessageSent(string userID, int messageType)
        {
        }
        //
        // 摘要:
        //     移除用户。通常在用户下线时被调用。
        public void RemoveUser(string userID, string cause)
        {
        }
    }
}
