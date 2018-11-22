using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ESBasic;
using ESBasic.ObjectManagement.Managers;
using RemoteReading.Core;
namespace RemoteReading.Server
{
     [Serializable]
   public class ServerCacheSave
    {
         public static string SystemSettingsDir = "C:\\RemoteReadingServerCache\\";
        private static string SystemSettingsFilePath = SystemSettingsDir + "RemoteReadingCache.dat";

        private static ServerCacheSave singleton;
        /// <summary>
        /// 单例模式。
        /// </summary>
        public static ServerCacheSave Singleton
        {
            get
            {
                if (ServerCacheSave.singleton == null)
                {
                    ServerCacheSave.singleton = ServerCacheSave.Load();
                    if (ServerCacheSave.singleton == null)
                    {
                        ServerCacheSave.singleton = new ServerCacheSave();
                    }
                }

                return ServerCacheSave.singleton;
            }           
        }

        private ServerCacheSave() { }
        public void Save()
        {
            byte[] data = ESBasic.Helpers.SerializeHelper.SerializeObject(this);
            ESBasic.Helpers.FileHelper.WriteBuffToFile(data, SystemSettingsFilePath);
        }
        #region CheckUserCache
        private ObjectManager<string,GGUser>  checkUserCache = null;
        public ObjectManager<string, GGUser> CheckUserCache
        {
            get { return checkUserCache; }
            set { checkUserCache = value; }
        }
        #endregion
        #region OfflineMessageTable
        private ObjectManager<string, List<OfflineMessage>> offlineMessageTable = null;
        public ObjectManager<string, List<OfflineMessage>> OfflineMessageTable
        {
            get { return offlineMessageTable; }
            set { offlineMessageTable = value; }
        }
        #endregion

        #region OfflineFileItem
        private ObjectManager<string, List<OfflineFileItem>> offlineFileTable = null;
        public ObjectManager<string, List<OfflineFileItem>> OfflineFileTable
        {
            get { return offlineFileTable; }
            set { offlineFileTable = value; }
        }
        #endregion

        #region ListOfflineMediclaReading 离线阅片消息
        private ObjectManager<string, List<MedicalReading>> listOfflineMediclaReading = null;
        public ObjectManager<string, List<MedicalReading>> ListOfflineMediclaReading
        {
            get { return listOfflineMediclaReading; }
            set { listOfflineMediclaReading = value; }
        }
        #endregion

        private static ServerCacheSave Load()
        {
            try
            {
                if (!File.Exists(SystemSettingsFilePath))
                {
                    return null;
                }

                byte[] data = ESBasic.Helpers.FileHelper.ReadFileReturnBytes(SystemSettingsFilePath);
                return (ServerCacheSave)ESBasic.Helpers.SerializeHelper.DeserializeBytes(data, 0, data.Length);
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show(ee.Message);
                return null;
            }
        }
    }
    

}
