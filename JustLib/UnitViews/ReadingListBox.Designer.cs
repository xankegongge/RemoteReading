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
            this.components = new System.ComponentModel.Container();
            CCWin.SkinControl.ChatListItem chatListItem1 = new CCWin.SkinControl.ChatListItem();
            CCWin.SkinControl.ChatListItem chatListItem2 = new CCWin.SkinControl.ChatListItem();
            CCWin.SkinControl.ChatListItem chatListItem3 = new CCWin.SkinControl.ChatListItem();
            CCWin.SkinControl.ChatListItem chatListItem4 = new CCWin.SkinControl.ChatListItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReadingListBox));
            this.chatListBox_MedicalReading = new CCWin.SkinControl.ChatListBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // chatListBox_MedicalReading
            // 
            this.chatListBox_MedicalReading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chatListBox_MedicalReading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatListBox_MedicalReading.DrawContentType = CCWin.SkinControl.DrawContentType.PersonalMsg;
            this.chatListBox_MedicalReading.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chatListBox_MedicalReading.ForeColor = System.Drawing.Color.Black;
            this.chatListBox_MedicalReading.FriendsMobile = true;
            chatListItem1.Bounds = new System.Drawing.Rectangle(0, 1, 284, 25);
            chatListItem1.IsOpen = true;
            chatListItem1.IsTwinkleHide = false;
            chatListItem1.OwnerChatListBox = this.chatListBox_MedicalReading;
            chatListItem1.Text = "未处理阅片";
            chatListItem1.TwinkleSubItemNumber = 0;
            chatListItem2.Bounds = new System.Drawing.Rectangle(0, 27, 284, 25);
            chatListItem2.IsTwinkleHide = false;
            chatListItem2.OwnerChatListBox = this.chatListBox_MedicalReading;
            chatListItem2.Text = "正处理阅片";
            chatListItem2.TwinkleSubItemNumber = 0;
            chatListItem3.Bounds = new System.Drawing.Rectangle(0, 53, 284, 25);
            chatListItem3.IsTwinkleHide = false;
            chatListItem3.OwnerChatListBox = this.chatListBox_MedicalReading;
            chatListItem3.Text = "已拒绝阅片";
            chatListItem3.TwinkleSubItemNumber = 0;
            chatListItem4.Bounds = new System.Drawing.Rectangle(0, 79, 284, 25);
            chatListItem4.IsTwinkleHide = false;
            chatListItem4.OwnerChatListBox = this.chatListBox_MedicalReading;
            chatListItem4.Text = "已完成阅片";
            chatListItem4.TwinkleSubItemNumber = 0;
            this.chatListBox_MedicalReading.Items.AddRange(new CCWin.SkinControl.ChatListItem[] {
            chatListItem1,
            chatListItem2,
            chatListItem3,
            chatListItem4});
            this.chatListBox_MedicalReading.ListHadOpenGroup = null;
            this.chatListBox_MedicalReading.ListSubItemMenu = null;
            this.chatListBox_MedicalReading.Location = new System.Drawing.Point(0, 0);
            this.chatListBox_MedicalReading.Name = "chatListBox_MedicalReading";
            this.chatListBox_MedicalReading.SelectSubItem = null;
            this.chatListBox_MedicalReading.ShowNicName = false;
            this.chatListBox_MedicalReading.Size = new System.Drawing.Size(284, 408);
            this.chatListBox_MedicalReading.SubItemMenu = null;
            this.chatListBox_MedicalReading.TabIndex = 0;
            this.chatListBox_MedicalReading.Text = "chatListBox1";
            this.chatListBox_MedicalReading.DoubleClickSubItem += new CCWin.SkinControl.ChatListBox.ChatListEventHandler(this.chatListBox_DoubleClickSubItem);
            this.chatListBox_MedicalReading.MouseEnterHead += new CCWin.SkinControl.ChatListBox.ChatListEventHandler(this.chatShow_MouseEnterHead);
            this.chatListBox_MedicalReading.MouseLeaveHead += new CCWin.SkinControl.ChatListBox.ChatListEventHandler(this.chatShow_MouseLeaveHead);
//            this.chatListBox_MedicalReading.DragSubItemDrop += new CCWin.SkinControl.ChatListBox.DragListEventHandler(this.chatListBox_DragSubItemDrop);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "medicalreadinghead.png");
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
        private CCWin.SkinControl.ChatListBox chatListBox_MedicalReading;
        #endregion
        private System.Windows.Forms.ImageList imageList1;
        
    }
}
