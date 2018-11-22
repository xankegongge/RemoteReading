using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Reflection;
using _CUSTOM_CONTROLS._ChatListBox;

namespace WindowsFormsForControlTest
{
    public partial class Form1 : Form
    {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            button1.Text = "闪动";
            button2.Text = "插入[离开]";
            button3.Text = "大/小图标";
            chatListBox1.Items.Clear();
            Random rnd = new Random();
            for (int i = 0; i < 10; i++) {
                ChatListItem item = new ChatListItem("Group " + i);
              
                for (int j = 0; j < 10; j++) {
                    ChatListSubItem subItem = new ChatListSubItem("NicName", "DisplayName" + j, "Personal Message...!");
                    subItem.HeadImage = Image.FromFile("head/1 (" + rnd.Next(0, 45) + ").png");
                    subItem.Status = (ChatListSubItem.UserStatus)(j % 6);
                    item.SubItems.AddAccordingToStatus(subItem);
                    
                }
                item.SubItems.Sort();
                chatListBox1.Items.Add(item);
            }
            ChatListItem itema = new ChatListItem("TEST");
            for (int i = 0; i < 5; i++) {
                chatListBox1.Items.Add(itema);
            }
            chatListBox1.Items.Remove(itema);
        }

        private void button1_Click(object sender, EventArgs e) {
            chatListBox1.Items[0].SubItems[0].IsTwinkle = !chatListBox1.Items[0].SubItems[0].IsTwinkle;
            chatListBox1.Items[0].SubItems[1].IsTwinkle = !chatListBox1.Items[0].SubItems[1].IsTwinkle;
        }

        private void chatListBox1_MouseEnterHead(object sender, ChatListEventArgs e) {
            this.Text = e.MouseOnSubItem.DisplayName;
        }

        private void chatListBox1_MouseLeaveHead(object sender, ChatListEventArgs e) {
            this.Text = "Null";
        }

        private void chatListBox1_DoubleClickSubItem(object sender, ChatListEventArgs e) {
            MessageBox.Show(e.SelectSubItem.DisplayName);
        }

        private void button2_Click(object sender, EventArgs e) {
            //AddAccordingToStatus根据状态自己插入到正确位置
            //Add就是默认的添加
            //当然也可以用Add添加 然后用SubItem.Sort()进行一个排序
            chatListBox1.Items[0].SubItems.AddAccordingToStatus(
                new ChatListSubItem(
                    "123", "珠海市第一人民医院", "xankes", "尿液镜检", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    ChatListSubItem.UserStatus.Online, new Bitmap("head/1 (0).png"))
                );
        }

        private void button3_Click(object sender, EventArgs e) {
            if (chatListBox1.IconSizeMode == ChatListItemIcon.Large)
                chatListBox1.IconSizeMode = ChatListItemIcon.Small;
            else
                chatListBox1.IconSizeMode = ChatListItemIcon.Large;
        }
    }
}
