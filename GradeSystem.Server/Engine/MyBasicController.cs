using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESBasic;
using ESBasic.ObjectManagement.Managers;
namespace GradeSystem.Server
{
    class MyBasicController:IBasicController
    {
        private HPSocketCS.TcpServer server;
        private ObjectManager<string, UserData> onLineList;
        public MyBasicController(HPSocketCS.TcpServer server,  ObjectManager<string, UserData> onLineUserList)
        {
            this.server = server;
            this.onLineList = onLineUserList;
        }
        // 摘要:
        //     将目标用户从当前AS中踢出，并关闭对应的连接。
       public void KickOut(string targetUserID)
        {
            IntPtr connId = onLineList.Get(targetUserID).ConnID;
                // 断开指定客户
            if (server.Disconnect(connId, true))
            {
                
            }
        }
    }
}
