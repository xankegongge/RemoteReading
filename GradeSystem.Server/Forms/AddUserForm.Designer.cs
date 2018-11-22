namespace GradeSystem.Server
{
    partial class AddUserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddUserForm));
            this.btnRegister = new CCWin.SkinControl.SkinButton();
            this.skinComboBoxHosptial = new CCWin.SkinControl.SkinComboBox();
            this.skinComboBoxCity = new CCWin.SkinControl.SkinComboBox();
            this.skinComboBoxTitle = new CCWin.SkinControl.SkinComboBox();
            this.skinComboBoxProvince = new CCWin.SkinControl.SkinComboBox();
            this.channelQualityDisplayer1 = new JustLib.Controls.ChannelQualityDisplayer();
            this.skinButton1 = new CCWin.SkinControl.SkinButton();
            this.skinTextBox_email = new CCWin.SkinControl.SkinTextBox();
            this.skinTextBox_moiblephone = new CCWin.SkinControl.SkinTextBox();
            this.skinTextBox_nickName = new CCWin.SkinControl.SkinTextBox();
            this.skinTextBox_pwd2 = new CCWin.SkinControl.SkinTextBox();
            this.skinTextBox_pwd = new CCWin.SkinControl.SkinTextBox();
            this.skinTextBox_id = new CCWin.SkinControl.SkinTextBox();
            this.skinLabel7 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel6 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel9 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel8 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel4 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel5 = new CCWin.SkinControl.SkinLabel();
            this.skinComboBox1 = new CCWin.SkinControl.SkinComboBox();
            this.panelClientorExpert = new System.Windows.Forms.Panel();
            this.skinTextBox_email.SuspendLayout();
            this.skinTextBox_moiblephone.SuspendLayout();
            this.skinTextBox_nickName.SuspendLayout();
            this.skinTextBox_pwd2.SuspendLayout();
            this.skinTextBox_pwd.SuspendLayout();
            this.skinTextBox_id.SuspendLayout();
            this.panelClientorExpert.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRegister
            // 
            this.btnRegister.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRegister.BackColor = System.Drawing.Color.Transparent;
            this.btnRegister.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(159)))), ((int)(((byte)(215)))));
            this.btnRegister.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnRegister.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRegister.DownBack = ((System.Drawing.Image)(resources.GetObject("btnRegister.DownBack")));
            this.btnRegister.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btnRegister.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRegister.Location = new System.Drawing.Point(321, 590);
            this.btnRegister.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegister.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnRegister.MouseBack")));
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnRegister.NormlBack")));
            this.btnRegister.Size = new System.Drawing.Size(92, 30);
            this.btnRegister.TabIndex = 12;
            this.btnRegister.Text = "确定";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // skinComboBoxHosptial
            // 
            this.skinComboBoxHosptial.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.skinComboBoxHosptial.FormattingEnabled = true;
            this.skinComboBoxHosptial.Location = new System.Drawing.Point(72, 36);
            this.skinComboBoxHosptial.Margin = new System.Windows.Forms.Padding(4);
            this.skinComboBoxHosptial.Name = "skinComboBoxHosptial";
            this.skinComboBoxHosptial.Size = new System.Drawing.Size(335, 26);
            this.skinComboBoxHosptial.TabIndex = 9;
            this.skinComboBoxHosptial.WaterText = "";
            this.skinComboBoxHosptial.SelectedIndexChanged += new System.EventHandler(this.skinComboBoxHosptial_SelectedIndexChanged);
            // 
            // skinComboBoxCity
            // 
            this.skinComboBoxCity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.skinComboBoxCity.FormattingEnabled = true;
            this.skinComboBoxCity.Location = new System.Drawing.Point(247, 5);
            this.skinComboBoxCity.Margin = new System.Windows.Forms.Padding(4);
            this.skinComboBoxCity.Name = "skinComboBoxCity";
            this.skinComboBoxCity.Size = new System.Drawing.Size(160, 26);
            this.skinComboBoxCity.TabIndex = 8;
            this.skinComboBoxCity.WaterText = "";
            this.skinComboBoxCity.SelectedIndexChanged += new System.EventHandler(this.skinComboBoxCity_SelectedIndexChanged);
            // 
            // skinComboBoxTitle
            // 
            this.skinComboBoxTitle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.skinComboBoxTitle.FormattingEnabled = true;
            this.skinComboBoxTitle.Items.AddRange(new object[] {
            "医师",
            "主治医师",
            "副主任医师",
            "主任医师"});
            this.skinComboBoxTitle.Location = new System.Drawing.Point(69, 75);
            this.skinComboBoxTitle.Margin = new System.Windows.Forms.Padding(4);
            this.skinComboBoxTitle.Name = "skinComboBoxTitle";
            this.skinComboBoxTitle.Size = new System.Drawing.Size(160, 26);
            this.skinComboBoxTitle.TabIndex = 10;
            this.skinComboBoxTitle.WaterText = "";
            this.skinComboBoxTitle.SelectedIndexChanged += new System.EventHandler(this.skinComboBoxTitle_SelectedIndexChanged);
            // 
            // skinComboBoxProvince
            // 
            this.skinComboBoxProvince.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.skinComboBoxProvince.FormattingEnabled = true;
            this.skinComboBoxProvince.Location = new System.Drawing.Point(72, 5);
            this.skinComboBoxProvince.Margin = new System.Windows.Forms.Padding(4);
            this.skinComboBoxProvince.Name = "skinComboBoxProvince";
            this.skinComboBoxProvince.Size = new System.Drawing.Size(160, 26);
            this.skinComboBoxProvince.TabIndex = 7;
            this.skinComboBoxProvince.WaterText = "";
            this.skinComboBoxProvince.SelectedIndexChanged += new System.EventHandler(this.skinComboBoxProvince_SelectedIndexChanged);
            // 
            // channelQualityDisplayer1
            // 
            this.channelQualityDisplayer1.BackColor = System.Drawing.Color.Transparent;
            this.channelQualityDisplayer1.ColorBadSignal = System.Drawing.Color.Red;
            this.channelQualityDisplayer1.ColorNoSignal = System.Drawing.Color.LightGray;
            this.channelQualityDisplayer1.ColorSignal = System.Drawing.Color.Green;
            this.channelQualityDisplayer1.Location = new System.Drawing.Point(501, 519);
            this.channelQualityDisplayer1.Margin = new System.Windows.Forms.Padding(5);
            this.channelQualityDisplayer1.Name = "channelQualityDisplayer1";
            this.channelQualityDisplayer1.Size = new System.Drawing.Size(59, 19);
            this.channelQualityDisplayer1.TabIndex = 137;
            // 
            // skinButton1
            // 
            this.skinButton1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.skinButton1.BackColor = System.Drawing.Color.Transparent;
            this.skinButton1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(159)))), ((int)(((byte)(215)))));
            this.skinButton1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.skinButton1.DownBack = ((System.Drawing.Image)(resources.GetObject("skinButton1.DownBack")));
            this.skinButton1.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.skinButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinButton1.Location = new System.Drawing.Point(221, 590);
            this.skinButton1.Margin = new System.Windows.Forms.Padding(4);
            this.skinButton1.MouseBack = ((System.Drawing.Image)(resources.GetObject("skinButton1.MouseBack")));
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = ((System.Drawing.Image)(resources.GetObject("skinButton1.NormlBack")));
            this.skinButton1.Size = new System.Drawing.Size(92, 30);
            this.skinButton1.TabIndex = 7;
            this.skinButton1.Text = "取消";
            this.skinButton1.UseVisualStyleBackColor = false;
            this.skinButton1.Click += new System.EventHandler(this.skinButton1_Click);
            // 
            // skinTextBox_email
            // 
            this.skinTextBox_email.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox_email.Icon = null;
            this.skinTextBox_email.IconIsButton = false;
            this.skinTextBox_email.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_email.Location = new System.Drawing.Point(111, 352);
            this.skinTextBox_email.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_email.MinimumSize = new System.Drawing.Size(37, 35);
            this.skinTextBox_email.MouseBack = null;
            this.skinTextBox_email.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_email.Name = "skinTextBox_email";
            this.skinTextBox_email.NormlBack = null;
            this.skinTextBox_email.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.skinTextBox_email.Size = new System.Drawing.Size(252, 35);
            // 
            // skinTextBox_email.BaseText
            // 
            this.skinTextBox_email.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_email.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_email.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_email.SkinTxt.Location = new System.Drawing.Point(7, 6);
            this.skinTextBox_email.SkinTxt.Name = "BaseText";
            this.skinTextBox_email.SkinTxt.Size = new System.Drawing.Size(238, 22);
            this.skinTextBox_email.SkinTxt.TabIndex = 1;
            this.skinTextBox_email.SkinTxt.Text = "gongetowy@q4.com";
            this.skinTextBox_email.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox_email.SkinTxt.WaterText = "";
            this.skinTextBox_email.TabIndex = 6;
            // 
            // skinTextBox_moiblephone
            // 
            this.skinTextBox_moiblephone.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox_moiblephone.Icon = null;
            this.skinTextBox_moiblephone.IconIsButton = false;
            this.skinTextBox_moiblephone.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_moiblephone.Location = new System.Drawing.Point(111, 308);
            this.skinTextBox_moiblephone.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_moiblephone.MinimumSize = new System.Drawing.Size(37, 35);
            this.skinTextBox_moiblephone.MouseBack = null;
            this.skinTextBox_moiblephone.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_moiblephone.Name = "skinTextBox_moiblephone";
            this.skinTextBox_moiblephone.NormlBack = null;
            this.skinTextBox_moiblephone.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.skinTextBox_moiblephone.Size = new System.Drawing.Size(252, 35);
            // 
            // skinTextBox_moiblephone.BaseText
            // 
            this.skinTextBox_moiblephone.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_moiblephone.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_moiblephone.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_moiblephone.SkinTxt.Location = new System.Drawing.Point(7, 6);
            this.skinTextBox_moiblephone.SkinTxt.Name = "BaseText";
            this.skinTextBox_moiblephone.SkinTxt.Size = new System.Drawing.Size(238, 22);
            this.skinTextBox_moiblephone.SkinTxt.TabIndex = 5;
            this.skinTextBox_moiblephone.SkinTxt.Text = "15211235567";
            this.skinTextBox_moiblephone.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox_moiblephone.SkinTxt.WaterText = "";
            this.skinTextBox_moiblephone.TabIndex = 5;
            // 
            // skinTextBox_nickName
            // 
            this.skinTextBox_nickName.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox_nickName.Icon = null;
            this.skinTextBox_nickName.IconIsButton = false;
            this.skinTextBox_nickName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_nickName.Location = new System.Drawing.Point(111, 261);
            this.skinTextBox_nickName.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_nickName.MinimumSize = new System.Drawing.Size(37, 35);
            this.skinTextBox_nickName.MouseBack = null;
            this.skinTextBox_nickName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_nickName.Name = "skinTextBox_nickName";
            this.skinTextBox_nickName.NormlBack = null;
            this.skinTextBox_nickName.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.skinTextBox_nickName.Size = new System.Drawing.Size(252, 35);
            // 
            // skinTextBox_nickName.BaseText
            // 
            this.skinTextBox_nickName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_nickName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_nickName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_nickName.SkinTxt.Location = new System.Drawing.Point(7, 6);
            this.skinTextBox_nickName.SkinTxt.Name = "BaseText";
            this.skinTextBox_nickName.SkinTxt.Size = new System.Drawing.Size(238, 22);
            this.skinTextBox_nickName.SkinTxt.TabIndex = 4;
            this.skinTextBox_nickName.SkinTxt.Text = "张客服";
            this.skinTextBox_nickName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox_nickName.SkinTxt.WaterText = "";
            this.skinTextBox_nickName.TabIndex = 4;
            // 
            // skinTextBox_pwd2
            // 
            this.skinTextBox_pwd2.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox_pwd2.Icon = null;
            this.skinTextBox_pwd2.IconIsButton = false;
            this.skinTextBox_pwd2.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_pwd2.Location = new System.Drawing.Point(111, 159);
            this.skinTextBox_pwd2.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_pwd2.MinimumSize = new System.Drawing.Size(37, 35);
            this.skinTextBox_pwd2.MouseBack = null;
            this.skinTextBox_pwd2.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_pwd2.Name = "skinTextBox_pwd2";
            this.skinTextBox_pwd2.NormlBack = null;
            this.skinTextBox_pwd2.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.skinTextBox_pwd2.Size = new System.Drawing.Size(319, 35);
            // 
            // skinTextBox_pwd2.BaseText
            // 
            this.skinTextBox_pwd2.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_pwd2.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_pwd2.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_pwd2.SkinTxt.Location = new System.Drawing.Point(7, 6);
            this.skinTextBox_pwd2.SkinTxt.Name = "BaseText";
            this.skinTextBox_pwd2.SkinTxt.PasswordChar = '*';
            this.skinTextBox_pwd2.SkinTxt.Size = new System.Drawing.Size(305, 22);
            this.skinTextBox_pwd2.SkinTxt.TabIndex = 2;
            this.skinTextBox_pwd2.SkinTxt.Text = "123";
            this.skinTextBox_pwd2.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox_pwd2.SkinTxt.WaterText = "";
            this.skinTextBox_pwd2.TabIndex = 1;
            // 
            // skinTextBox_pwd
            // 
            this.skinTextBox_pwd.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox_pwd.Icon = null;
            this.skinTextBox_pwd.IconIsButton = false;
            this.skinTextBox_pwd.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_pwd.Location = new System.Drawing.Point(111, 206);
            this.skinTextBox_pwd.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_pwd.MinimumSize = new System.Drawing.Size(37, 35);
            this.skinTextBox_pwd.MouseBack = null;
            this.skinTextBox_pwd.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_pwd.Name = "skinTextBox_pwd";
            this.skinTextBox_pwd.NormlBack = null;
            this.skinTextBox_pwd.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.skinTextBox_pwd.Size = new System.Drawing.Size(319, 35);
            // 
            // skinTextBox_pwd.BaseText
            // 
            this.skinTextBox_pwd.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_pwd.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_pwd.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_pwd.SkinTxt.Location = new System.Drawing.Point(7, 6);
            this.skinTextBox_pwd.SkinTxt.Name = "BaseText";
            this.skinTextBox_pwd.SkinTxt.PasswordChar = '*';
            this.skinTextBox_pwd.SkinTxt.Size = new System.Drawing.Size(305, 22);
            this.skinTextBox_pwd.SkinTxt.TabIndex = 3;
            this.skinTextBox_pwd.SkinTxt.Text = "123";
            this.skinTextBox_pwd.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox_pwd.SkinTxt.WaterText = "";
            this.skinTextBox_pwd.TabIndex = 2;
            // 
            // skinTextBox_id
            // 
            this.skinTextBox_id.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox_id.Icon = null;
            this.skinTextBox_id.IconIsButton = false;
            this.skinTextBox_id.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_id.Location = new System.Drawing.Point(111, 105);
            this.skinTextBox_id.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_id.MinimumSize = new System.Drawing.Size(37, 35);
            this.skinTextBox_id.MouseBack = null;
            this.skinTextBox_id.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_id.Name = "skinTextBox_id";
            this.skinTextBox_id.NormlBack = null;
            this.skinTextBox_id.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.skinTextBox_id.Size = new System.Drawing.Size(252, 35);
            // 
            // skinTextBox_id.BaseText
            // 
            this.skinTextBox_id.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_id.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_id.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_id.SkinTxt.Location = new System.Drawing.Point(7, 6);
            this.skinTextBox_id.SkinTxt.Name = "BaseText";
            this.skinTextBox_id.SkinTxt.Size = new System.Drawing.Size(238, 22);
            this.skinTextBox_id.SkinTxt.TabIndex = 0;
            this.skinTextBox_id.SkinTxt.Text = "411160430";
            this.skinTextBox_id.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox_id.SkinTxt.WaterText = "";
            this.skinTextBox_id.TabIndex = 0;
            // 
            // skinLabel7
            // 
            this.skinLabel7.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel7.AutoSize = true;
            this.skinLabel7.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel7.BorderColor = System.Drawing.Color.White;
            this.skinLabel7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel7.Location = new System.Drawing.Point(7, 81);
            this.skinLabel7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel7.Name = "skinLabel7";
            this.skinLabel7.Size = new System.Drawing.Size(54, 20);
            this.skinLabel7.TabIndex = 0;
            this.skinLabel7.Text = "职称：";
            // 
            // skinLabel6
            // 
            this.skinLabel6.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel6.AutoSize = true;
            this.skinLabel6.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel6.BorderColor = System.Drawing.Color.White;
            this.skinLabel6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel6.Location = new System.Drawing.Point(7, 11);
            this.skinLabel6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel6.Name = "skinLabel6";
            this.skinLabel6.Size = new System.Drawing.Size(54, 20);
            this.skinLabel6.TabIndex = 0;
            this.skinLabel6.Text = "医院：";
            // 
            // skinLabel3
            // 
            this.skinLabel3.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel3.AutoSize = true;
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel3.Location = new System.Drawing.Point(16, 214);
            this.skinLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(84, 20);
            this.skinLabel3.TabIndex = 0;
            this.skinLabel3.Text = "确认密码：";
            // 
            // skinLabel9
            // 
            this.skinLabel9.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel9.AutoSize = true;
            this.skinLabel9.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel9.BorderColor = System.Drawing.Color.White;
            this.skinLabel9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel9.Location = new System.Drawing.Point(48, 365);
            this.skinLabel9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel9.Name = "skinLabel9";
            this.skinLabel9.Size = new System.Drawing.Size(54, 20);
            this.skinLabel9.TabIndex = 0;
            this.skinLabel9.Text = "邮箱：";
            // 
            // skinLabel8
            // 
            this.skinLabel8.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel8.AutoSize = true;
            this.skinLabel8.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel8.BorderColor = System.Drawing.Color.White;
            this.skinLabel8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel8.Location = new System.Drawing.Point(48, 316);
            this.skinLabel8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel8.Name = "skinLabel8";
            this.skinLabel8.Size = new System.Drawing.Size(69, 20);
            this.skinLabel8.TabIndex = 0;
            this.skinLabel8.Text = "手机号：";
            // 
            // skinLabel4
            // 
            this.skinLabel4.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel4.AutoSize = true;
            this.skinLabel4.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel4.BorderColor = System.Drawing.Color.White;
            this.skinLabel4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel4.Location = new System.Drawing.Point(48, 166);
            this.skinLabel4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel4.Name = "skinLabel4";
            this.skinLabel4.Size = new System.Drawing.Size(54, 20);
            this.skinLabel4.TabIndex = 0;
            this.skinLabel4.Text = "密码：";
            // 
            // skinLabel2
            // 
            this.skinLabel2.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel2.AutoSize = true;
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(48, 264);
            this.skinLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(54, 20);
            this.skinLabel2.TabIndex = 0;
            this.skinLabel2.Text = "姓名：";
            // 
            // skinLabel1
            // 
            this.skinLabel1.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(21, 111);
            this.skinLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(54, 20);
            this.skinLabel1.TabIndex = 0;
            this.skinLabel1.Text = "帐号：";
            // 
            // skinLabel5
            // 
            this.skinLabel5.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel5.AutoSize = true;
            this.skinLabel5.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel5.BorderColor = System.Drawing.Color.White;
            this.skinLabel5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel5.Location = new System.Drawing.Point(21, 66);
            this.skinLabel5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel5.Name = "skinLabel5";
            this.skinLabel5.Size = new System.Drawing.Size(84, 20);
            this.skinLabel5.TabIndex = 0;
            this.skinLabel5.Text = "角色类型：";
            // 
            // skinComboBox1
            // 
            this.skinComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.skinComboBox1.FormattingEnabled = true;
            this.skinComboBox1.Items.AddRange(new object[] {
            "客服",
            "用户",
            "专家"});
            this.skinComboBox1.Location = new System.Drawing.Point(120, 66);
            this.skinComboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.skinComboBox1.Name = "skinComboBox1";
            this.skinComboBox1.Size = new System.Drawing.Size(160, 26);
            this.skinComboBox1.TabIndex = 138;
            this.skinComboBox1.WaterText = "";
            this.skinComboBox1.SelectedIndexChanged += new System.EventHandler(this.skinComboBox1_SelectedIndexChanged);
            // 
            // panelClientorExpert
            // 
            this.panelClientorExpert.BackColor = System.Drawing.Color.Transparent;
            this.panelClientorExpert.Controls.Add(this.skinComboBoxTitle);
            this.panelClientorExpert.Controls.Add(this.skinLabel6);
            this.panelClientorExpert.Controls.Add(this.skinComboBoxHosptial);
            this.panelClientorExpert.Controls.Add(this.skinLabel7);
            this.panelClientorExpert.Controls.Add(this.skinComboBoxCity);
            this.panelClientorExpert.Controls.Add(this.skinComboBoxProvince);
            this.panelClientorExpert.Location = new System.Drawing.Point(52, 406);
            this.panelClientorExpert.Margin = new System.Windows.Forms.Padding(4);
            this.panelClientorExpert.Name = "panelClientorExpert";
            this.panelClientorExpert.Size = new System.Drawing.Size(425, 146);
            this.panelClientorExpert.TabIndex = 139;
            this.panelClientorExpert.Visible = false;
            // 
            // AddUserForm
            // 
            this.AcceptButton = this.btnRegister;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Back = ((System.Drawing.Image)(resources.GetObject("$this.Back")));
            this.BorderPalace = ((System.Drawing.Image)(resources.GetObject("$this.BorderPalace")));
            this.ClientSize = new System.Drawing.Size(581, 631);
            this.Controls.Add(this.panelClientorExpert);
            this.Controls.Add(this.skinComboBox1);
            this.Controls.Add(this.channelQualityDisplayer1);
            this.Controls.Add(this.skinButton1);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.skinTextBox_email);
            this.Controls.Add(this.skinTextBox_moiblephone);
            this.Controls.Add(this.skinTextBox_nickName);
            this.Controls.Add(this.skinTextBox_pwd2);
            this.Controls.Add(this.skinTextBox_pwd);
            this.Controls.Add(this.skinTextBox_id);
            this.Controls.Add(this.skinLabel3);
            this.Controls.Add(this.skinLabel9);
            this.Controls.Add(this.skinLabel8);
            this.Controls.Add(this.skinLabel4);
            this.Controls.Add(this.skinLabel2);
            this.Controls.Add(this.skinLabel5);
            this.Controls.Add(this.skinLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AddUserForm";
            this.Text = "添加用户";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.skinTextBox_email.ResumeLayout(false);
            this.skinTextBox_email.PerformLayout();
            this.skinTextBox_moiblephone.ResumeLayout(false);
            this.skinTextBox_moiblephone.PerformLayout();
            this.skinTextBox_nickName.ResumeLayout(false);
            this.skinTextBox_nickName.PerformLayout();
            this.skinTextBox_pwd2.ResumeLayout(false);
            this.skinTextBox_pwd2.PerformLayout();
            this.skinTextBox_pwd.ResumeLayout(false);
            this.skinTextBox_pwd.PerformLayout();
            this.skinTextBox_id.ResumeLayout(false);
            this.skinTextBox_id.PerformLayout();
            this.panelClientorExpert.ResumeLayout(false);
            this.panelClientorExpert.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinLabel skinLabel4;
        private CCWin.SkinControl.SkinTextBox skinTextBox_id;
        private CCWin.SkinControl.SkinTextBox skinTextBox_pwd;
        private CCWin.SkinControl.SkinTextBox skinTextBox_nickName;
        private CCWin.SkinControl.SkinButton btnRegister;
        private CCWin.SkinControl.SkinButton skinButton1;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private CCWin.SkinControl.SkinTextBox skinTextBox_pwd2;
        private CCWin.SkinControl.SkinLabel skinLabel6;
        private CCWin.SkinControl.SkinLabel skinLabel7;
        private JustLib.Controls.ChannelQualityDisplayer channelQualityDisplayer1;
        private CCWin.SkinControl.SkinComboBox skinComboBoxProvince;
        private CCWin.SkinControl.SkinTextBox skinTextBox_moiblephone;
        private CCWin.SkinControl.SkinComboBox skinComboBoxCity;
        private CCWin.SkinControl.SkinComboBox skinComboBoxHosptial;
        private CCWin.SkinControl.SkinLabel skinLabel8;
        private CCWin.SkinControl.SkinLabel skinLabel9;
        private CCWin.SkinControl.SkinTextBox skinTextBox_email;
        private CCWin.SkinControl.SkinComboBox skinComboBoxTitle;
        private CCWin.SkinControl.SkinLabel skinLabel5;
        private CCWin.SkinControl.SkinComboBox skinComboBox1;
        private System.Windows.Forms.Panel panelClientorExpert;
    }
}