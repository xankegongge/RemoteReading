using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CCWin;
using CCWin.Win32;
using CCWin.Win32.Const;
using System.Diagnostics;
using System.Configuration;
using ESBasic.Security;


namespace GradeSystem.Server
{
    public partial class AddUserForm : BaseForm
    {
        private int headImageIndex = 0;
        private GlobalCache globalCache;
        private string administratorID;
        public AddUserForm(GlobalCache g, string administratorID)
        {
            this.globalCache = g;
            this.administratorID = administratorID;
            InitializeComponent();
            //int registerPort = int.Parse(ConfigurationManager.AppSettings["RemotingPort"]);
            //this.ggService = (IRemotingService)Activator.GetObject(typeof(IRemotingService), string.Format("tcp://{0}:{1}/RemotingService", ConfigurationManager.AppSettings["ServerIP"], registerPort)); ;              
            Random ran = new Random();
            this.headImageIndex = ran.Next(0,10);
        //    this.pnlImgTx.BackgroundImage = GlobalResourceManager.HeadImages[this.headImageIndex];//根据ID获取头像            
        }

        #region RegisteredUser
        private LoginUser registeredUser = null;
        public LoginUser RegisteredUser
        {
            get
            {
                return this.registeredUser;
            }
        } 
        #endregion       

        private void skinButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;            
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string userID = this.skinTextBox_id.SkinTxt.Text.Trim();
            if (curIndex == -1)
            {
                this.skinComboBox1.Focus();
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                MessageBoxEx.Show("选择一个角色类型！");
                return;
            }
            if (userID.Length == 0)
            {
                this.skinTextBox_id.SkinTxt.Focus();
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                MessageBoxEx.Show("帐号不能为空！");
                return;
            }
            if (this.globalCache.IsUserExist(userID))
            {
                 this.skinTextBox_id.SkinTxt.Focus();
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                MessageBoxEx.Show("用户已存在！");
                return;
            }
            string pwd = this.skinTextBox_pwd.SkinTxt.Text ;
            if (pwd != this.skinTextBox_pwd2.SkinTxt.Text)
            {
                MessageBoxEx.Show("两次输入的密码不一致！");
                this.skinTextBox_pwd.SkinTxt.SelectAll() ;
                this.skinTextBox_pwd.SkinTxt.Focus();
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            if (curIndex == 1 || curIndex == 2)//如果是用户或者专家
            {
                if (selectedHospitalID == -1)
                {
                    MessageBoxEx.Show("医院还没选呢！");
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
                if (etitle == (EProfessionTitle)(-1))
                {
                    MessageBoxEx.Show("职业还没选呢！");
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }
            }
            else
            {
                selectedHospitalID = -1;//客服的话没有医院和职业选择
                etitle = (EProfessionTitle) (- 1);
            }
            string email = this.skinTextBox_email.SkinTxt.Text;
            string useridbyemail = "";
            if (!ValidUtils.IsEmail(email))
            {
                MessageBoxEx.Show("Email格式不正确！");
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            if (this.globalCache.IsExitEmail(email, out useridbyemail))
            {
                MessageBoxEx.Show("Email已存在");
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            string mobilephone = this.skinTextBox_moiblephone.SkinTxt.Text;

            if (!ValidUtils.IsMobile(mobilephone))
            {
                MessageBoxEx.Show("手机格式不正确！");
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }

            if (this.globalCache.IsExitMobilePhone(mobilephone))
            {
                MessageBoxEx.Show("手机号已存在");
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            try
            {
                //  GGUser user=null;

                EUserType usertype = (EUserType)(curIndex+1);
                PersonInfo us = new PersonInfo(this.skinTextBox_nickName.SkinTxt.Text, mobilephone, email);
                LoginUser user = new LoginUser(userID, SecurityHelper.MD5String2(pwd),  this.headImageIndex,
                                     usertype, false, us);
                user.IsActivited = true;//管理员添加都是默认激活的;
                //user.IsChecking = false;
                user.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//设置创建时间;
                //user.CheckType = CheckType.Register;
                //if (usertype != EUserType.Servicer) 
                //{
                //    Hospital hs = listHosp[selectedHospitalID];
                //    user.Hospi = hs;
                //}
                user.activitedByUserID = administratorID;
                this.globalCache.InsertUser(user);

                this.registeredUser = user;                
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ee)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                MessageBoxEx.Show("添加失败！" + ee.Message);
            }

        }
       
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //this.headImageIndex = (++this.headImageIndex) % GlobalResourceManager.HeadImages.Length;
            //this.pnlImgTx.BackgroundImage = GlobalResourceManager.HeadImages[this.headImageIndex];
            //this.selfPhoto = false;
        }

        private bool selfPhoto = false;
        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //PhotoForm form = new PhotoForm();
            //if (form.ShowDialog() == DialogResult.OK)
            //{
            //    this.pnlImgTx.BackgroundImage = form.CurrentImage;
            //    this.selfPhoto = true;
            //}
        }     

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //HeadImageForm form = new HeadImageForm();
            //if (form.ShowDialog() == DialogResult.OK)
            //{
            //    this.pnlImgTx.BackgroundImage = form.CurrentImage;
            //    this.selfPhoto = true;
            //}
        }
       // private  static IList<Hospital> listHosp=new List<Hospital>();

       // private static IDictionary<string, List<string>> hospsbycity = new Dictionary<string, List<string>>();


      //  private static IDictionary<string, List<string>> citiesbyprovince = new Dictionary<string, List<string>>();
       
        //private static void GetProvinceAndCities()
        //{
       
        //    foreach (Hospital hs in listHosp)
        //    {
        //        if(!hospsbycity.ContainsKey(hs.City))
        //        {
                    
        //            List<string> hosps=new List<string>();
        //            foreach(Hospital h in listHosp)
        //            {
        //                if(h.City==hs.City&&!hosps.Contains(h.HospitalName))
        //                {
        //                    hosps.Add(h.HospitalName);
        //                }
        //            }
        //            hospsbycity.Add(hs.City,hosps);
        //        }
        //        if(!citiesbyprovince.ContainsKey(hs.Province))
        //        {
        //            List<string> cities = new List<string>();
        //            foreach (Hospital h in listHosp)
        //            {
        //                if (h.Province == hs.Province && !cities.Contains(h.City))
        //                {
        //                    cities.Add(h.City);
        //                }
        //            }
        //            citiesbyprovince.Add(hs.Province, cities);
        //        }
               
               
        //    }

        //}
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            try
            {
	           //   listHosp = this.globalCache.GetAllHospitals();
                // GetProvinceAndCities();//得到省份以及城市列表
                //List<string> provices = new List<string>();
                //foreach (string key in citiesbyprovince.Keys)
                //{
                //    provices.Add(key);
                //}
                //skinComboBoxProvince.Items.AddRange(provices.ToArray());
                //skinComboBoxProvince.SelectedIndex = 0;
              }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }

        }

        private void skinComboBoxProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            skinComboBoxCity.Items.Clear();
            skinComboBoxCity.Text = String.Empty;
            skinComboBoxHosptial.Text = String.Empty;
            
            //string selectedProvice = ((CCWin.SkinControl.SkinComboBox)sender).SelectedItem.ToString();
            //List<string> selectedcities=citiesbyprovince[selectedProvice];
            //skinComboBoxCity.Items.AddRange(selectedcities.ToArray());
            //skinComboBoxCity.SelectedIndex = 0;
        }
        private string selectedCity="";
        private int selectedHospitalID=-1;
        private void skinComboBoxHosptial_SelectedIndexChanged(object sender, EventArgs e)
        {
             
            string  selectedHosptial = ((CCWin.SkinControl.SkinComboBox)sender).SelectedItem.ToString();
            if(selectedCity=="")
                return ;
            //foreach(Hospital hs in listHosp)
            //{
            //    if (hs.City == selectedCity && hs.HospitalName == selectedHosptial)
            //    {
            //        selectedHospitalID = hs.HospitalID;
            //        return;
            //    }
            //}
          
        }

        private void skinComboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            skinComboBoxHosptial.Items.Clear();
            skinComboBoxHosptial.Text = "";
             selectedCity = ((CCWin.SkinControl.SkinComboBox)sender).SelectedItem.ToString();
            //List<string> selectedhosp = hospsbycity[selectedCity];
           // skinComboBoxHosptial.Items.AddRange(selectedhosp.ToArray());
            skinComboBoxHosptial.SelectedIndex = 0;
        }
        private bool isExpert=false;
        private void skinCheckBoxisExperts_CheckedChanged(object sender, EventArgs e)
        {
            isExpert= ((CCWin.SkinControl.SkinCheckBox)sender).Checked;
        }
        private EProfessionTitle etitle=(EProfessionTitle)(-1);
        private void skinComboBoxTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            etitle = (EProfessionTitle)(((CCWin.SkinControl.SkinComboBox)sender).SelectedIndex);
        }
        private int curIndex = -1;
        private int lastIndex = -1;
        private void skinComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            curIndex=skinComboBox1.SelectedIndex;
            if (lastIndex == skinComboBox1.SelectedIndex)
            {
                return;
            }
           
             if (curIndex == 1|| curIndex == 2)
             {
                 this.panelClientorExpert.Visible = true;
             }
             else
             {
                 this.panelClientorExpert.Visible = false;
             }

             lastIndex = curIndex;
        }                   
    }
}
