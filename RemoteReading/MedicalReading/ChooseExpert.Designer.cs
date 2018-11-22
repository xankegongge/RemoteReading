namespace RemoteReading
{
    partial class ChooseExpert
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
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.expertListBox = new RemoteReading.ExpertListBox();
            this.SuspendLayout();
            // 
            // skinLabel1
            // 
            this.skinLabel1.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.skinLabel1.Location = new System.Drawing.Point(47, 34);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(152, 17);
            this.skinLabel1.TabIndex = 1;
            this.skinLabel1.Text = "双击选择专家进行阅片咨询";
            // 
            // expertListBox
            // 
            this.expertListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expertListBox.BackColor = System.Drawing.Color.White;
            this.expertListBox.IconSizeMode = CCWin.SkinControl.ChatListItemIcon.Large;
            this.expertListBox.Location = new System.Drawing.Point(-1, 63);
            this.expertListBox.Name = "expertListBox";
            this.expertListBox.Size = new System.Drawing.Size(258, 379);
            this.expertListBox.TabIndex = 137;
            // 
            // ChooseExpert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 443);
            this.Controls.Add(this.skinLabel1);
            this.Controls.Add(this.expertListBox);
            this.Name = "ChooseExpert";
            this.ShadowColor = System.Drawing.Color.White;
            this.Text = "选择专家";
            this.TitleColor = System.Drawing.Color.White;
            this.TopMost = false;
            this.Load += new System.EventHandler(this.ChooseExpert_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinLabel skinLabel1;
        private ExpertListBox expertListBox;
    }
}