using System;
using System.Collections.Generic;
using System.Text;
using ESBasic;
using System.Drawing;
namespace JustLib
{
    #region IUnit
    public interface IUnit
    {
        string ID { get; }
        string Name { get; }
        int Version { get; set; }
        string LastWords { get; }

        bool IsGroup { get; }

        object Tag { get; set; }
        Parameter<string, string> GetIDName();
    } 
    #endregion

     
    public interface IMedicalReading
    {
       // List<string> ListPics { get; }
        string MedicalReadingID { get; }
        string UserFromPersonName { get; }
        string UserToPersonName { get; }
        string UserFromHospitalName { get; }
        string UserToHospitalName { get; }
        System.String UserIDFrom { get; }
        string UserIDTo { get;}
        EReadingStatus ReadingStatus { get; }
         System.DateTime CreatedTime { get; }
         System.Int32 MedicalPictureCount { get; }
        
    }

    #region IUser
    public interface IUser : IUnit
    {
        List<string> GroupList { get; }
        UserStatus UserStatus { get; set; }
       
        //Hospital Hospi { get; set; }
        object Tag { get; set; }
        Dictionary<string, List<string>> FriendDicationary { get; }
        string Signature { get; }
        string Name { get; }
        string PersonName { get; }
        string HospitalName { get; }
        EPlatformType PlatformType { get; }
        string DefaultFriendCatalog { get; }
        string DefaultExpertCatalog { get; }
        string Province { get; }
        /// <summary>
        /// 如果为空字符串，则表示位于组织外。
        /// </summary>
        bool IsExpert { get; }
        List<string> GetAllFriendList();
        List<string> GetFriendCatalogList();       
    }
    #endregion

    #region IGroup
    public interface IGroup : IUnit
    {
        string CreatorID { get; }

        List<string> MemberList { get; }

        void AddMember(string userID);
        void RemoveMember(string userID);

    }
    #endregion

    public interface IHeadImageGetter
    {
        Image GetHeadImage(IUser user);
    }

    public interface IUserInformationForm
    {
        void SetUser(IUser user);
       // void SetMD(MedicalReading mr);
    }

    //在线状态
    public enum UserStatus
    {
        Online = 2,//在线
        Away = 3,//离开
        Busy = 4,
        DontDisturb = 5,
        OffLine = 6,
        Hide = 7
    }
    public enum EPlatformType
    {
        PC = 0,
        WebQQ = 1,
        IPhone = 2,
        Android = 3,
    }
    /// <summary>
    /// 阅片状态
    /// </summary>
    public enum EReadingStatus
    {
        UnProcessed = 0,
        Processing = 1,
        Rejected = 2,
        Completed = 3
    }
     public enum ProfessionTitle
    {
        医师 = 0,
        主治医师,//
        副主任医师,
        主任医师
    }
     /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        Administrator = 0,//管理员
        Servicer,//客服人员
        NormalClient,//普通客户
        Expert//专家
    }
    public enum GroupChangedType
    {
        /// <summary>
        /// 成员的资料发生变化
        /// </summary>
        MemberInfoChanged = 0,
        /// <summary>
        /// 组的资料（如组名称、公告等）发生变化
        /// </summary>
        GroupInfoChanged,
        SomeoneJoin,
        SomeoneQuit,
        GroupDeleted
    }

    #region ContactRTDatas
    public class UserRTData
    {
        public UserRTData() { }
        public UserRTData(UserStatus status, int ver)
        {
            this.UserStatus = status;
            this.Version = ver;
        }

        public UserStatus UserStatus { get; set; }
        public int Version { get; set; }
    }

    public class ContactRTDatas
    {
        public Dictionary<string, UserRTData> UserStatusDictionary { get; set; }
        public Dictionary<string, int> GroupVersionDictionary { get; set; }
    } 
    #endregion
}
