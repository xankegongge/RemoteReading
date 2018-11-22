using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPSocketCS;
using System.Configuration;
namespace GradeSystem.Server
{
    static class Program
    {
        private static HPServerEngine RapidServerEngine = HPServerEngine.GetInstance();
       
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //ESPlus.GlobalUtil.SetAuthorizedUser("FreeUser", "");
            //ESPlus.GlobalUtil.SetMaxLengthOfUserID(20);
            //ESPlus.GlobalUtil.SetMaxLengthOfMessage(1024 * 1024 * 10);
          
           RealDB persister = new RealDB( ConfigurationManager.AppSettings["DBName"] ,ConfigurationManager.AppSettings["DBIP"], ConfigurationManager.AppSettings["SaPwd"]);
           GlobalCache globalCache = new GlobalCache(persister);

           CustomizeHandler handler = new CustomizeHandler();
           
           //RapidServerEngine.setCore(server);
           Program.RapidServerEngine.Initialize(ushort.Parse(ConfigurationManager.AppSettings["Port"]), handler, new BasicHandler(globalCache));
            //设置重登陆模式
           Program.RapidServerEngine.UserManager.RelogonMode = RelogonMode.ReplaceOld;
           string administratorID;
           handler.Initialize(globalCache, Program.RapidServerEngine);
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
            //如果不需要默认的UI显示，可以替换下面这句为自己的Form
            MainServerForm mainForm = new MainServerForm(Program.RapidServerEngine, globalCache, administratorID);
            mainForm.Text = ConfigurationManager.AppSettings["SoftwareName"] + " 服务器";
            Application.Run(mainForm);
            //Application.Run(new Form1());
        }
    }
}
