using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ESBasic;
using ESBasic.ObjectManagement.Managers;
using System.Threading.Tasks;

namespace GradeSystem.Server
{
    // 摘要:
    //     用于显示所有在线用户的状态信息。 zhuweisky 2004.04.22
    public interface IUserDisplayer
    {
        void AddUser(string userID, ClientType clientType, string userAddress);
        //
        // 摘要:
        //     清除所有。通常是通信引擎停止时被调用。
        void ClearAll();
        void OnMessageReceived(string userID, int messageType);
        void OnMessageSent(string userID, int messageType);
        //
        // 摘要:
        //     移除用户。通常在用户下线时被调用。
        void RemoveUser(string userID, string cause);
    }
    public interface IBasicHandler
    {
        // 摘要:
        //     客户端登陆验证。
        //
        // 参数:
        //   userID:
        //     登陆用户账号
        //
        //   systemToken:
        //     系统标志。用于验证客户端是否与服务端属于同一系统。
        //
        //   password:
        //     登陆密码
        //
        //   failureCause:
        //     如果登录失败，该out参数指明失败的原因
        //
        // 返回结果:
        //     如果密码和系统标志都正确则返回true；否则返回false。
        bool VerifyUser( string userID, string password, out string failureCause);
    }
    // 摘要:
    //     自定义信息处理器接口。
    public interface ICustomizeHandler
    {
        // 摘要:
        //     处理接收到的自定义信息（包括大数据块信息）。
        //
        // 参数:
        //   sourceUserID:
        //     发送该信息的用户ID。如果为null，表示信息发送者为服务端。
        //
        //   informationType:
        //     自定义信息类型
        //
        //   info:
        //     信息
        void HandleInformation(string sourceUserID, int informationType, byte[] info);
        //
        // 摘要:
        //     处理接收到的请求并返回应答信息。
        //
        // 参数:
        //   sourceUserID:
        //     发送该请求信息的用户ID。如果为null，表示信息发送者为服务端。
        //
        //   informationType:
        //     自定义请求信息的类型
        //
        //   info:
        //     请求信息
        //
        // 返回结果:
        //     应答信息
        byte[] HandleQuery(string sourceUserID, int informationType, byte[] info);
    }
    public interface IUserManager
    {
        // 摘要:
        //     重登陆模式。
        RelogonMode RelogonMode { get; set; }
        //
        // 摘要:
        //     当前在线用户的数量。
        int UserCount { get; }
        //
        // 摘要:
        //     用户管理器依赖该属性显示所有在线用户的状态信息。
        IUserDisplayer UserDisplayer { set; }

        // 摘要:
        //     如果RelogonMode为IgnoreNew，并且当从一个新连接上收到一个同名ID用户的消息时将触发此事件。 注意，只有在该事件处理完毕后，才会关闭新连接。可以在该事件的处理函数中，将相关情况通知给客户端。
        //     【一般情况下，都会由登录回复告知客户端已经登录，而不会进入到这里触发该事件！】
        event CbGeneric<string, IPEndPoint> NewConnectionIgnored;
       
        //
        // 摘要:
        //     如果RelogonMode为ReplaceOld，并且当从另外一个新连接上收到一个同名ID用户的消息时将触发此事件。 注意，只有在该事件处理完毕后，才会真正关闭旧的连接并使用新的地址取代旧的地址。可以在该事件的处理函数中，将相关情况通知给旧连接的客户端。
        void TrigSomeOneBeingPushedOut(UserData ud);//
        event CbGeneric<UserData> SomeOneBeingPushedOut;
        //
        // 摘要:
        //     当客户端登录成功时，触发此事件。不要远程预定该事件。
       
        void TrigSomeOneConnected(UserData ud);//
        event CbGeneric<UserData> SomeOneConnected;
        //
        // 摘要:
        //     客户端连接断开下线时，触发此事件。不要远程预定该事件。
        void TrigSomeOneDisconnected(UserData target, DisconnectedType type);
        event CbGeneric<UserData, DisconnectedType> SomeOneDisconnected;
        //
        // 摘要:
        //     用户心跳超时。 只有在该事件处理完毕后，才关闭对应的连接，并将其从用户列表中删除。可以在该事件的处理函数中，将相关情况通知给客户端。
        event CbGeneric<UserData> SomeOneTimeOuted;
        //
        // 摘要:
        //     当在线用户数发生变化时，触发此事件。
        event CbGeneric<int> UserCountChanged;
        //
        // 摘要:
        //     当某个用户的携带数据被修改时，将触发此事件。参数为 UserID - tag
        event CbGeneric<string, object> UserTagChanged;

        // 摘要:
        //     获取所有在线用户信息。
        List<UserData> GetAllUserData();
        //
        // 摘要:
        //     获取在线用户的ID列表。
        List<string> GetOnlineUserList();
        //
        // 摘要:
        //     如果用户不在线，返回null
        IPEndPoint GetUserAddress(string userID);
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
        UserData GetUserData(string userID);
        //
        // 摘要:
        //     初始化用户管理器。
        void Initialize();
        //
        // 摘要:
        //     目标用户是否在线。
        bool IsUserOnLine(string userID);
        //
        // 摘要:
        //     该方法用于Platform，用于接受平台的回调通知。当RelogonMode为ReplaceOld时，同名用户在同一群集的其它的应用服务器上登陆。将触发SomeOneBeingPushedOut事件。
        //
        // 参数:
        //   userID:
        //     同名登陆的用户ID。
        //
        //   newServerID:
        //     新登录到哪台服务器。
        //void LogonOtherServer(string userID, string newServerID);
        //
        // 摘要:
        //     从目标用户集合中挑出在线用户的ID列表。
       // List<string> SelectOnlineUserFrom(IEnumerable<string> users);
       
    }
    // 摘要:
    //     服务端主动向用户发送/投递自定义信息以及同步调用客户端的控制接口。 zhuweisky 2010.08.17
    public interface ICustomizeController
    {
        // 摘要:
        //     当服务端接收到来自客户端的信息时（包括转发的信息，但不包括Blob信息），触发此事件。
        //event CbGeneric<Information> InformationReceived;
        

        
        //
        // 摘要:
        //     向ID为userID的在线用户发送信息。如果用户不在线，则直接返回。
        //
        // 参数:
        //   userID:
        //     接收消息的用户ID
        //
        //   informationType:
        //     自定义信息类型
        //
        //   info:
        //     信息
        void Send(string userID, int informationType, byte[] info);
        //
        // 摘要:
        //     向ID为userID的在线用户发送信息。如果用户不在线，则直接返回。
        //
        // 参数:
        //   userID:
        //     接收消息的用户ID
        //
        //   informationType:
        //     自定义信息类型
        //
        //   info:
        //     信息
        //
        //   post:
        //     是否采用Post模式发送消息
        //
        //   action:
        //     当通道繁忙时所采取的动作
        //
        // 返回结果:
        //     如果成功发送，将返回true；否则，（比如丢弃）返回false。
       
        //
        // 摘要:
        //     向ID为userID的在线用户发送大数据块信息。直到数据发送完毕，该方法才会返回。如果担心长时间阻塞调用线程，可考虑异步调用本方法。
        //
        // 参数:
        //   userID:
        //     接收消息的用户ID
        //
        //   informationType:
        //     自定义信息类型
        //
        //   blobInfo:
        //     大数据块信息
        //
        //   fragmentSize:
        //     分片传递时，片段的大小
        void SendBlob(string userID, int informationType, byte[] blobInfo, int fragmentSize);
        //
        // 摘要:
        //     向当前AS上的在线用户发送信息，并等待其ACK。当前调用线程会一直阻塞，直到收到ACK；如果超时都没有收到ACK，则将抛出Timeout异常。
        //
        // 参数:
        //   userID:
        //     接收消息的用户ID
        //
        //   informationType:
        //     自定义信息类型
        //
        //   info:
        //     信息
        //void SendCertainlyToLocalClient(string userID, int informationType, byte[] info);
        //
        // 摘要:
        //     向当前AS上的在线用户发送信息。当前调用线程会立即返回。当收到ACK或者超时都没有收到ACK，将回调ResultHandler。
        //
        // 参数:
        //   userID:
        //     接收消息的用户ID
        //
        //   informationType:
        //     自定义信息类型
        //
        //   info:
        //     信息
        //
        //   handler:
        //     被回调的处理器
        //
        //   tag:
        //     携带的状态数据，将被传递给回调函数handler
        //void SendCertainlyToLocalClient(string userID, int informationType, byte[] info, ResultHandler handler, object tag);
    }
    // 摘要:
    //     直接在从服务端发出相关控制指令（如踢人等）。
    public interface IBasicController
    {
        // 摘要:
        //     将目标用户从当前AS中踢出，并关闭对应的连接。
        void KickOut(string targetUserID);
    }
    // 摘要:
    //     迅捷的服务端引擎。基于TCP、使用二进制协议。
    public interface IRapidServerEngine 
    {
        
        //
        // 摘要:
        //     是否自动回复客户端发过来的心跳消息（将心跳消息原封不动地发回客户端）。默认值为false。（如果要set该属性，则必须在调用Initialize方法之前设置才有效。）
        bool AutoRespondHeartBeat { get; set; }
        //
        // 摘要:
        //     通过此接口，服务端可以将目标用户从服务器中踢出，并关闭其对应的tcp连接。（只有在RapidServerEngine初始化成功后，才能正常使用。）
        IBasicController BasicController { get; set; }
        //
        // 摘要:
        //     当前在线连接的数量。
        int ConnectionCount { get; }
        
        //
        // 摘要:
        //     通过此接口，服务端可以主动向在线用户发送/投递自定义信息。（只有在RapidServerEngine初始化成功后，才能正常使用。）
        ICustomizeController CustomizeController { get; }
        
        //
        // 摘要:
        //     心跳超时间隔（秒）。即服务端多久没有收到客户端的心跳消息，就视客户端为超时掉线。默认值为30秒。如果设置小于等于0，则表示不做心跳检查。（如果要set该属性，则必须在调用Initialize方法之前设置才有效。）
        int HeartbeatTimeoutInSecs { get; set; }
        //
        // 摘要:
        //     通过哪个IP地址提供服务。如果设为null，则表示绑定本地所有IP。默认值为null。（如果要set该属性，则必须在调用Initialize方法之前设置才有效。）
        string IPAddressBinding { get; set; }
        //
        // 摘要:
        //     服务器允许最大的同时连接数。
        int MaxConnectionCount { get; }
        
        //
        // 摘要:
        //     当前监听的端口。
        ushort ServerPort { get; }
        //
        // 摘要:
        //     服务端实例的唯一编号。该属性用于ESPlatform。
        string ServerID { get; }
        
        //
        // 摘要:
        //     通过此接口，可以获取用户的相关信息以及用户上/下线的事件通知。（只有在RapidServerEngine初始化成功后，才能正常使用。）
        IUserManager UserManager { get; }
        //
        // 摘要:
        //     当通过ICustomizeController.QueryClient方法进行同步调用时，等待回复的最长时间。如果小于等于0，表示一直阻塞调用线程直到等到回复为止。默认值为30秒。（如果要set该属性，则必须在调用Initialize方法之前设置才有效。）
        int WaitResponseTimeoutInSecs { get; set; }
        //
        // 摘要:
        //     发送消息的超时，单位：秒。默认值：5秒。如果设置为小于等于0，则表示无限。（如果要set该属性，则必须在调用Initialize方法之前设置才有效。）
        int WriteTimeoutInSecs { get; set; }

        // 摘要:
        //     当接收到来自客户端的消息时，触发此事件。 事件参数：sourceUserID - informationType - message - tag
        //     。
       // event CbGeneric<string, int, byte[], string> MessageReceived;

        // 摘要:
        //     关闭服务端引擎。
        void Close();
        //
        // 摘要:
        //     完成服务端引擎的初始化，并启动服务端引擎。
        //
        // 参数:
        //   port:
        //     用于提供tcp通信服务的端口
        //
        //   customizeHandler:
        //     服务器通过此接口来处理客户端提交给服务端自定义信息。
        void Initialize(ushort port, ICustomizeHandler customizeHandler);
        //
        // 摘要:
        //     完成服务端引擎的初始化，并启动服务端引擎。
        //
        // 参数:
        //   port:
        //     用于提供tcp通信服务的端口
        //
        //   customizeHandler:
        //     服务器通过此接口来处理客户端提交给服务端自定义信息。
        //
        //   basicHandler:
        //     用于验证客户端登陆密码。如果不需要验证，则直接传入null。
        void Initialize(ushort port, ICustomizeHandler customizeHandler, IBasicHandler basicHandler);
        //
        // 摘要:
        //     向在线用户发送消息。如果目标用户不在线，消息将被丢弃。
        //
        // 参数:
        //   targetUserID:
        //     接收者的UserID
        //
        //   informationType:
        //     消息类型
        //
        //   message:
        //     消息内容
        //
        //   tag:
        //     附加内容
        void SendMessage(string targetUserID, int informationType, byte[] message, string tag);
        //
        // 摘要:
        //     向在线用户发送消息。如果目标用户不在线，消息将被丢弃。
        //
        // 参数:
        //   targetUserID:
        //     接收者的UserID
        //
        //   informationType:
        //     消息类型
        //
        //   message:
        //     消息内容
        //
        //   tag:
        //     附加内容
        //
        //   fragmentSize:
        //     消息将被分块发送，分块的大小
        void SendMessage(string targetUserID, int informationType, byte[] message, string tag, int fragmentSize);
    }
}
