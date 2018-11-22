namespace RemoteReading
{
    partial class frmRemark
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
            this.tbRemark = new System.Windows.Forms.TextBox();
            this.skbtOK = new CCWin.SkinControl.SkinButton();
            this.skbtnClear = new CCWin.SkinControl.SkinButton();
            this.skLabel = new CCWin.SkinControl.SkinLabel();
            this.SuspendLayout();
            // 
            // tbRemark
            // 
            this.tbRemark.Location = new System.Drawing.Point(84, 40);
            this.tbRemark.Multiline = true;
            this.tbRemark.Name = "tbRemark";
            this.tbRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbRemark.Size = new System.Drawing.Size(236, 61);
            this.tbRemark.TabIndex = 0;
            // 
            // skbtOK
            // 
            this.skbtOK.BackColor = System.Drawing.Color.Transparent;
            this.skbtOK.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skbtOK.DownBack = null;
            this.skbtOK.Location = new System.Drawing.Point(84, 107);
            this.skbtOK.MouseBack = null;
            this.skbtOK.Name = "skbtOK";
            this.skbtOK.NormlBack = null;
            this.skbtOK.Size = new System.Drawing.Size(75, 23);
            this.skbtOK.TabIndex = 2;
            this.skbtOK.Text = "确定";
            this.skbtOK.UseVisualStyleBackColor = false;
            this.skbtOK.Click += new System.EventHandler(this.skbtOK_Click);
            // 
            // skbtnClear
            // 
            this.skbtnClear.BackColor = System.Drawing.Color.Transparent;
            this.skbtnClear.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skbtnClear.DownBack = null;
            this.skbtnClear.Location = new System.Drawing.Point(229, 107);
            this.skbtnClear.MouseBack = null;
            this.skbtnClear.Name = "skbtnClear";
            this.skbtnClear.NormlBack = null;
            this.skbtnClear.Size = new System.Drawing.Size(75, 23);
            this.skbtnClear.TabIndex = 2;
            this.skbtnClear.Text = "清空";
            this.skbtnClear.UseVisualStyleBackColor = false;
            this.skbtnClear.Click += new System.EventHandler(this.skbtnClear_Click);
            // 
            // skLabel
            // 
            this.skLabel.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skLabel.AutoSize = true;
            this.skLabel.BackColor = System.Drawing.Color.Transparent;
            this.skLabel.BorderColor = System.Drawing.Color.White;
            this.skLabel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skLabel.ForeColor = System.Drawing.Color.Red;
            this.skLabel.Location = new System.Drawing.Point(7, 40);
            this.skLabel.Name = "skLabel";
            this.skLabel.Size = new System.Drawing.Size(69, 19);
            this.skLabel.TabIndex = 3;
            this.skLabel.Text = "标注内容:";
            // 
            // frmRemark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 137);
            this.Controls.Add(this.skLabel);
            this.Controls.Add(this.skbtnClear);
            this.Controls.Add(this.skbtOK);
            this.Controls.Add(this.tbRemark);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmRemark";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "信息";
            this.Load += new System.EventHandler(this.frmRemark_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbRemark;
        private CCWin.SkinControl.SkinButton skbtOK;
        private CCWin.SkinControl.SkinButton skbtnClear;
        private CCWin.SkinControl.SkinLabel skLabel;
    }
}