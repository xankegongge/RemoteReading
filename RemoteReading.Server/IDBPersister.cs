using System;
using System.Collections.Generic;
using System.Text;
using JustLib.Records;
using System.Drawing;
using RemoteReading.Core;
namespace RemoteReading.Server
{
    public interface IDBPersister : IChatRecordPersister
    {
        bool InsertUser(GGUser t,string friends,string activitedbyuserid);
        bool UpdateUserFriends(GGUser t);
        void InsertGroup(GGGroup t);       
        bool UpdateUser(GGUser t);
        bool UpdateGroup(GGGroup t);
        void DeleteGroup(string groupID);
        List<GGUser> GetAllUser();
        List<GGGroup> GetAllGroup();      
        

        //bool ChangeUserPassword(string userID, string newPasswordMD5);
        bool ChangeUserGroups(string userID, string groups);        


        /*新增方法,获取数据库中医院列表*/
        IList<Hospital> GetHospitals();
        List<GGUser> GetAllExperts();
        MedicalReading InsertMedicalReading(MedicalReading mr);
        
        //IList<SamplesType> GetSamplesTypes();
        //IList<DyedMethod> GetDyedMethods();
        //IList<Zoom> GetZooms();

        IList<MedicalReading> GetAllMedicalReading();
        MedicalReading GetMedicalReadingByGuid(string gid);
        List<byte[]> GetBitmapsbypath(ReadingPicture rp,int piccount);
        List<string> GetClientOrExpertMDGuids(string id, bool isexpert);
        List<MedicalReading> GetSomeMedicalReading(List<string> listguids);

        bool UpdateMedicalReading(string id, string reason,out string  updatetime);

        bool UpdateMedicalReading(string id, out string updatetime);

        bool UpdateMedicalReading(string id, List<ReadingPicture> listpics, out List<ReadingPicture> expertmarks,out string updatetime);

        GGUser GetUser(string userID);

        bool UpdatePasswd(string userID, string passMD5);

        bool DeleteUser(string deluserid);

        bool CheckUserResult(GGUser checkUser,string serviceID, bool isexpert);

        List<byte[]> GetSmallBitmapsbypath(ReadingPicture rp, int p);

        byte[] GetSingleBitmapsbypath(ReadingPicture tarMD);

        bool UpdateUserContactInfo(GGUser localUser);

        bool InsertAdvice(string userid, string advice);

        List<MedicalReading> GetMoreMedicalReadings(string userid, bool isexpert, int readingstatus, DateTime foremostTime, int currPage, int pageSize);

         List<MedicalReading> GetLastUpdateMedicalReadings(string userid, bool isexpert, int readingstatus, DateTime lastupdatetime);
    }    
}
