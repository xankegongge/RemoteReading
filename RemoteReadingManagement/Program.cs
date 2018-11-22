using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ESPlus.Rapid;
using ESPlus.Application.CustomizeInfo;
using CCWin;
using System.Drawing;
using RemoteReading.Core;
namespace RemoteReadingManagement
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

              
                ESPlus.GlobalUtil.SetMaxLengthOfUserID(20);
                ESPlus.GlobalUtil.SetMaxLengthOfMessage(1024 * 1024 * 10);//10M最大消息
               
                IRapidPassiveEngine passiveEngine = RapidEngineFactory.CreatePassiveEngine();
                
                MainForm mainForm = new MainForm();
                ICustomizeHandler complexHandler = new ComplexCustomizeHandler();//V 2.0
                LoginForm loginForm = new LoginForm(passiveEngine, complexHandler);

                if (loginForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
               
            //    mainForm.Initialize(passiveEngine, loginForm.LoginStatus, loginForm.StateImage);
                Application.Run(mainForm);//启动主界面
                //MedicalReading mr = new MedicalReading();
                //Application.Run(new frmMain());
                //Application.Run(new frmRejectedReason());
            }
            catch (Exception ee)
            {
                MessageBoxEx.Show(ee.Message);
            }
        }
    }
}
