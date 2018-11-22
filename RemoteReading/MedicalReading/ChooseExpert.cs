using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESPlus.Rapid;
using ESPlus.Application.CustomizeInfo;
using ESBasic;
using ESPlus.Rapid;
using ESBasic.ObjectManagement.Forms;
using ESPlus.Application.FileTransfering;
using ESPlus.FileTransceiver;
using ESPlus.Application.Basic;
using ESBasic.Helpers;
using JustLib;
using JustLib.UnitViews;
using RemoteReading.Core;
namespace RemoteReading
{
    public partial class ChooseExpert : BaseForm, IHeadImageGetter
    {
        IRapidPassiveEngine rapidPassiveEngine;
        private GlobalUserCache globalUserCache; //缓存用户资料
        public ChooseExpert()
        {
            InitializeComponent();
        }
        public ChooseExpert(IRapidPassiveEngine rapid,GlobalUserCache g):this()
        {
            this.rapidPassiveEngine = rapid;//获取发送信息机器
            this.globalUserCache = g;
            this.expertListBox.Initialize(this.globalUserCache.CurrentUser, this, new UserInformationForm(new Point(this.Location.X - 284, this.expertListBox.Location.Y)));
            UiSafeInvoker invoker = new UiSafeInvoker(this, true, true, GlobalResourceManager.Logger);
            GlobalResourceManager.SetUiSafeInvoker(invoker);
            this.expertListBox.UserDoubleClicked += new CbGeneric<IUser>(expertListBox_UserDoubleClicked);
        }

        private void expertListBox_UserDoubleClicked(IUser obj)
        {
            
          frmMain frm = new frmMain(this.rapidPassiveEngine,null, this.globalUserCache.CurrentUser, (GGUser)obj, null);

            //frm.Show();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                
                MedicalReading mr = frm.GetSubmitMD();
                if (mr.UserFrom == null)
                {
                    GGUser userfrom = this.globalUserCache.GetUser(mr.UserIDFrom);
                    mr.UserFrom = userfrom;
                }
                if (mr.UserTo == null)
                {
                    GGUser userto = this.globalUserCache.GetUser(mr.UserIDTo);
                    mr.UserTo = userto;
                }
                
                this.globalUserCache.AddMedicalReading(mr);
                //this.globalUserCache.MedicalReadingInfoChanged(mr);
            }
            //this.Close();
        }
        public Image GetHeadImage(IUser user)
        {
            return GlobalResourceManager.GetHeadImage((GGUser)user);
        }
        private void ChooseExpert_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (GGUser expert in this.globalUserCache.GetAllExperts())
                {

                    if (expert != null)
                    {
                        this.expertListBox.AddUser(expert);
                    }
                }
                this.expertListBox.SortAllUser();
                this.expertListBox.ExpandRoot();
            }
            catch (Exception ex)
            {

            }
        }

    }
}
