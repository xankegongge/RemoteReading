using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CCWin;
using CCWin.SkinControl;
using JustLib;
using RemoteReading.Core;

namespace RemoteReading
{
    public partial class UserInformationForm : BaseForm ,IUserInformationForm
    {
        private Point pt;
        public UserInformationForm(Point pt)
        {
            this.Location = pt;
            InitializeComponent();            
        }

        public void SetUser(IUser user)
        {
            this.lblQm.Text = user.Signature;
            this.skinLabelName.Text = "账号";
            this.skinLabelHosptial.Text = "昵称";
            this.skinLabel_tbid.Text = user.ID;
            this.skinLabel_tbhospital.Text = user.Name;           
            this.pnlImgTx.BackgroundImage = GlobalResourceManager.GetHeadImageOnline((GGUser)user);
        }

        public void SetMD(MedicalReading mr)
        {
            this.lblQm.Text = "有几张"+mr.MedicalPictureCount;
            this.skinLabelName.Text = "姓名";
            this.skinLabelHosptial.Text = "医院";
            this.skinLabel_tbid.Text = mr.UserTo.PersonName;
            this.skinLabel_tbhospital.Text = mr.UserTo.HospitalName;
            this.pnlImgTx.BackgroundImage = GlobalResourceManager.GetHeadImageOnline((GGUser)mr.UserTo);
        }
        private void UserInformationForm_Load(object sender, EventArgs e)
        {
            this.Location = this.pt;
        }         

        //窗体重绘时
        private void FrmUserInformation_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush sb = new SolidBrush(Color.FromArgb(100, 255, 255, 255));
            g.FillRectangle(sb, new Rectangle(new Point(1, Height - 103), new Size(Width - 2, 80)));
        }

        //计时器
        private bool flag = false;
        private void timShow_Tick(object sender, EventArgs e)
        {
            //鼠标不在窗体内时
            if (!this.Bounds.Contains(Cursor.Position) && flag)
            {
                this.Hide();
                flag = false;
            }
            else if (this.Bounds.Contains(Cursor.Position))
            {
                flag = true;
            }
        }

       
    }
}
