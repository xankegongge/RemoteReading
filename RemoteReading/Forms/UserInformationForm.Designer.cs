namespace RemoteReading
{
    partial class UserInformationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInformationForm));
            this.timShow = new System.Windows.Forms.Timer(this.components);
            this.pnlTx = new CCWin.SkinControl.SkinPanel();
            this.pnlImgTx = new CCWin.SkinControl.SkinPanel();
            this.skinLabelName = new CCWin.SkinControl.SkinLabel();
            this.skinLabelHosptial = new CCWin.SkinControl.SkinLabel();
            this.skinLabel_tbid = new CCWin.SkinControl.SkinLabel();
            this.skinLabel_tbhospital = new CCWin.SkinControl.SkinLabel();
            this.lblQm = new CCWin.SkinControl.SkinLabel();
            this.pnlTx.SuspendLayout();
            this.SuspendLayout();
            // 
            // timShow
            // 
            this.timShow.Enabled = true;
            this.timShow.Interval = 500;
            this.timShow.Tick += new System.EventHandler(this.timShow_Tick);
            // 
            // pnlTx
            // 
            this.pnlTx.BackColor = System.Drawing.Color.Transparent;
            this.pnlTx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlTx.Controls.Add(this.pnlImgTx);
            this.pnlTx.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.pnlTx.DownBack = ((System.Drawing.Image)(resources.GetObject("pnlTx.DownBack")));
            this.pnlTx.Location = new System.Drawing.Point(4, 13);
            this.pnlTx.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTx.MouseBack = ((System.Drawing.Image)(resources.GetObject("pnlTx.MouseBack")));
            this.pnlTx.Name = "pnlTx";
            this.pnlTx.NormlBack = ((System.Drawing.Image)(resources.GetObject("pnlTx.NormlBack")));
            this.pnlTx.Palace = true;
            this.pnlTx.Size = new System.Drawing.Size(57, 57);
            this.pnlTx.TabIndex = 136;
            // 
            // pnlImgTx
            // 
            this.pnlImgTx.BackColor = System.Drawing.Color.Transparent;
            this.pnlImgTx.BackgroundImage = global::RemoteReading.Properties.Resources.GG64;
            this.pnlImgTx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlImgTx.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.pnlImgTx.DownBack = null;
            this.pnlImgTx.Location = new System.Drawing.Point(2, 2);
            this.pnlImgTx.Margin = new System.Windows.Forms.Padding(0);
            this.pnlImgTx.MouseBack = null;
            this.pnlImgTx.Name = "pnlImgTx";
            this.pnlImgTx.NormlBack = null;
            this.pnlImgTx.Radius = 4;
            this.pnlImgTx.Size = new System.Drawing.Size(53, 53);
            this.pnlImgTx.TabIndex = 6;
            // 
            // skinLabelName
            // 
            this.skinLabelName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinLabelName.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.Anamorphosis;
            this.skinLabelName.BackColor = System.Drawing.Color.Transparent;
            this.skinLabelName.BorderColor = System.Drawing.Color.Black;
            this.skinLabelName.BorderSize = 4;
            this.skinLabelName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.skinLabelName.ForeColor = System.Drawing.Color.White;
            this.skinLabelName.Location = new System.Drawing.Point(65, 10);
            this.skinLabelName.Name = "skinLabelName";
            this.skinLabelName.Size = new System.Drawing.Size(53, 20);
            this.skinLabelName.TabIndex = 102;
            this.skinLabelName.Text = "姓名：";
            // 
            // skinLabelHosptial
            // 
            this.skinLabelHosptial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinLabelHosptial.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.Anamorphosis;
            this.skinLabelHosptial.BackColor = System.Drawing.Color.Transparent;
            this.skinLabelHosptial.BorderColor = System.Drawing.Color.Black;
            this.skinLabelHosptial.BorderSize = 4;
            this.skinLabelHosptial.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.skinLabelHosptial.ForeColor = System.Drawing.Color.White;
            this.skinLabelHosptial.Location = new System.Drawing.Point(65, 42);
            this.skinLabelHosptial.Name = "skinLabelHosptial";
            this.skinLabelHosptial.Size = new System.Drawing.Size(53, 20);
            this.skinLabelHosptial.TabIndex = 102;
            this.skinLabelHosptial.Text = "医院：";
            // 
            // skinLabel_tbid
            // 
            this.skinLabel_tbid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinLabel_tbid.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.Anamorphosis;
            this.skinLabel_tbid.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_tbid.BorderColor = System.Drawing.Color.Black;
            this.skinLabel_tbid.BorderSize = 4;
            this.skinLabel_tbid.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.skinLabel_tbid.ForeColor = System.Drawing.Color.White;
            this.skinLabel_tbid.Location = new System.Drawing.Point(110, 10);
            this.skinLabel_tbid.Name = "skinLabel_tbid";
            this.skinLabel_tbid.Size = new System.Drawing.Size(162, 20);
            this.skinLabel_tbid.TabIndex = 102;
            this.skinLabel_tbid.Text = "10010";
            // 
            // skinLabel_tbhospital
            // 
            this.skinLabel_tbhospital.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinLabel_tbhospital.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.Anamorphosis;
            this.skinLabel_tbhospital.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel_tbhospital.BorderColor = System.Drawing.Color.Black;
            this.skinLabel_tbhospital.BorderSize = 4;
            this.skinLabel_tbhospital.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.skinLabel_tbhospital.ForeColor = System.Drawing.Color.White;
            this.skinLabel_tbhospital.Location = new System.Drawing.Point(110, 42);
            this.skinLabel_tbhospital.Name = "skinLabel_tbhospital";
            this.skinLabel_tbhospital.Size = new System.Drawing.Size(162, 20);
            this.skinLabel_tbhospital.TabIndex = 102;
            this.skinLabel_tbhospital.Text = "Tom";
            // 
            // lblQm
            // 
            this.lblQm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQm.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.Anamorphosis;
            this.lblQm.BackColor = System.Drawing.Color.Transparent;
            this.lblQm.BorderColor = System.Drawing.Color.Black;
            this.lblQm.BorderSize = 4;
            this.lblQm.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblQm.ForeColor = System.Drawing.Color.White;
            this.lblQm.Location = new System.Drawing.Point(7, 81);
            this.lblQm.Name = "lblQm";
            this.lblQm.Size = new System.Drawing.Size(257, 20);
            this.lblQm.TabIndex = 102;
            this.lblQm.Text = "退一步海阔天空！";
            // 
            // UserInformationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderPalace = global::RemoteReading.Properties.Resources.BackPalace;
            this.ClientSize = new System.Drawing.Size(279, 181);
            this.CloseDownBack = global::RemoteReading.Properties.Resources.btn_close_down;
            this.CloseMouseBack = global::RemoteReading.Properties.Resources.btn_close_highlight;
            this.CloseNormlBack = global::RemoteReading.Properties.Resources.btn_close_disable;
            this.ControlBox = false;
            this.Controls.Add(this.skinLabel_tbhospital);
            this.Controls.Add(this.pnlTx);
            this.Controls.Add(this.skinLabelHosptial);
            this.Controls.Add(this.skinLabel_tbid);
            this.Controls.Add(this.skinLabelName);
            this.Controls.Add(this.lblQm);
            this.MaxDownBack = global::RemoteReading.Properties.Resources.btn_max_down;
            this.MaxMouseBack = global::RemoteReading.Properties.Resources.btn_max_highlight;
            this.MaxNormlBack = global::RemoteReading.Properties.Resources.btn_max_normal;
            this.MiniDownBack = global::RemoteReading.Properties.Resources.btn_mini_down;
            this.MiniMouseBack = global::RemoteReading.Properties.Resources.btn_mini_highlight;
            this.MiniNormlBack = global::RemoteReading.Properties.Resources.btn_mini_normal;
            this.Mobile = CCWin.MobileStyle.None;
            this.Name = "UserInformationForm";
            this.RestoreDownBack = global::RemoteReading.Properties.Resources.btn_restore_down;
            this.RestoreMouseBack = global::RemoteReading.Properties.Resources.btn_restore_highlight;
            this.RestoreNormlBack = global::RemoteReading.Properties.Resources.btn_restore_normal;
            this.ShowBorder = false;
            this.ShowDrawIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.UserInformationForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmUserInformation_Paint);
            this.pnlTx.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timShow;
        private CCWin.SkinControl.SkinPanel pnlTx;
        private CCWin.SkinControl.SkinPanel pnlImgTx;
        private CCWin.SkinControl.SkinLabel skinLabelName;
        private CCWin.SkinControl.SkinLabel skinLabelHosptial;
        private CCWin.SkinControl.SkinLabel skinLabel_tbid;
        private CCWin.SkinControl.SkinLabel skinLabel_tbhospital;
        private CCWin.SkinControl.SkinLabel lblQm;
    }
}