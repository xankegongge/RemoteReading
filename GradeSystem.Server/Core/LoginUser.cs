using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ESBasic.Helpers;
using ESBasic;
using JustLib;
using JustLib.Controls;
namespace GradeSystem.Server
{
	[Serializable]
	public partial class LoginUser 
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
            public LoginUser() { }
            public string activitedByUserID = null;

            public string ActivitedByUserID
            {
                get { return activitedByUserID; }
                 
            }

            public LoginUser(string id, string pwd,  int headIndex, 
                EUserType usertype, bool isacted, PersonInfo us)
            {
                this.UserID = id;
                this.passwordMD5 = pwd;
                this.m_UserType = usertype;
                this.m_IsActivited = isacted;
                this.userContact = us;
 
            }
        //从服务器获取数据
            public LoginUser(string id, string pwd,   int headIndex
                    , EUserType usertype, bool isacted, PersonInfo us,string time, string activitedByUserID)
            {
                this.UserID = id;
                this.passwordMD5 = pwd;
                this.HeadImageIndex = headIndex;
                this.m_UserType = usertype;
                this.m_IsActivited = isacted;
                this.userContact = us;
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
                    private EUserType m_UserType = EUserType.Judge;//默认是普通客户
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

          

            #region 用户真实资料
            private PersonInfo userContact;//用户真实资料
            public PersonInfo UserContact
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

            #region OnlineOrHide
            public bool OnlineOrHide
            {
                get
                {
                    return this.m_OnlineState != UserStatus.OffLine;
                }
            }
            #endregion

            #region OfflineOrHide
            public bool OfflineOrHide
            {
                get
                {
                    return this.m_OnlineState == UserStatus.OffLine || this.m_OnlineState == UserStatus.Hide;
                }
            }
            #endregion

            #region PartialCopy
            [NonSerialized]
            private LoginUser partialCopy = null;
            public LoginUser PartialCopy
            {
                get
                {
                    if (this.partialCopy == null)
                    {
                        this.partialCopy = (LoginUser)this.MemberwiseClone();//浅表复制，
                    
                        //this.partialCopy.HeadImageIndex = this.HeadImageIndex;
                        //this.partialCopy.HeadImageData = this.HeadImageData;
                    }
                    else
                    {
                        this.partialCopy.m_OnlineState = this.m_OnlineState;
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

         
           
          
            public Parameter<string, string> GetIDName()
            {
                return new Parameter<string, string>(this.UserID, this.PersonName);
            }
         

         

          

            #region 用户平台
            private EPlatformType m_PlatformType = EPlatformType.PC;
            public EPlatformType PlatformType
            {
                get { return this.m_PlatformType; }
            }
           #endregion
     #endregion

          

           
            public override string ToString()
            {
                return string.Format("{0}({1})-{2}，Ver：{3}", this.PersonName, this.UserID, this.m_OnlineState, this.version);
            }

            #endregion

            
          
    }
}
