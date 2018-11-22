namespace GradeSystem.Server
{
    partial class UserManagement
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
            this.components = new System.ComponentModel.Container();
            this.bs = new System.Windows.Forms.BindingSource(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加用户ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRefresh = new CCWin.SkinControl.SkinButton();
            this.skinPanel1 = new CCWin.SkinControl.SkinPanel();
            this.btnCheck = new CCWin.SkinControl.SkinButton();
            this.skinTextBox1 = new CCWin.SkinControl.SkinTextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.UserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PasswordMD5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActivited = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VIPLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PersonName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MobilePhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfessionTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HospitalID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bs)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.skinPanel1.SuspendLayout();
            this.skinTextBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem,
            this.添加用户ToolStripMenuItem,
            this.编辑ToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(165, 70);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.删除ToolStripMenuItem.Text = "删除(选中一行）";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 添加用户ToolStripMenuItem
            // 
            this.添加用户ToolStripMenuItem.Name = "添加用户ToolStripMenuItem";
            this.添加用户ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.添加用户ToolStripMenuItem.Text = "添加用户";
            this.添加用户ToolStripMenuItem.Click += new System.EventHandler(this.添加用户ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.编辑ToolStripMenuItem.Text = "编辑";
            this.编辑ToolStripMenuItem.Click += new System.EventHandler(this.编辑ToolStripMenuItem_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnRefresh.DownBack = null;
            this.btnRefresh.Location = new System.Drawing.Point(31, 45);
            this.btnRefresh.MouseBack = null;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.NormlBack = null;
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // skinPanel1
            // 
            this.skinPanel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.skinPanel1.Controls.Add(this.btnCheck);
            this.skinPanel1.Controls.Add(this.skinTextBox1);
            this.skinPanel1.Controls.Add(this.comboBox1);
            this.skinPanel1.Controls.Add(this.skinLabel2);
            this.skinPanel1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel1.DownBack = null;
            this.skinPanel1.Location = new System.Drawing.Point(267, 31);
            this.skinPanel1.MouseBack = null;
            this.skinPanel1.Name = "skinPanel1";
            this.skinPanel1.NormlBack = null;
            this.skinPanel1.Size = new System.Drawing.Size(566, 46);
            this.skinPanel1.TabIndex = 4;
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.Black;
            this.btnCheck.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCheck.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnCheck.DownBack = null;
            this.btnCheck.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCheck.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnCheck.Location = new System.Drawing.Point(440, 8);
            this.btnCheck.MouseBack = null;
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.NormlBack = null;
            this.btnCheck.Size = new System.Drawing.Size(110, 29);
            this.btnCheck.TabIndex = 4;
            this.btnCheck.Text = "查询";
            this.btnCheck.UseVisualStyleBackColor = false;
            // 
            // skinTextBox1
            // 
            this.skinTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox1.Icon = null;
            this.skinTextBox1.IconIsButton = false;
            this.skinTextBox1.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox1.Location = new System.Drawing.Point(233, 8);
            this.skinTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox1.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox1.MouseBack = null;
            this.skinTextBox1.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox1.Name = "skinTextBox1";
            this.skinTextBox1.NormlBack = null;
            this.skinTextBox1.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox1.Size = new System.Drawing.Size(204, 28);
            // 
            // skinTextBox1.BaseText
            // 
            this.skinTextBox1.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox1.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox1.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox1.SkinTxt.Name = "BaseText";
            this.skinTextBox1.SkinTxt.Size = new System.Drawing.Size(175, 18);
            this.skinTextBox1.SkinTxt.TabIndex = 0;
            this.skinTextBox1.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox1.SkinTxt.WaterText = "";
            this.skinTextBox1.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.comboBox1.DropDownHeight = 120;
            this.comboBox1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.IntegralHeight = false;
            this.comboBox1.Items.AddRange(new object[] {
            "全文",
            "UserID",
            "姓名",
            "医院名称"});
            this.comboBox1.Location = new System.Drawing.Point(83, 8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(139, 27);
            this.comboBox1.TabIndex = 3;
            // 
            // skinLabel2
            // 
            this.skinLabel2.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel2.AutoSize = true;
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.skinLabel2.Location = new System.Drawing.Point(3, 15);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(74, 21);
            this.skinLabel2.TabIndex = 2;
            this.skinLabel2.Text = "查询方式";
            // 
            // skinLabel1
            // 
            this.skinLabel1.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(7, 84);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(0, 17);
            this.skinLabel1.TabIndex = 2;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv.BackgroundColor = System.Drawing.Color.DodgerBlue;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserID,
            this.PasswordMD5,
            this.UserType,
            this.IsActivited,
            this.CreateTime,
            this.VIPLevel,
            this.PersonName,
            this.MobilePhone,
            this.Email,
            this.ProfessionTitle,
            this.HospitalID});
            this.dgv.ContextMenuStrip = this.contextMenu;
            this.dgv.Location = new System.Drawing.Point(4, 87);
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(1151, 384);
            this.dgv.TabIndex = 0;
            this.dgv.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_CellFormatting);
            this.dgv.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_ColumnHeaderMouseClick);
            this.dgv.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_RowHeaderMouseClick);
            this.dgv.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_RowPostPaint);
            this.dgv.Sorted += new System.EventHandler(this.dgv_Sorted);
            this.dgv.Paint += new System.Windows.Forms.PaintEventHandler(this.dgv_Paint);
            // 
            // UserID
            // 
            this.UserID.DataPropertyName = "UserID";
            this.UserID.HeaderText = "用户ID";
            this.UserID.Name = "UserID";
            // 
            // PasswordMD5
            // 
            this.PasswordMD5.DataPropertyName = "PasswordMD5";
            this.PasswordMD5.HeaderText = "密码";
            this.PasswordMD5.Name = "PasswordMD5";
            // 
            // UserType
            // 
            this.UserType.DataPropertyName = "UserType";
            this.UserType.HeaderText = "角色类型";
            this.UserType.Name = "UserType";
            // 
            // IsActivited
            // 
            this.IsActivited.DataPropertyName = "IsActivited";
            this.IsActivited.HeaderText = "是否激活";
            this.IsActivited.Name = "IsActivited";
            // 
            // CreateTime
            // 
            this.CreateTime.DataPropertyName = "CreateTime";
            this.CreateTime.HeaderText = "注册时间";
            this.CreateTime.Name = "CreateTime";
            // 
            // VIPLevel
            // 
            this.VIPLevel.DataPropertyName = "VIPLevel";
            this.VIPLevel.HeaderText = "用户等级";
            this.VIPLevel.Name = "VIPLevel";
            // 
            // PersonName
            // 
            this.PersonName.DataPropertyName = "PersonName";
            this.PersonName.HeaderText = "姓名";
            this.PersonName.Name = "PersonName";
            // 
            // MobilePhone
            // 
            this.MobilePhone.DataPropertyName = "MobilePhone";
            this.MobilePhone.HeaderText = "移动手机";
            this.MobilePhone.Name = "MobilePhone";
            // 
            // Email
            // 
            this.Email.DataPropertyName = "Email";
            this.Email.HeaderText = "邮箱";
            this.Email.Name = "Email";
            // 
            // ProfessionTitle
            // 
            this.ProfessionTitle.DataPropertyName = "ProfessionTitle";
            this.ProfessionTitle.HeaderText = "头衔";
            this.ProfessionTitle.Name = "ProfessionTitle";
            // 
            // HospitalID
            // 
            this.HospitalID.DataPropertyName = "HospitalID";
            this.HospitalID.HeaderText = "医院名称";
            this.HospitalID.Name = "HospitalID";
            // 
            // UserManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CanResize = true;
            this.ClientSize = new System.Drawing.Size(1159, 475);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.skinPanel1);
            this.Controls.Add(this.skinLabel1);
            this.Controls.Add(this.dgv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = true;
            this.Name = "UserManagement";
            this.Text = "用户管理";
            this.Load += new System.EventHandler(this.UserManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bs)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.skinPanel1.ResumeLayout(false);
            this.skinPanel1.PerformLayout();
            this.skinTextBox1.ResumeLayout(false);
            this.skinTextBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private CCWin.SkinControl.SkinTextBox skinTextBox1;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinPanel skinPanel1;
        private CCWin.SkinControl.SkinButton btnCheck;
        private System.Windows.Forms.BindingSource bs;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加用户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PasswordMD5;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserType;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsActivited;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn VIPLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn PersonName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MobilePhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfessionTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn HospitalID;
        private CCWin.SkinControl.SkinButton btnRefresh;
    }
}