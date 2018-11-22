using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using HPSocketCS;
using System.Net;
using ESBasic.ObjectManagement.Managers;
using ESBasic.Security;
using System.Configuration;
using ESBasic;
using System.Configuration;
namespace GradeSystem.Server
{

    [StructLayout(LayoutKind.Sequential)]
    public class ClientInfo
    {
        public IntPtr ConnId { get; set; }
        public string IpAddress { get; set; }
        public ushort Port { get; set; }
    }
    class HPServerEngine:IRapidServerEngine
    {
        private AppState appState = AppState.Stoped;
        private static readonly object locker = new object();
        private static readonly object relogin = new object();
        private delegate void ShowMsg(string msg);
        public ushort severPort;
        public ushort ServerPort 
        { 
            get { return severPort; } 
        }
        public bool AutoRespondHeartBeat { get; set; }
        //
        // 摘要:
        //     通过此接口，服务端可以将目标用户从服务器中踢出，并关闭其对应的tcp连接。（只有在RapidServerEngine初始化成功后，才能正常使用。）
        public IBasicController BasicController { get; set; }
        //
        // 摘要:
        //     当前连接的数量。
        private int m_ConnectionCount = 0;
        public int ConnectionCount
        {
            get
            {
                return this.m_ConnectionCount;
            }
        }
        //
        // 摘要:
        //     通过此接口，服务端可以主动向在线用户发送/投递自定义信息。（只有在RapidServerEngine初始化成功后，才能正常使用。）
        private ICustomizeController customizeController ;
        public ICustomizeController CustomizeController 
        { 
            get { return customizeController;}
        }
        //
        // 摘要:
        //     心跳超时间隔（秒）。即服务端多久没有收到客户端的心跳消息，就视客户端为超时掉线。默认值为30秒。如果设置小于等于0，则表示不做心跳检查。（如果要set该属性，则必须在调用Initialize方法之前设置才有效。）
        public int m_HeartbeatTimeoutInSecs = 30;
        public int HeartbeatTimeoutInSecs 
        {
            get { return this.m_HeartbeatTimeoutInSecs; } 
            set{this.m_HeartbeatTimeoutInSecs=value;} 
        }
        //
        // 摘要:
        //     通过哪个IP地址提供服务。如果设为null，则表示绑定本地所有IP。默认值为null。（如果要set该属性，则必须在调用Initialize方法之前设置才有效。）
        public string IPAddressBinding { get; set; }
        //
        // 摘要:
        //     服务器允许最大的同时连接数。
        public int maxConnectionCount = 10000;
        public int MaxConnectionCount 
        {
            get { return maxConnectionCount; }
        }
        //
        // 摘要:
        //     服务端实例的唯一编号。该属性用于ESPlatform。
        private string serverID = "0x01";
        public string ServerID 
        { 
            get { return serverID; } 
        }

        //
        // 摘要:
        //     通过此接口，可以获取用户的相关信息以及用户上/下线的事件通知。（只有在RapidServerEngine初始化成功后，才能正常使用。）
        private IUserManager userManager;
        public IUserManager UserManager 
        { 
            get {return userManager; }
        }
        //
        // 摘要:
        //     当通过ICustomizeController.QueryClient方法进行同步调用时，等待回复的最长时间。如果小于等于0，表示一直阻塞调用线程直到等到回复为止。默认值为30秒。（如果要set该属性，则必须在调用Initialize方法之前设置才有效。）
        private int m_WaitResponseTimeoutInsecs=30;//同步调用的等待时间
        public int WaitResponseTimeoutInSecs 
        {
            get{return m_WaitResponseTimeoutInsecs;}
            set{m_WaitResponseTimeoutInsecs=value;} 
        }
        //
        // 摘要:
        //     发送消息的超时，单位：秒。默认值：5秒。如果设置为小于等于0，则表示无限。（如果要set该属性，则必须在调用Initialize方法之前设置才有效。）
        private int m_WriteTimeoutInSecs=5; 
        public int WriteTimeoutInSecs
        { 
            get{return m_WriteTimeoutInSecs; }
            set { m_WriteTimeoutInSecs = value; } 
        }
        private ShowMsg AddMsgDelegate;

        HPSocketCS.TcpServer server = new HPSocketCS.TcpServer();

        //连接服务器的数量，并不代表用户已经登录
        private HPSocketCS.Extra<ClientInfo> onConnectionList;
        //已经登录的在线用户列表;
        private ObjectManager<string, UserData> onLineList;
        private HPSocketCS.Extra<string> onLineUserList;
        
        private string title = "GradeSystemServer";
       
        private static HPServerEngine engine;
        private ICustomizeHandler handler;
        private IBasicHandler basicHander;
        private UserData delUserData;
        private bool bePushOut=false;

        
        private HPServerEngine()
        {
            
            onConnectionList = new HPSocketCS.Extra<ClientInfo>();
            onLineList = new ObjectManager<string, UserData>(); // key:用户ID 。 Value：用户信息
            onLineUserList = new Extra<string>();
            userManager = new MyUserMannager(this.onLineList);//传递在线用户List过去;
            customizeController = new MyCustomizeController(server,onLineList);
        }
        public static HPServerEngine GetInstance()
        {
            lock (locker)
            {
                if (engine == null)
                {
                    engine = new HPServerEngine();
                }
            }
            return engine;
        }
        public void Initialize(ushort port, ICustomizeHandler handler, IBasicHandler basicHander)
        {
            try
            {
                this.Initialize(port, handler);//初始化服务器端口以及自定义处理器
                userManager.Initialize();//初始化用户管理器;
                //userManager.UserDisplayer = this;
                this.basicHander = basicHander;//定义基础验证接口方法;
            }
            catch (Exception ex)
            {

            }
        }
        public void Initialize(ushort port, ICustomizeHandler customizeHandler)
        {
            this.severPort = port;
            this.handler = customizeHandler;//定义自定义回调接口;
            server.AcceptSocketCount = (uint)maxConnectionCount;
            server.MaxConnectionCount = (uint)maxConnectionCount;//最大连接数
            //server.KeepAliveTime = (uint)m_HeartbeatTimeoutInSecs;//心跳包时间;
            server.SocketBufferSize = 8096;//设置缓冲区大小;
            server.SendPolicy = SendPolicy.Pack;//设置发送策略;

            server.OnPrepareListen += new TcpServerEvent.OnPrepareListenEventHandler(OnPrepareListen);
            server.OnAccept += new TcpServerEvent.OnAcceptEventHandler(OnAccept);
            //server.OnSend += new TcpServerEvent.OnSendEventHandler(OnSend);
            // 两个数据到达事件的一种
            server.OnPointerDataReceive += new TcpServerEvent.OnPointerDataReceiveEventHandler(OnPointerDataReceive);
            // 两个数据到达事件的一种
            server.OnReceive += new TcpServerEvent.OnReceiveEventHandler(OnReceive);
            server.OnClose += new TcpServerEvent.OnCloseEventHandler(OnClose);
            server.OnShutdown += new TcpServerEvent.OnShutdownEventHandler(OnShutdown);
            SetAppState(AppState.Stoped);
            try
            {
                String ip = "0.0.0.0";
                // 写在这个位置是上面可能会异常
                SetAppState(AppState.Starting);
                server.IpAddress = ip;
                server.Port = ServerPort;
                // 启动服务
                if (server.Start())
                {
                    //this.Text = string.Format("{2} - ({0}:{1})", ip, port, title);
                    SetAppState(AppState.Started);
                   // AddMsg(string.Format("$Server Start OK -> ({0}:{1})", ip, port));
                }
                else
                {
                    SetAppState(AppState.Stoped);
                    throw new Exception(string.Format("$Server Start Error -> {0}({1})", server.ErrorMessage, server.ErrorCode));
                }
            }
            catch (Exception ex)
            {
               // AddMsg(ex.Message);
            }
        }
        public void SendMessage(string targetUserID, int informationType, byte[] message, string tag, int fragmentSize)
        {

            if (targetUserID == null)
            {
                return;
            }
            UserData ud = onLineList.Get(targetUserID);
            if (ud == null) return;
            try
            {
                ByteBuf write = ByteBuf.Allocate(1024);//写入内容;默认缓存区大小2M;
                write.AutoExpand = true;
                if (write.CanWrite)
                {
                    int len = 0;
                    write.WriteReverseInt(len);//数据总长度;
                    len += 4;

                    len += 4;
                    write.WriteReverseInt(informationType);//写入类型;
                    if (message == null)
                    {
                        len += 4;
                        write.WriteReverseInt(-1);
                    }
                    else
                    {
                        len += message.Length;//对象数据流长度;
                        write.Write(message, 0, message.Length);//附加对象字节流;
                    }
                    if (tag != null)
                    {
                        byte[] btag = System.Text.Encoding.UTF8.GetBytes(tag);
                        len += btag.Length;
                        write.Write(btag, 0, btag.Length);//附加tag内容;

                    }
                    else
                    {
                        len += 4;
                        write.WriteReverseInt(-1);
                    }
                    write.Rewind();//指针移植到最顶端;
                    write.WriteReverseInt(len);//重新写入总长度;
                    // 
                    byte[] des = write.ToArray();

                    server.Send(ud.ConnID, des, des.Length);//发送字节流;
                    //int sendtimes=0;//发送次数;
                    //int lastSize=0;//最后一次发送字节数;
                    // sendtimes=des.Length/fragmentSize;
                    //lastSize=des.Length%fragmentSize;
                    //if(lastSize>0)
                    //{
                    //    sendtimes+=1;
                    //}else
                    //    lastSize=fragmentSize;
                    //if(sendtimes==1)
                    //{
                    //    server.Send(ud.ConnID, des, des.Length);//发送字节流;
                    //}
                    //else 
                    //{
                    //    for (int i = 0; i < sendtimes; i++)
                    //    {
                    //        byte[] array = (i == sendtimes - 1) ? new byte[lastSize] : new byte[fragmentSize];
                    //        Array.Copy(des,0,array,0,array.Length);//复制数据流;
                    //        server.Send(ud.ConnID, array, array.Length);
                    //    }
                    //}
                    
                }


            }
            catch (Exception ex)
            {
            }

        }
        public void SendMessage(string targetUserID, int informationType, byte[] message, string tag)
        {
            SendMessage( targetUserID,  informationType,  message,  tag,2048);
        }
        
        //关闭服务器引擎
        public void Close()
        {
            try
            {
                SetAppState(AppState.Stoping);

                // 停止服务
                AddMsg("$Server Stop");
                if (server.Stop())
                {
                    SetAppState(AppState.Stoped);
                }
                else
                {
                    AddMsg(string.Format("$Stop Error -> {0}({1})", server.ErrorMessage, server.ErrorCode));
                }
            }
            catch (Exception ex)
            {

            }
        }
        void SetAppState(AppState state)
        {
            appState = state;
        }
        /// <summary>
        /// 可以往日志文件中加一条项目
        /// </summary>
        /// <param name="msg"></param>
        void AddMsg(string msg)
        {
            //if (this.lbxMsg.InvokeRequired)
            //{
            //    // 很帅的调自己
            //    this.lbxMsg.Invoke(AddMsgDelegate, msg);
            //}
            //else
            //{
            //    if (this.lbxMsg.Items.Count > 100)
            //    {
            //        this.lbxMsg.Items.RemoveAt(0);
            //    }
            //    this.lbxMsg.Items.Add(msg);
            //    this.lbxMsg.TopIndex = this.lbxMsg.Items.Count - (int)(this.lbxMsg.Height / this.lbxMsg.ItemHeight);
            //}
        }
        
        
        
        HandleResult OnPrepareListen(IntPtr soListen)
        {
            // 监听事件到达了,一般没什么用吧?

            return HandleResult.Ok;
        }

        HandleResult OnAccept(IntPtr connId, IntPtr pClient)
        {
            // 客户进入了


            // 获取客户端ip和端口
            string ip = string.Empty;
            ushort port = 0;
            if (server.GetRemoteAddress(connId, ref ip, ref port))
            {
               // AddMsg(string.Format(" > [{0},OnAccept] -> PASS({1}:{2})", connId, ip.ToString(), port));
            }
            else
            {
                //AddMsg(string.Format(" > [{0},OnAccept] -> Server_GetClientAddress() Error", connId));
            }


            // 设置附加数据
            ClientInfo clientInfo = new ClientInfo();
            clientInfo.IpAddress = ip;
            clientInfo.ConnId = connId;
            clientInfo.Port = port;
            if (onConnectionList.Set(connId, clientInfo) == false)
            {
                //AddMsg(string.Format(" > [{0},OnAccept] -> SetConnectionExtra fail", connId));
            }
            
            m_ConnectionCount++;//连接数增1;
            return HandleResult.Ok;
        }


        HandleResult OnPointerDataReceive(IntPtr connId, IntPtr pData, int length)
        {
            // 数据到达了
            try
            {
                // 可以通过下面的方法转换到byte[]
                byte[] bytes = new byte[length];
                Marshal.Copy(pData, bytes, 0, length);
                ByteBuf bs = ByteBuf.Wrap(bytes);//创建字节处理流;
                int dataLen = bs.ReadReverseInt();//获取数据总长度
                if (dataLen != length)//相对于一个校验吧;
                {
                    //IntPtr oldconnId = onLineList.Get(userID).ConnID;
                    server.Send(connId, new byte[] { 0x02 },1);//发送踢出代码，避免客户端重连;
                    server.Disconnect(connId, true);//
                    return HandleResult.Ignore;
                    //return HandleResult.Error;//断开连接并且不能重连; 
                }
                int informationtype = bs.ReadReverseInt();//不管什么都是先类型；之后才是对象流;
                if (informationtype < 0 || informationtype > 100)
                {
                    return HandleResult.Ignore;//不管，忽略;
                }
                if (informationtype == InformationTypes.LOGIN)//如果是登录的话
                {
                    string userID = SerializeHelper.ReadStrIntLen(bs);//读取userID;
                    string password = SerializeHelper.ReadStrIntLen(bs);//读取md5密码
                    ClientType type = (ClientType)Enum.ToObject(typeof(ClientType), bs.ReadReverseInt()); ;//读取客户端
                    string failedReason; LogonResult succ = LogonResult.Failed;
                  
                    if (basicHander.VerifyUser(userID, password, out failedReason))
                    {
                        //如果登录成功，则增加user信息：
                        ClientInfo ci = onConnectionList.Get(connId);
                        UserData ud = new UserData(ci);
                        ud.UserID = userID;
                        ud.TimeLogon = DateTime.Now;//登录时间;
                        ud.ClientType = type;//客户端类型;
                        succ=LogonResult.Succeed;//登录成功
                        if (onLineList.Contains(userID))//如果在线用户列表中有此用户
                        {
                            if (userManager.RelogonMode == RelogonMode.IgnoreNew)//如果是忽略新的
                            {
                                succ = LogonResult.HadLoggedOn;//已经有人登陆了
                                failedReason = "已经在别处登陆了";
                            }
                            else//踢出原先的用户连接;保留现在的连接
                            {
                                IntPtr oldconnId = onLineList.Get(userID).ConnID;
                                server.Send(oldconnId, new byte[] { 0x02 },1);//发送踢出代码，避免客户端重连;
                                //bePushOut = true;//表示被挤掉
                                
                                if (server.Disconnect(oldconnId, true))//踢出原先的用户
                                {
                                    //在Close事件中移除连接;
                                    //onConnectionList.Remove(oldconnId);//移除连接;
                                    lock (relogin)
                                    {
                                        delUserData = onLineList.Get(userID);//在线列表中移除此用户;
                                        userManager.TrigSomeOneDisconnected(delUserData, DisconnectedType.BeingPushedOut);
                                        onLineList.Remove(userID);//移除旧用户;
                                        onLineUserList.Remove(oldconnId);//移除

                                        onLineUserList.Set(connId, userID);//新连接与用户名的对应;
                                        onLineList.Add(userID, ud);//添加此用户;
                                        userManager.TrigSomeOneConnected(ud);//触发成功连接事件
                                    }
                                }
                            }
                        }
                        else//如果不包含此用户，
                        {
                            onLineUserList.Set(connId, userID);
                            onLineList.Add(userID, ud);//添加此用户;//触发登录成功连接事件
                            userManager.TrigSomeOneConnected(ud);//触发登录成功连接事件
                        }
                        
                       
                    }else{//登录失败，则发送失败理由
                        succ = LogonResult.Failed;//登录失败；
                    }
                     RespLogon resp=  new RespLogon(succ, failedReason);
                    byte[] info= ESPlus.Serialization.CompactPropertySerializer.Default.Serialize<RespLogon>(resp);
                    if (server.Send(connId, info,info.Length))//发送登录结果
                    {
                        return HandleResult.Ok;
                    }
                 }

                return HandleResult.Ok;
            }
            catch (Exception ex)
            {

                return HandleResult.Ignore;
            }
        }
        HandleResult OnReceive(IntPtr connId, byte[] bytes)
        {
            // 数据到达了
            try
            {
                // 获取附加数据
                //UserData clientInfo = onLineList.Get(onLineUserList.Get(connId));
                //if (clientInfo != null)
                //{
                //    // clientInfo 就是accept里传入的附加数据了
                //    string tag = System.Text.Encoding.UTF8.GetString(bytes);//得到utf-8字符串，解析数据；
                //    //AddMsg(string.Format(" > [{0},OnReceive msg is] -> {1}", clientInfo.ConnId, tag));
                //    //AddMsg(string.Format(" > [{0},OnReceive] -> {1}:{2} ({3} bytes)", clientInfo.ConnId, clientInfo.IpAddress, clientInfo.Port, bytes.Length));
                //}
                //else
                //{
                //    AddMsg(string.Format(" > [{0},OnReceive] -> ({1} bytes)", connId, bytes.Length));
                //}

                ////if (server.Send(connId, bytes, bytes.Length))
                //{
                //    return HandleResult.Ok;
                //}

                return HandleResult.Ignore;
            }
            catch (Exception)
            {

                return HandleResult.Ignore;
            }
        }
        
        HandleResult OnClose(IntPtr connId, SocketOperation enOperation, int errorCode)
        {
        //    string errdes = server.(SocketError(errorCode));
           // if (errorCode == 0)//如果正确移除用户;
            lock(relogin)
            {
                if (onConnectionList.Remove(connId))
                {
                   
                    string userid=onLineUserList.Get(connId);//得到被删除的userid;
                    if (userid != null)
                    {
                        UserData target = onLineList.Get(userid);
                        if (target != null)
                        {
                            userManager.TrigSomeOneDisconnected(target, DisconnectedType.HeartBeatTimeout);
                            onLineList.Remove(userid);//移除
                            onLineUserList.Remove(connId);//移除
                        }
                    }
                    m_ConnectionCount--;//连接数减1;
                    
                   
                }
                
            }


            return HandleResult.Ok;
        }

        HandleResult OnShutdown()
        {
            // 服务关闭了


            //AddMsg(" > [OnShutdown]");
            return HandleResult.Ok;
        }
    }
}
