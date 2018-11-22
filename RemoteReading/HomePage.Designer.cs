namespace RemoteReading
{
    partial class HomePage
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
            this.skinPanel1 = new CCWin.SkinControl.SkinPanel();
            this.skinButton4 = new CCWin.SkinControl.SkinButton();
            this.skinButton3 = new CCWin.SkinControl.SkinButton();
            this.skbtnReading = new CCWin.SkinControl.SkinButton();
            this.skbtnChating = new CCWin.SkinControl.SkinButton();
            this.skinPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinLabel1
            // 
            this.skinLabel1.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(137, 28);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(206, 31);
            this.skinLabel1.TabIndex = 0;
            this.skinLabel1.Text = "远程阅片管理系统";
            // 
            // skinPanel1
            // 
            this.skinPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinPanel1.BackColor = System.Drawing.Color.Gainsboro;
            this.skinPanel1.Controls.Add(this.skinButton4);
            this.skinPanel1.Controls.Add(this.skinButton3);
            this.skinPanel1.Controls.Add(this.skbtnReading);
            this.skinPanel1.Controls.Add(this.skbtnChating);
            this.skinPanel1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel1.DownBack = null;
            this.skinPanel1.Location = new System.Drawing.Point(7, 130);
            this.skinPanel1.MouseBack = null;
            this.skinPanel1.Name = "skinPanel1";
            this.skinPanel1.NormlBack = null;
            this.skinPanel1.Size = new System.Drawing.Size(486, 185);
            this.skinPanel1.TabIndex = 1;
            // 
            // skinButton4
            // 
            this.skinButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.skinButton4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.skinButton4.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton4.DownBack = null;
            this.skinButton4.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinButton4.Location = new System.Drawing.Point(245, 93);
            this.skinButton4.MouseBack = null;
            this.skinButton4.Name = "skinButton4";
            this.skinButton4.NormlBack = null;
            this.skinButton4.Size = new System.Drawing.Size(237, 88);
            this.skinButton4.TabIndex = 0;
            this.skinButton4.Text = "个人信息";
            this.skinButton4.UseVisualStyleBackColor = false;
            // 
            // skinButton3
            // 
            this.skinButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.skinButton3.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton3.DownBack = null;
            this.skinButton3.Location = new System.Drawing.Point(3, 93);
            this.skinButton3.MouseBack = null;
            this.skinButton3.Name = "skinButton3";
            this.skinButton3.NormlBack = null;
            this.skinButton3.Size = new System.Drawing.Size(246, 88);
            this.skinButton3.TabIndex = 0;
            this.skinButton3.UseVisualStyleBackColor = false;
            // 
            // skbtnReading
            // 
            this.skbtnReading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.skbtnReading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.skbtnReading.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skbtnReading.DownBack = null;
            this.skbtnReading.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skbtnReading.Location = new System.Drawing.Point(245, 3);
            this.skbtnReading.MouseBack = null;
            this.skbtnReading.Name = "skbtnReading";
            this.skbtnReading.NormlBack = null;
            this.skbtnReading.Size = new System.Drawing.Size(237, 88);
            this.skbtnReading.TabIndex = 0;
            this.skbtnReading.Text = "阅片";
            this.skbtnReading.UseVisualStyleBackColor = false;
            this.skbtnReading.Click += new System.EventHandler(this.skbtnReading_Click);
            // 
            // skbtnChating
            // 
            this.skbtnChating.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.skbtnChating.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skbtnChating.DownBack = null;
            this.skbtnChating.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skbtnChating.ForeColor = System.Drawing.Color.Black;
            this.skbtnChating.Location = new System.Drawing.Point(3, 3);
            this.skbtnChating.MouseBack = null;
            this.skbtnChating.Name = "skbtnChating";
            this.skbtnChating.NormlBack = null;
            this.skbtnChating.Size = new System.Drawing.Size(246, 88);
            this.skbtnChating.TabIndex = 0;
            this.skbtnChating.Text = "聊天";
            this.skbtnChating.UseVisualStyleBackColor = false;
            this.skbtnChating.Click += new System.EventHandler(this.skbtnChating_Click);
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 323);
            this.Controls.Add(this.skinPanel1);
            this.Controls.Add(this.skinLabel1);
            this.Name = "HomePage";
            this.Text = "首页";
            this.skinPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinPanel skinPanel1;
        private CCWin.SkinControl.SkinButton skinButton4;
        private CCWin.SkinControl.SkinButton skinButton3;
        private CCWin.SkinControl.SkinButton skbtnReading;
        private CCWin.SkinControl.SkinButton skbtnChating;
    }
}