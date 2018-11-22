namespace RemoteReading
{
    partial class frmRejectedReason
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
            this.skinPanel1 = new CCWin.SkinControl.SkinPanel();
            this.skLabelRejectReason = new CCWin.SkinControl.SkinLabel();
            this.skinRichTextBoxOtherReason = new CCWin.SkinControl.SkinRichTextBox();
            this.skbReajectedOK = new CCWin.SkinControl.SkinButton();
            this.skinLabelOtherReason = new CCWin.SkinControl.SkinLabel();
            this.skinPanel2 = new CCWin.SkinControl.SkinPanel();
            this.skinPanel3 = new CCWin.SkinControl.SkinPanel();
            this.skinComboBoxReason = new System.Windows.Forms.ComboBox();
            this.skinPanel1.SuspendLayout();
            this.skinPanel2.SuspendLayout();
            this.skinPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinPanel1
            // 
            this.skinPanel1.AutoSize = true;
            this.skinPanel1.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel1.Controls.Add(this.skinComboBoxReason);
            this.skinPanel1.Controls.Add(this.skLabelRejectReason);
            this.skinPanel1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.skinPanel1.DownBack = null;
            this.skinPanel1.Location = new System.Drawing.Point(4, 28);
            this.skinPanel1.MouseBack = null;
            this.skinPanel1.Name = "skinPanel1";
            this.skinPanel1.NormlBack = null;
            this.skinPanel1.Size = new System.Drawing.Size(344, 34);
            this.skinPanel1.TabIndex = 3;
            // 
            // skLabelRejectReason
            // 
            this.skLabelRejectReason.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skLabelRejectReason.AutoSize = true;
            this.skLabelRejectReason.BackColor = System.Drawing.Color.Transparent;
            this.skLabelRejectReason.BorderColor = System.Drawing.Color.White;
            this.skLabelRejectReason.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skLabelRejectReason.Location = new System.Drawing.Point(16, 17);
            this.skLabelRejectReason.Name = "skLabelRejectReason";
            this.skLabelRejectReason.Size = new System.Drawing.Size(59, 17);
            this.skLabelRejectReason.TabIndex = 7;
            this.skLabelRejectReason.Text = "拒绝理由:";
            // 
            // skinRichTextBoxOtherReason
            // 
            this.skinRichTextBoxOtherReason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinRichTextBoxOtherReason.Location = new System.Drawing.Point(81, 6);
            this.skinRichTextBoxOtherReason.Name = "skinRichTextBoxOtherReason";
            this.skinRichTextBoxOtherReason.Size = new System.Drawing.Size(234, 59);
            this.skinRichTextBoxOtherReason.TabIndex = 9;
            this.skinRichTextBoxOtherReason.Text = "";
            // 
            // skbReajectedOK
            // 
            this.skbReajectedOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.skbReajectedOK.BackColor = System.Drawing.Color.LimeGreen;
            this.skbReajectedOK.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skbReajectedOK.DownBack = null;
            this.skbReajectedOK.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skbReajectedOK.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.skbReajectedOK.Location = new System.Drawing.Point(116, 20);
            this.skbReajectedOK.MouseBack = null;
            this.skbReajectedOK.Name = "skbReajectedOK";
            this.skbReajectedOK.NormlBack = null;
            this.skbReajectedOK.Size = new System.Drawing.Size(93, 23);
            this.skbReajectedOK.TabIndex = 2;
            this.skbReajectedOK.Text = "确定";
            this.skbReajectedOK.UseVisualStyleBackColor = false;
            this.skbReajectedOK.Click += new System.EventHandler(this.skbReajectedOK_Click);
            // 
            // skinLabelOtherReason
            // 
            this.skinLabelOtherReason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinLabelOtherReason.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabelOtherReason.AutoSize = true;
            this.skinLabelOtherReason.BackColor = System.Drawing.Color.Transparent;
            this.skinLabelOtherReason.BorderColor = System.Drawing.Color.White;
            this.skinLabelOtherReason.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabelOtherReason.Location = new System.Drawing.Point(16, 33);
            this.skinLabelOtherReason.Name = "skinLabelOtherReason";
            this.skinLabelOtherReason.Size = new System.Drawing.Size(59, 17);
            this.skinLabelOtherReason.TabIndex = 6;
            this.skinLabelOtherReason.Text = "其他理由:";
            // 
            // skinPanel2
            // 
            this.skinPanel2.AutoSize = true;
            this.skinPanel2.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel2.Controls.Add(this.skinLabelOtherReason);
            this.skinPanel2.Controls.Add(this.skinRichTextBoxOtherReason);
            this.skinPanel2.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.skinPanel2.DownBack = null;
            this.skinPanel2.Location = new System.Drawing.Point(4, 62);
            this.skinPanel2.MouseBack = null;
            this.skinPanel2.Name = "skinPanel2";
            this.skinPanel2.NormlBack = null;
            this.skinPanel2.Size = new System.Drawing.Size(344, 73);
            this.skinPanel2.TabIndex = 3;
            this.skinPanel2.Visible = false;
            // 
            // skinPanel3
            // 
            this.skinPanel3.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel3.Controls.Add(this.skbReajectedOK);
            this.skinPanel3.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.skinPanel3.DownBack = null;
            this.skinPanel3.Location = new System.Drawing.Point(4, 135);
            this.skinPanel3.MouseBack = null;
            this.skinPanel3.Name = "skinPanel3";
            this.skinPanel3.NormlBack = null;
            this.skinPanel3.Size = new System.Drawing.Size(344, 46);
            this.skinPanel3.TabIndex = 4;
            // 
            // skinComboBoxReason
            // 
            this.skinComboBoxReason.FormattingEnabled = true;
            this.skinComboBoxReason.Items.AddRange(new object[] {
            "图片不清晰",
            "信息不全",
            "其他"});
            this.skinComboBoxReason.Location = new System.Drawing.Point(81, 11);
            this.skinComboBoxReason.Name = "skinComboBoxReason";
            this.skinComboBoxReason.Size = new System.Drawing.Size(227, 20);
            this.skinComboBoxReason.TabIndex = 8;
            // 
            // frmRejectedReason
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(352, 196);
            this.Controls.Add(this.skinPanel3);
            this.Controls.Add(this.skinPanel2);
            this.Controls.Add(this.skinPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmRejectedReason";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "拒绝理由";
            this.Load += new System.EventHandler(this.frmRejectedReason_Load);
            this.skinPanel1.ResumeLayout(false);
            this.skinPanel1.PerformLayout();
            this.skinPanel2.ResumeLayout(false);
            this.skinPanel2.PerformLayout();
            this.skinPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinButton skbReajectedOK;
        private CCWin.SkinControl.SkinPanel skinPanel1;
        private CCWin.SkinControl.SkinRichTextBox skinRichTextBoxOtherReason;
        private CCWin.SkinControl.SkinLabel skinLabelOtherReason;
        private CCWin.SkinControl.SkinLabel skLabelRejectReason;
        private CCWin.SkinControl.SkinPanel skinPanel2;
        private CCWin.SkinControl.SkinPanel skinPanel3;
        private System.Windows.Forms.ComboBox skinComboBoxReason;
    }
}