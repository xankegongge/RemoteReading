﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CCWin.SkinControl;
using ESBasic.ObjectManagement.Managers;
using ESBasic;
using JustLib;
using JustLib.UnitViews;
using System.Threading;

namespace RemoteReading
{
    public partial class ExpertListBox : UserControl
    {
        private IUserInformationForm userInformationForm;//悬浮至头像时   
        private ChatListSubItem myselfChatListSubItem;
        private IUser currentUser;
        public ExpertListBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 添加分组的菜单被点击。参数:组名称
        /// </summary>
        public event CbGeneric AddCatalogClicked;
        /// <summary>
        /// 修改组名的菜单被点击。参数:组名称
        /// </summary>
        public event CbGeneric<string> ChangeCatalogNameClicked;
        public event CbGeneric<IUser> UserDoubleClicked;
        public event CbGeneric<IUser> ChatRecordClicked;
        public event CbGeneric<IUser> RemoveUserClicked;
        /// <summary>
        /// 当修改分组名称时，触发此事件。参数：oldName - newName - isMerge
        /// </summary>
        public event CbGeneric<string, string, bool> CatalogNameChanged;
        /// <summary>
        /// 当增加一个分组时，触发此事件。参数：CatelogName
        /// </summary>
        public event CbGeneric<string> CatalogAdded;
        /// <summary>
        /// 当删除一个分组时，触发此事件。参数：CatelogName
        /// </summary>
        public event CbGeneric<string> CatalogRemoved;
        /// <summary>
        /// 当将好友转移到另一组时，触发此事件。参数： FriendID - oldCatalog - newCatalogName
        /// </summary>
        public event CbGeneric<string, string, string> FriendCatalogMoved;

        public ChatListItemIcon IconSizeMode
        {
            get
            {
                return this.chatListBoxexpert.IconSizeMode;
            }
            set
            {
                this.chatListBoxexpert.IconSizeMode = value;
                this.chatListBoxexpert.Invalidate();
            }
        }

        private IHeadImageGetter resourceGetter;
        public void Initialize(IUser current ,IHeadImageGetter getter ,IUserInformationForm form)
        {
            this.resourceGetter = getter;
            this.currentUser = current;
            this.userInformationForm = form;//用户提示信息窗口
            if (this.userInformationForm != null)
            {
                ((Form)this.userInformationForm).Visible = false;
            }

            this.AssureCatalog(this.currentUser.DefaultExpertCatalog);//添加默认分组"我的收藏"

            //foreach (string catalog in this.currentUser.GetFriendCatalogList())
            //{
            //    this.AssureCatalog(catalog);
            //}
        }

        private void AssureCatalog(string catalog)
        {
            if (!this.catelogManager.Contains(catalog))
            {
                ChatListItem item = new ChatListItem(catalog);
                this.catelogManager.Add(catalog, item);
                this.chatListBoxexpert.Items.Add(item);
                this.chatListBoxexpert.Items.Sort();
            }
        }

        #region GetCatelogChatListItem
        private ObjectManager<string, ChatListItem> catelogManager = new ObjectManager<string, ChatListItem>();
        private ChatListItem GetCatelogChatListItem(IUser user)
        {
            string catelog = "专家列表";
            //foreach (KeyValuePair<string, List<string>> pair in this.currentUser.FriendDicationary)
            //{
            //    if (pair.Value.Contains(user.ID))
            //    {
            //        catelog = pair.Key;
            //        break;
            //    }
            //}
            //this.AssureCatalog(catelog);
            return this.catelogManager.Get(catelog);
        } 
        #endregion

        public void AddUser(IUser friend)//添加好友至子列表中
        {
            ChatListSubItem[] items = this.chatListBoxexpert.GetSubItemsById(friend.ID);
            if (items != null && items.Length > 0)
            {
                return;
            }
            string strIsexperts =friend.IsExpert? "      专家":"";
            EPlatformType e = friend.PlatformType;
            ChatListSubItem subItem = new ChatListSubItem(friend.ID, "", friend.PersonName+strIsexperts, friend.HospitalName, this.ConvertUserStatus(friend.UserStatus),( PlatformType)e, this.resourceGetter.GetHeadImage(friend));
            subItem.Tag = friend;
            this.GetCatelogChatListItem(friend).SubItems.AddAccordingToStatus(subItem);
            if (friend.ID == this.currentUser.ID)
            {
                this.myselfChatListSubItem = subItem;
            }
            subItem.OwnerListItem.SubItems.Sort();           
        }

        public void ExpandRoot()
        {
            this.chatListBoxexpert.Items[0].IsOpen = true;
        }

        public void SetAllUserOffline()
        {
            foreach (ChatListItem item in this.chatListBoxexpert.Items)
            {
                foreach (ChatListSubItem sub in item.SubItems)
                {
                    sub.Status = ChatListSubItem.UserStatus.OffLine;
                }                
            }

            this.chatListBoxexpert.Invalidate();
        }

        public void SortAllUser()
        {
            foreach (ChatListItem item in this.catelogManager.GetAll())
            {
                if (item.SubItems.Count > 0)
                {
                    item.SubItems.Sort();
                }
            }
        }

        public void RemoveUser(string userD)
        {
            this.chatListBoxexpert.RemoveSubItemsById(userD);
            this.chatListBoxexpert.Invalidate();           
        }

        public bool ContainsUser(string userID)
        {
            ChatListSubItem[] items = this.chatListBoxexpert.GetSubItemsById(userID);
            return (items != null && items.Length > 0);
        }

        public void SetTwinkleState(string userID, bool twinkle)
        {
            ChatListSubItem[] items = this.chatListBoxexpert.GetSubItemsById(userID);
            if (items == null || items.Length == 0)
            {
                return;
            }
            items[0].IsTwinkle = twinkle;
        }

        public void UserStatusChanged(IUser user)
        {           
            ChatListSubItem[] items = this.chatListBoxexpert.GetSubItemsById(user.ID);
            if (items == null || items.Length == 0)
            {
                return;
            }

            items[0].HeadImage = this.resourceGetter.GetHeadImage(user);
            items[0].Status = this.ConvertUserStatus(user.UserStatus);
            ChatListItem item = this.GetCatelogChatListItem(user);
            if (item != null)
            {
                item.SubItems.Sort();
            }
            this.chatListBoxexpert.Invalidate();           
        }

        public List<ChatListSubItem> SearchChatListSubItem(string idOrName)
        {
            ChatListSubItem[] items = this.chatListBoxexpert.GetSubItemsByText(idOrName);
            List<ChatListSubItem> list = new List<ChatListSubItem>();
            if (items != null)
            {
                foreach (ChatListSubItem item in items)
                {
                    if (item.ID != this.currentUser.ID)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public void UserInfoChanged(IUser user)
        {          
            ChatListSubItem[] items = this.chatListBoxexpert.GetSubItemsById(user.ID);
            if (items != null && items.Length > 0) //有可能部门发生了变化
            {
                IUser origin = (IUser)items[0].Tag;
                ChatListItem ownerItem = this.GetCatelogChatListItem(origin);
                ownerItem.SubItems.Remove(items[0]);
                this.AddUser(user); //有可能是新添加的好友
            }
            else//新用户
            {
                this.AddUser(user);
            }
        }

        private void toolStripMenuItem51_Click(object sender, EventArgs e)
        {
            ChatListSubItem item = this.chatListBoxexpert.SelectSubItem;
            IUser friend = (IUser)item.Tag;
            item.IsTwinkle = false;

            if (friend.ID == this.currentUser.ID)
            {
                return;
            }

            if (this.UserDoubleClicked != null)
            {
                this.UserDoubleClicked(friend);
            }
        }

        private void 消息记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentUser.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            IUser friend = (IUser)this.chatListBoxexpert.SelectSubItem.Tag;
            if (friend.ID == this.currentUser.ID)
            {
                return;
            }

            if (this.ChatRecordClicked != null)
            {
                this.ChatRecordClicked(friend);
            }
        }

        private void 删除好友ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentUser.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            IUser friend = (IUser)this.chatListBoxexpert.SelectSubItem.Tag;
            if (friend.ID == this.currentUser.ID)
            {
                return;
            }

            if (this.RemoveUserClicked != null)
            {
                this.RemoveUserClicked(friend);
            }
        }

        private void chatListBox_DoubleClickSubItem(object sender, ChatListEventArgs e)
        {
            ChatListSubItem item = e.SelectSubItem;
            IUser friend = (IUser)item.Tag;
            item.IsTwinkle = false;
            
            if (friend.ID == this.currentUser.ID)
            {
                return;
            }

            if (this.UserDoubleClicked != null)
            {
                this.UserDoubleClicked(friend);
            }
        }

        #region 显示用户资料
        private bool firstShow = false;
        private void chatShow_MouseEnterHead(object sender, ChatListEventArgs e)
        {
            if (this.userInformationForm == null)
            {
                return;
            }

            ChatListSubItem item = e.MouseOnSubItem;
            if (item == null)
            {
                item = e.SelectSubItem;
            }

            Point loc = this.PointToScreen(this.Location);

            //int top = this.Top + this.chatListBox.Top + (item.HeadRect.Y - this.chatListBox.chatVScroll.Value);
            //int left = this.Left - 279 - 5;
            int top = loc.Y + (item.HeadRect.Y - this.chatListBoxexpert.chatVScroll.Value) - this.Location.Y;
            int left = loc.X - 279 - 5;
            //int ph = Screen.GetWorkingArea(this).Height;

            //if (top + 181 > ph)
            //{
            //    top = ph - 181 - 5;
            //}

            if (left < 0)
            {
                left = this.Right + 5;
            }

            IUser user = (IUser)item.Tag;
            Form form = (Form)this.userInformationForm;            
            this.userInformationForm.SetUser(user);           
            form.Location = new Point(left, top);
            if (!this.firstShow)
            {
                form.Show();
            }
            else
            {
                this.firstShow = true;
            }
            form.Location = new Point(left, top);
        }

        private void chatShow_MouseLeaveHead(object sender, ChatListEventArgs e)
        {
            if (this.userInformationForm == null)
            {
                return;
            }

            Thread.Sleep(100);
            Form form = (Form)this.userInformationForm;
            if (!form.Bounds.Contains(Cursor.Position))
            {
                form.Hide();
            }
        }
        #endregion

        private void chatListBox_DragSubItemDrop(object sender, DragListEventArgs e)
        {
            if (this.FriendCatalogMoved != null)
            {
                IUser user = (IUser) e.QSubItem.Tag ;
                this.FriendCatalogMoved(user.ID, e.QSubItem.OwnerListItem.Text, e.HSubItem.OwnerListItem.Text);
            }
        }

        public void ChangeCatelogName(string oldName, string newName)
        {
            if (this.currentUser.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            this.catelogManager.Remove(oldName);
            ChatListItem existedItem = null;
            foreach (ChatListItem item in this.chatListBoxexpert.Items)
            {
                if (item.Text == newName)
                {
                    existedItem = item;
                    break;
                }
            }
            if (existedItem != null)
            {
                foreach (ChatListSubItem sub in this.chatListBoxexpert.SelectItem.SubItems)
                {
                    sub.OwnerListItem = existedItem;
                    existedItem.SubItems.Add(sub);
                }
                existedItem.SubItems.Sort();
                this.chatListBoxexpert.Items.Remove(this.chatListBoxexpert.SelectItem);
                existedItem.IsOpen = true;
                if (this.CatalogNameChanged != null)
                {
                    this.CatalogNameChanged(oldName, newName, true);
                }
                return;
            }

            this.catelogManager.Add(newName, this.chatListBoxexpert.SelectItem);
            this.chatListBoxexpert.SelectItem.Text = newName;
            if (this.CatalogNameChanged != null)
            {
                this.CatalogNameChanged(oldName, newName, false);
            }
        }

        private void 修改名称ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentUser.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            string oldName = this.chatListBoxexpert.SelectItem.Text;
            if (this.ChangeCatalogNameClicked != null)
            {
                this.ChangeCatalogNameClicked(oldName);
            }
        }

        public void AddCatalog(string catelogName)
        {
            if (this.currentUser.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            foreach (ChatListItem item in this.chatListBoxexpert.Items)
            {
                if (item.Text == catelogName)
                {
                    MessageBox.Show("已经存在同名的分组！");
                    return;
                }
            }

            ChatListItem newItem = new ChatListItem(catelogName);
            this.catelogManager.Add(catelogName, newItem);
            this.chatListBoxexpert.Items.Add(newItem);
            this.chatListBoxexpert.Items.Sort();

            if (this.CatalogAdded != null)
            {
                this.CatalogAdded(catelogName);
            }
        }

        private void 添加分组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentUser.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            if (this.AddCatalogClicked != null)
            {
                this.AddCatalogClicked();
            }
        }

        private void 删除分组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentUser.UserStatus == UserStatus.OffLine)
            {
                return;
            }

            string name = this.chatListBoxexpert.SelectItem.Text;
            if (this.currentUser.DefaultFriendCatalog == name)
            {
                MessageBox.Show(string.Format("分组 [{0}] 是默认分组，不能删除！", name));
                return;
            }

            if (this.chatListBoxexpert.SelectItem.SubItems.Count > 0)
            {
                MessageBox.Show(string.Format("分组 [{0}] 不为空，不能删除！" ,name));
                return;
            }

            if (!ESBasic.Helpers.WindowsHelper.ShowQuery(string.Format("您确定要删除分组 [{0}] 吗？", name)))
            {
                return;
            }

            this.chatListBoxexpert.Items.Remove(this.chatListBoxexpert.SelectItem);
            this.catelogManager.Remove(name);
            if (this.CatalogRemoved != null)
            {
                this.CatalogRemoved(name);
            }
        }

        #region ConvertUserStatus
        private ChatListSubItem.UserStatus ConvertUserStatus(UserStatus status)
        {
            if (status == UserStatus.Hide)
            {
                return ChatListSubItem.UserStatus.OffLine;
            }

            return (ChatListSubItem.UserStatus)((int)status);
        }
        #endregion
    }
}
