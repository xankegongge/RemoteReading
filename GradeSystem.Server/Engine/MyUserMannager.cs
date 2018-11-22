using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESBasic;
using ESBasic.ObjectManagement.Managers;
using System.Net;
namespace GradeSystem.Server
{
    public class MyUserMannager:IUserManager
    {
       
       private ObjectManager<string, UserData> onLineList;
       public MyUserMannager(ObjectManager<string, UserData> onLineList)
       {
           this.onLineList = onLineList;
       }
       //
       // 摘要:
       //     初始化用户管理器。
       public void Initialize()
       {
           //RelogonMode = RelogonMode.ReplaceOld;
           userCount = 0;//在线人数;
         //  SomeOneConnected = new CbGeneric<UserData>(new UserData());
       }
       // 摘要:
       //     重登陆模式。
       public RelogonMode RelogonMode { get; set; }
       //
       // 摘要:
       //     当前在线用户的数量。
       private int userCount = 0;
       public int UserCount { get { return this.onLineList.Count; } }
       //
       // 摘要:
       //     用户管理器依赖该属性显示所有在线用户的状态信息。
       private IUserDisplayer userDisplayer;
       public IUserDisplayer UserDisplayer { set { userDisplayer = value; } }

       // 摘要:
       //     如果RelogonMode为IgnoreNew，并且当从一个新连接上收到一个同名ID用户的消息时将触发此事件。 注意，只有在该事件处理完毕后，才会关闭新连接。可以在该事件的处理函数中，将相关情况通知给客户端。
       //     【一般情况下，都会由登录回复告知客户端已经登录，而不会进入到这里触发该事件！】
       public event CbGeneric<string, IPEndPoint> NewConnectionIgnored;

       //
       // 摘要:
       //     如果RelogonMode为ReplaceOld，并且当从另外一个新连接上收到一个同名ID用户的消息时将触发此事件。 注意，只有在该事件处理完毕后，才会真正关闭旧的连接并使用新的地址取代旧的地址。可以在该事件的处理函数中，将相关情况通知给旧连接的客户端。

      public void TrigSomeOneBeingPushedOut(UserData ud)
       {
           SomeOneBeingPushedOut(ud);//触发此事件;
       }
        public event CbGeneric<UserData> SomeOneBeingPushedOut;
       //
       // 摘要:
       //     当客户端登录成功时，触发此事件。不要远程预定该事件。
        public void TrigSomeOneConnected(UserData ud)
        {
            userDisplayer.AddUser(ud.UserID, ud.ClientType, ud.IPAddress) ;
            SomeOneConnected(ud);//触发此事件;
        }
       public event CbGeneric<UserData> SomeOneConnected;
       //
       // 摘要:
       //     客户端连接断开下线时，触发此事件。不要远程预定该事件。
       public void TrigSomeOneDisconnected(UserData target, DisconnectedType type)
       {
           userDisplayer.RemoveUser(target.UserID,  "");
           SomeOneDisconnected(target, type);//触发此事件;
       }
       public event CbGeneric<UserData, DisconnectedType> SomeOneDisconnected;
       //
       // 摘要:
       //     用户心跳超时。 只有在该事件处理完毕后，才关闭对应的连接，并将其从用户列表中删除。可以在该事件的处理函数中，将相关情况通知给客户端。
       public event CbGeneric<UserData> SomeOneTimeOuted;
       //
       // 摘要:
       //     当在线用户数发生变化时，触发此事件。
       public event CbGeneric<int> UserCountChanged;
       //
       // 摘要:
       //     当某个用户的携带数据被修改时，将触发此事件。参数为 UserID - tag
       public event CbGeneric<string, object> UserTagChanged;
       //
       // 摘要:
       //     目标用户是否在线。
       public bool IsUserOnLine(string userID)
       {
           return this.onLineList.Contains(userID);
          // return false;
       }
       // 摘要:
       //     获取所有在线用户信息。
       public List<UserData> GetAllUserData()
       {
           return this.onLineList.GetAll();
       }
       //
       // 摘要:
       //     获取在线用户的ID列表。
       public List<string> GetOnlineUserList()
       {
           return this.onLineList.GetKeyList();
       }
       //
       // 摘要:
       //     如果用户不在线，返回null
       public IPEndPoint GetUserAddress(string userID)
       {
           UserData target = onLineList.Get(userID);
           if (target == null) return null;
           System.Net.IPAddress IPadr = System.Net.IPAddress.Parse(target.IPAddress);//先把string类型转
           int port = target.Port;
           return new IPEndPoint(IPadr, port);
       }
       //
       // 摘要:
       //     获取目标在线用户的基础信息。
       //
       // 参数:
       //   userID:
       //     目标用户的ID
       //
       // 返回结果:
       //     如果目标用户不在线，则返回null
       public UserData GetUserData(string userID)
       {
           return onLineList.Get(userID);
       }
    }
}
