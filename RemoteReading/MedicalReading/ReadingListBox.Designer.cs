namespace RemoteReading
{
    partial class ReadingListBox
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
            _CUSTOM_CONTROLS._ChatListBox.ChatListItem chatListItem1 = new _CUSTOM_CONTROLS._ChatListBox.ChatListItem();
            _CUSTOM_CONTROLS._ChatListBox.ChatListItem chatListItem2 = new _CUSTOM_CONTROLS._ChatListBox.ChatListItem();
            _CUSTOM_CONTROLS._ChatListBox.ChatListItem chatListItem3 = new _CUSTOM_CONTROLS._ChatListBox.ChatListItem();
            _CUSTOM_CONTROLS._ChatListBox.ChatListItem chatListItem4 = new _CUSTOM_CONTROLS._ChatListBox.ChatListItem();
            this.chatListBox_MedicalReading = new _CUSTOM_CONTROLS.ChatListBox();
            this.SuspendLayout();
            // 
            // chatListBox_MedicalReading
            // 
            this.chatListBox_MedicalReading.BackColor = System.Drawing.Color.White;
            this.chatListBox_MedicalReading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatListBox_MedicalReading.ForeColor = System.Drawing.Color.Black;
            this.chatListBox_MedicalReading.IconSizeMode = _CUSTOM_CONTROLS._ChatListBox.ChatListItemIcon.Large;
            chatListItem1.Text = "未处理阅片";
            chatListItem2.Text = "正处理阅片";
            chatListItem3.Text = "已拒绝阅片";
            chatListItem4.Text = "已完成阅片";
            this.chatListBox_MedicalReading.Items.AddRange(new _CUSTOM_CONTROLS._ChatListBox.ChatListItem[] {
            chatListItem1,
            chatListItem2,
            chatListItem3,
            chatListItem4});
            this.chatListBox_MedicalReading.Location = new System.Drawing.Point(0, 0);
            this.chatListBox_MedicalReading.Name = "chatListBox_MedicalReading";
            this.chatListBox_MedicalReading.Size = new System.Drawing.Size(284, 408);
            this.chatListBox_MedicalReading.TabIndex = 0;
            this.chatListBox_MedicalReading.Text = "chatListBox1";
            this.chatListBox_MedicalReading.DoubleClickSubItem += new _CUSTOM_CONTROLS.ChatListBox.ChatListEventHandler(this.chatListBox_DoubleClickSubItem);
            // 
            // ReadingListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.chatListBox_MedicalReading);
            this.Name = "ReadingListBox";
            this.Size = new System.Drawing.Size(284, 408);
            this.ResumeLayout(false);

        }
        #endregion

        private _CUSTOM_CONTROLS.ChatListBox chatListBox_MedicalReading;
        
    }
}
