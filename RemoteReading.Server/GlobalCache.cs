using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ESBasic.ObjectManagement.Managers;
using ESBasic.Security;
using System.Configuration;
using ESBasic;
using System.Drawing;
using JustLib.Records;
using JustLib;
using RemoteReading.Core;
using System.Linq;
namespace RemoteReading.Server
{   
    public class GlobalCache 
    {
        private IDBPersister dbPersister ;
        private ObjectManager<string, GGUser> userCache = new ObjectManager<string, GGUser>(); // key:用户ID 。 Value：用户信息
        private ObjectManager<string, GGUser> clientCache = new ObjectManager<string, GGUser>();
        private ObjectManager<string, GGUser> expertCache = new ObjectManager<string, GGUser>(); // key:用户ID 。 Value：用户信息
        private ObjectManager<string, GGUser> servicesCache = new ObjectManager<string, GGUser>(); // key:用户ID 。 Value：用户信息
        private ObjectManager<string, GGUser> CheckUserCache = new ObjectManager<string, GGUser>(); // key:用户ID 。 Value：用户信息
        private ObjectManager<string, GGGroup> groupCache = new ObjectManager<string, GGGroup>();  // key:组ID 。 Value：Group信息
        private ObjectManager<int, Hospital> hospitalCache = new ObjectManager<int, Hospital>();  // key:医院序号。 Value：医院信息
        private ObjectManager<string, MedicalReading> medicalReadingCache = new ObjectManager<string, MedicalReading>();  // key:医院序号。 Value：医院信息
        //private ObjectManager<int, SamplesType> sampleTypeCache = new ObjectManager<int, SamplesType>();//标本类型
        //private ObjectManager<int, DyedMethod> dyedmethodCache = new ObjectManager<int, DyedMethod>();//染色方法
        //private ObjectManager<int, Zoom> zoomCache = new ObjectManager<int, Zoom>();//标注放大倍数

        private ObjectManager<string, List<OfflineMessage>> offlineMessageTable = new ObjectManager<string, List<OfflineMessage>>();//key:用户ID 。 
        private ObjectManager<string, List<OfflineFileItem>> offlineFileTable = new ObjectManager<string, List<OfflineFileItem>>();//key:用户ID 。 

        public GlobalCache(IDBPersister persister)
        {
            this.dbPersister = persister;
            foreach(Hospital hs in this.dbPersister.GetHospitals())
            {

                this.hospitalCache.Add(hs.HospitalID,hs);//缓存医院信息
            }
           
            foreach (GGUser user in this.dbPersister.GetAllUser())//获取所有用户信息；
            {
                this.userCache.Add(user.UserID, user);
            }
            DictonarySortUser(userCache.ToDictionary());//按时间顺序排序用户；
            //从所有用户中挑选所有专家;
            foreach (GGUser user in this.GetAllUser())//获取所有用户信息;
            {
                if(user.UserType==EUserType.Expert)
                    expertCache.Add(user.UserID, user);
                else if (user.UserType == EUserType.Servicer)
                    servicesCache.Add(user.UserID, user);
                else if (user.UserType == EUserType.NormalClient)
                    clientCache.Add(user.UserID, user);
 
            }
            ObjectManager<string, GGUser> uncheckusercache = ServerCacheSave.Singleton.CheckUserCache;//加载本地文件中的保存的未审核列表;
            if (uncheckusercache != null&&uncheckusercache.Count>0)
            {
                Dictionary<string,GGUser> dic= uncheckusercache.ToDictionary();
                SortCheckUser(dic);
                
            }
            ObjectManager<string, List<OfflineMessage>> offlineMessageCache = ServerCacheSave.Singleton.OfflineMessageTable;//加载本地文件中的保存的未审核列表;
            if (offlineMessageCache != null&&offlineMessageCache.Count>0)
            {
                this.offlineMessageTable = offlineMessageCache;
            }
            ObjectManager<string, List<OfflineFileItem>> offlineFileCache = ServerCacheSave.Singleton.OfflineFileTable;//加载本地文件中的保存的未审核列表;
            if (offlineFileCache != null && offlineFileCache.Count>0)
            {
                this.offlineFileTable = offlineFileCache;
            }
            //加载离线阅片缓存
            ObjectManager<string, List<MedicalReading>> offlineMedicalReadingCache = ServerCacheSave.Singleton.ListOfflineMediclaReading;//加载本地文件中的保存的未审核列表;
            if (offlineMedicalReadingCache != null)
            {
                this.listOffLineMedicalReading = offlineMedicalReadingCache;
            }

            foreach (GGGroup group in this.dbPersister.GetAllGroup())//获取所有组信息；
            {
                this.groupCache.Add(group.GroupID, group);
            }
            //获取所有的阅片列表进行缓存；（不包含图片，只是包含其路径）
            foreach (MedicalReading mr in this.dbPersister.GetAllMedicalReading())//获取所有阅片信息;
            {
                this.medicalReadingCache.Add(mr.MedicalReadingID, mr);//进行缓存 
            }
            DictonarySort(medicalReadingCache.ToDictionary());//按时间顺序排序；
        }

        private void SortCheckUser(Dictionary<string, GGUser> dic)
        {
            var dicSort = from objDic in dic orderby objDic.Value.CreateTime descending select objDic;
            CheckUserCache.Clear();
            foreach (KeyValuePair<string, GGUser> kvp in dicSort)
            {
                CheckUserCache.Add(kvp.Key, kvp.Value);//对未审核的用户列表进行排序;
                if(!userCache.Contains(kvp.Key))
                {
                    userCache.Add(kvp.Key,kvp.Value);
                }
            }
        }
        private void DictonarySort(Dictionary<string, MedicalReading> dic)
        {
            var dicSort = from objDic in dic orderby objDic.Value.CreatedTime descending select objDic;
            this.medicalReadingCache.Clear();//清空
            foreach (KeyValuePair<string, MedicalReading> kvp in dicSort)
            {
                medicalReadingCache.Add(kvp.Key, kvp.Value);
            }
           
        }
        private void DictonarySortUser(Dictionary<string, GGUser> dic)
        {
            var dicSort = from objDic in dic orderby objDic.Value.CreateTime descending select objDic;
            this.userCache.Clear();//清空
            foreach (KeyValuePair<string, GGUser> kvp in dicSort)
            {
                userCache.Add(kvp.Key, kvp.Value);
            }

        }
        #region UserTable       

        /// <summary>
        /// 根据ID或Name搜索用户【完全匹配】。
        /// </summary>   
        public List<GGUser> SearchUser(string idOrName)
        {
            List<GGUser> list = new List<GGUser>();
            foreach (GGUser user in this.userCache.GetAllReadonly())
            {
                if (user.ID == idOrName || user.PersonName == idOrName)
                {
                    list.Add(user);
                }
            }
            return list;
        }

        /// <summary>
        /// 插入一个新用户。
        /// </summary>      
        public bool InsertUser(GGUser user)
        {
            bool result = false;string friends=null;
          
            {
                if (user.UserType == EUserType.NormalClient)
                {
                    friends = getExpertUserIDList();//获取专家列表作为好友;

                }
                else
                    friends = "";
               //if (user.UserType == EUserType.Expert)
               //{
               //    friends = getNormalClientsIDList();//获取客户列表作为好友;
               //}
               
               //else if (user.UserType == EUserType.Servicer)
               //{
               //    friends = getExpertServiceIDList();//客服获取客服和专家作为好友;
                  
               //}
              //  friends = "";
               user.Friends = friends;
               if (this.dbPersister.InsertUser(user, friends, user.activitedByUserID))
               {
                 
                   if (user.UserType == EUserType.NormalClient)
                   {
                       //插入一个客户，则更新所有的专家将其加为好友，
                       foreach (GGUser expert in this.expertCache.GetAll())
                       { 
                           this.AddFriendSingleDirect(expert.UserID, user.UserID, user.DefaultFriendCatalog);
                       }
                       this.clientCache.Add(user.UserID, user);
                   }
                   else   if (user.UserType == EUserType.Expert)
                   {
                       //插入一个专家，则更新所有的客户和servicer将其添加为好友
                       //foreach (GGUser u in this.clientCache.GetAll())
                       //{
                       //        this.AddFriendSingleDirect(u.UserID, user.UserID, user.DefaultFriendCatalog);
                       //}
                       //foreach (GGUser u in this.servicesCache.GetAll())
                       //{
                       //    this.AddFriendSingleDirect(u.UserID, user.UserID, user.DefaultFriendCatalog);
                       //}
                       this.expertCache.Add(user.UserID, user);
                   }
                   else if (user.UserType == EUserType.Servicer)
                   {
                       //插入一个客服，则更新所有的客服和专家将其加为好友
                       //foreach (GGUser u in this.expertCache.GetAll())
                       //{
                       //    this.AddFriendSingleDirect(u.UserID, user.UserID, user.DefaultFriendCatalog);
                       //}
                       //foreach (GGUser u in this.servicesCache.GetAll())
                       //{
                       //    this.AddFriendSingleDirect(u.UserID, user.UserID, user.DefaultFriendCatalog);
                       //}
                       this.servicesCache.Add(user.UserID, user);
                   }
                   this.userCache.Add(user.UserID, user);
                   DictonarySortUser(this.userCache.ToDictionary());
                   result = true;
               }
             
            }
            return result;
        }
        private string getExpertUserIDList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("我的好友:");
            foreach (GGUser expert in expertCache.GetAll())
            {
                sb.Append(expert.UserID + ",");
            }
            string expertfriends = sb.ToString();
            return expertfriends;
        }
        private string getNormalClientsIDList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("我的好友:");
            foreach (GGUser normal in this.userCache.GetAll())
            {
                if(normal.UserType==EUserType.NormalClient)
                    sb.Append(normal.UserID + ",");
            }
            string friends = sb.ToString();
            return friends;
        }

        private string getExpertServiceIDList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(getExpertUserIDList());
            //sb.Append(",");
            foreach (GGUser g in this.servicesCache.GetAll())
            {
                sb.Append(g.UserID + ",");
            }
            string expertservicesfriends = sb.ToString();
            return expertservicesfriends;
        }

        public void UpdateUser(GGUser user)
        {
            GGUser old = this.userCache.Get(user.UserID);
            if (old == null)
            {
                return;
            }
            //old.HeadImageIndex = user.HeadImageIndex;
            //old.HeadImageData = user.HeadImageData;
            //old.Name = user.Name;
            //old.Signature = user.Signature;
            //old.Version += 1;
            user.Friends = old.Friends;       //0922 
            user.Groups = old.Groups;  //0922   
            user.Version = old.Version + 1;//应该只是更新了自己的头像、昵称之类的
            user.Hospi = old.Hospi;
            user.activitedByUserID = old.activitedByUserID;//也更新下这个

            this.dbPersister.UpdateUser(user);
            this.userCache.Remove(old.UserID);//先移除，在添加
            this.userCache.Add(user.UserID, user);
            DictonarySortUser(this.userCache.ToDictionary());
        }       

        /// <summary>
        /// 目标帐号是否已经存在？
        /// </summary>    
        public bool IsUserExist(string userID)
        {
            return this.userCache.Contains(userID);
        }
        public bool IsCheckUserExist(string userID)
        {
            return this.CheckUserCache.Contains(userID);
        }
        public bool IsExitMobilePhoneInCache(string mobilephone)
        {
            foreach (GGUser user in this.CheckUserCache.GetAll())
            {
                if (user.UserContact.MobilePhone == mobilephone)
                {
                    return true;
                }

            }
            return false;
        }
        public bool IsExitMobilePhone(string mobilephone)
        {
            foreach (GGUser user in userCache.GetAll())
            {
                if (user.UserContact.MobilePhone == mobilephone)
                {
                    return true;
                }

            }
            return false;
        }

        public bool IsExitEmailInCheckCache(string email)
        {
          
            foreach (GGUser user in userCache.GetAll())
            {
                if (user.UserContact.Email == email)
                {
            
                    return true;
                }

            }
            return false;
        }
        public bool IsExitEmail(string email,out string userID)
        {
            userID = null;
            foreach (GGUser user in userCache.GetAll())
            {
                if (user.UserContact.Email == email)
                {
                    
                    userID = user.UserID;
                    return true;
                }

            }
            return false;
        }
        /// <summary>
        /// 根据ID获取用户信息。
        /// </summary>        
        public GGUser GetUser(string userID)
        {
            GGUser onecache=this.userCache.Get(userID) ;
            if (onecache== null)
            {
                //通过向服务器查询看是否有这个用户
                GGUser one=this.dbPersister.GetUser(userID);
                if (one != null)
                {
                    this.userCache.Add(one.UserID,one);
                    DictonarySortUser(this.userCache.ToDictionary());
                    return one;
                }
            }
            return onecache;
        } 

        public ChangePasswordResult ChangePassword(string userID, string oldPasswordMD5, string newPasswordMD5)
        {
            GGUser user = this.userCache.Get(userID);
            if (user == null)
            {
                return ChangePasswordResult.UserNotExist;
            }

            if (user.PasswordMD5 != oldPasswordMD5)
            {
                return ChangePasswordResult.OldPasswordWrong;
            }
            
            user.PasswordMD5 = newPasswordMD5;

            this.dbPersister.UpdatePasswd(userID, newPasswordMD5);
            return ChangePasswordResult.Succeed;
        }
      
        /// <summary>
        /// 获取某个用户的好友列表。
        /// </summary>      
        public List<string> GetFriends(string userID)
        {
            GGUser user = this.userCache.Get(userID);
            if (user == null)
            {
                return new List<string>();
            }

            return user.GetAllFriendList(); 
        }

        /// <summary>
        /// 添加好友，建立双向关系
        /// </summary>  
        public void AddFriend(string ownerID, string friendID, string catalogName)
        {
            GGUser user1 = this.userCache.Get(ownerID);
            GGUser user2 = this.userCache.Get(friendID);
            if (user1 == null || user2 == null)
            {
                return;
            }

            user1.AddFriend(friendID, catalogName);
            user2.AddFriend(ownerID ,user2.DefaultFriendCatalog);
            this.dbPersister.UpdateUserFriends(user1);
            this.dbPersister.UpdateUserFriends(user2);
        }
        /// <summary>
        /// 添加好友，建立单向关系
        /// </summary>  
        public void AddFriendSingleDirect(string ownerID, string friendID, string catalogName)
        {
            GGUser user1 = this.userCache.Get(ownerID);
         //   GGUser user2 = this.userCache.Get(friendID);
            if (user1 == null )
            {
                return;
            }

            user1.AddFriend(friendID, catalogName);
         //   user2.AddFriend(ownerID, user2.DefaultFriendCatalog);
            this.dbPersister.UpdateUserFriends(user1);
         //   this.dbPersister.UpdateUserFriends(user2);
        }
        /// <summary>
        /// 删除好友，并删除双向关系
        /// </summary>  
        public void RemoveFriend(string ownerID, string friendID)
        {
            GGUser user1 = this.userCache.Get(ownerID);
            if (user1 != null)
            {
                user1.RemoveFriend(friendID);
                
                this.dbPersister.UpdateUserFriends(user1);
            }

            GGUser user2 = this.userCache.Get(friendID);
            if (user2 != null)
            {
                user2.RemoveFriend(ownerID);
                this.dbPersister.UpdateUserFriends(user2);
            }
        }
        /// <summary>
        /// 单边删除好友
        /// </summary>
        /// <param name="ownerID"></param>
        /// <param name="friendID"></param>
        public void RemoveFriendSingleDirector(string ownerID, string friendID)
        {
            GGUser user1 = this.userCache.Get(ownerID);
            if (user1 != null)
            {
                user1.RemoveFriend(friendID);

                this.dbPersister.UpdateUserFriends(user1);
            }
        }
        public void ChangeFriendCatalogName(string ownerID, string oldName, string newName)
        {
            GGUser user = this.userCache.Get(ownerID);
            if (user == null)
            {
                return ;
            }

            user.ChangeFriendCatalogName(oldName, newName);
            this.dbPersister.UpdateUserFriends(user);
        }

        public void AddFriendCatalog(string ownerID, string catalogName)
        {
            GGUser user = this.userCache.Get(ownerID);
            if (user == null)
            {
                return;
            }

            user.AddFriendCatalog(catalogName);
            this.dbPersister.UpdateUserFriends(user);
        }

        public void RemoveFriendCatalog(string ownerID, string catalogName)
        {
            GGUser user = this.userCache.Get(ownerID);
            if (user == null)
            {
                return;
            }
            user.RemvoeFriendCatalog(catalogName);
            this.dbPersister.UpdateUserFriends(user);
        }

        public void MoveFriend(string ownerID, string friendID, string oldCatalog, string newCatalog)
        {
            GGUser user = this.userCache.Get(ownerID);
            if (user == null)
            {
                return;
            }
            user.MoveFriend(friendID, oldCatalog ,newCatalog);
            this.dbPersister.UpdateUserFriends(user);
        }
        #endregion

        #region GroupTable       

        /// <summary>
        /// 获取某用户所在的所有组列表。
        /// 建议：可将某个用户所在的组ID列表挂接在用户资料的某个字段上，以避免遍历计算。
        /// </summary>       
        public List<GGGroup> GetMyGroups(string userID)
        {
            List<GGGroup> groups = new List<GGGroup>();
            GGUser user = this.userCache.Get(userID);
            if (user == null)
            {
                return groups;
            }

            foreach (string groupID in user.GroupList)
            {
                GGGroup g = this.groupCache.Get(groupID);
                if (g != null)
                {
                    groups.Add(g);
                }
            }           
            return groups;
        }

        public Dictionary<string, int> GetMyGroupVersions(string userID)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            GGUser user = this.userCache.Get(userID);
            if (user == null)
            {
                return dic;
            }
           
            foreach (string groupID in user.GroupList)
            {
                GGGroup g = this.groupCache.Get(groupID);
                if (g != null)
                {
                    dic.Add(groupID,g.Version);
                }
            }
            return dic;
        }

        /// <summary>
        /// 获取某个组
        /// </summary>       
        public GGGroup GetGroup(string groupID)
        {
            return this.groupCache.Get(groupID);     
        }

        /// <summary>
        /// 创建组
        /// </summary>       
        public CreateGroupResult CreateGroup(string creatorID, string groupID, string groupName, string announce)
        {
            if (this.groupCache.Contains(groupID))
            {
                return CreateGroupResult.GroupExisted;
            }

            GGGroup group = new GGGroup(groupID, groupName, creatorID, announce, creatorID);            
            this.groupCache.Add(groupID, group);
            this.dbPersister.InsertGroup(group);

            GGUser user = this.userCache.Get(creatorID);          
            user.JoinGroup(groupID);
            this.dbPersister.ChangeUserGroups(user.UserID, user.Groups);
            return CreateGroupResult.Succeed;
        }

        /// <summary>
        /// 退出组
        /// </summary>       
        public void QuitGroup(string userID, string groupID)
        {
            GGGroup group = this.groupCache.Get(groupID);
            if (group != null)
            {
                group.RemoveMember(userID);                
            }
            if (this.dbPersister.UpdateGroup(group))
            {
                GGUser user = this.userCache.Get(userID);
                user.QuitGroup(groupID);
                this.dbPersister.ChangeUserGroups(user.UserID, user.Groups);
            }
        }

        public void DeleteGroup(string groupID)
        {
            GGGroup group = this.groupCache.Get(groupID);
            if (group == null)
            {
                return;
            }
            foreach (string userID in group.MemberList)
            {
                GGUser user = this.userCache.Get(userID);
                if (user != null)
                {
                    user.QuitGroup(groupID);
                    this.dbPersister.ChangeUserGroups(user.UserID, user.Groups);
                }
            }
            this.dbPersister.DeleteGroup(groupID);
        }

        /// <summary>
        /// 加入某个组。
        /// </summary>        
        public JoinGroupResult JoinGroup(string userID, string groupID)
        {
            GGGroup group = this.groupCache.Get(groupID);
            if (group == null)
            {
                return JoinGroupResult.GroupNotExist;
            }

            GGUser user = this.userCache.Get(userID);
            if (!user.GroupList.Contains(groupID))
            {
                user.JoinGroup(groupID);
                this.dbPersister.ChangeUserGroups(user.UserID, user.Groups);
            }            

            if (!group.MemberList.Contains(userID))
            {
                group.AddMember(userID);
                this.dbPersister.UpdateGroup(group);      
            }

            return JoinGroupResult.Succeed;
        }

        /// <summary>
        /// 获取某个用户的所有联系人（组友，好友）。
        /// 建议：由于该方法经常被调用，可将组友关系缓存在内存中，而非每次都遍历计算一遍。
        /// </summary>        
        public List<string> GetAllContacts(string userID)
        {
            List<string> contacts = new List<string>();
            GGUser user = this.userCache.Get(userID);
            if (user == null)
            {
                return contacts;
            }

            contacts = user.GetAllFriendList();
            //这里解除群友的获取;
            //foreach (string groupID in user.GroupList)
            //{
            //    GGGroup g = this.groupCache.Get(groupID);
            //    if (g != null)
            //    {
            //        foreach (string memberID in g.MemberList)
            //        {
            //            if (memberID != userID && !contacts.Contains(memberID))
            //            {
            //                contacts.Add(memberID);
            //            }
            //        }
            //    }
            //}

            return contacts;  
        }
        #endregion

        #region OfflineMessage
         /// <summary>
        /// 存储离线消息。
        /// </summary>       
        /// <param name="msg">要存储的离线消息</param>
        public void StoreOfflineMessage(OfflineMessage msg)
        {
            if (!this.offlineMessageTable.Contains(msg.DestUserID))
            {
                this.offlineMessageTable.Add(msg.DestUserID, new List<OfflineMessage>());
            }

            this.offlineMessageTable.Get(msg.DestUserID).Add(msg);
            ServerCacheSave.Singleton.OfflineMessageTable = this.offlineMessageTable;
        }

        /// <summary>
        /// 提取目标用户的所有离线消息。
        /// </summary>       
        /// <param name="destUserID">接收离线消息用户的ID</param>
        /// <returns>属于目标用户的离线消息列表，按时间升序排列</returns>
        public List<OfflineMessage> PickupOfflineMessage(string destUserID)
        {
            if (!this.offlineMessageTable.Contains(destUserID))
            {
                return new List<OfflineMessage>();
            }

            List<OfflineMessage> list = this.offlineMessageTable.Get(destUserID);
            this.offlineMessageTable.Remove(destUserID);
            return list;
        }
        #endregion
        //user,MDID;离线阅片消息。
        private ObjectManager<string, List<MedicalReading>> listOffLineMedicalReading = new ObjectManager<string, List<MedicalReading>>();
        private int FristItemsCount=10;
        public void SoreOffMedicalReading(string userid, MedicalReading mdid)
        {
            if (!listOffLineMedicalReading.Contains(userid))
            {
                listOffLineMedicalReading.Add(userid, new List<MedicalReading>());

            }

            listOffLineMedicalReading.Get(userid).Add(mdid);
            ServerCacheSave.Singleton.ListOfflineMediclaReading = this.listOffLineMedicalReading;//更新需要保存的缓存;

        }

        public List<MedicalReading> PickupOfflineMDMessage(string destUserID)
        {
            if (!this.listOffLineMedicalReading.Contains(destUserID))
            {
                return new List<MedicalReading>();
            }

            List<MedicalReading> list = this.listOffLineMedicalReading.Get(destUserID);
            this.listOffLineMedicalReading.Remove(destUserID);
            return list;
        }
        #region OfflineFile
        /// <summary>
        /// 将一个离线文件条目保存到数据库中。
        /// </summary>     
        public void StoreOfflineFileItem(OfflineFileItem item)
        {
            if (!this.offlineFileTable.Contains(item.AccepterID))
            {
                this.offlineFileTable.Add(item.AccepterID, new List<OfflineFileItem>());
            }

            this.offlineFileTable.Get(item.AccepterID).Add(item);
            ServerCacheSave.Singleton.OfflineFileTable = this.offlineFileTable;//更新需要保存的缓存;
        }

        /// <summary>
        /// 从数据库中提取接收者为指定用户的所有离线文件条目。
        /// </summary>       
        public List<OfflineFileItem> PickupOfflineFileItem(string accepterID)
        {
            if (!this.offlineFileTable.Contains(accepterID))
            {
                return new List<OfflineFileItem>();
            }

            List<OfflineFileItem> list = this.offlineFileTable.Get(accepterID);
            this.offlineFileTable.Remove(accepterID);
            return list;
        }

        #endregion

        #region ChatRecord,插入数据库
        public void StoreChatRecord(string senderID, string accepterID, byte[] content)
        {
            this.dbPersister.InsertChatMessageRecord(new ChatMessageRecord(senderID, accepterID, content, false));
        }


        public ChatRecordPage GetChatRecordPage(ChatRecordTimeScope timeScope, string senderID, string accepterID, int pageSize, int pageIndex)
        {
            return this.dbPersister.GetChatRecordPage(timeScope, senderID, accepterID, pageSize, pageIndex);
        }

        public void StoreGroupChatRecord(string groupID, string senderID, byte[] content)
        {
            this.dbPersister.InsertChatMessageRecord(new ChatMessageRecord(senderID, groupID, content, true));
        }

        public ChatRecordPage GetGroupChatRecordPage(ChatRecordTimeScope timeScope, string groupID, int pageSize, int pageIndex)
        {
            return this.dbPersister.GetGroupChatRecordPage(timeScope, groupID, pageSize, pageIndex);
        }
        #endregion               

        #region Hosptials
           public List<Hospital> GetAllHospitals()
        {
            List<Hospital> hosps = new List<Hospital>();
            
            if (this.hospitalCache == null)
            {
                return hosps;
            }

            foreach (int hospitalID in this.hospitalCache.GetKeyList())
            {
                Hospital h = this.hospitalCache.Get(hospitalID);
                if (h != null)
                {
                    hosps.Add(h);
                }
            }
            return hosps;
        
        }
           public List<MedicalReading> GetMedicalReadingsByUserID(string userid,bool isfrom)
           {
               if(this.medicalReadingCache==null)
                    return null;
               List<MedicalReading> listMDsFrom = new List<MedicalReading>();
               int[] counts = new int[4]{0,0,0,0};//4中状态的阅片，每种不超过20.首次获取阅片数量;
               try
               {
                   foreach (MedicalReading kvp in this.medicalReadingCache.GetAll())
                   {
                       if (isfrom)
                       {
                           if (kvp.UserIDFrom == userid)
                           {
                               switch (kvp.ReadingStatus)
                               {
                                   case EReadingStatus.UnProcessed: if (counts[0] < FristItemsCount) { listMDsFrom.Add(kvp); counts[0]++; } else continue; break;
                                   case EReadingStatus.Processing: if (counts[1] < FristItemsCount) { listMDsFrom.Add(kvp); counts[1]++; } else continue; break;
                                   case EReadingStatus.Rejected: if (counts[2] < FristItemsCount) { listMDsFrom.Add(kvp); counts[2]++; } else continue; break;
                                   case EReadingStatus.Completed: if (counts[3] < FristItemsCount) { listMDsFrom.Add(kvp); counts[3]++; } else continue; break;
                               }
                           }
                       }
                       else
                       {
                           if (kvp.UserIDTo == userid)
                           {
                               switch (kvp.ReadingStatus)
                               {
                                   case EReadingStatus.UnProcessed: if (counts[0] < FristItemsCount) { listMDsFrom.Add(kvp); counts[0]++; } else continue; break;
                                   case EReadingStatus.Processing: if (counts[1] < FristItemsCount) { listMDsFrom.Add(kvp); counts[1]++; } else continue; break;
                                   case EReadingStatus.Rejected: if (counts[2] < FristItemsCount) { listMDsFrom.Add(kvp); counts[2]++; } else continue; break;
                                   case EReadingStatus.Completed: if (counts[3] < FristItemsCount) { listMDsFrom.Add(kvp); counts[3]++; } else continue; break;
                               }
                           }
                       }
                   }
               }
               catch (System.Exception ex)
               {
                   listMDsFrom = null;
               }
               return listMDsFrom;

           }
           public List<GGUser> GetAllExperts()
           {
               return this.expertCache.GetAll();//获取所有专家列表
           }

           public MedicalReading GetMedicalReadingByGuid(string id)
           {
               return this.medicalReadingCache.Get(id);
           }

           public List<byte[]> GetSmallBitmapsByMedicalReadingGuid(string id)
           {
               List<byte[]> listBitmap = new List<byte[]>();
               MedicalReading tarMD;
               try
               {
                   if (this.medicalReadingCache.Contains(id))
                   {
                       tarMD = this.medicalReadingCache.Get(id);
                   }
                   else
                   {
                       tarMD = this.dbPersister.GetMedicalReadingByGuid(id);
                       if (tarMD != null)
                       {
                           this.medicalReadingCache.Add(tarMD.MedicalReadingID, tarMD);
                           DictonarySort(medicalReadingCache.ToDictionary());//按时间顺序排序；
                       }
                   }
                   if (tarMD != null && tarMD.ListPics != null)
                   {

                       ReadingPicture rp = tarMD.ListPics[0];//第0个图片对象;
                       {
                           string picpath = rp.PicturePath;//获取图片路径
                           listBitmap = this.dbPersister.GetSmallBitmapsbypath(rp, tarMD.ListPics.Count);

                       }
                   }
               }

               catch (Exception ex)
               {
                   listBitmap = null;
               }
               return listBitmap;
           }
           public List<byte[]> GetBitmapsByMedicalReadingGuid(string  id)
           {
               List<byte[]> listBitmap = null;
               MedicalReading tarMD;
               try
               {
                   if (this.medicalReadingCache.Contains(id))
                   {
                        tarMD = this.medicalReadingCache.Get(id);
                   }   
                   else
                   {
                       tarMD = this.dbPersister.GetMedicalReadingByGuid(id);
                       if (tarMD != null)
                       {
                           this.medicalReadingCache.Add(tarMD.MedicalReadingID, tarMD);
                           DictonarySort(medicalReadingCache.ToDictionary());//按时间顺序排序；
                       }
                   }
                   if (tarMD == null)
                   {
                       return null;
                   }
                   if (tarMD.ListPics!=null)
                   {

                      ReadingPicture rp = tarMD.ListPics[0];//第0个图片对象;
                       {
                           string picpath = rp.PicturePath;//获取图片路径
                           listBitmap = this.dbPersister.GetBitmapsbypath(rp,tarMD.ListPics.Count);
                           
                       }
                   }
                   
               }

               catch (Exception ex)
               {
                   listBitmap = null;
               }
               return listBitmap;
           }

           public List<MedicalReading> GetAllMedicalReadings(string usertoid,bool isfrom)
           {
               if (this.medicalReadingCache == null)
                   return null;
               List<MedicalReading> listMDsTo = new List<MedicalReading>();
               try
               {
                   foreach (MedicalReading kvp in this.medicalReadingCache.GetAll())
                   {
                       if (isfrom)
                       {
                           if (kvp.UserIDFrom == usertoid)
                           {
                               listMDsTo.Add(kvp);
                           }
                       }
                       else
                       {
                           if (kvp.UserIDTo == usertoid)
                           {
                               listMDsTo.Add(kvp);
                           }
                       }
                   }
               }
               catch (System.Exception ex)
               {
                   listMDsTo = null;
               }
               return listMDsTo;
           }

           public MedicalReading InsertMedicalReading(MedicalReading mr)
           {
               MedicalReading mrsave=this.dbPersister.InsertMedicalReading(mr);//插入数据库保存
               
               if (mrsave==null)//插入失败
               {
                   return null;
               }
               else//本地缓存
               {
                   mrsave.UserFrom = mrsave.UserTo = null;
                   this.medicalReadingCache.Add(mrsave.MedicalReadingID, mrsave);
                   DictonarySort(medicalReadingCache.ToDictionary());//按时间顺序排序；
               }
               return mrsave;
           }
        //public MedicalReading GetMedicalReading(Guid gid)
        //   {
        //      MedicalReading 
        //   }
        #endregion
        //获取最新的guid列表;
           public  List<string> GetClientOrExpertMDGuids(string sourceUserID, bool isexpert)
           {
               //最新列表
               List<string> list = new List<string>();
             //  List<string> list = this.dbPersister.GetClientOrExpertMDGuids(sourceUserID, isexpert);
               if (this.medicalReadingCache == null || this.medicalReadingCache.Count == 0)
               {
                   list = this.dbPersister.GetClientOrExpertMDGuids(sourceUserID, isexpert);
               }
               else
               {
                 
                   foreach (MedicalReading mr in this.medicalReadingCache.GetAll())
                   {
                       if (isexpert)
                       {
                           if (sourceUserID == mr.UserIDTo)
                           {
                               list.Add(mr.MedicalReadingID);
                           }

                       }
                       else
                       {
                           if (sourceUserID == mr.UserIDFrom)
                               list.Add(mr.MedicalReadingID);
                       }
                   }
               }
               return list;
           }

           public List<MedicalReading> GetSomeMedicalReading(List<string> listguids)
           {
               List<MedicalReading> list = new List<MedicalReading>();
              //最新列表
              // return this.dbPersister.GetSomeMedicalReading(listguids);
               foreach (string guid in listguids)
               {

                   MedicalReading mr = this.medicalReadingCache.Get(guid);
                   if(mr!=null)
                     list.Add(mr);
               }
               return list;
           }
         public bool UpdateMedicalReading(string id, string reason)//拒绝阅片更新
        {
            MedicalReading  mr= this.medicalReadingCache.Get(id);
            if (mr != null)
            {
                string updatetime;
                if (this.dbPersister.UpdateMedicalReading(id, reason, out updatetime))
                {
                    mr.IsRejected = true;
                    mr.ReadingStatus = EReadingStatus.Rejected;
                    mr.RejectedReason = reason;
                    mr.CreatedTime = updatetime;
                    return true;
               }
            }
            return false;

        }

         public bool UpdateMedicalReading(string id)//更新状态，表示阅片可以接收，处于正在处理状态
         {
             string updatetime;
             if (this.medicalReadingCache.Get(id) != null)
             {
                 this.medicalReadingCache.Get(id).ReadingStatus = EReadingStatus.Processing;//更新状态

                 if (this.dbPersister.UpdateMedicalReading(id, out updatetime))//更新数据库
                 {
                     this.medicalReadingCache.Get(id).CreatedTime = updatetime;
                     return true;
                 }
             }
             return false;
         }

         public bool UpdateMedicalReading(string id, List<ReadingPicture> listpics)
         {
          //   string expertmarks;
             List<ReadingPicture> listnew; string updatetime;
             if (this.medicalReadingCache.Get(id) != null)
             {
                 if (this.dbPersister.UpdateMedicalReading(id, listpics, out listnew,out updatetime))//更新数据库
                 {
                     this.medicalReadingCache.Get(id).ReadingStatus = EReadingStatus.Completed;//更新状态,已完成
                     this.medicalReadingCache.Get(id).CreatedTime = updatetime;
                     //更新专家标签列表;
                     for (int i = 0; i < this.medicalReadingCache.Get(id).ListPics.Count;i++ )
                     {
                         ReadingPicture oldrp = this.medicalReadingCache.Get(id).ListPics[i];
                         ReadingPicture newrp = listnew[i];
                         oldrp.ListExpertMarks = newrp.ListExpertMarks;
                         oldrp.ExpertPictureMarks = newrp.ExpertPictureMarks;
                         oldrp.ExpertPictureMarksCount = newrp.ExpertPictureMarksCount;
                         oldrp.ExpertConclusion = newrp.ExpertConclusion;
                         oldrp.ClientNote = newrp.ClientNote;
                     }
                     //this.medicalReadingCache.Get(id).ListPics = listnew;//更新阅片基础信息，这里暂时更新了专家标记；
                     return true;
                 }
             }
             return false;
         }

         public GGUser GetExpert(string sourceUserID)
         {
             return this.expertCache.Get(sourceUserID);
         }



         public bool UpdatePasswd(string userID, string passMD5)
         {
             
             GGUser user = this.userCache.Get(userID);
             if (user == null)
             {
                 return false;
                 
             }
             user.PasswordMD5 = passMD5;
            return  this.dbPersister.UpdatePasswd(userID, passMD5);
           

         }

         public List<GGUser> GetAllUser()
         {
             return this.userCache.GetAll();
         }

         public bool DeleteUser(string deluserid)
         {
             bool bresult = false;
             GGUser user = this.userCache.Get(deluserid);
             
             if (user != null)
             {
                     if (this.dbPersister.DeleteUser(deluserid))//数据库删除成功后
                     {
                         foreach (string friendID in user.GetAllFriendList())
                         {
                             RemoveFriendSingleDirector(friendID,deluserid );//单边删除所有好友关系;
                         }
                         if (user.UserType == EUserType.NormalClient)
                         {
                             clientCache.Remove(deluserid);
                         }
                         if (user.UserType == EUserType.Expert)
                         {
                             expertCache.Remove(deluserid);
                         }
                         if (user.UserType == EUserType.Servicer)
                         {
                             servicesCache.Remove(deluserid);
                         }
                         userCache.Remove(deluserid);
                         bresult = true;
                     }

             }
             return bresult;
         }
        /// <summary>
        /// 插入待审核的用户，供客服审核,注册或者更新用户信息;
        /// </summary>
        /// <param name="user"></param>
         public bool InsertCheckUserList(GGUser user)
         {
             bool isOK = false;
             if (user.CheckType==CheckType.Update)//如果处于待审核状态
             {
                 if (!userCache.Contains(user.UserID))//按道理缓冲中应该有此用户。没有说明有问题。
                 {
                     return false;
                 }
                 //GGUser userc = userCache.Get(user.UserID);//获取缓存中的原来用户
                 //userc.IsChecking = true;//让缓存中的用户只是更新审核状态;
                 //userc.CheckType = CheckType.Update;//更新审核类型;
                 //if (this.dbPersister.UpdateUserContactInfo(userc))//更新数据库只是让用户处于审核状态;其修改后的信息，存于审核缓存中。
                 //{
                    
                     //GGUser userc = userCache.Get(user.UserID);
                     
                 //}
             }
            else if (user.CheckType == CheckType.Register)//如果是注册审核，缓存中本没有此用户
             {
                
               //  if (this.InsertUser(user))//如果插入成功，都只是保留在缓存中。

                     
                     //GGUser checkuser = new GGUser(user.UserID, user.PasswordMD5, user.PersonName, user.Friends, user.Signature
                     //  , user.HeadImageIndex, user.Groups, user.UserType, user.IsActivited, user.UserContact, user.Hospi
                     //  , user.CreateTime, user.ActivitedByUserID);
                     //checkuser.IsChecking = true;
                     //checkuser.CheckType = CheckType.Register;//在缓存中需要是一致类型
                     //if (!CheckUserCache.Contains(user.UserID))//注册与更新不可能同时存在,只在缓冲中添加;
                     //{
                     //    CheckUserCache.Add(user.UserID, user);//加入审核缓存列表;
                     //    isOK = true;
                     //    SortCheckUser(CheckUserCache.ToDictionary());
                     //}

                 
             }
             if (!CheckUserCache.Contains(user.UserID))//注册与更新不可能同时存在
             {
                 CheckUserCache.Add(user.UserID, user);//加入审核缓存列表;
                 isOK = true;
                 SortCheckUser(CheckUserCache.ToDictionary());
             }
          
             return isOK;
         }

         public List<string> GetServicersID()
         {
             List<string> servicesonline = new List<string>();
             foreach (GGUser servicer in servicesCache.GetAll())
             {
                 if (servicer.UserStatus != UserStatus.OffLine)
                 {
                     servicesonline.Add(servicer.UserID);
                 }
             }
             return servicesonline;

         }
        //获取所有已经是审核的人员;
         public List<GGUser> GetAllCheckedUserByPage(string serviceID)
         {
             List<GGUser> checkUserlist = new List<GGUser>();
             int count = 0;
            
             foreach (GGUser g in userCache.GetAll())
             {
                 if (g.ActivitedByUserID == serviceID)
                 {
                     if (count < 10)//首次获取20次已审核列表;
                     {
                         checkUserlist.Add(g);
                         count++;
                     }
                 }
             }
             return checkUserlist;
         }
         //获取所有已经是审核的人员;
         public List<GGUser> GetAllCheckedUser(string serviceID)
         {
             List<GGUser> checkUserlist = new List<GGUser>();
             foreach(GGUser g in userCache.GetAll())
                 if (g.ActivitedByUserID == serviceID)
                 {
                         checkUserlist.Add(g);   
                 }
             
             return checkUserlist;
         }
         public List<GGUser> GetAllUnCheckedUser()
         {
              List<GGUser> uncheckedlist = new List<GGUser>();
             int count = 0;
             foreach (GGUser g in CheckUserCache.GetAll())
             {
                 if (count < 10)//首次获取20次未审核列表;
                 {
                     uncheckedlist.Add(g);
                     count++;
                 }
                 else
                     break;
             }
            return uncheckedlist;//缓存中的未审核人员;
         }
        /// <summary>
        /// 客服提交审核结果。
        /// </summary>
        /// <param name="isPassed"></param>
        /// <param name="checkUserID"></param>
        /// <param name="servicerID"></param>
        /// <param name="isexpert"></param>
        /// <returns></returns>
         public bool CheckUserResult(bool isPassed, GGUser checkUser)
         {
             bool result = true;
             GGUser localUser = userCache.Get(checkUser.UserID);//获取本地缓存用户;
             try
             {
                 MailSend ms = new MailSend(this);
                 string email = checkUser.UserContact.Email;
                 //更新本地缓存与数据库;
                 if (isPassed)//如果通过了
                 {
                     if (checkUser.CheckType == CheckType.Update)
                     {
                         if (this.dbPersister.UpdateUserContactInfo(checkUser))//更新数据库，如果更新成功，更新本地缓存;
                         {
                             localUser.UserContact.PersonName = checkUser.PersonName;
                             localUser.UserContact.MobilePhone = checkUser.UserContact.MobilePhone;
                             localUser.UserContact.Email = checkUser.UserContact.Email;
                             localUser.PasswordMD5 = checkUser.PasswordMD5;
                             localUser.UserType = checkUser.UserType;
                             localUser.UserContact.HospitalID = checkUser.UserContact.HospitalID;//所属医院;这个可能有点问题;
                             localUser.Hospi = this.GetHostpitalName(localUser.UserContact.HospitalID);//更新医院属性;
                             localUser.UserContact.ProfessionTitle = checkUser.UserContact.ProfessionTitle;
                             localUser.activitedByUserID = checkUser.activitedByUserID;//激活更新
                             localUser.CreateTime = checkUser.CreateTime;
                             localUser.CheckType = checkUser.CheckType;
                             localUser.IsChecking = false;//审核完成;
                             localUser.Version += 1;//自增1;更新
                             //发送邮件
                             result = ms.MailSendOneCheckPass(checkUser,email, true);
                         }
                         else
                             result = false;//没有更新成功;
                     }
                     else if (checkUser.CheckType == CheckType.Register)//如果是注册审核，则插入数据库和缓存;
                     {
                         userCache.Remove(checkUser.UserID);//先移除之前的
                         this.InsertUser(checkUser);//插入新的用户至数据库中
                         //发送邮件
                         result = ms.MailSendOneCheckPass(checkUser,email, true);
                     }

                 }
                 else//更新资料不通过，则发送一封邮件给对方：
                 {
                     userCache.Remove(checkUser.UserID);//审核不通过，也要删除user缓存中的数据;
                     result = ms.MailSendOneCheckPass(checkUser,email, false);
                 }
                 CheckUserCache.Remove(checkUser.UserID);//清除缓存;
             }
             catch (Exception ex)
             {
                 result = false;
             }
             return result;
         }

         public Hospital GetHostpitalName(int hospitalID)
         {
             if (hospitalCache == null || hospitalCache.Count == 0)
             {
                 return null;
             }
             Hospital hs = this.hospitalCache.Get(hospitalID);
            
                 return hs;
            
         }

         public  byte[] GetSingleBitmapsByReadingPictureGuid(string rpgid)
         {
             byte[] bitmap=null ;
             ReadingPicture tarMD;
             try
             {
                 tarMD=this.GetReadingPictureByGid(rpgid);
                 if (tarMD==null)
                 {
                     return null;
                 }
                 //获取图片
                 if ( tarMD.Imagebyte== null)
                 {
                     bitmap = this.dbPersister.GetSingleBitmapsbypath(tarMD);
                     //更新本地缓存
                    // tarMD.Imagebyte = bitmap;
                 }
                 else//如果已经有了，返回缓存内容
                 {
                     bitmap = tarMD.Imagebyte;
                 }
             }

             catch (Exception ex)
             {
                 bitmap = null;
             }
             return bitmap;
         }

         private ReadingPicture GetReadingPictureByGid(string rpgid)
         {
             foreach (MedicalReading mr in this.medicalReadingCache.GetAll())
             {
                 if (mr.ListPics != null)
                 {
                     foreach (ReadingPicture rp in mr.ListPics)
                     {
                         if (rp.ReadingPictureID == rpgid)
                         {
                             return rp;
                         }
                     }
                 }

             }
             return null;
         }
      

         public bool IsCheckedCacheExist(string userID)
         {
             return this.CheckUserCache.Contains(userID);
         }

         public  bool InsertAdvice(string userid, string advice)
         {
             return this.dbPersister.InsertAdvice(userid, advice);
         }
         public ObjectManager<string, GGUser> GetCheckUserCache()
         {
             return this.CheckUserCache;
         }
        //下拉获取最新阅片
         public List<MedicalReading> GetNewMedicalReadings(string userid, bool isexpert, int readingstatus, DateTime lastupdatetime)
         {
             List<MedicalReading> list= this.dbPersister.GetLastUpdateMedicalReadings( userid,  isexpert,  readingstatus,  lastupdatetime);
             if (list != null && list.Count > 0)//还是加入至缓存中，最新的阅片
             {
                 foreach (MedicalReading mr in list)//获取所有阅片信息;
                 {
                     if (!this.medicalReadingCache.Contains(mr.MedicalReadingID))
                         this.medicalReadingCache.Add(mr.MedicalReadingID, mr);//进行缓存
                 }
                 DictonarySort(medicalReadingCache.ToDictionary());//按时间顺序排序；
             }
             return list;
         }

         //上拉获取历史阅片
         public List<MedicalReading> GetMoreMedicalReadings(string userid, bool isexpert, int readingstatus, DateTime foremosttime,int currPage,int pageSize)
         {
             List<MedicalReading> list=this.dbPersister.GetMoreMedicalReadings(userid, isexpert, readingstatus, foremosttime,currPage,pageSize);
             if (list != null && list.Count > 0)//还是加入至缓存中，历史阅片
             {
                 foreach (MedicalReading mr in list)//获取所有阅片信息;
                 {
                     if (!this.medicalReadingCache.Contains(mr.MedicalReadingID))
                         this.medicalReadingCache.Add(mr.MedicalReadingID, mr);//进行缓存
                 }
                 DictonarySort(medicalReadingCache.ToDictionary());//按时间顺序排序；
             }
             return list;
         }

         public List<GGUser> GetNewCheckUserInfo(string userid, bool isChecked, DateTime lastupdatetime)
         {
             List<GGUser> listNew = new List<GGUser>();
             if (isChecked)//已经审核
             {
                 listNew = GetAllCheckedUser(userid);//获取所有该客服审核过的列表；
                 
             }
             else//未审核
             {
                 listNew =this.CheckUserCache.GetAll();//获取所有未审核的列表;
             }
             listNew = listNew.Where(c => DateTime.Parse(c.CreateTime) > lastupdatetime).ToList();
             return listNew;
         }

         public List<GGUser> GetMoreCheckUserInfo(string userid, bool isChecked, DateTime foremosttime, int currPage, int pageSize)
         {
             List<GGUser> listNew = new List<GGUser>();
             if (isChecked)//已经审核
             {
                 listNew = GetAllCheckedUser(userid);//获取所有该客服审核过的列表；

             }
             else//未审核
             {
                 listNew = this.CheckUserCache.GetAll();//获取所有未审核的列表;
             }
             listNew = listNew.Where(c => DateTime.Parse(c.CreateTime) < foremosttime).Skip((currPage-1) * pageSize).Take(pageSize).ToList(); ;
             return listNew;
         }
    }    
}
