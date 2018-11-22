using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESPlus.Rapid;
using ESPlus.Application.CustomizeInfo.Server;
using ESFramework;
using ESFramework.Server.UserManagement;
using ESPlus.Core;
using ESPlus.Application.Friends.Server;
using System.Configuration;
using OMCS;
using OMCS.Server;
using ESPlus.Application.CustomizeInfo;
using System.Runtime.Remoting;
using JustLib.NetworkDisk.Server;
using System.Drawing;
/*
 * 
 */
namespace RemoteReading.Server
{
    static class Program
    {
        private static IRapidServerEngine RapidServerEngine = RapidEngineFactory.CreateServerEngine();
        private static IMultimediaServer MultimediaServer;
        public  delegate void ServerRPCDelegate(GlobalCache globalCache);
        private static void ServersStart(GlobalCache globalCache)
        {
            ServerRPC server = new ServerRPC(globalCache, Program.RapidServerEngine);

            server.Start();//启动RPC注册服务
        }
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                IDBPersister persister; string administratorID;
                //if (bool.Parse(ConfigurationManager.AppSettings["UseVirtualDB"]))
                //{
                //    persister = new VirtualDB();
                //}
                //else
                {
                    persister = new RealDB( ConfigurationManager.AppSettings["DBName"] ,ConfigurationManager.AppSettings["DBIP"], ConfigurationManager.AppSettings["SaPwd"]);
                    //IList<Hospital> ls=persister.GetHospitals();//测试用

                }
                //string path="D:\\RemoteaPicData\\2016-08-23\\100001-100002-13288-1";
                //string savepath="D:\\RemoteaPicData\\2016-08-23\\1.jpg";
                //Bitmap bm = new Bitmap(path);

                //ThumbnailMaker.MakeThumbnail(bm, savepath,200, 200, ThumbnailMode.UsrHeightWidthBound);
                // ThumbnailMaker.MakeThumbnail(path, savepath,200, 200,ThumbnailMode.UsrHeightWidthBound);
                GlobalCache globalCache = new GlobalCache(persister);


                #region 初始化ESFramework服务端引擎
                ESPlus.GlobalUtil.SetAuthorizedUser("FreeUser", "");
                ESPlus.GlobalUtil.SetMaxLengthOfUserID(20);
                ESPlus.GlobalUtil.SetMaxLengthOfMessage(1024 * 1024 * 10);
                
                //自定义的好友管理器
                FriendsManager friendManager = new FriendsManager(globalCache);
                Program.RapidServerEngine.FriendsManager = friendManager;
                //自定义的组管理器
                GroupManager groupManager = new GroupManager(globalCache);
                Program.RapidServerEngine.GroupManager = groupManager;


                NDiskHandler nDiskHandler = new NDiskHandler(); //网盘处理器 V1.9
                CustomizeHandler handler = new CustomizeHandler();
                ComplexCustomizeHandler complexHandler = new ComplexCustomizeHandler(nDiskHandler, handler);
                LoginForm loginForm = new LoginForm();

                if (loginForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                else
                {
                    administratorID = loginForm.GetAdministratorID();

                }
                if (administratorID == null)
                {
                    return;
                }
                //初始化服务端引擎
                Program.RapidServerEngine.SecurityLogEnabled = false;
              //Program.RapidServerEngine.HeartbeatTimeoutInSecs =8;//8秒

                Program.RapidServerEngine.Initialize(int.Parse(ConfigurationManager.AppSettings["Port"]), complexHandler, new BasicHandler(globalCache));
                //让IGroupManager的GetGroupmates方法返回所有联系人（组友，好友），则就可以关闭好友上显现通知了。          

                Program.RapidServerEngine.FriendsController.FriendNotifyEnabled = true;
                Program.RapidServerEngine.FriendsController.UseFriendNotifyThread = true;//同一个线程通知
                
                Program.RapidServerEngine.GroupController.GroupNotifyEnabled = true;
                Program.RapidServerEngine.GroupController.BroadcastBlobListened = true; //为群聊天记录

                //初始化网盘处理器 V1.9
                NetworkDiskPathManager networkDiskPathManager = new NetworkDiskPathManager() ;
                NetworkDisk networkDisk = new NetworkDisk(networkDiskPathManager, Program.RapidServerEngine.FileController);
                nDiskHandler.Initialize(Program.RapidServerEngine.FileController, networkDisk);

                //设置重登陆模式
                Program.RapidServerEngine.UserManager.RelogonMode = RelogonMode.ReplaceOld; 

                //离线消息控制器 V3.2
                OfflineFileController offlineFileController = new OfflineFileController(Program.RapidServerEngine, globalCache);

                handler.Initialize(globalCache, Program.RapidServerEngine, offlineFileController);
                #endregion            

                #region 初始化OMCS服务器
                OMCS.GlobalUtil.SetAuthorizedUser("FreeUser", "");
                OMCS.GlobalUtil.SetMaxLengthOfUserID(20);
                OMCSConfiguration config = new OMCSConfiguration();

                //用于验证登录用户的帐密
                DefaultUserVerifier userVerifier = new DefaultUserVerifier();
                Program.MultimediaServer = MultimediaServerFactory.CreateMultimediaServer(int.Parse(ConfigurationManager.AppSettings["OmcsPort"]), userVerifier, config,false);                          
                
                #endregion

                #region 发布用于注册的Remoting服务
                RemotingConfiguration.Configure("RemoteReading.Server.exe.config", false);
                RemotingService registerService = new Server.RemotingService(globalCache ,Program.RapidServerEngine);
                RemotingServices.Marshal(registerService, "RemotingService");      
                #endregion       

                #region Thrift RPC服务启动
             //   BusinessImpl businessImpl = new BusinessImpl(globalCache);
                ServerRPCDelegate d= ServersStart;
                d.BeginInvoke(globalCache, null, null);

                #endregion

                //如果不需要默认的UI显示，可以替换下面这句为自己的Form
                MainServerForm mainForm = new MainServerForm(Program.RapidServerEngine, globalCache, administratorID);
                mainForm.Text = ConfigurationManager.AppSettings["SoftwareName"] + " 服务器";
                Application.Run(mainForm);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }       
    }
}
