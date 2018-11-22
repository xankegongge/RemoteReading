using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ESBasic.Helpers;
using ESBasic;
using JustLib;
using JustLib.Controls;
namespace RemoteReading.Core
{
	[Serializable]
	public partial class GGUser : IUser
	{
	    
       
            #region Force Static Check
            public const string TableName = "GGUser";
            public const string _UserID = "UserID";
            public const string _UserType = "UserType";
            public const string _PasswordMD5 = "PasswordMD5";
            public const string _Name = "Name";
            public const string _Friends = "Friends";
            public const string _Signature = "Signature";
            public const string _HeadImageIndex = "HeadImageIndex";
            public const string _HeadImageData = "HeadImageData";
            public const string _Groups = "Groups";
            public const string _CreateTime = "CreateTime";
            public const string _LastLoginTime = "LastLoginTime";
            public const string _LastPasswordChangeTime = "LastPasswordChangeTime";
            public const string _OnlineState = "OnlineState";
            public const string _ClientIP = "ClientIP";
            public const string _LoginRegionCode = "LoginRegionCode";
            public const string _ClientSystem = "ClientSystem";
            public const string _DefaultFriendCatalog = "DefaultFriendCatalog";
            public const string _Version = "Version";
            public const string _User_ContactOID = "User_ContactOID";
            public const string _IsActivited = "IsActivited";
            #endregion

            #region 构造方法
            public GGUser() { }
            public string activitedByUserID = null;

            public string ActivitedByUserID
            {
                get { return activitedByUserID; }
                 
            }

            

            public GGUser(string id, string pwd, string _name, string _friends, string _signature, int headIndex, string _groups
                , EUserType usertype, bool isacted, UserContact us, Hospital hos)
            {
                this.UserID = id;
                this.passwordMD5 = pwd;
                this.Name = _name;
                this.friends = _friends;
                this.Signature = _signature;
                this.HeadImageIndex = headIndex;
                this.groups = _groups;
                this.m_UserType = usertype;
                this.m_IsActivited = isacted;
                this.userContact = us;
                this.hospi = hos;
                
            }
        //从服务器获取数据
            public GGUser(string id, string pwd, string _name, string _friends, string _signature, int headIndex, string _groups
                    , EUserType usertype, bool isacted, UserContact us, Hospital hos,string time, string activitedByUserID)
            {
                this.UserID = id;
                this.passwordMD5 = pwd;
                this.Name = _name;
                this.friends = _friends;
                this.Signature = _signature;
                this.HeadImageIndex = headIndex;
                this.groups = _groups;
                this.m_UserType = usertype;
                this.m_IsActivited = isacted;
                this.userContact = us;
                this.hospi = hos;
                this.CreateTime = time;
                this.activitedByUserID = activitedByUserID;
            }
            #endregion

            #region 数据库字段
            #region PasswordMD5
            private string m_CreateTime;

            public string CreateTime
            {
                get { return m_CreateTime; }
                set { m_CreateTime = value; }
            }

            private string passwordMD5 = "";
                    /// <summary>
                    /// 登录密码(MD5加密)。
                    /// </summary>
                    public string PasswordMD5
                    {
                        get { return passwordMD5; }
                        set { passwordMD5 = value; }
                    }
                    #endregion

                    #region UserID
                    private string userID = "";
                    /// <summary>
                    /// 用户登录帐号。
                    /// </summary>
                    public string UserID
                    {
                        get { return userID; }
                        set { userID = value; }
                    }
                    #endregion

                    #region UserType
                    private EUserType m_UserType = EUserType.NormalClient;//默认是普通客户
                    public EUserType UserType
                    {
                        get
                        {
                            return this.m_UserType;
                        }
                        set
                        {
                            this.m_UserType = value;
                        }
                    }
                    #endregion

                    #region IsActivited
                    private bool m_IsActivited = false;//默认是没有激活
                    public bool IsActivited
                    {
                        get
                        {
                            return this.m_IsActivited;
                        }
                        set
                        {
                            this.m_IsActivited = value;
                        }
                    }
                    #endregion

                    #region Name
                    private string name = "";
                    /// <summary>
                    /// 昵称
                    /// </summary>
                    public string Name
                    {
                        get { return name; }
                        set { name = value; }
                    }
                    #endregion

                    #region Friends
                    private string friends = "";
                    /// <summary>
                    /// 好友。如 我的好友：10000,10001,1234;家人:1200,1201 。
                    /// </summary>
                    public string Friends
                    {
                        get { return friends; }
                        set
                        {
                            friends = value;
                            this.friendDicationary = null;
                            this.allFriendList = null;
                        }
                    }

                    
                    
                   
                    #endregion

                    #region Groups
                    private string groups = "";
                    /// <summary>
                    /// 该用户所属的组。组ID用英文逗号隔开
                    /// </summary>
                    public string Groups
                    {
                        get { return groups; }
                        set
                        {
                            groups = value;
                            this.groupList = null;
                        }
                    }

                    #region 非DB字段
                    private List<string> groupList = null;
                    /// <summary>
                    /// 所属组ID的数组。非DB字段。
                    /// </summary>
                    public List<string> GroupList
                    {
                        get
                        {
                            if (this.groupList == null)
                            {
                                this.groupList = new List<string>(this.groups.Split(','));
                                if (this.groupList.Count == 1)
                                {
                                    this.groupList.Remove("");
                                }
                            }
                            return groupList;
                        }
                    }
                    #endregion
                    public void JoinGroup(string groupID)
                    {
                        if (this.GroupList.Contains(groupID))
                        {
                            return;
                        }
                        this.GroupList.Add(groupID);
                        this.groups = ESBasic.Helpers.StringHelper.ContactString(this.GroupList, ",");
                    }

                    public void QuitGroup(string groupID)
                    {
                        this.GroupList.Remove(groupID);
                        this.groups = ESBasic.Helpers.StringHelper.ContactString(this.GroupList, ",");
                    }
                    #endregion

                    #region Signature
                    private string signature = "";
                    /// <summary>
                    /// 签名
                    /// </summary>
                    public string Signature
                    {
                        get { return signature; }
                        set { signature = value; }
                    }
                    #endregion

                    #region HeadImageIndex
                    private int headImageIndex = 0;
                    /// <summary>
                    /// 头像图片的索引。如果为-1，表示自定义头像。
                    /// </summary>
                    public int HeadImageIndex
                    {
                        get { return headImageIndex; }
                        set
                        {
                            headImageIndex = value;
                            this.headIcon = null;
                        }
                    }
                    #endregion

                    #region HeadImageData
                    private byte[] headImageData = null;
                    public byte[] HeadImageData
                    {
                        get { return headImageData; }
                        set
                        {
                            headImageData = value;
                            this.headImage = null;
                            this.headImageGrey = null;
                            this.headIcon = null;
                        }
                    }
                    #endregion

                    #region LastLoginTime
                    private string m_LastLoginTime ;
                    public string LastLoginTime
                    {
                        get
                        {
                            return this.m_LastLoginTime;
                        }
                        set
                        {
                            this.m_LastLoginTime = value;
                        }
                    }
                    #endregion

                    #region LastPasswordChangeTime
                    private string m_LastPasswordChangeTime;
                    public string LastPasswordChangeTime
                    {
                        get
                        {
                            return this.m_LastPasswordChangeTime;
                        }
                        set
                        {
                            this.m_LastPasswordChangeTime = value;
                        }
                    }
                    #endregion
                    //传入数据库中的状态
                    #region OnlineState
                    private UserStatus m_OnlineState = UserStatus.OffLine;
                    public UserStatus OnlineState
                    {
                        get
                        {
                            return this.m_OnlineState;
                        }
                        set
                        {
                            this.m_OnlineState = value;
                        }
                    }
                    #endregion

                    #region ClientIP
                    private System.String m_ClientIP = "";
                    public System.String ClientIP
                    {
                        get
                        {
                            return this.m_ClientIP;
                        }
                        set
                        {
                            this.m_ClientIP = value;
                        }
                    }
                    #endregion

                    #region LoginRegionCode
                    private System.String m_LoginRegionCode = "";
                    public System.String LoginRegionCode
                    {
                        get
                        {
                            return this.m_LoginRegionCode;
                        }
                        set
                        {
                            this.m_LoginRegionCode = value;
                        }
                    }
                    #endregion

                    #region Version
                    private int version = 0;
                    public int Version
                    {
                        get { return version; }
                        set { version = value; }
                    }
                    #endregion

        #endregion 

            #region 非数据库字段与方法

            #region 用户所在医院对象
            private Hospital hospi;//用户所在医院对象
            public Hospital Hospi   
            {
                get { return hospi; }
                set { hospi = value; }
            }
            #endregion

            #region 用户真实资料
            private UserContact userContact;//用户真实资料
            public UserContact UserContact
            {
                get { return userContact; }
                set { userContact = value; }
            }
            #endregion

            #region HeadImage
            [NonSerialized]
            private Image headImage = null;
            /// <summary>
            /// 自定义头像。非DB字段。
            /// </summary>
            public Image HeadImage
            {
                get
                {
                    if (this.headImage == null && this.headImageData != null)
                    {
                        this.headImage = ESBasic.Helpers.ImageHelper.Convert(this.headImageData);
                    }
                    return headImage;
                }
            }
            #endregion

            #region HeadImageGrey
            [NonSerialized]
            private Image headImageGrey = null;
            /// <summary>
            /// 自定义头像。非DB字段。
            /// </summary>
            public Image HeadImageGrey
            {
                get
                {
                    if (this.headImageGrey == null && this.headImageData != null)
                    {
                        this.headImageGrey = ESBasic.Helpers.ImageHelper.ConvertToGrey(this.HeadImage);
                    }
                    return this.headImageGrey;
                }
            }
            #endregion

            #region GetHeadIcon
            [NonSerialized]
            private Icon headIcon = null;
            /// <summary>
            /// 自定义头像。非DB字段。
            /// </summary>
            public Icon GetHeadIcon(Image[] defaultHeadImages)
            {
                if (this.headIcon != null)
                {
                    return this.headIcon;
                }

                if (this.HeadImage != null)
                {
                    this.headIcon = ImageHelper.ConvertToIcon(this.headImage, 64);
                    return this.headIcon;
                }

                this.headIcon = ImageHelper.ConvertToIcon(defaultHeadImages[this.headImageIndex], 64);
                return this.headIcon;
            }
            #endregion

            #region UserStatus
            private UserStatus userStatus = UserStatus.OffLine;
            /// <summary>
            /// 在线状态。非DB字段。
            /// </summary>
            public UserStatus UserStatus
            {
                get { return userStatus; }
                set { userStatus = value; }
            }
            #endregion

            #region Tag
            private object tag;
            /// <summary>
            /// 可用于存储 LastWordsRecord。
            /// </summary>
            public object Tag
            {
                get { return tag; }
                set { tag = value; }
            }
            #endregion

            #region LastWords
            public string LastWords
            {
                get
                {
                    if (this.tag == null)
                    {
                        return "";
                    }

                    LastWordsRecord record = this.tag as LastWordsRecord;
                    if (record == null)
                    {
                        return "";
                    }

                    string content = record.ChatBoxContent.GetTextWithPicPlaceholder("[图]");
                    return string.Format("{0}： {1}", record.IsMe ? "我" : "TA", content);
                }
            }
            #endregion

            #region OnlineOrHide
            public bool OnlineOrHide
            {
                get
                {
                    return this.userStatus != UserStatus.OffLine;
                }
            }
            #endregion

            #region OfflineOrHide
            public bool OfflineOrHide
            {
                get
                {
                    return this.userStatus == UserStatus.OffLine || this.userStatus == UserStatus.Hide;
                }
            }
            #endregion

            #region PartialCopy
            [NonSerialized]
            private GGUser partialCopy = null;
            public GGUser PartialCopy
            {
                get
                {
                    if (this.partialCopy == null)
                    {
                        this.partialCopy = (GGUser)this.MemberwiseClone();//浅表复制，
                        this.partialCopy.Groups = "";
                        this.partialCopy.Friends = "";
                        //this.partialCopy.HeadImageIndex = this.HeadImageIndex;
                        //this.partialCopy.HeadImageData = this.HeadImageData;
                    }
                    else
                    {
                        this.partialCopy.userStatus = this.userStatus;
                    }
                    return this.partialCopy;
                }
            }
            #endregion

            #region IUser 接口
            public string ID
            {
                get { return this.userID; }
            }
            public string PersonName
            {
                get
                {
                    if (this.userContact == null)
                    {
                        return "";
                    }
                    return this.userContact.PersonName;
                }
            }

            public string HospitalName
            {
                
                get 
                {
                    if (hospi == null)
                    {
                        return "";
                    }
                    return this.hospi.HospitalName;
                }
            }
            private Dictionary<string, List<string>> friendDicationary = null;
            /// <summary>
            /// 好友ID的分组。非DB字段。key是组名，value是朋友ID;
            /// </summary>
            public Dictionary<string, List<string>> FriendDicationary
            {
                get
                {
                    if (this.friendDicationary == null)
                    {
                        if (string.IsNullOrEmpty(this.friends))
                        {
                            this.friends = "我的好友:";
                        }
                        this.friendDicationary = new Dictionary<string, List<string>>();
                        string[] catalogs = this.friends.Split(';');
                        foreach (string catalog in catalogs)
                        {
                            string[] ary = catalog.Split(':');
                            string catalogName = ary[0];//分组名
                            List<string> tempfriends = new List<string>(ary[1].Split(','));
                            List<string> friends=new List<string>();
                            foreach (string fre in tempfriends)
                            {
                                if (fre != null && fre != "")
                                    friends.Add(fre);
                            }
                            //if (friends.Count == 1)
                            //{
                            //    friends.Remove("");
                            //}
                            this.friendDicationary.Add(catalogName, friends);//分组以及组下的成员
                        }
                    }
                    return friendDicationary;
                }
            }
            public List<string> GetFriendCatalogList()
            {
                return new List<string>(this.FriendDicationary.Keys);
            }
            private List<string> allFriendList = null;
            public List<string> GetAllFriendList()
            {
                if (this.allFriendList == null)
                {
                    List<string> list = new List<string>();
                    foreach (List<string> tmp in this.FriendDicationary.Values)
                    {
                        list.AddRange(tmp);
                    }
                    this.allFriendList = list;
                }

                return this.allFriendList;
            }
            public bool IsExpert
            {
                get { return this.userContact.IsExpert; }
            }
            public bool IsGroup
            {
                get { return false; }
            }

            public List<string> FriendList
            {
                get { return this.GetAllFriendList(); }
            }
            public Parameter<string, string> GetIDName()
            {
                return new Parameter<string, string>(this.UserID, this.Name);
            }
            #region 省份
            private string province;
            public string Province
            {
                get 
                {
                    if (this.hospi == null)
                    {
                        return "";
                    }
                    return this.hospi.Province; 
                }
                //set { province = value; }
            }
            #endregion

            #region DefaultFriendCatalog
            private string defaultFriendCatalog = "我的好友";
                   
            /// <summary>
            /// 默认好友分组。不能被删除。
            /// </summary>
            public string DefaultFriendCatalog
            {
                get
                {
                    if (string.IsNullOrEmpty(this.defaultFriendCatalog))
                    {
                        this.defaultFriendCatalog = "我的好友";
                    }
                    return defaultFriendCatalog;
                }
                set { defaultFriendCatalog = value; }
            }
            #endregion

            #region 专家列表
            private string defaultExpertCatalog = "专家列表";
            public string DefaultExpertCatalog
            {
                get
                {
                    if (string.IsNullOrEmpty(this.defaultExpertCatalog))
                    {
                        this.defaultExpertCatalog = "专家列表";
                    }
                    return defaultExpertCatalog;
                }
                set { defaultExpertCatalog = value; }
            }
            #endregion

            #region 用户平台
            private EPlatformType m_PlatformType = EPlatformType.PC;
            public EPlatformType PlatformType
            {
                get { return this.m_PlatformType; }
            }
           #endregion
     #endregion

            #region 方法列表
            private string GetFriendsVal(Dictionary<string, List<string>> friendDic)
            {
                StringBuilder sb = new StringBuilder("");
                int count = 0;
                foreach (KeyValuePair<string, List<string>> pair in friendDic)
                {
                    if (count > 0)
                    {
                        sb.Append(";");
                    }
                 //   sb.Append(pair.Value);
                    string ff = ESBasic.Helpers.StringHelper.ContactString(pair.Value, ",");
                    sb.Append(string.Format("{0}:{1}", pair.Key, ff));
                    ++count;
                }
                return sb.ToString();
            }


            public void AddFriend(string friendID, string catalog)
            {
                if (!this.FriendDicationary.ContainsKey(catalog))
                {
                    return;
                }
                if (this.FriendDicationary[catalog].Contains(friendID))
                {
                    return;
                }

                this.FriendDicationary[catalog].Add(friendID);
                this.friends = this.GetFriendsVal(this.friendDicationary);
                this.allFriendList = null;
            }

            public void RemoveFriend(string friendID)
            {
                foreach (KeyValuePair<string, List<string>> pair in this.FriendDicationary)
                {
                    pair.Value.Remove(friendID);
                }

                this.friends = this.GetFriendsVal(this.friendDicationary);
                this.allFriendList = null;
            }

            public void ChangeFriendCatalogName(string oldName, string newName)
            {
                if (!this.FriendDicationary.ContainsKey(oldName))
                {
                    return;
                }

                List<string> merged = new List<string>();
                if (this.FriendDicationary.ContainsKey(newName))
                {
                    merged = this.FriendDicationary[newName];
                    this.FriendDicationary.Remove(newName);
                }
                List<string> friends = this.friendDicationary[oldName];
                friends.AddRange(merged);
                this.FriendDicationary.Remove(oldName);
                this.FriendDicationary.Add(newName, friends);
                this.friends = this.GetFriendsVal(this.friendDicationary);
                if (oldName == this.defaultFriendCatalog)
                {
                    this.defaultFriendCatalog = newName;
                }
            }

            public void AddFriendCatalog(string name)
            {
                if (this.FriendDicationary.ContainsKey(name))
                {
                    return;
                }

                this.FriendDicationary.Add(name, new List<string>());
                this.friends = this.GetFriendsVal(this.friendDicationary);
            }

            public void RemvoeFriendCatalog(string name)
            {
                if (!this.FriendDicationary.ContainsKey(name) || this.defaultFriendCatalog == name)
                {
                    return;
                }

                this.FriendDicationary.Remove(name);
                this.friends = this.GetFriendsVal(this.friendDicationary);
            }

            public void MoveFriend(string friendID, string oldCatalog, string newCatalog)
            {
                if (!this.FriendDicationary.ContainsKey(oldCatalog) || !this.FriendDicationary.ContainsKey(newCatalog))
                {
                    return;
                }
                this.friendDicationary[oldCatalog].Remove(friendID);
                if (!this.friendDicationary[newCatalog].Contains(friendID))
                {
                    this.friendDicationary[newCatalog].Add(friendID);
                }
                this.friends = this.GetFriendsVal(this.friendDicationary);
            }
            #endregion
            public override string ToString()
            {
                return string.Format("{0}({1})-{2}，Ver：{3}", this.name, this.UserID, this.userStatus, this.version);
            }

            #endregion

            private CheckType m_CheckType = CheckType.Register;//--默认是注册

            public CheckType CheckType
            {
                get { return m_CheckType; }
                set { m_CheckType = value; }
            }
            private bool m_IsChecking = false;//是否处于待审核状态;

            public bool IsChecking
            {
                get { return m_IsChecking; }
                set { m_IsChecking = value; }
            }
    }
}
