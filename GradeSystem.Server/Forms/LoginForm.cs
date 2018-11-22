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
using System.Diagnostics;
using ESPlus.Rapid;
using ESPlus.Application.Basic;
using ESPlus.Application.CustomizeInfo;
using System.Configuration;
using ESBasic.Security;
using JustLib;
using DataProvider;

namespace GradeSystem.Server
{
    public partial class LoginForm : GradeSystem.Server.BaseForm
    {
       // private IRemotingService remotingService;

        private string pwdMD5;            

        public LoginForm()
        {
           
            InitializeComponent();          
        }

        private string id = null;
        public string GetAdministratorID()
        {
            return id;
        }
        #region buttonLogin_Click        
        private void buttonLogin_Click(object sender, EventArgs e)
        { 
            id = this.textBoxId.SkinTxt.Text;
            string pwd = this.textBoxPwd.SkinTxt.Text ;
            if (id.Length == 0) { return; }

            this.Cursor = Cursors.WaitCursor;
            this.buttonLogin.Text = "正在登陆...";
            this.buttonLogin.Enabled = false;
            try
            {
               
                if (!this.pwdIsMD5)
                { 
                    pwdMD5 = SecurityHelper.MD5String2(pwd);
                }
                SqlServerProvider ssp = new SqlServerProvider();
                if (!ssp.Login(id, pwdMD5))
                {
                    MessageBox.Show("用户或密码错误!");
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

        private void textBoxId_Load(object sender, EventArgs e)
        {

        }
    }
}
