using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CCWin.SkinControl;
using ESBasic;
using ESBasic.ObjectManagement.Managers;
using JustLib.UnitViews;
using System.Threading;
using JustLib;
using RemoteReading;
namespace RemoteReading
{
    public partial class ReadingListBox : UserControl
    {
        private IUserInformationForm userInformationForm;//悬浮至头像时   
        private ChatListSubItem myselfChatListSubItem;
        private GGUser currentUser;
        public ReadingListBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 添加分组的菜单被点击。参数:组名称
        /// </summary>
        //public event CbGeneric AddCatalogClicked;
        /// <summary>
        /// 修改组名的菜单被点击。参数:组名称
        /// </summary>
        //public event CbGeneric<string> ChangeCatalogNameClicked;
        public event CbGeneric<MedicalReading> MedicalDoubleClicked;
        //public event CbGeneric<GGUser> ChatRecordClicked;
        //public event CbGeneric<GGUser> RemoveUserClicked;
        /// <summary>
        /// 当修改分组名称时，触发此事件。参数：oldName - newName - isMerge
        /// </summary>
        //public event CbGeneric<string, string, bool> CatalogNameChanged;
        ///// <summary>
        ///// 当增加一个分组时，触发此事件。参数：CatelogName
        ///// </summary>
        //public event CbGeneric<string> CatalogAdded;
        ///// <summary>
        ///// 当删除一个分组时，触发此事件。参数：CatelogName
        ///// </summary>
        //public event CbGeneric<string> CatalogRemoved;
        ///// <summary>
        ///// 当将好友转移到另一组时，触发此事件。参数： FriendID - oldCatalog - newCatalogName
        ///// </summary>
        //public event CbGeneric<string, string, string> FriendCatalogMoved;

        public ChatListItemIcon IconSizeMode
        {
            get
            {
                return this.chatListBox_MedicalReading.IconSizeMode;
            }
            set
            {
                this.chatListBox_MedicalReading.IconSizeMode = value;
                this.chatListBox_MedicalReading.Invalidate();
            }
        }
       

        private IHeadImageGetter resourceGetter;
        public void Initialize(GGUser current , IHeadImageGetter getter ,IUserInformationForm form)
        {
            this.resourceGetter = getter;
            this.currentUser = current;
            this.userInformationForm = form;
            if (this.userInformationForm != null)
            {
                ((Form)this.userInformationForm).Visible = false;
            }

        }
        //添加组名，组名与ChatListItem对应。  朋友列表与ChatSubListItem对应
        //private void AssureCatalog(string catalog)
        //{
        //    if (!this.catelogManager.Contains(catalog))
        //    {
        //        ChatListItem item = new ChatListItem(catalog);//组名
        //        this.catelogManager.Add(catalog, item);
        //        this.chatListBox_MedicalReading.Items.Add(item);
        //        this.chatListBox_MedicalReading.Items.Sort();
        //    }
        //}

       

        public void RemoveMedicalReading(string gid)
        {
            this.chatListBox_MedicalReading.RemoveSubItemsById(gid);
            this.chatListBox_MedicalReading.Invalidate();
        }
        public void AddMedicalReading(MedicalReading md,bool isclient)
        {
            ChatListSubItem[] items = this.chatListBox_MedicalReading.GetSubItemsById(md.MedicalReadingID);//阅片编号
            if (items != null && items.Length > 0)
            {
                return;
            }
            ChatListSubItem subItem=null;
            //string strIsexperts = md.UserIDTo;//
            if (isclient&&md.UserTo!=null)//是客户
            {
                 subItem = new ChatListSubItem(md.MedicalReadingID, "尿液镜检", md.UserToPersonName, md.UserToHospitalName, this.ConvertUserStatus(md.UserStatus), PlatformType.PC, this.imageList1.Images[0]);
               
            }
            else//是专家
            {
                if (md.UserFrom != null)
                {
                     subItem = new ChatListSubItem(md.MedicalReadingID, "尿液镜检", md.UserFrom.PersonName, md.UserFrom.HospitalName, this.ConvertUserStatus(md.UserFrom.UserStatus), PlatformType.PC, this.resourceGetter.GetHeadImage(md.UserFrom));
               
                }
            }
            if(subItem!=null)
          subItem.Tag = md;
            
            switch (md.ReadingStatus)
            {
                case EReadingStatus.UnProcessed:
                    this.chatListBox_MedicalReading.Items[0].SubItems.Add(subItem); break;
                case EReadingStatus.Processing:
                    this.chatListBox_MedicalReading.Items[1].SubItems.Add(subItem); break;
                case EReadingStatus.Rejected:
                    this.chatListBox_MedicalReading.Items[2].SubItems.Add(subItem); break;
                case EReadingStatus.Completed:
                    this.chatListBox_MedicalReading.Items[3].SubItems.Add(subItem); break;
                default: break;

            }
         
            subItem.OwnerListItem.SubItems.Sort();
        }
      

        public void ExpandRoot()
        {
            this.chatListBox_MedicalReading.Items[0].IsOpen = true;
        }

        public void SetAllUserOffline()
        {
            foreach (ChatListItem item in this.chatListBox_MedicalReading.Items)
            {
                foreach (ChatListSubItem sub in item.SubItems)
                {
                    sub.Status = ChatListSubItem.UserStatus.OffLine;
                }                
            }

            this.chatListBox_MedicalReading.Invalidate();
        }

        public void SortAllUser()
        {
            foreach (ChatListItem item in this.chatListBox_MedicalReading.Items)
            {
                if (item.SubItems.Count > 0)
                {
                    item.SubItems.Sort();
                }
            }
        }

      
        public bool ContainsMedicalReading(string gid)
        {
            ChatListSubItem[] items = this.chatListBox_MedicalReading.GetSubItemsById(gid);
            return (items != null && items.Length > 0);
        }

        public void SetTwinkleState(string gid, bool twinkle)
        {
            ChatListSubItem[] items = this.chatListBox_MedicalReading.GetSubItemsById(gid);
            if (items == null || items.Length == 0)
            {
                return;
            }
            items[0].IsTwinkle = twinkle;
        }

        //public void UserStatusChanged(GGUser user)
        //{           
        //    ChatListSubItem[] items = this.chatListBox_MedicalReading.GetSubItemsById(user.ID);
        //    if (items == null || items.Length == 0)
        //    {
        //        return;
        //    }

        //    items[0].HeadImage = this.resourceGetter.GetHeadImage(user);
        //    items[0].Status = this.ConvertUserStatus(user.UserStatus);
        //    ChatListItem item = this.GetCatelogChatListItem(user);
        //    if (item != null)
        //    {
        //        item.SubItems.Sort();
        //    }
        //    this.chatListBox_MedicalReading.Invalidate();           
        //}

        public List<ChatListSubItem> SearchChatListSubItem(string idOrName)
        {
            ChatListSubItem[] items = this.chatListBox_MedicalReading.GetSubItemsByText(idOrName);
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

        //public void UserInfoChanged(GGUser user)
        //{          
        //    ChatListSubItem[] items = this.chatListBox_MedicalReading.GetSubItemsById(user.ID);
        //    if (items != null && items.Length > 0) //有可能部门发生了变化
        //    {
        //        GGUser origin = (GGUser)items[0].Tag;
        //        ChatListItem ownerItem = this.GetCatelogChatListItem(origin);
        //        ownerItem.SubItems.Remove(items[0]);
        //        this.AddUser(user); //有可能是新添加的好友
        //    }            
        //}

       
       

        private void chatListBox_DoubleClickSubItem(object sender, ChatListEventArgs e)
        {
            ChatListSubItem item = e.SelectSubItem;
            MedicalReading md = (MedicalReading)item.Tag;
            item.IsTwinkle = false;
            
           

            if (this.MedicalDoubleClicked != null)
            {
                this.MedicalDoubleClicked(md);
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

            int top = loc.Y + (item.HeadRect.Y - this.chatListBox_MedicalReading.chatVScroll.Value) - this.Location.Y;
            int left = loc.X - 279 - 5;

            if (left < 0)
            {
                left = this.Right + 5;
            }

            MedicalReading md = (MedicalReading)item.Tag;
            Form form = (Form)this.userInformationForm;            
            this.userInformationForm.SetMD(md);           
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

        //private void chatListBox_DragSubItemDrop(object sender, DragListEventArgs e)
        //{
        //    if (this.FriendCatalogMoved != null)
        //    {
        //        GGUser user = (GGUser) e.QSubItem.Tag ;
        //        this.FriendCatalogMoved(user.ID, e.QSubItem.OwnerListItem.Text, e.HSubItem.OwnerListItem.Text);
        //    }
        //}

        //public void ChangeCatelogName(string oldName, string newName)
        //{
        //    if (this.currentUser.UserStatus == UserStatus.OffLine)
        //    {
        //        return;
        //    }

        //    this.catelogManager.Remove(oldName);
        //    ChatListItem existedItem = null;
        //    foreach (ChatListItem item in this.chatListBox_MedicalReading.Items)
        //    {
        //        if (item.Text == newName)
        //        {
        //            existedItem = item;
        //            break;
        //        }
        //    }
        //    if (existedItem != null)
        //    {
        //        foreach (ChatListSubItem sub in this.chatListBox_MedicalReading.SelectItem.SubItems)
        //        {
        //            sub.OwnerListItem = existedItem;
        //            existedItem.SubItems.Add(sub);
        //        }
        //        existedItem.SubItems.Sort();
        //        this.chatListBox_MedicalReading.Items.Remove(this.chatListBox_MedicalReading.SelectItem);
        //        existedItem.IsOpen = true;
        //        if (this.CatalogNameChanged != null)
        //        {
        //            this.CatalogNameChanged(oldName, newName, true);
        //        }
        //        return;
        //    }

        //    this.catelogManager.Add(newName, this.chatListBox_MedicalReading.SelectItem);
        //    this.chatListBox_MedicalReading.SelectItem.Text = newName;
        //    if (this.CatalogNameChanged != null)
        //    {
        //        this.CatalogNameChanged(oldName, newName, false);
        //    }
        //}


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
