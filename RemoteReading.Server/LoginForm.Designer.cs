namespace RemoteReading.Server
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.buttonLogin = new CCWin.SkinControl.SkinButton();
            this.textBoxId = new CCWin.SkinControl.SkinTextBox();
            this.skinLabel_SoftName = new CCWin.SkinControl.SkinLabel();
            this.btnRegister = new CCWin.SkinControl.SkinButton();
            this.textBoxPwd = new CCWin.SkinControl.SkinTextBox();
            this.textBoxId.SuspendLayout();
            this.textBoxPwd.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonLogin.BackRectangle = new System.Drawing.Rectangle(50, 23, 50, 23);
            this.buttonLogin.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(118)))), ((int)(((byte)(156)))));
            this.buttonLogin.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.buttonLogin.Create = true;
            this.buttonLogin.DownBack = null;
            this.buttonLogin.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.buttonLogin.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonLogin.ForeColor = System.Drawing.Color.White;
            this.buttonLogin.Location = new System.Drawing.Point(124, 274);
            this.buttonLogin.Margin = new System.Windows.Forms.Padding(0);
            this.buttonLogin.MouseBack = null;
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.NormlBack = null;
            this.buttonLogin.Palace = true;
            this.buttonLogin.Size = new System.Drawing.Size(247, 61);
            this.buttonLogin.TabIndex = 5;
            this.buttonLogin.Text = "登        录";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // textBoxId
            // 
            this.textBoxId.BackColor = System.Drawing.Color.Transparent;
            this.textBoxId.Icon = null;
            this.textBoxId.IconIsButton = false;
            this.textBoxId.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.textBoxId.Location = new System.Drawing.Point(99, 131);
            this.textBoxId.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxId.MinimumSize = new System.Drawing.Size(37, 35);
            this.textBoxId.MouseBack = null;
            this.textBoxId.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.NormlBack = null;
            this.textBoxId.Padding = new System.Windows.Forms.Padding(7, 6, 37, 6);
            this.textBoxId.Size = new System.Drawing.Size(333, 35);
            // 
            // textBoxId.BaseText
            // 
            this.textBoxId.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxId.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxId.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.textBoxId.SkinTxt.Location = new System.Drawing.Point(7, 6);
            this.textBoxId.SkinTxt.Name = "BaseText";
            this.textBoxId.SkinTxt.Size = new System.Drawing.Size(289, 22);
            this.textBoxId.SkinTxt.TabIndex = 0;
            this.textBoxId.SkinTxt.Text = "411160430";
            this.textBoxId.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.textBoxId.SkinTxt.WaterText = "管理员帐号";
            this.textBoxId.SkinTxt.TextChanged += new System.EventHandler(this.textBoxId_SkinTxt_TextChanged);
            this.textBoxId.TabIndex = 35;
            this.textBoxId.Load += new System.EventHandler(this.textBoxId_Load);
            // 
            // skinLabel_SoftName
            // 
            this.skinLabel_SoftName.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel_SoftName.AutoSize = true;
            this.skinLabel_SoftName.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_SoftName.BorderColor = System.Drawing.Color.White;
            this.skinLabel_SoftName.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel_SoftName.ForeColor = System.Drawing.Color.Black;
            this.skinLabel_SoftName.Location = new System.Drawing.Point(69, 20);
            this.skinLabel_SoftName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skinLabel_SoftName.Name = "skinLabel_SoftName";
            this.skinLabel_SoftName.Size = new System.Drawing.Size(342, 31);
            this.skinLabel_SoftName.TabIndex = 18;
            this.skinLabel_SoftName.Text = "RemoteReading后台管理系统";
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.Transparent;
            this.btnRegister.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(118)))), ((int)(((byte)(156)))));
            this.btnRegister.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnRegister.Create = true;
            this.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegister.DownBack = null;
            this.btnRegister.DrawType = CCWin.SkinControl.DrawStyle.Img;
            this.btnRegister.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRegister.Location = new System.Drawing.Point(415, 260);
            this.btnRegister.Margin = new System.Windows.Forms.Padding(0);
            this.btnRegister.MouseBack = null;
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.NormlBack = null;
            this.btnRegister.Size = new System.Drawing.Size(68, 20);
            this.btnRegister.TabIndex = 8;
            this.btnRegister.UseVisualStyleBackColor = false;
            // 
            // textBoxPwd
            // 
            this.textBoxPwd.BackColor = System.Drawing.Color.Transparent;
            this.textBoxPwd.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.textBoxPwd.Icon = ((System.Drawing.Image)(resources.GetObject("textBoxPwd.Icon")));
            this.textBoxPwd.IconIsButton = true;
            this.textBoxPwd.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.textBoxPwd.Location = new System.Drawing.Point(99, 191);
            this.textBoxPwd.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxPwd.MinimumSize = new System.Drawing.Size(0, 35);
            this.textBoxPwd.MouseBack = null;
            this.textBoxPwd.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.textBoxPwd.Name = "textBoxPwd";
            this.textBoxPwd.NormlBack = null;
            this.textBoxPwd.Padding = new System.Windows.Forms.Padding(7, 6, 80, 6);
            this.textBoxPwd.Size = new System.Drawing.Size(333, 35);
            // 
            // textBoxPwd.BaseText
            // 
            this.textBoxPwd.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPwd.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPwd.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.textBoxPwd.SkinTxt.Location = new System.Drawing.Point(7, 6);
            this.textBoxPwd.SkinTxt.Name = "BaseText";
            this.textBoxPwd.SkinTxt.PasswordChar = '●';
            this.textBoxPwd.SkinTxt.Size = new System.Drawing.Size(246, 22);
            this.textBoxPwd.SkinTxt.TabIndex = 0;
            this.textBoxPwd.SkinTxt.Text = "1";
            this.textBoxPwd.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.textBoxPwd.SkinTxt.WaterText = "密码";
            this.textBoxPwd.SkinTxt.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxPwd_SkinTxt_KeyUp);
            this.textBoxPwd.TabIndex = 36;
            this.textBoxPwd.IconClick += new System.EventHandler(this.textBoxPwd_IconClick);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.buttonLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackToColor = false;
            this.ClientSize = new System.Drawing.Size(505, 365);
            this.Controls.Add(this.textBoxId);
            this.Controls.Add(this.skinLabel_SoftName);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.textBoxPwd);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LoginForm";
            this.ShowBorder = false;
            this.ShowDrawIcon = false;
            this.UseCustomBackImage = true;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.textBoxId.ResumeLayout(false);
            this.textBoxId.PerformLayout();
            this.textBoxPwd.ResumeLayout(false);
            this.textBoxPwd.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinButton buttonLogin;
        private CCWin.SkinControl.SkinButton btnRegister;
        private CCWin.SkinControl.SkinLabel skinLabel_SoftName;
        private CCWin.SkinControl.SkinTextBox textBoxPwd;
        private CCWin.SkinControl.SkinTextBox textBoxId;
    }
}