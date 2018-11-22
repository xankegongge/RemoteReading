namespace RemoteReading
{
    partial class MedicalReadingForm
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
            CCWin.SkinControl.Animation animation1 = new CCWin.SkinControl.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MedicalReadingForm));
            this.skinTabControl1 = new CCWin.SkinControl.SkinTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.readingListBoxNoproceed = new JustLib.UnitViews.ReadingListBox();
            this.readingListBoxProcessing = new JustLib.UnitViews.ReadingListBox();
            this.readingListBoxRejected = new JustLib.UnitViews.ReadingListBox();
            this.readingListBox1 = new JustLib.UnitViews.ReadingListBox();
            this.skinTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinTabControl1
            // 
            animation1.AnimateOnlyDifferences = false;
            animation1.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.BlindCoeff")));
            animation1.LeafCoeff = 0F;
            animation1.MaxTime = 1F;
            animation1.MinTime = 0F;
            animation1.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicCoeff")));
            animation1.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicShift")));
            animation1.MosaicSize = 0;
            animation1.Padding = new System.Windows.Forms.Padding(0);
            animation1.RotateCoeff = 0F;
            animation1.RotateLimit = 0F;
            animation1.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.ScaleCoeff")));
            animation1.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.SlideCoeff")));
            animation1.TimeCoeff = 2F;
            animation1.TransparencyCoeff = 0F;
            this.skinTabControl1.Animation = animation1;
            this.skinTabControl1.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
            this.skinTabControl1.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.skinTabControl1.Controls.Add(this.tabPage1);
            this.skinTabControl1.Controls.Add(this.tabPage2);
            this.skinTabControl1.Controls.Add(this.tabPage3);
            this.skinTabControl1.Controls.Add(this.tabPage4);
            this.skinTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTabControl1.ItemSize = new System.Drawing.Size(70, 36);
            this.skinTabControl1.Location = new System.Drawing.Point(4, 28);
            this.skinTabControl1.Name = "skinTabControl1";
            this.skinTabControl1.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageArrowDown")));
            this.skinTabControl1.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageArrowHover")));
            this.skinTabControl1.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageCloseHover")));
            this.skinTabControl1.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageCloseNormal")));
            this.skinTabControl1.PageDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageDown")));
            this.skinTabControl1.PageHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageHover")));
            this.skinTabControl1.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Left;
            this.skinTabControl1.PageNorml = null;
            this.skinTabControl1.SelectedIndex = 0;
            this.skinTabControl1.Size = new System.Drawing.Size(284, 444);
            this.skinTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.skinTabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.readingListBoxNoproceed);
            this.tabPage1.Location = new System.Drawing.Point(0, 36);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(284, 408);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "未处理";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.readingListBoxProcessing);
            this.tabPage2.Location = new System.Drawing.Point(0, 36);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(284, 408);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "正在处理";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.readingListBoxRejected);
            this.tabPage3.Location = new System.Drawing.Point(0, 36);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(284, 408);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "已拒绝";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.readingListBox1);
            this.tabPage4.Location = new System.Drawing.Point(0, 36);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(284, 408);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "已完成";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // readingListBoxNoproceed
            // 
            this.readingListBoxNoproceed.BackColor = System.Drawing.Color.White;
            this.readingListBoxNoproceed.IconSizeMode = CCWin.SkinControl.ChatListItemIcon.Large;
            this.readingListBoxNoproceed.Location = new System.Drawing.Point(3, 3);
            this.readingListBoxNoproceed.Name = "readingListBoxNoproceed";
            this.readingListBoxNoproceed.Size = new System.Drawing.Size(281, 402);
            this.readingListBoxNoproceed.TabIndex = 0;
            // 
            // readingListBoxProcessing
            // 
            this.readingListBoxProcessing.BackColor = System.Drawing.Color.White;
            this.readingListBoxProcessing.IconSizeMode = CCWin.SkinControl.ChatListItemIcon.Large;
            this.readingListBoxProcessing.Location = new System.Drawing.Point(3, 3);
            this.readingListBoxProcessing.Name = "readingListBoxProcessing";
            this.readingListBoxProcessing.Size = new System.Drawing.Size(281, 402);
            this.readingListBoxProcessing.TabIndex = 0;
            // 
            // readingListBoxRejected
            // 
            this.readingListBoxRejected.BackColor = System.Drawing.Color.White;
            this.readingListBoxRejected.IconSizeMode = CCWin.SkinControl.ChatListItemIcon.Large;
            this.readingListBoxRejected.Location = new System.Drawing.Point(3, 3);
            this.readingListBoxRejected.Name = "readingListBoxRejected";
            this.readingListBoxRejected.Size = new System.Drawing.Size(281, 402);
            this.readingListBoxRejected.TabIndex = 0;
            // 
            // readingListBox1
            // 
            this.readingListBox1.BackColor = System.Drawing.Color.White;
            this.readingListBox1.IconSizeMode = CCWin.SkinControl.ChatListItemIcon.Large;
            this.readingListBox1.Location = new System.Drawing.Point(3, 3);
            this.readingListBox1.Name = "readingListBox1";
            this.readingListBox1.Size = new System.Drawing.Size(281, 402);
            this.readingListBox1.TabIndex = 0;
            // 
            // MedicalReading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 476);
            this.Controls.Add(this.skinTabControl1);
            this.MinimizeBox = true;
            this.Name = "MedicalReading";
            this.Text = "阅片";
            this.skinTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinTabControl skinTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private JustLib.UnitViews.ReadingListBox readingListBoxNoproceed;
        private JustLib.UnitViews.ReadingListBox readingListBoxProcessing;
        private JustLib.UnitViews.ReadingListBox readingListBoxRejected;
        private JustLib.UnitViews.ReadingListBox readingListBox1;
    }
}