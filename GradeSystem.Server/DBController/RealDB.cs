using System;
using System.Collections.Generic;
using System.Text;
using ESBasic.Security;
using ESBasic.ObjectManagement.Managers;
using System.Configuration;
using ESBasic;
using JustLib.Records;
using DataRabbit.DBAccessing.Application;
using DataRabbit.DBAccessing;
using DataRabbit.DBAccessing.ORM;
using DataRabbit;
using DataRabbit.DBAccessing.Relation;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using DataProvider;
using JustLib;
namespace GradeSystem.Server
{  
    /// <summary>
    /// 真实数据库。
    /// </summary>
    public class RealDB : DefaultChatRecordPersister
    {
        private TransactionScopeFactory transactionScopeFactory;

        public RealDB()
        {

        }
        public RealDB(string dbName, string dbIP,string saPwd )
        {
            DataConfiguration config = new SqlDataConfiguration(dbIP, "sa", saPwd, dbName);
            this.transactionScopeFactory = new TransactionScopeFactory(config);
            this.transactionScopeFactory.Initialize();
            base.Initialize(this.transactionScopeFactory);//服务器的sql数据库，本地的是sqllite数据库;
        }
       

        private string picFixSavePath = @"C:\GradeSystemPicData\" ;
      //将医学图片保存至服务器本地
       private string SaveMedicaPic(string useridfrom,string useridto,byte[] bytes,string filetype,string random,int picsaveNum)
       {
                   using(MemoryStream ms = new MemoryStream(bytes))
                   {
                    Bitmap bmp = new Bitmap(ms);
                    string saveDirectory = picFixSavePath + DateTime.Now.ToString("yyyy-MM-dd");
                    string savefilename;
                    try
                    {

                        if (System.IO.Directory.Exists(saveDirectory) == false)
                        {
                            System.IO.Directory.CreateDirectory(saveDirectory);//创建目录
                        }
                        savefilename = saveDirectory + "\\" + useridfrom + "-" + useridto + "-" + random + "-" + picsaveNum;
                        filetype = filetype.ToLower();
                        switch (filetype)
                        {
                            case "jpeg":
                            case "jpg":
                                bmp.Save(savefilename, ImageFormat.Jpeg);
                                break;
                            case "png":
                                bmp.Save(savefilename,ImageFormat.Png);
                                break;
                            case "bmp":
                                bmp.Save(savefilename, ImageFormat.Bmp);
                                break;
                            case "gif":
                                bmp.Save(savefilename, ImageFormat.Gif);
                                break;
                            default: break;
                        }
                       // bmp.Save(savefilename, System.Drawing.Imaging.ImageFormat.Jpeg);
                        bmp.Dispose();
                    }
                    catch (System.Exception ex)
                    {
                        return null;
                    }
                    return savefilename;
                
       }
         
        }
       
        /// <summary>
        /// 更新用户信息（好友，小组友、密码、昵称，签名
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool UpdateUser(LoginUser t)
        {
            bool isupdateok = false;
            Dictionary<string, object> outvalues = null;

            SqlParameter UserID = new SqlParameter("UserID", SqlDbType.NVarChar);
            UserID.Direction = ParameterDirection.Input; UserID.Value = t.UserID;

            SqlParameter PasswordMD5 = new SqlParameter("PasswordMD5", SqlDbType.NVarChar);
            PasswordMD5.Direction = ParameterDirection.Input; PasswordMD5.Value = t.PasswordMD5;

          

          
            SqlParameter HeadImageIndex = new SqlParameter("HeadImageIndex", SqlDbType.Int);
            HeadImageIndex.Direction = ParameterDirection.Input; HeadImageIndex.Value = t.HeadImageIndex;

        
            SqlParameter HeadImageData = new SqlParameter("HeadImageData", SqlDbType.Image);
            HeadImageData.Direction = ParameterDirection.Input;
            //if (t.HeadImage != null)
            //{
            //    MemoryStream ms = new MemoryStream(t.HeadImageData);
            //    image = System.Drawing.Image.FromStream(ms);
            //} 
            HeadImageData.Value = t.HeadImageData;

            SqlParameter Version = new SqlParameter("Version", SqlDbType.Int);
            Version.Direction = ParameterDirection.Input; Version.Value = t.Version;

            SqlParameter[] sparray = new SqlParameter[]
                  {
                    UserID,
                    PasswordMD5,
                  
                    HeadImageIndex,//默认头像,
                    HeadImageData,
                    Version
                  };
            SqlServerProvider ssp = new SqlServerProvider();

            try
            {
                if (ssp.SPExcuteNoneQuery("spupdateuser", sparray, out outvalues))//更新成功
                {
                    isupdateok = true;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return isupdateok;
        }
        //具体更新本人的真实信息;
        public bool UpdateUserContactInfo(LoginUser t)
        {
            bool isupdateok = false;
            Dictionary<string, object> outvalues = new Dictionary<string, object>();

            SqlParameter UserID = new SqlParameter("UserID", SqlDbType.NVarChar);
            UserID.Direction = ParameterDirection.Input; UserID.Value = t.UserID;

            SqlParameter UserType = new SqlParameter("UserType", SqlDbType.Int);
            UserType.Direction = ParameterDirection.Input; UserType.Value = (int)t.UserType;

            SqlParameter IsActivited = new SqlParameter("IsActivited", SqlDbType.Bit);
            IsActivited.Direction = ParameterDirection.Input; IsActivited.Value = t.IsActivited;

            SqlParameter ActivitedByUserID = new SqlParameter("ActivitedByUserID", SqlDbType.NVarChar);
            ActivitedByUserID.Direction = ParameterDirection.Input; ActivitedByUserID.Value = t.activitedByUserID;

            SqlParameter PasswordMD5 = new SqlParameter("PasswordMD5", SqlDbType.NVarChar);
            PasswordMD5.Direction = ParameterDirection.Input; PasswordMD5.Value = t.PasswordMD5;

            SqlParameter Version = new SqlParameter("Version", SqlDbType.Int);//版本号；
            Version.Direction = ParameterDirection.Input; Version.Value = t.Version;
           
            SqlParameter PersonName = new SqlParameter("PersonName", SqlDbType.NVarChar);
            PersonName.Direction = ParameterDirection.Input; PersonName.Value = t.UserContact.PersonName;

            SqlParameter Email = new SqlParameter("Email", SqlDbType.NVarChar);
            Email.Direction = ParameterDirection.Input; Email.Value = t.UserContact.Email;

            SqlParameter MobilePhone = new SqlParameter("MobilePhone", SqlDbType.NVarChar);
            MobilePhone.Direction = ParameterDirection.Input; MobilePhone.Value = t.UserContact.MobilePhone;


            SqlParameter CreateTime = new SqlParameter("CreateTime", SqlDbType.DateTime);
            CreateTime.Direction = ParameterDirection.Input; CreateTime.Value = DateTime.Parse(t.CreateTime);


          
            SqlParameter[] sparray = new SqlParameter[]
                  {
                    UserID
                    ,UserType
                    ,IsActivited
                    ,ActivitedByUserID
                   ,PasswordMD5
		           ,PersonName
                   ,Email
		           ,MobilePhone
                   ,CreateTime
                   ,Version
                  };

            SqlServerProvider ssp = new SqlServerProvider();

            try
            {
                if (ssp.SPExcuteNoneQuery("spupdateusercontact", sparray, out outvalues))
                {
                    isupdateok= true;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return isupdateok;
        }
       
        public List<LoginUser> GetAllUser()
        {
            List<LoginUser> listUsers = new List<LoginUser>();

            Dictionary<string, object> outVals = new Dictionary<string, object>();
            DataSet ds;
  
            SqlServerProvider ssp = new SqlServerProvider();
            SqlParameter[] parms = { };
            ds = ssp.SPGetDataSet("spgetallusers", parms, out outVals);
           
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    try
                    {
                        PersonInfo uc = null; 
                            uc = new PersonInfo(dr["PersonName"].ToString(), dr["MobilePhone"].ToString(), dr["Email"].ToString());
                             uc.Introduction = dr["Introduction"].ToString();
                             LoginUser   user = new LoginUser(dr["UserID"].ToString(), dr["PasswordMD5"].ToString(), 
                             int.Parse(dr["HeadImageIndex"].ToString()),(EUserType)int.Parse(dr["UserType"].ToString()), bool.Parse(dr["IsActivited"].ToString()), uc,  (DateTime.Parse(dr["CreateTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss")),(dr["ActivitedByUserID"].ToString()));
                          //user.CheckType = (CheckType)int.Parse(dr["CheckType"].ToString());
                          user.Version = int.Parse(dr["Version"].ToString());//添加版本
                          //user.IsChecking = bool.Parse(dr["IsChecking"].ToString());
                        if(dr["HeadImageData"]!=null&&dr["HeadImageData"].ToString()!="")
                          user.HeadImageData = (byte[])dr["HeadImageData"];
                                if (user != null)
                                {
                                    listUsers.Add(user);
                                }
                        
                    }
                    catch (System.Exception ex)
                    {
                        return null;
                    }


                }
            }
            return listUsers;
        }
        //public List<GGUser> GetAllUser()
        //{
        //    List<GGUser> list = new List<GGUser>();
        //    using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
        //    {
        //        IOrmAccesser<GGUser> accesser = scope.NewOrmAccesser<GGUser>();
        //        list = accesser.GetAll();
        //        scope.Commit();
        //    }
        //    return list;
        //}

        
        

      

        public LoginUser GetUser(string userID)
        {
             Dictionary<string, object> outVals = new Dictionary<string, object>();
            DataSet ds;  LoginUser user = null;
     
                SqlServerProvider ssp = new SqlServerProvider();
                SqlParameter sqmdid = new SqlParameter("UserID", userID);

                SqlParameter[] parms = { sqmdid };
                ds = ssp.SPGetDataSet("spgetuserbyuserid", parms, out outVals);
          
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    try
                    {
                        PersonInfo uc = null; 
                             uc = new PersonInfo(dr["PersonName"].ToString(), dr["MobilePhone"].ToString(), dr["Email"].ToString());
                             uc.Introduction = dr["Introduction"].ToString();
                        
                             user = new LoginUser(dr["UserID"].ToString(), dr["PasswordMD5"].ToString(),
                              int.Parse(dr["HeadImageIndex"].ToString()),
                               (EUserType)int.Parse(dr["UserType"].ToString()), bool.Parse(dr["IsActivited"].ToString()), uc);
                             //user.CheckType = (CheckType)int.Parse(dr["CheckType"].ToString());
                             user.Version = int.Parse(dr["Version"].ToString());//添加版本
                             //user.IsChecking = bool.Parse(dr["IsChecking"].ToString());
                             if (dr["HeadImageData"] != null && dr["HeadImageData"].ToString() != "")
                                 user.HeadImageData = (byte[])dr["HeadImageData"];
                    }
                    catch (System.Exception ex)
                    {
                        return null;
                    }
                    

                }
            }
                return user;
        }
        

        public string GetUserPassword(string userID)
        {
            object pwd = null;
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<LoginUser> accesser = scope.NewOrmAccesser<LoginUser>();
                pwd = accesser.GetColumnValue(userID, LoginUser._PasswordMD5);
                scope.Commit();
            }
            if (pwd == null)
            {
                return null;
            }
            return pwd.ToString();
        }

        
         public byte[] GetSingleBitmapsbypath(string path,string type)
        {
            byte[] result;
            try
             {
	             if (!File.Exists(path))
	             {
	                     return new byte[0];
	             }
                 else
                 {
                         Bitmap bm = new Bitmap(path);
                         using( MemoryStream ms=new MemoryStream())
                         {
                             switch (type)
                             {
                                 case "jpeg":
                                 case "jpg":
                                     bm.Save(ms, ImageFormat.Jpeg);
                                     break;
                                 case "png":
                                     bm.Save(ms, ImageFormat.Png);
                                     break;
                                 case "bmp":
                                     bm.Save(ms, ImageFormat.Bmp);
                                     break;
                                 case "gif":
                                     bm.Save(ms, ImageFormat.Gif);
                                     break;
                                 default: break;
                             }
                             if (ms == null)
                             {
                                 return null;
                             }
                             else
                                 result=ms.ToArray();
                         }
                        

                     }
                
             }
             catch (System.Exception ex)
             {
                 result = null;
             }
            return result;
        }
        //public List<byte[]> GetSmallBitmapsbypath(ReadingPicture rp, int piccount)
        //{
        //    List<byte[]> listMaps = new List<byte[]>();
        //    try
        //    {
        //        if (!File.Exists(rp.PicturePath))
        //        {
        //            return listMaps;//为空
        //        }
        //        else
        //        {
        //            string pathfix = rp.PicturePath.Substring(0, rp.PicturePath.Length - 2);
        //            for (int i = 0; i < piccount; i++)
        //            {
        //                Bitmap bm = new Bitmap(pathfix + "-" + (i + 1).ToString());
        //                Image small = ThumbnailMaker.MakeThumbnail(bm, 200, 200, ThumbnailMode.UsrHeightWidthBound);
        //                using (MemoryStream ms = new MemoryStream())
        //                {
        //                    rp.FileType = rp.FileType.ToLower();
        //                    switch (rp.FileType)
        //                    {
        //                        case "jpeg":
        //                        case "jpg":
        //                            small.Save(ms, ImageFormat.Jpeg);
        //                            break;
        //                        case "png":
        //                            small.Save(ms, ImageFormat.Png);
        //                            break;
        //                        case "bmp":
        //                            small.Save(ms, ImageFormat.Bmp);
        //                            break;
        //                        case "gif":
        //                            small.Save(ms, ImageFormat.Gif);
        //                            break;
        //                        default: break;
        //                    }
        //                    if (ms == null)
        //                    {
        //                        return null;
        //                    }
        //                    else
        //                        listMaps.Add(ms.ToArray());
        //                }


        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        listMaps = null;
        //    }
        //    return listMaps;

        //}
        //public List<byte[]> GetBitmapsbypath(ReadingPicture rp,int piccount)
        //{
        //    List<byte[]> listMaps = new List<byte[]>();
        //    try
        //     {
        //         if (!File.Exists(rp.PicturePath))
        //         {
        //                        return null;
        //         }
        //         else
        //         {
        //             string pathfix = rp.PicturePath.Substring(0, rp.PicturePath.Length - 2);
        //             for (int i = 0; i < piccount; i++)
        //             {
        //                 Bitmap bm = new Bitmap(pathfix + "-"+(i + 1).ToString());
                       
        //                 using( MemoryStream ms=new MemoryStream())
        //                 {
        //                     rp.FileType = rp.FileType.ToLower();
        //                     switch (rp.FileType)
        //                     {
        //                         case "jpeg":
        //                         case "jpg":
        //                             bm.Save(ms, ImageFormat.Jpeg);
        //                             break;
        //                         case "png":
        //                             bm.Save(ms, ImageFormat.Png);
        //                             break;
        //                         case "bmp":
        //                             bm.Save(ms, ImageFormat.Bmp);
        //                             break;
        //                         case "gif":
        //                             bm.Save(ms, ImageFormat.Gif);
        //                             break;
        //                         default: break;
        //                     }
        //                     if (ms == null)
        //                     {
        //                         return null;
        //                     }
        //                     else
        //                         listMaps.Add(ms.ToArray());
        //                 }
                        

        //             }
        //         }
        //     }
        //     catch (System.Exception ex)
        //     {
        //         listMaps = null;
        //     }
        //    return listMaps;
            
        //}
        
       
       
      public bool DeleteUser(string deluserid)
      {
          bool result = true;
          Dictionary<string, object> outvalues = null;
          SqlParameter UserID = new SqlParameter("UserID", SqlDbType.NVarChar);
          UserID.Direction = ParameterDirection.Input; UserID.Value = deluserid;


          SqlParameter[] sparray = new SqlParameter[]
                  {
                    UserID
                  };
          SqlServerProvider ssp = new SqlServerProvider();

          try
          {
              if (ssp.SPExcuteNoneQuery("spdeluserbyid", sparray, out outvalues))//
              {
                  result = true;
              }
          }
          catch (System.Exception ex)
          {
              result = false;
          }
          return result;
      }
      public bool UpdatePasswd(string userID, string passMD5)
      {
          bool result = false;
          Dictionary<string, object> outvalues = null;
          SqlParameter UserID = new SqlParameter("UserID", SqlDbType.NVarChar);
          UserID.Direction = ParameterDirection.Input; UserID.Value = userID;

          SqlParameter PasswdMD5 = new SqlParameter("PasswdMD5", SqlDbType.NVarChar);
          PasswdMD5.Direction = ParameterDirection.Input; PasswdMD5.Value = passMD5;

          SqlParameter[] sparray = new SqlParameter[]
                  {
                    UserID
                   ,PasswdMD5
                  };
          SqlServerProvider ssp = new SqlServerProvider();

          try
          {
              if (ssp.SPExcuteNoneQuery("spupdatepasswd", sparray, out outvalues))//更新成功
              {
                  result = true;
              }
          }
          catch (System.Exception ex)
          {
              result = false;
          }
          return result;
      }
     
        public  bool InsertAdvice(string userid, string advice)
        {
            bool result = false;
            Dictionary<string, object> outvalues = null;
            SqlParameter UserID = new SqlParameter("UserID", SqlDbType.NVarChar);
            UserID.Direction = ParameterDirection.Input; UserID.Value = userid;

            SqlParameter Advice = new SqlParameter("Advice", SqlDbType.NVarChar);
            Advice.Direction = ParameterDirection.Input; Advice.Value = advice;

            SqlParameter[] sparray = new SqlParameter[]
                  {
                    UserID
                    ,Advice
                  };

            SqlServerProvider ssp = new SqlServerProvider();

            try
            {
                if (ssp.SPExcuteNoneQuery("spinsertadvice", sparray, out outvalues))//更新成功
                {
                    result = true;
                }
            }
            catch (System.Exception ex)
            {
                result = false;
            }
            return result;
        }
        
    }
   
}
