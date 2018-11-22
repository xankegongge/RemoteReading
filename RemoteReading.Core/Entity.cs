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
    

    #region GGGroup
    [Serializable]
    public class GGGroup : IGroup
    {
        #region Force Static Check
        public const string TableName = "GGGroup";
        public const string _GroupID = "GroupID";
        public const string _Name = "Name";
        public const string _CreatorID = "CreatorID";
        public const string _Announce = "Announce";
        public const string _Members = "Members";
        public const string _CreateTime = "CreateTime";
        public const string _Version = "Version";
        #endregion

        #region IEntity Members
        public System.String GetPKeyValue()
        {
            return this.GroupID;
        }
        #endregion

        public GGGroup() { }
        public GGGroup(string id, string _name, string _creator ,string _announce ,string _members)
        {
            this.groupID = id;
            this.name = _name;
            this.creatorID = _creator;          
            this.announce = _announce;
            this.members = _members;
        }

        #region GroupID
        private string groupID = "";
        public string GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
        #endregion

        #region Name
        private string name = "";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion

        #region CreatorID
        private string creatorID = "";
        /// <summary>
        /// 群创建者。
        /// </summary>
        public string CreatorID
        {
            get { return creatorID; }
            set { creatorID = value; }
        } 
        #endregion

        #region CreateTime
        private string createTime ;
        /// <summary>
        /// 创建时间。
        /// </summary>
        public string CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        } 
        #endregion

        #region Announce
        private string announce = "";
        /// <summary>
        /// 公告。
        /// </summary>
        public string Announce
        {
            get { return announce; }
            set { announce = value; }
        } 
        #endregion

        #region Members
        private string members = "";
        /// <summary>
        /// 组成员，ID使用英文逗号隔开。
        /// </summary>
        public string Members
        {
            get { return members; }
            set 
            { 
                members = value;
                this.memberList = null;
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

        #region 非DB字段
        #region Tag
        private object tag;
        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        #endregion

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
                return string.Format("{0}： {1}", record.SpeakerName, content);
            }
        }

        #region MemberList
        private List<string> memberList = null;
        /// <summary>
        /// 非DB字段
        /// </summary>
        public List<string> MemberList
        {
            get
            {
                if (memberList == null)
                {
                    this.memberList = new List<string>(this.members.Split(','));
                    this.memberList.Remove("");
                }

                return memberList;
            }
        }
        #endregion 
        #endregion

        public void AddMember(string userID)
        {
            if (!this.MemberList.Contains(userID))
            {
                this.MemberList.Add(userID);
                this.Members = ESBasic.Helpers.StringHelper.ContactString<string>(this.MemberList, ",");
            }
        }

        public void RemoveMember(string userID)
        {
            this.MemberList.Remove(userID);
            this.Members = ESBasic.Helpers.StringHelper.ContactString<string>(this.MemberList, ",");
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", this.name, this.groupID);
        }

        public Parameter<string, string> GetIDName()
        {
            return new Parameter<string, string>(this.GroupID, this.Name);
        }

        public string ID
        {
            get { return this.groupID; }
        }


        public bool IsGroup
        {
            get { return true; }
        }
    } 
    #endregion        

    #region OfflineMessage
    /// <summary>
    /// 离线消息。
    /// </summary>
    [Serializable]
    public class OfflineMessage
    {
        #region Ctor
        public OfflineMessage() { }
        public OfflineMessage(string _sourceUserID, string _destUserID, int _informationType, byte[] info)
        {
            this.sourceUserID = _sourceUserID;
            this.destUserID = _destUserID;
            this.informationType = _informationType;
            this.information = info;
        }
        #endregion

        #region SourceUserID
        private string sourceUserID = "";
        /// <summary>
        /// 发送离线消息的用户ID。
        /// </summary>
        public string SourceUserID
        {
            get { return sourceUserID; }
            set { sourceUserID = value; }
        }
        #endregion

        #region DestUserID
        private string destUserID = "";
        /// <summary>
        /// 接收离线消息的用户ID。
        /// </summary>
        public string DestUserID
        {
            get { return destUserID; }
            set { destUserID = value; }
        }
        #endregion

        #region InformationType
        private int informationType = 0;
        /// <summary>
        /// 信息的类型。
        /// </summary>
        public int InformationType
        {
            get { return informationType; }
            set { informationType = value; }
        }
        #endregion

        #region Information
        private byte[] information;
        /// <summary>
        /// 信息内容
        /// </summary>
        public byte[] Information
        {
            get { return information; }
            set { information = value; }
        }
        #endregion

        #region Time
        private string time = DateTime.Now.ToString();
        /// <summary>
        /// 服务器接收到要转发离线消息的时间。
        /// </summary>
        public string Time
        {
            get { return time; }
            set { time = value; }
        }
        #endregion
    }
    #endregion

    #region OfflineFileItem
    /// <summary>
    /// 离线文件条目
    /// </summary>
    [Serializable]
    public class OfflineFileItem
    {        
        /// <summary>
        /// 条目的唯一编号，数据库自增序列，主键。
        /// </summary>
        public string AutoID { get; set; }
       
        /// <summary>
        /// 离线文件的名称。
        /// </summary>
        public string FileName { get; set; }
       
        /// <summary>
        /// 文件的大小。
        /// </summary>
        public ulong FileLength { get; set; }
       
        /// <summary>
        /// 发送者ID。
        /// </summary>
        public string SenderID { get; set; }
     
        /// <summary>
        /// 接收者ID。
        /// </summary>
        public string AccepterID { get; set; }
       
        /// <summary>
        /// 在服务器上存储离线文件的临时路径。
        /// </summary>
        public string RelayFilePath { get; set; }
    }
    #endregion         

    
}