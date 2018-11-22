namespace RemoteReading
{
    partial class ExpertListBox
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.chatListBoxexpert = new CCWin.SkinControl.ChatListBox();
            this.skinContextMenuStrip1 = new CCWin.SkinControl.SkinContextMenuStrip();
            this.修改名称ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加分组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除分组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skinContextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chatListBoxexpert
            // 
            this.chatListBoxexpert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chatListBoxexpert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatListBoxexpert.DrawContentType = CCWin.SkinControl.DrawContentType.PersonalMsg;
            this.chatListBoxexpert.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chatListBoxexpert.ForeColor = System.Drawing.Color.Black;
            this.chatListBoxexpert.FriendsMobile = true;
            this.chatListBoxexpert.ListHadOpenGroup = null;
            this.chatListBoxexpert.Location = new System.Drawing.Point(0, 0);
            this.chatListBoxexpert.Name = "chatListBoxexpert";
            this.chatListBoxexpert.SelectSubItem = null;
            this.chatListBoxexpert.ShowNicName = false;
            this.chatListBoxexpert.Size = new System.Drawing.Size(136, 369);
            this.chatListBoxexpert.SubItemMenu = this.skinContextMenuStrip1;
            this.chatListBoxexpert.TabIndex = 0;
            this.chatListBoxexpert.Text = "chatListBox1";
            this.chatListBoxexpert.DoubleClickSubItem += new CCWin.SkinControl.ChatListBox.ChatListEventHandler(this.chatListBox_DoubleClickSubItem);
            this.chatListBoxexpert.MouseEnterHead += new CCWin.SkinControl.ChatListBox.ChatListEventHandler(this.chatShow_MouseEnterHead);
            this.chatListBoxexpert.MouseLeaveHead += new CCWin.SkinControl.ChatListBox.ChatListEventHandler(this.chatShow_MouseLeaveHead);
            this.chatListBoxexpert.DragSubItemDrop += new CCWin.SkinControl.ChatListBox.DragListEventHandler(this.chatListBox_DragSubItemDrop);
            // 
            // skinContextMenuStrip1
            // 
            this.skinContextMenuStrip1.Arrow = System.Drawing.Color.Black;
            this.skinContextMenuStrip1.Back = System.Drawing.Color.White;
            this.skinContextMenuStrip1.BackRadius = 4;
            this.skinContextMenuStrip1.Base = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(200)))), ((int)(((byte)(254)))));
            this.skinContextMenuStrip1.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.skinContextMenuStrip1.Fore = System.Drawing.Color.Black;
            this.skinContextMenuStrip1.HoverFore = System.Drawing.Color.White;
            this.skinContextMenuStrip1.ItemAnamorphosis = true;
            this.skinContextMenuStrip1.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinContextMenuStrip1.ItemBorderShow = true;
            this.skinContextMenuStrip1.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinContextMenuStrip1.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinContextMenuStrip1.ItemRadius = 4;
            this.skinContextMenuStrip1.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.修改名称ToolStripMenuItem,
            this.添加分组ToolStripMenuItem,
            this.删除分组ToolStripMenuItem});
            this.skinContextMenuStrip1.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinContextMenuStrip1.Name = "skinContextMenuStrip1";
            this.skinContextMenuStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinContextMenuStrip1.Size = new System.Drawing.Size(153, 92);
            this.skinContextMenuStrip1.SkinAllColor = true;
            this.skinContextMenuStrip1.TitleAnamorphosis = true;
            this.skinContextMenuStrip1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.skinContextMenuStrip1.TitleRadius = 4;
            this.skinContextMenuStrip1.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // 修改名称ToolStripMenuItem
            // 
            this.修改名称ToolStripMenuItem.Name = "修改名称ToolStripMenuItem";
            this.修改名称ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.修改名称ToolStripMenuItem.Text = "修改组名";
            this.修改名称ToolStripMenuItem.Click += new System.EventHandler(this.修改名称ToolStripMenuItem_Click);
            // 
            // 添加分组ToolStripMenuItem
            // 
            this.添加分组ToolStripMenuItem.Name = "添加分组ToolStripMenuItem";
            this.添加分组ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.添加分组ToolStripMenuItem.Text = "添加分组";
            this.添加分组ToolStripMenuItem.Click += new System.EventHandler(this.添加分组ToolStripMenuItem_Click);
            // 
            // 删除分组ToolStripMenuItem
            // 
            this.删除分组ToolStripMenuItem.Name = "删除分组ToolStripMenuItem";
            this.删除分组ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除分组ToolStripMenuItem.Text = "删除分组";
            this.删除分组ToolStripMenuItem.Click += new System.EventHandler(this.删除分组ToolStripMenuItem_Click);
            // 
            // ExpertListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.chatListBoxexpert);
            this.Name = "ExpertListBox";
            this.Size = new System.Drawing.Size(136, 369);
            this.skinContextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.ChatListBox chatListBoxexpert;
        private CCWin.SkinControl.SkinContextMenuStrip skinContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 修改名称ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加分组ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除分组ToolStripMenuItem;

    }
}
