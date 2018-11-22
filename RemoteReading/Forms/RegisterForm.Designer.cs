namespace RemoteReading
{
    partial class RegisterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            this.btnRegister = new CCWin.SkinControl.SkinButton();
            this.skinComboBoxHosptial = new CCWin.SkinControl.SkinComboBox();
            this.skinComboBoxCity = new CCWin.SkinControl.SkinComboBox();
            this.skinComboBoxTitle = new CCWin.SkinControl.SkinComboBox();
            this.skinComboBoxProvince = new CCWin.SkinControl.SkinComboBox();
            this.channelQualityDisplayer1 = new JustLib.Controls.ChannelQualityDisplayer();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.pnlTx = new CCWin.SkinControl.SkinPanel();
            this.pnlImgTx = new CCWin.SkinControl.SkinPanel();
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
            this.pnlTx.SuspendLayout();
            this.skinTextBox_email.SuspendLayout();
            this.skinTextBox_moiblephone.SuspendLayout();
            this.skinTextBox_nickName.SuspendLayout();
            this.skinTextBox_pwd2.SuspendLayout();
            this.skinTextBox_pwd.SuspendLayout();
            this.skinTextBox_id.SuspendLayout();
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
            this.btnRegister.Location = new System.Drawing.Point(241, 472);
            this.btnRegister.MouseBack = ((System.Drawing.Image)(resources.GetObject("btnRegister.MouseBack")));
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.NormlBack = ((System.Drawing.Image)(resources.GetObject("btnRegister.NormlBack")));
            this.btnRegister.Size = new System.Drawing.Size(69, 24);
            this.btnRegister.TabIndex = 12;
            this.btnRegister.Text = "确定";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // skinComboBoxHosptial
            // 
            this.skinComboBoxHosptial.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.skinComboBoxHosptial.FormattingEnabled = true;
            this.skinComboBoxHosptial.Location = new System.Drawing.Point(85, 345);
            this.skinComboBoxHosptial.Name = "skinComboBoxHosptial";
            this.skinComboBoxHosptial.Size = new System.Drawing.Size(252, 22);
            this.skinComboBoxHosptial.TabIndex = 9;
            this.skinComboBoxHosptial.WaterText = "";
            this.skinComboBoxHosptial.SelectedIndexChanged += new System.EventHandler(this.skinComboBoxHosptial_SelectedIndexChanged);
            // 
            // skinComboBoxCity
            // 
            this.skinComboBoxCity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.skinComboBoxCity.FormattingEnabled = true;
            this.skinComboBoxCity.Location = new System.Drawing.Point(216, 320);
            this.skinComboBoxCity.Name = "skinComboBoxCity";
            this.skinComboBoxCity.Size = new System.Drawing.Size(121, 22);
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
            this.skinComboBoxTitle.Location = new System.Drawing.Point(83, 376);
            this.skinComboBoxTitle.Name = "skinComboBoxTitle";
            this.skinComboBoxTitle.Size = new System.Drawing.Size(121, 22);
            this.skinComboBoxTitle.TabIndex = 10;
            this.skinComboBoxTitle.WaterText = "";
            this.skinComboBoxTitle.SelectedIndexChanged += new System.EventHandler(this.skinComboBoxTitle_SelectedIndexChanged);
            // 
            // skinComboBoxProvince
            // 
            this.skinComboBoxProvince.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.skinComboBoxProvince.FormattingEnabled = true;
            this.skinComboBoxProvince.Location = new System.Drawing.Point(85, 320);
            this.skinComboBoxProvince.Name = "skinComboBoxProvince";
            this.skinComboBoxProvince.Size = new System.Drawing.Size(121, 22);
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
            this.channelQualityDisplayer1.Location = new System.Drawing.Point(376, 415);
            this.channelQualityDisplayer1.Name = "channelQualityDisplayer1";
            this.channelQualityDisplayer1.Size = new System.Drawing.Size(44, 15);
            this.channelQualityDisplayer1.TabIndex = 137;
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel3.Location = new System.Drawing.Point(275, 101);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(56, 17);
            this.linkLabel3.TabIndex = 136;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "上传头像";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel1.Location = new System.Drawing.Point(275, 76);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(56, 17);
            this.linkLabel1.TabIndex = 135;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "自拍头像";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked_1);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel2.Location = new System.Drawing.Point(275, 51);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(56, 17);
            this.linkLabel2.TabIndex = 135;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "更换头像";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // pnlTx
            // 
            this.pnlTx.BackColor = System.Drawing.Color.Transparent;
            this.pnlTx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlTx.Controls.Add(this.pnlImgTx);
            this.pnlTx.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.pnlTx.DownBack = ((System.Drawing.Image)(resources.GetObject("pnlTx.DownBack")));
            this.pnlTx.Location = new System.Drawing.Point(332, 51);
            this.pnlTx.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTx.MouseBack = ((System.Drawing.Image)(resources.GetObject("pnlTx.MouseBack")));
            this.pnlTx.Name = "pnlTx";
            this.pnlTx.NormlBack = ((System.Drawing.Image)(resources.GetObject("pnlTx.NormlBack")));
            this.pnlTx.Palace = true;
            this.pnlTx.Size = new System.Drawing.Size(104, 104);
            this.pnlTx.TabIndex = 134;
            // 
            // pnlImgTx
            // 
            this.pnlImgTx.BackColor = System.Drawing.Color.Transparent;
            this.pnlImgTx.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlImgTx.BackgroundImage")));
            this.pnlImgTx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlImgTx.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.pnlImgTx.DownBack = null;
            this.pnlImgTx.Location = new System.Drawing.Point(2, 2);
            this.pnlImgTx.Margin = new System.Windows.Forms.Padding(0);
            this.pnlImgTx.MouseBack = null;
            this.pnlImgTx.Name = "pnlImgTx";
            this.pnlImgTx.NormlBack = null;
            this.pnlImgTx.Radius = 4;
            this.pnlImgTx.Size = new System.Drawing.Size(100, 100);
            this.pnlImgTx.TabIndex = 6;
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
            this.skinButton1.Location = new System.Drawing.Point(166, 472);
            this.skinButton1.MouseBack = ((System.Drawing.Image)(resources.GetObject("skinButton1.MouseBack")));
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = ((System.Drawing.Image)(resources.GetObject("skinButton1.NormlBack")));
            this.skinButton1.Size = new System.Drawing.Size(69, 24);
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
            this.skinTextBox_email.Location = new System.Drawing.Point(83, 282);
            this.skinTextBox_email.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_email.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox_email.MouseBack = null;
            this.skinTextBox_email.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_email.Name = "skinTextBox_email";
            this.skinTextBox_email.NormlBack = null;
            this.skinTextBox_email.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox_email.Size = new System.Drawing.Size(189, 28);
            // 
            // skinTextBox_email.BaseText
            // 
            this.skinTextBox_email.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_email.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_email.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_email.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox_email.SkinTxt.Name = "BaseText";
            this.skinTextBox_email.SkinTxt.Size = new System.Drawing.Size(179, 18);
            this.skinTextBox_email.SkinTxt.TabIndex = 1;
            this.skinTextBox_email.SkinTxt.Text = "15920873564@qq.com";
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
            this.skinTextBox_moiblephone.Location = new System.Drawing.Point(83, 246);
            this.skinTextBox_moiblephone.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_moiblephone.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox_moiblephone.MouseBack = null;
            this.skinTextBox_moiblephone.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_moiblephone.Name = "skinTextBox_moiblephone";
            this.skinTextBox_moiblephone.NormlBack = null;
            this.skinTextBox_moiblephone.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox_moiblephone.Size = new System.Drawing.Size(189, 28);
            // 
            // skinTextBox_moiblephone.BaseText
            // 
            this.skinTextBox_moiblephone.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_moiblephone.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_moiblephone.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_moiblephone.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox_moiblephone.SkinTxt.Name = "BaseText";
            this.skinTextBox_moiblephone.SkinTxt.Size = new System.Drawing.Size(179, 18);
            this.skinTextBox_moiblephone.SkinTxt.TabIndex = 5;
            this.skinTextBox_moiblephone.SkinTxt.Text = "15217171111";
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
            this.skinTextBox_nickName.Location = new System.Drawing.Point(83, 209);
            this.skinTextBox_nickName.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_nickName.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox_nickName.MouseBack = null;
            this.skinTextBox_nickName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_nickName.Name = "skinTextBox_nickName";
            this.skinTextBox_nickName.NormlBack = null;
            this.skinTextBox_nickName.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox_nickName.Size = new System.Drawing.Size(189, 28);
            // 
            // skinTextBox_nickName.BaseText
            // 
            this.skinTextBox_nickName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_nickName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_nickName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_nickName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox_nickName.SkinTxt.Name = "BaseText";
            this.skinTextBox_nickName.SkinTxt.Size = new System.Drawing.Size(179, 18);
            this.skinTextBox_nickName.SkinTxt.TabIndex = 4;
            this.skinTextBox_nickName.SkinTxt.Text = "张弓";
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
            this.skinTextBox_pwd2.Location = new System.Drawing.Point(83, 127);
            this.skinTextBox_pwd2.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_pwd2.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox_pwd2.MouseBack = null;
            this.skinTextBox_pwd2.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_pwd2.Name = "skinTextBox_pwd2";
            this.skinTextBox_pwd2.NormlBack = null;
            this.skinTextBox_pwd2.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox_pwd2.Size = new System.Drawing.Size(239, 28);
            // 
            // skinTextBox_pwd2.BaseText
            // 
            this.skinTextBox_pwd2.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_pwd2.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_pwd2.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_pwd2.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox_pwd2.SkinTxt.Name = "BaseText";
            this.skinTextBox_pwd2.SkinTxt.PasswordChar = '*';
            this.skinTextBox_pwd2.SkinTxt.Size = new System.Drawing.Size(229, 18);
            this.skinTextBox_pwd2.SkinTxt.TabIndex = 2;
            this.skinTextBox_pwd2.SkinTxt.Text = "1";
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
            this.skinTextBox_pwd.Location = new System.Drawing.Point(83, 165);
            this.skinTextBox_pwd.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_pwd.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox_pwd.MouseBack = null;
            this.skinTextBox_pwd.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_pwd.Name = "skinTextBox_pwd";
            this.skinTextBox_pwd.NormlBack = null;
            this.skinTextBox_pwd.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox_pwd.Size = new System.Drawing.Size(239, 28);
            // 
            // skinTextBox_pwd.BaseText
            // 
            this.skinTextBox_pwd.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_pwd.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_pwd.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_pwd.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox_pwd.SkinTxt.Name = "BaseText";
            this.skinTextBox_pwd.SkinTxt.PasswordChar = '*';
            this.skinTextBox_pwd.SkinTxt.Size = new System.Drawing.Size(229, 18);
            this.skinTextBox_pwd.SkinTxt.TabIndex = 3;
            this.skinTextBox_pwd.SkinTxt.Text = "1";
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
            this.skinTextBox_id.Location = new System.Drawing.Point(83, 84);
            this.skinTextBox_id.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox_id.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox_id.MouseBack = null;
            this.skinTextBox_id.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox_id.Name = "skinTextBox_id";
            this.skinTextBox_id.NormlBack = null;
            this.skinTextBox_id.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox_id.Size = new System.Drawing.Size(189, 28);
            // 
            // skinTextBox_id.BaseText
            // 
            this.skinTextBox_id.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox_id.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox_id.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox_id.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox_id.SkinTxt.Name = "BaseText";
            this.skinTextBox_id.SkinTxt.Size = new System.Drawing.Size(179, 18);
            this.skinTextBox_id.SkinTxt.TabIndex = 0;
            this.skinTextBox_id.SkinTxt.Text = "1111";
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
            this.skinLabel7.Location = new System.Drawing.Point(36, 381);
            this.skinLabel7.Name = "skinLabel7";
            this.skinLabel7.Size = new System.Drawing.Size(44, 17);
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
            this.skinLabel6.Location = new System.Drawing.Point(36, 325);
            this.skinLabel6.Name = "skinLabel6";
            this.skinLabel6.Size = new System.Drawing.Size(44, 17);
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
            this.skinLabel3.Location = new System.Drawing.Point(12, 171);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(68, 17);
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
            this.skinLabel9.Location = new System.Drawing.Point(36, 292);
            this.skinLabel9.Name = "skinLabel9";
            this.skinLabel9.Size = new System.Drawing.Size(44, 17);
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
            this.skinLabel8.Location = new System.Drawing.Point(36, 253);
            this.skinLabel8.Name = "skinLabel8";
            this.skinLabel8.Size = new System.Drawing.Size(56, 17);
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
            this.skinLabel4.Location = new System.Drawing.Point(36, 133);
            this.skinLabel4.Name = "skinLabel4";
            this.skinLabel4.Size = new System.Drawing.Size(44, 17);
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
            this.skinLabel2.Location = new System.Drawing.Point(36, 211);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(44, 17);
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
            this.skinLabel1.Location = new System.Drawing.Point(16, 89);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(68, 17);
            this.skinLabel1.TabIndex = 0;
            this.skinLabel1.Text = "用户帐号：";
            // 
            // RegisterForm
            // 
            this.AcceptButton = this.btnRegister;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Back = ((System.Drawing.Image)(resources.GetObject("$this.Back")));
            this.BorderPalace = ((System.Drawing.Image)(resources.GetObject("$this.BorderPalace")));
            this.ClientSize = new System.Drawing.Size(436, 505);
            this.CloseDownBack = global::RemoteReading.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::RemoteReading.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::RemoteReading.Properties.Resources.btn_close_disable;
            this.Controls.Add(this.skinComboBoxHosptial);
            this.Controls.Add(this.skinComboBoxCity);
            this.Controls.Add(this.skinComboBoxTitle);
            this.Controls.Add(this.skinComboBoxProvince);
            this.Controls.Add(this.channelQualityDisplayer1);
            this.Controls.Add(this.linkLabel3);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.pnlTx);
            this.Controls.Add(this.skinButton1);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.skinTextBox_email);
            this.Controls.Add(this.skinTextBox_moiblephone);
            this.Controls.Add(this.skinTextBox_nickName);
            this.Controls.Add(this.skinTextBox_pwd2);
            this.Controls.Add(this.skinTextBox_pwd);
            this.Controls.Add(this.skinTextBox_id);
            this.Controls.Add(this.skinLabel7);
            this.Controls.Add(this.skinLabel6);
            this.Controls.Add(this.skinLabel3);
            this.Controls.Add(this.skinLabel9);
            this.Controls.Add(this.skinLabel8);
            this.Controls.Add(this.skinLabel4);
            this.Controls.Add(this.skinLabel2);
            this.Controls.Add(this.skinLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaxDownBack = global::RemoteReading.Properties.Resources.btn_max_down;
            this.MaxMouseBack = global::RemoteReading.Properties.Resources.btn_max_highlight;
            this.MaxNormlBack = global::RemoteReading.Properties.Resources.btn_max_normal;
            this.MiniDownBack = global::RemoteReading.Properties.Resources.btn_mini_down;
            this.MiniMouseBack = global::RemoteReading.Properties.Resources.btn_mini_highlight;
            this.MiniNormlBack = global::RemoteReading.Properties.Resources.btn_mini_normal;
            this.Name = "RegisterForm";
            this.RestoreDownBack = global::RemoteReading.Properties.Resources.btn_restore_down;
            this.RestoreMouseBack = global::RemoteReading.Properties.Resources.btn_restore_highlight;
            this.RestoreNormlBack = global::RemoteReading.Properties.Resources.btn_restore_normal;
            this.Text = "远程阅片系统注册";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.Click += new System.EventHandler(this.RegisterForm_Click);
            this.pnlTx.ResumeLayout(false);
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
        private CCWin.SkinControl.SkinPanel pnlTx;
        private CCWin.SkinControl.SkinPanel pnlImgTx;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private CCWin.SkinControl.SkinTextBox skinTextBox_pwd2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel3;
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
    }
}