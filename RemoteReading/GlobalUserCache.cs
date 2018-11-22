using System;
using System.Collections.Generic;
using System.Text;
using ESBasic.ObjectManagement.Managers;
using ESPlus.Rapid;
using ESPlus.Serialization;
using ESBasic;
using System.IO;
using System.Threading;
using JustLib.Caches;
using JustLib;
using System.Drawing;
using ESBasic.Loggers;
using RemoteReading.Core;
namespace RemoteReading
{
    public class GlobalUserCache : BaseGlobalUserCache<GGUser, GGGroup>, IUserNameGetter
    {
        private IRapidPassiveEngine rapidPassiveEngine;
      //  private IAgileLogger logger;
        private string mdcacheFilePath;
        private ObjectManager<string, MedicalReading> medicalreadingManager = new ObjectManager<string, MedicalReading>();
        private ObjectManager<string, GGUser> expertCache = new ObjectManager<string, GGUser>(); // key:用户ID 。 Value：用户信息
        public event CbGeneric<MedicalReading> MedicalReadingInfoChanged;
        public event CbGeneric<MedicalReading> MedicalReadingAdded;

        public event CbGeneric<MedicalReading> MedicalReadingStatusChanged;
        public event CbGeneric<string> MedicalReadingRemoved;
        public event CbGeneric MedicalReadingRTDataRefreshCompleted;
        List<string> newlistGuids;

        public List<string> NewlistGuids
        {
            get { return newlistGuids; }
            set { newlistGuids = value; }
        }

        public GlobalUserCache(IRapidPassiveEngine engine)
        {
            this.rapidPassiveEngine = engine;
            string persistenceFilePath = SystemSettings.SystemSettingsDir + engine.CurrentUserID + ".dat";//加载好友列表以及组列表设定
            mdcacheFilePath = SystemSettings.SystemSettingsDir + engine.CurrentUserID + ".md";//加载阅片列表信息；
            Initialize(this.rapidPassiveEngine.CurrentUserID, persistenceFilePath, GlobalConsts.CompanyGroupID, GlobalResourceManager.Logger);
            //获取最新的GUID列表
            newlistGuids = this.CurrentUser.UserType == EUserType.Expert ? this.GetAllClientOrExpertMDGuids(true) : this.GetAllClientOrExpertMDGuids(false);
            
        }

        public override void Initialize(string curUserID, string persistencePath, string _companyGroupID, ESBasic.Loggers.IAgileLogger _logger)
        {

            if (mdcacheFilePath != null)
            {
               List<MedicalReading> list= LoadMedicalReadings(mdcacheFilePath);

               if (list != null)
               {
                   foreach (MedicalReading mr in list)
                   {
                       //if (mr.ListPics != null && mr.ListPics[0].Imagebyte != null)//如果有图片流的话，就装载进image对象去;
                       //{
                       //    for (int i = 0; i < mr.ListPics.Count; i++)
                       //    {
                       //        MemoryStream ms = new MemoryStream(mr.ListPics[i].Imagebyte);
                       //        Bitmap bmp = new Bitmap(ms);
                       //        mr.ListPics[i].Image = bmp;
                       //        ms.Dispose();
                       //    }
                       //}
                       this.medicalreadingManager.Add(mr.MedicalReadingID, mr);//添加进来
                    }
                }
            }
            List<GGUser> listexperts = DoGetAllExperts();
            foreach (GGUser user in listexperts)
            {
                this.expertCache.Add(user.UserID, user);//专家列表；
            }
            base.Initialize(curUserID, persistencePath, _companyGroupID, _logger);
        }

        protected  List<GGUser> DoGetAllExperts()
        {
            //同步调用，获取用户信息
            byte[] bUser = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetAllExperts, null);
            if (bUser == null)
            {
                return null;
            }
            return ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<List<GGUser>>(bUser, 0);
        }

        protected override GGUser DoGetUser(string userID)
        {
            //同步调用，获取用户信息
            byte[] bUser = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetUserInfo, System.Text.Encoding.UTF8.GetBytes(userID));
            if (bUser == null)
            {
                return null;
            }
            return ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<GGUser>(bUser, 0);
        }

        protected override GGGroup DoGetGroup(string groupID)
        {
            byte[] bGroup = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetGroup, System.Text.Encoding.UTF8.GetBytes(groupID));
            return CompactPropertySerializer.Default.Deserialize<GGGroup>(bGroup, 0);
        }
        protected override List<GGGroup> DoGetMyGroups()
        {
            byte[] bMyGroups = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetMyGroups, null);
            return CompactPropertySerializer.Default.Deserialize<List<GGGroup>>(bMyGroups, 0);
        }
        protected override List<GGGroup> DoGetSomeGroups(List<string> groupIDList)
        {
            byte[] bMyGroups = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetSomeGroups, CompactPropertySerializer.Default.Serialize(groupIDList));
            return CompactPropertySerializer.Default.Deserialize<List<GGGroup>>(bMyGroups, 0);
        }
        protected override ContactRTDatas DoGetContactsRTDatas()
        {
            byte[] res = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetContactsRTData, null);
            return ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<ContactsRTDataContract>(res, 0);
        }
        protected override List<GGUser> DoGetSomeUsers(List<string> userIDList)
        {
            byte[] bFriends = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetSomeUsers, CompactPropertySerializer.Default.Serialize(userIDList));
            return CompactPropertySerializer.Default.Deserialize<List<GGUser>>(bFriends, 0);
        }

        protected override List<GGUser> DoGetAllContacts() //好友,群友，这里改成好友
        {
            byte[] bFriends = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetAllContacts, null);
            return CompactPropertySerializer.Default.Deserialize<List<GGUser>>(bFriends, 0);
        }

        public IUnit GetUnit(string id ,bool isGroup)
        {            
            if (isGroup)
            {
                return this.GetGroup(id);
            }

            return this.GetUser(id);
        }
        public List<MedicalReading> GetAllMedicalReading()
        {
            if (this.medicalreadingManager.Count > 0)
                return this.medicalreadingManager.GetAll();
            else
                return null;
        }
        private List<MedicalReading> DoGetSomeMDs(List<string> tmp)
        {
            byte[] info = ESPlus.Serialization.CompactPropertySerializer.Default.Serialize<List<string>>(tmp);
            //byte[] info = new byte[temp.Length + 1];
            //if (isexpert)
            //{
            //    info[0] = 0x01;
            //}
            //else
            //{
            //    info[0] = 0x00;
            //}
            //Array.Copy(temp, 0, info, 1, temp.Length);
            byte[] blistMedical = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetSomeMedicalReading, info);
            List<MedicalReading> list = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<List<MedicalReading>>(blistMedical, 0);
            return list;
        }
        /// <summary>
        /// 获取所有阅片列表
        /// </summary>
        /// <param name="isexpert"></param>
        /// <returns></returns>
        public List<MedicalReading> GetAllMedicalReading(bool isexpert)
        {
            byte[] info;
            if (isexpert)
            {
                info = new byte[] { 0x01};//专家
            }
            else
            {
                info = new byte[] { 0x00 };
            }
            byte[] blistMedical = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetAllMedicalReadings, info);
            List<MedicalReading> list=ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<List<MedicalReading>>(blistMedical, 0);
            return list;
        }
       public void AddMedicalReading(MedicalReading mr)
        {
            if (mr != null && !this.medicalreadingManager.Contains(mr.MedicalReadingID))
            {
                this.medicalreadingManager.Add(mr.MedicalReadingID, mr);
                this.MedicalReadingInfoChanged(mr);
            }
        }

       public  List<byte[]> GetPicsByGid(string gid)
       {
           List<byte[]> list=null;
           byte[] blistMaps = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetMDImages, System.Text.Encoding.UTF8.GetBytes(gid));
          if(blistMaps!=null)
             list = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<List<byte[]>>(blistMaps, 0);
           return list;
       }
      

       private List<MedicalReading> LoadMedicalReadings(string mdcacheFilePath)
       {
           try
           {
               if (!File.Exists(mdcacheFilePath))
               {
                   return null;
               }

               byte[] data = ESBasic.Helpers.FileHelper.ReadFileReturnBytes(mdcacheFilePath);
               return (List<MedicalReading>)ESBasic.Helpers.SerializeHelper.DeserializeBytes(data, 0, data.Length);
           }
           catch
           {
               return null;
           }
       }
       public void SaveMedicalReadingCache()
       {
           byte[] data = ESBasic.Helpers.SerializeHelper.SerializeObject(this.medicalreadingManager.GetAll());
           ESBasic.Helpers.FileHelper.WriteBuffToFile(data, this.mdcacheFilePath);
       }

       private Thread getMDThread;
       private int pageSizeMD = 20; 
       public  void StartRefreshMDInfo()
       {
           //直接使用线程，可以快速启动。后台线程池初始化需要10秒左右，太慢了。
           this.getMDThread = (this.medicalreadingManager.Count > 0) ? new Thread(new ParameterizedThreadStart(this.RefreshMDsRTData)) : new Thread(new ParameterizedThreadStart(this.LoadMDsFromServer));

           this.getMDThread.Start();
       }

       private void RefreshMDsRTData(object obj)
       {
           //throw new NotImplementedException();
       }
        /// <summary>
        /// 获取guid列表
        /// </summary>
        /// <param name="isexpert"></param>
        /// <returns></returns>
       public List<string> GetAllClientOrExpertMDGuids(bool isexpert)
       {
           
           byte[] info;
           if (isexpert)
           {
               info = new byte[] { 0x01 };
           }
           else
           {
               info = new byte[] { 0x00 };//专家
           }
           byte[] blistMDGuids = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.GetClientOrExpertMDGuids, info);
           List<string> list = ESPlus.Serialization.CompactPropertySerializer.Default.Deserialize<List<string>>(blistMDGuids, 0);
           return list;
       }

      
       #region LoadMDsFromServer
    
       private void LoadMDsFromServer(object state)
       {
           try
           {
               //List<string> tmpFriendList = null;
               //if(this.currentUser.UserType==EUserType.NormalClient)
               //{
               //    tmpFriendList = this.GetAllClientOrExpertMDGuids(false);//获取该用户/专家所有的阅片GUID
               //}
               //if (this.currentUser.UserType == EUserType.Expert)
               //{
               //    tmpFriendList = this.GetAllClientOrExpertMDGuids(true);//获取该用户/专家所有的阅片GUID
               //}
               ESBasic.Collections.SortedArray<string> allMDGuids = new ESBasic.Collections.SortedArray<string>(this.newlistGuids);
               

               List<string> allMDGuidList = allMDGuids.GetAllReadonly(); //所有联系人
               int pageCount = allMDGuidList.Count / this.pageSizeMD;
               int lastPageSize = allMDGuidList.Count % this.pageSizeMD;
               if (lastPageSize > 0)
               {
                   pageCount += 1;
               }
               else
               {
                   lastPageSize = this.pageSizeMD;
               }

               if (pageCount == 1)//如果只有一页的话，就一次性获取所有联系人;
               {
                   //MedicalReadings   
                   List<MedicalReading> lists=null;
                   if (this.currentUser.UserType == EUserType.NormalClient)
                   {
                        lists = this.GetAllMedicalReading(false);//通过UserID,与是否是专家获取所有阅片信息；
                   }
                   else if (this.currentUser.UserType == EUserType.Expert)
                   {
                        lists = this.GetAllMedicalReading(true);
                   }
                   foreach (MedicalReading mr in lists)
                   {
                       //添加发送者与接受者的附属性
                       GGUser userto = this.GetUser(mr.UserIDTo);//获取专家信息
                       mr.UserTo = userto;
                       GGUser userfrom = this.GetUser(mr.UserIDFrom);//获取客户信息；
                       mr.UserFrom = userfrom;
                       if(!this.medicalreadingManager.Contains(mr.MedicalReadingID))
                       {
                           this.medicalreadingManager.Add(mr.MedicalReadingID, mr);
                           this.MedicalReadingInfoChanged(mr);//通知更新界面已经formmanager
                       }
                   }
               }
               else//多页的时候，就分批获取并加载;
               {
                   for (int i = 0; i < pageCount; i++)
                   {
                       string[] ary = (i == pageCount - 1) ? new string[lastPageSize] : new string[this.pageSizeMD];
                       allMDGuidList.CopyTo(i * this.pageSizeMD, ary, 0, ary.Length);
                       List<string> tmp = new List<string>(ary);
                       List<MedicalReading> someMDslist = this.DoGetSomeMDs(tmp);//获取一批

                       foreach (MedicalReading mr in someMDslist)
                       {
                           GGUser userto = this.GetUser(mr.UserIDTo);//获取专家信息
                           mr.UserTo = userto;
                           GGUser userfrom = this.GetUser(mr.UserIDFrom);//获取客户信息；
                           mr.UserFrom = userfrom;
                           if (!this.medicalreadingManager.Contains(mr.MedicalReadingID))
                           {
                               this.medicalreadingManager.Add(mr.MedicalReadingID, mr);
                               this.MedicalReadingInfoChanged(mr);
                           }
                       }
                   }
               }

          
               this.MedicalReadingRTDataRefreshCompleted();
           }
           catch (Exception ee)
           {
               GlobalResourceManager.Logger.Log(ee, "GlobalUserCache.LoadMdsFromServer", ESBasic.Loggers.ErrorLevel.Standard);
           }
       }

      
       
       #endregion

       public MedicalReading GetMedicalReading(string guid)
       {
           return this.medicalreadingManager.Get(guid);
       }



       public  void UpdateMedicalReading(MedicalReading mr)
       {
           MedicalReading ori = this.medicalreadingManager.Get(mr.MedicalReadingID);
           if (ori != null)
           {
              
               this.medicalreadingManager.Remove(ori.MedicalReadingID);
               this.medicalreadingManager.Add(mr.MedicalReadingID, mr);//manager的更改将导致界面缓存的更改
               this.MedicalReadingInfoChanged(mr);//先更新界面，然后在更新缓存；
              
           }
       }

       public  List<GGUser> GetAllExperts()
       {
           return expertCache.GetAll();
       }
    }    
}
