using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CCWin;
using CCWin.SkinControl;
using CCWin.SkinClass;
using System.Runtime.InteropServices;
using System.Threading;
using RemoteReadingManagement.Properties;
using System.Diagnostics;
using ESPlus.Rapid;
using ESPlus.Application.Basic;
using ESPlus.Application.CustomizeInfo;
using System.Configuration;
using ESBasic.Security;
using JustLib;
using RemoteReading.Core;


namespace RemoteReadingManagement
{
    public partial class LoginForm : BaseForm
    {
       // private IRemotingService remotingService;
        private IRapidPassiveEngine rapidPassiveEngine;
        private ICustomizeHandler customizeHandler;
        private string pwdMD5;            

        public LoginForm(IRapidPassiveEngine engine ,ICustomizeHandler handler)
        {
            this.rapidPassiveEngine = engine;
            this.customizeHandler = handler;

            //int registerPort = int.Parse(ConfigurationManager.AppSettings["RemotingPort"]);
            //this.remotingService = (IRemotingService)Activator.GetObject(typeof(IRemotingService), string.Format("tcp://{0}:{1}/RemotingService", ConfigurationManager.AppSettings["ServerIP"], registerPort)); ;
             
            InitializeComponent();          

            
        //    GlobalResourceManager.SetStatusImage(statusImageDictionary);

           // this.skinLabel_SoftName.Text = GlobalResourceManager.SoftwareName;            

            //this.checkBoxRememberPwd.Checked = SystemSettings.Singleton.RememberPwd;
            //this.checkBoxAutoLogin.Checked = SystemSettings.Singleton.AutoLogin;   
            //this.textBoxId.SkinTxt.Text = SystemSettings.Singleton.LastLoginUserID;
            //if (SystemSettings.Singleton.RememberPwd)
            //{
            //    this.textBoxPwd.SkinTxt.Text = "11111111";
            //    this.pwdMD5 = SystemSettings.Singleton.LastLoginPwdMD5;
            //    this.pwdIsMD5 = true;
            //}
        }



        #region buttonLogin_Click        
        private void buttonLogin_Click(object sender, EventArgs e)
        { 
            string id = this.textBoxId.SkinTxt.Text;
            string pwd = this.textBoxPwd.SkinTxt.Text ;
            if (id.Length == 0) { return; }

            this.Cursor = Cursors.WaitCursor;
            this.buttonLogin.Text = "正在登陆...";
            this.buttonLogin.Enabled = false;
            try
            {
                this.rapidPassiveEngine.SecurityLogEnabled = false;
                
                if (!this.pwdIsMD5)
                { 
                    pwdMD5 = SecurityHelper.MD5String2(pwd);
                }
                LogonResponse response = this.rapidPassiveEngine.Initialize(id, pwdMD5, "127.0.0.1", 4530, this.customizeHandler);
                if (response.LogonResult == LogonResult.Failed)
                {
                    MessageBoxEx.Show(response.FailureCause);
                    this.buttonLogin.Text = "登陆";
                    return;
                }

                //0923
                if (response.LogonResult == LogonResult.VersionMismatched)
                {
                    MessageBoxEx.Show("客户端与服务器的ESFramework版本不一致！");
                    this.buttonLogin.Text = "登陆";
                    return;
                }

                if (response.LogonResult == LogonResult.HadLoggedOn)
                {
                    MessageBoxEx.Show("该帐号已经在其它地方登录！");
                    this.buttonLogin.Text = "登陆";
                    return;
                }

             
            }
            catch (Exception ee)
            {
                //this.toolShow.Show(ee.Message, this.buttonLogin, new Point(this.buttonLogin.Width/2,-this.buttonLogin.Height), 3000);
                //this.buttonLogin.Text = "登陆";
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.buttonLogin.Enabled = true;
                this.buttonLogin.Text = "登陆";
            }
            //同步调用，获取用户信息
            byte[] bUser = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetUserInfo, System.Text.Encoding.UTF8.GetBytes(rapidPassiveEngine.CurrentUserID));
            if (bUser == null)
            {
                return;
            }
            GGUser gg = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<GGUser>(bUser, 0);
            if (gg.UserType != EUserType.Administrator)
            {
                MessageBox.Show("非管理员");
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        } 
        #endregion

      
      

        //点击 软键盘
        private void textBoxPwd_IconClick(object sender, EventArgs e)
        {
            PassKey pass = new PassKey(this.Left + this.textBoxPwd.Left - 25, this.Top + this.textBoxPwd.Bottom, this.textBoxPwd.SkinTxt);
            pass.Show(this);
        }

      
        
        //关闭
        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool pwdIsMD5 = false;
        

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
               // this.Back = GlobalResourceManager.LoginBackImage;
               // this.panelHeadImage.BackgroundImage = GlobalResourceManager.Png64;
            }

            //if (SystemSettings.Singleton.AutoLogin)
            //{
            //    this.buttonLogin.PerformClick();                
            //}
        }

      
        private void textBoxPwd_SkinTxt_KeyUp(object sender, KeyEventArgs e)
        {
            this.pwdIsMD5 = false;
        }

        private void textBoxId_SkinTxt_TextChanged(object sender, EventArgs e)
        {
            this.textBoxPwd.SkinTxt.Clear();
            this.pwdIsMD5 = false;
        }

        private void skinButtom1_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.medicaldl.com/");
        }
    }
}
