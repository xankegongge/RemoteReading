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
using RemoteReading.Core;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using DataProvider;
using JustLib;
namespace RemoteReading.Server
{  
    /// <summary>
    /// 真实数据库。
    /// </summary>
    public class RealDB : DefaultChatRecordPersister, IDBPersister
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
        //插入一条图片备注信息,返回guid
        private  Guid InsertPitureMark(PictureMark pm)
        {
             Guid returngid =Guid.Empty;
            
              Dictionary<string,object> outvalues=new Dictionary<string,object>();
              
                SqlParameter PictureScale=new SqlParameter("PictureScale",SqlDbType.Float);
                PictureScale.Direction=ParameterDirection.Input;PictureScale.Value=pm.PictureScale;

               SqlParameter Remark=new SqlParameter("Remark",SqlDbType.NText);
                Remark.Direction=ParameterDirection.Input;Remark.Value=pm.Remark;

                SqlParameter RotateCount=new SqlParameter("RotateCount",SqlDbType.Int);
                RotateCount.Direction=ParameterDirection.Input;RotateCount.Value=pm.RotateCount;

                SqlParameter MarkColor = new SqlParameter("MarkColor", SqlDbType.NVarChar);
                MarkColor.Direction = ParameterDirection.Input; MarkColor.Value = pm.MarkColor;

                SqlParameter MarkLocation = new SqlParameter("MarkLocation", SqlDbType.NVarChar);
                MarkLocation.Direction = ParameterDirection.Input; MarkLocation.Value = pm.MarkLocation;

                SqlParameter MarkVision = new SqlParameter("MarkVision", SqlDbType.NVarChar);
                MarkVision.Direction = ParameterDirection.Input; MarkVision.Value = pm.MarkVision;    

                SqlParameter ReturnPictureMarkID=new SqlParameter("ReturnPictureMarkID",SqlDbType.UniqueIdentifier);
                ReturnPictureMarkID.Direction=ParameterDirection.Output;


                SqlParameter[] sparray = new SqlParameter[]
                  {
                 
		           PictureScale
		           ,RotateCount
		           ,Remark
                   ,MarkColor
                   ,MarkLocation
                   ,MarkVision
                   ,ReturnPictureMarkID
                  };
           
               SqlServerProvider ssp=new SqlServerProvider();
                
                try
                {
                     if(ssp.SPExcuteNoneQuery("spinsertpicturemark",sparray,out outvalues))
                         returngid = new Guid((outvalues["ReturnPictureMarkID"].ToString()));
                }
                catch (System.Exception ex)
                {
                    returngid = Guid.Empty;
                }
                return returngid;
        }
        
        //插入单个图片信息,返回图片GUID
        private  bool  InsertReadingPicture(ReadingPicture rp,string picpath)
        {
             bool  returngid = false;
              Dictionary<string,object> outvalues=new Dictionary<string,object>();

              SqlParameter ReadingPictureID = new SqlParameter("ReadingPictureID", SqlDbType.UniqueIdentifier);
              ReadingPictureID.Direction = ParameterDirection.Input; ReadingPictureID.Value = new Guid(rp.ReadingPictureID);

               SqlParameter PicturePath=new SqlParameter("PicturePath",SqlDbType.NVarChar);
                PicturePath.Direction=ParameterDirection.Input;PicturePath.Value=rp.PicturePath;
            
                SqlParameter DyedMethodID=new SqlParameter("DyedMethodID",SqlDbType.Int);
                DyedMethodID.Direction=ParameterDirection.Input;DyedMethodID.Value=rp.DyedMethodID;

                SqlParameter SamplesTypeID=new SqlParameter("SamplesTypeID",SqlDbType.Int);
                SamplesTypeID.Direction=ParameterDirection.Input;SamplesTypeID.Value=rp.SamplesTypeID;

                SqlParameter ZoomID=new SqlParameter("ZoomID",SqlDbType.Int);
                ZoomID.Direction=ParameterDirection.Input;ZoomID.Value=rp.ZoomID;
 
                SqlParameter ClientPictureMarksCount=new SqlParameter("ClientPictureMarksCount",SqlDbType.Int);
                ClientPictureMarksCount.Direction=ParameterDirection.Input;ClientPictureMarksCount.Value=rp.ClientPictureMarksCount;
 
                SqlParameter ClientPictureMarks=new SqlParameter("ClientPictureMarks",SqlDbType.NVarChar);
                ClientPictureMarks.Direction=ParameterDirection.Input;ClientPictureMarks.Value=rp.ClientPictureMarks;
                
                SqlParameter ExpertPictureMarksCount=new SqlParameter("ExpertPictureMarksCount",SqlDbType.Int);
                ExpertPictureMarksCount.Direction=ParameterDirection.Input;ExpertPictureMarksCount.Value=rp.ExpertPictureMarksCount;
                
                SqlParameter ExpertPictureMarks=new SqlParameter("ExpertPictureMarks",SqlDbType.NVarChar);
                ExpertPictureMarks.Direction=ParameterDirection.Input;ExpertPictureMarks.Value=rp.ExpertPictureMarks;

                SqlParameter FileType = new SqlParameter("FileType", SqlDbType.NVarChar);
                FileType.Direction = ParameterDirection.Input; FileType.Value = rp.FileType;


                SqlParameter ClientNote = new SqlParameter("ClientNote", SqlDbType.NText);
                ClientNote.Direction = ParameterDirection.Input; ClientNote.Value = rp.ClientNote;


               
               

                SqlParameter[] sparray = new SqlParameter[]
                  {
                      ReadingPictureID,
                    PicturePath
                   ,DyedMethodID
                   ,SamplesTypeID
                   ,ZoomID
		           ,ClientPictureMarksCount
		           ,ClientPictureMarks
		           ,ExpertPictureMarksCount
		           ,ExpertPictureMarks
                   ,FileType
                   ,ClientNote
                   
                  };
           
               SqlServerProvider ssp=new SqlServerProvider();
                
                try
                {
                    if (ssp.SPExcuteNoneQuery("spinsertreadingpicture", sparray, out outvalues))
                    {
                        returngid = true;
                    }
                      //  returngid = new Guid(outvalues["ReturnReadingPictureID"].ToString());
                }
                catch (System.Exception ex)
                {
                    returngid = false;
                }
                return returngid;
                  
        }

        private string picFixSavePath =    @"C:\RemoteaPicData\" ;
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
        //插入一次阅片信息,包含多组图片返回阅片
        public MedicalReading InsertMedicalReading(MedicalReading mr)
        {
            string returnid = null; int count = 0;
            int picsavenum = 1;
            if (mr.ListPics == null)
            {
                return null;
            }
            List<ReadingPicture> listPics = mr.ListPics;
            StringBuilder sbpic = new StringBuilder();
            System.Random aa = new Random();
                int randomfilename=aa.Next(10000, 99999);
            foreach (ReadingPicture rp in listPics)
            {
                List<PictureMark> listPms = rp.ListMarks;
                if(listPms!=null&&listPms.Count>0)
                { 
                    StringBuilder sb=new StringBuilder();
                    //插入客户标注
                    foreach(PictureMark pm in listPms)
                    { 
                        
                         Guid  i=InsertPitureMark(pm);
                         if (i ==Guid.Empty)
                             return null;
                         else
                         {
                             count++;//客户标注个数
                             sb.Append(i.ToString()+";");
                         }
                    }
                    rp.ClientPictureMarksCount = count;
                    rp.ClientPictureMarks=sb.ToString();
                }
                //插入专家标注
                count = 0;
                List<PictureMark> listExpertMarks = rp.ListExpertMarks;
                if (listExpertMarks != null && listExpertMarks.Count > 0)
                {
                    StringBuilder sbexp = new StringBuilder();
                    //int count=0;
                    foreach (PictureMark pm in listExpertMarks)
                    {

                        Guid pmID = InsertPitureMark(pm);
                        if (pmID==Guid.Empty)
                            return null;
                        else
                        {
                               count++;//专家标注个数
                            sbexp.Append(pmID.ToString() + ";");
                        }
                    }
                    rp.ExpertPictureMarksCount = count;
                    rp.ExpertPictureMarks = sbexp.ToString();
                }
                //先保存图片，得到路径(包含随机产生的5位数字字符串），然后插入图片信息至数据库中
                //Bitmap original = new Bitmap(200, 200);
                //Bitmap copy = new Bitmap(rp.Image.Width, rp.Image.Height,System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                //using (Graphics graphics = Graphics.FromImage(copy))
                //{
                //    Rectangle imageRectangle = new Rectangle(0, 0, copy.Width, copy.Height);
                //    graphics.DrawImage(rp.Image, imageRectangle, imageRectangle, GraphicsUnit.Pixel);
                //}
               // Bitmap bmp =(Bitmap) rp.Image.Clone();
               // copy.Save(@"C:\1.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                string picSavepath = SaveMedicaPic(mr.UserIDFrom,mr.UserIDTo,rp.Imagebyte,rp.FileType, randomfilename.ToString(), picsavenum++);

                if (picSavepath == null)
                {
                    return null;
                }
                if (picSavepath != null)//保存完图片，开始插入数据库
                {
                    rp.PicturePath = picSavepath;//保存路径，并清空图片
                    //if (rp.Image != null)
                    //    rp.Image.Dispose();//释放图片
                    if (rp.Imagebyte != null)
                    {
                        rp.Imagebyte = null;//清空图片字节流；
                    }
                  if (!InsertReadingPicture(rp, picSavepath))
                        return null;
                    else
                    {
                        sbpic.Append(rp.ReadingPictureID + ";");
                       // rp.ReadingPictureID = picID.ToString();
                    }
                }
            }
            mr.MedicalPictrues = sbpic.ToString();//插入图片ID列表;
           
            Dictionary<string, object> outvalues = new Dictionary<string, object>();
            SqlParameter MedicalReadingID = new SqlParameter("MedicalReadingID", SqlDbType.UniqueIdentifier);
            MedicalReadingID.Direction = ParameterDirection.Input; MedicalReadingID.Value = new Guid(mr.MedicalReadingID);

            SqlParameter UserIDFrom = new SqlParameter("UserIDFrom", SqlDbType.NVarChar);
            UserIDFrom.Direction = ParameterDirection.Input; UserIDFrom.Value = mr.UserIDFrom;

            SqlParameter UserIDTo = new SqlParameter("UserIDTo", SqlDbType.NVarChar);
            UserIDTo.Direction = ParameterDirection.Input; UserIDTo.Value = mr.UserIDTo;

            SqlParameter CreatedTime = new SqlParameter("CreatedTime", SqlDbType.DateTime);
            CreatedTime.Direction = ParameterDirection.Input; CreatedTime.Value = mr.CreatedTime;

            SqlParameter ReadingStatus = new SqlParameter("ReadingStatus", SqlDbType.Int);
            ReadingStatus.Direction = ParameterDirection.Input; ReadingStatus.Value =(int) mr.ReadingStatus;

            SqlParameter MedicalPictureCount = new SqlParameter("MedicalPictureCount", SqlDbType.Int);
            MedicalPictureCount.Direction = ParameterDirection.Input; MedicalPictureCount.Value = mr.ListPics.Count;

            SqlParameter MedicalPictrues = new SqlParameter("MedicalPictrues", SqlDbType.NVarChar);
            MedicalPictrues.Direction = ParameterDirection.Input; MedicalPictrues.Value = mr.MedicalPictrues;



            
            SqlParameter[] sparray = new SqlParameter[]
                  {
                    MedicalReadingID,
                    UserIDFrom
                   ,UserIDTo
                   ,ReadingStatus
                   ,CreatedTime
                   ,MedicalPictureCount
		           ,MedicalPictrues
                  
                  };

            SqlServerProvider ssp = new SqlServerProvider();

            try
            {
                if (ssp.SPExcuteNoneQuery("spinsertmedicalreading", sparray, out outvalues))
                {
                    //returnid = (outvalues["ReturnMedicalReadingID"].ToString());
                    //mr.MedicalReadingID = returnid;
                }
            }
            catch (System.Exception ex)
            {
               return  null;
            }
               
            return mr;
        }

        public bool InsertUser(GGUser t,string friends,string activitedbyuserID)
        {
            
            Dictionary<string, object> outvalues = new Dictionary<string, object>();
        
            SqlParameter UserID = new SqlParameter("UserID", SqlDbType.NVarChar);
            UserID.Direction = ParameterDirection.Input; UserID.Value = t.UserID;

            SqlParameter Friends = new SqlParameter("Friends", SqlDbType.NVarChar);
            Friends.Direction = ParameterDirection.Input; Friends.Value = friends;

            SqlParameter UserType = new SqlParameter("UserType", SqlDbType.Int);
            UserType.Direction = ParameterDirection.Input; UserType.Value =(int) t.UserType;

            SqlParameter IsActivited = new SqlParameter("IsActivited", SqlDbType.Bit);
            IsActivited.Direction = ParameterDirection.Input; IsActivited.Value = t.IsActivited;

            SqlParameter ActivitedByUserID = new SqlParameter("ActivitedByUserID", SqlDbType.NVarChar);
            ActivitedByUserID.Direction = ParameterDirection.Input; ActivitedByUserID.Value = activitedbyuserID;

            SqlParameter PasswordMD5 = new SqlParameter("PasswordMD5", SqlDbType.NVarChar);
            PasswordMD5.Direction = ParameterDirection.Input; PasswordMD5.Value = t.PasswordMD5;

            SqlParameter HeadImageIndex = new SqlParameter("HeadImageIndex", SqlDbType.Int);
            HeadImageIndex.Direction = ParameterDirection.Input; HeadImageIndex.Value = t.HeadImageIndex;

            SqlParameter PersonName = new SqlParameter("PersonName", SqlDbType.NVarChar);
            PersonName.Direction = ParameterDirection.Input; PersonName.Value = t.UserContact.PersonName;

            SqlParameter Email = new SqlParameter("Email", SqlDbType.NVarChar);
            Email.Direction = ParameterDirection.Input; Email.Value = t.UserContact.Email;

            SqlParameter MobilePhone = new SqlParameter("MobilePhone", SqlDbType.NVarChar);
            MobilePhone.Direction = ParameterDirection.Input; MobilePhone.Value = t.UserContact.MobilePhone;

          

            SqlParameter ProfessionTitle = new SqlParameter("ProfessionTitle", SqlDbType.Int);
            ProfessionTitle.Direction = ParameterDirection.Input; ProfessionTitle.Value =(int) t.UserContact.ProfessionTitle;
            
            SqlParameter HospitalID = new SqlParameter("HospitalID", SqlDbType.Int);
            HospitalID.Direction = ParameterDirection.Input; HospitalID.Value =t.UserContact.HospitalID;

            SqlParameter CreateTime = new SqlParameter("CreateTime", SqlDbType.DateTime);
            CreateTime.Direction = ParameterDirection.Input; CreateTime.Value = DateTime.Parse(t.CreateTime);

            SqlParameter IsChecking = new SqlParameter("IsChecking", SqlDbType.Bit);
            IsChecking.Direction = ParameterDirection.Input; IsChecking.Value = t.IsChecking;

            SqlParameter CheckType = new SqlParameter("CheckType", SqlDbType.Int);
            CheckType.Direction = ParameterDirection.Input; CheckType.Value = t.CheckType;
            SqlParameter[] sparray = new SqlParameter[]
                  {
                    UserID
                    ,UserType
                    ,Friends
                    ,IsActivited
                    ,ActivitedByUserID
                   ,PasswordMD5
                   ,HeadImageIndex
		           ,PersonName
                   ,Email
		           ,MobilePhone
                   ,ProfessionTitle
                   ,HospitalID
                   ,CreateTime
                   ,IsChecking
                   ,CheckType
                  };

            SqlServerProvider ssp = new SqlServerProvider();

            try
            {
                if (ssp.SPExcuteNoneQuery("spinsertuser", sparray, out outvalues))
                {
                        return true;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return false;
        }

       
        private IDictionary<int, Hospital> iHospDic=new Dictionary<int,Hospital>();
        public IList<Hospital> GetHospitals()//得到hospital列表
        {
            Dictionary<string, object> outVals = new Dictionary<string, object>();
            DataSet ds;
            IList<Hospital> ilHospitals = new List<Hospital>();

            SqlServerProvider ssp = new SqlServerProvider();
            SqlParameter[] parms = {  };
            ds = ssp.SPGetDataSet("spgethospitals", parms, out outVals);
            
             if (ds.Tables[0].Rows.Count > 0)
             {
                 foreach (DataRow dr in ds.Tables[0].Rows)
                 {
                        try
                        {
                            Hospital hs = new Hospital(int.Parse(dr["HospitalID"].ToString()), dr["HospitalName"].ToString(), dr["Province"].ToString(), dr["City"].ToString(),int.Parse(dr["Version"].ToString()));
                            if (hs != null)
                            {
                                ilHospitals.Add(hs);
                                iHospDic.Add(hs.HospitalID, hs);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            return null;
                        }
                 }            
             }
             return ilHospitals;
        }
       
        public void InsertGroup(GGGroup t)
        {
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<GGGroup> accesser = scope.NewOrmAccesser<GGGroup>();
                accesser.Insert(t);
                scope.Commit();
            }
        }

        public void DeleteGroup(string groupID)
        {
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<GGGroup> accesser = scope.NewOrmAccesser<GGGroup>();
                accesser.Delete(groupID);
                scope.Commit();
            }
        }
        /// <summary>
        /// 更新用户信息（好友，小组友、密码、昵称，签名
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool UpdateUser(GGUser t)
        {
            bool isupdateok = false;
            Dictionary<string, object> outvalues = null;

            SqlParameter UserID = new SqlParameter("UserID", SqlDbType.NVarChar);
            UserID.Direction = ParameterDirection.Input; UserID.Value = t.UserID;

            SqlParameter PasswordMD5 = new SqlParameter("PasswordMD5", SqlDbType.NVarChar);
            PasswordMD5.Direction = ParameterDirection.Input; PasswordMD5.Value = t.PasswordMD5;

            SqlParameter Friends = new SqlParameter("Friends", SqlDbType.NVarChar);
            Friends.Direction = ParameterDirection.Input; Friends.Value = t.Friends;

            SqlParameter Name = new SqlParameter("Name", SqlDbType.NVarChar);
            Name.Direction = ParameterDirection.Input; Name.Value = t.Name;

            SqlParameter Groups = new SqlParameter("Groups", SqlDbType.NVarChar);
            Groups.Direction = ParameterDirection.Input; Groups.Value = t.Groups;

            SqlParameter Signature = new SqlParameter("Signature", SqlDbType.NVarChar);
            Signature.Direction = ParameterDirection.Input; Signature.Value = t.Signature;

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
                    Friends,
                    Name,
                    Groups,
                    Signature,
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
        public bool UpdateUserContactInfo(GGUser t)
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



            SqlParameter ProfessionTitle = new SqlParameter("ProfessionTitle", SqlDbType.Int);
            ProfessionTitle.Direction = ParameterDirection.Input; ProfessionTitle.Value = (int)t.UserContact.ProfessionTitle;

            SqlParameter HospitalID = new SqlParameter("HospitalID", SqlDbType.Int);
            HospitalID.Direction = ParameterDirection.Input; HospitalID.Value = t.UserContact.HospitalID;



            SqlParameter CreateTime = new SqlParameter("CreateTime", SqlDbType.DateTime);
            CreateTime.Direction = ParameterDirection.Input; CreateTime.Value = DateTime.Parse(t.CreateTime);


            SqlParameter CheckType = new SqlParameter("CheckType", SqlDbType.Int);
            CheckType.Direction = ParameterDirection.Input; CheckType.Value = t.CheckType;

            SqlParameter IsChecking = new SqlParameter("IsChecking", SqlDbType.Bit);
            IsChecking.Direction = ParameterDirection.Input; IsChecking.Value = t.IsChecking;

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
                   ,ProfessionTitle
                   ,HospitalID
                   ,CreateTime
                   ,CheckType
                   ,IsChecking
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
        public bool UpdateUserFriends(GGUser t)
        {
          
           bool isupdateok = false;
           Dictionary<string, object> outvalues = null;

           SqlParameter UserID = new SqlParameter("UserID", SqlDbType.NVarChar);
           UserID.Direction = ParameterDirection.Input; UserID.Value = t.UserID;

           SqlParameter Friends = new SqlParameter("Friends", SqlDbType.NVarChar);
           Friends.Direction = ParameterDirection.Input; Friends.Value = t.Friends;


           SqlParameter[] sparray = new SqlParameter[]
                  {
                    UserID,
                    Friends
                  };
           SqlServerProvider ssp = new SqlServerProvider();

           try
           {
               if (ssp.SPExcuteNoneQuery("spupdateuserfriends", sparray, out outvalues))//更新成功
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

        public bool UpdateGroup(GGGroup t)
        {
            //using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            //{
            //    IOrmAccesser<GGGroup> accesser = scope.NewOrmAccesser<GGGroup>();
            //    accesser.Update(t);
            //    scope.Commit();
            //}
            bool result = false;
            Dictionary<string, object> outvalues = null;
            SqlParameter GroupID = new SqlParameter("GroupID", SqlDbType.NVarChar);
            GroupID.Direction = ParameterDirection.Input; GroupID.Value = t.GroupID;

            SqlParameter Name = new SqlParameter("Name", SqlDbType.NVarChar);
            Name.Direction = ParameterDirection.Input; Name.Value = t.Name;

            SqlParameter CreatorID = new SqlParameter("CreatorID", SqlDbType.NVarChar);
            CreatorID.Direction = ParameterDirection.Input; CreatorID.Value = t.CreatorID;

            SqlParameter Announce = new SqlParameter("Announce", SqlDbType.NVarChar);
            Announce.Direction = ParameterDirection.Input; Announce.Value = t.Announce;

            SqlParameter Members = new SqlParameter("Members", SqlDbType.NVarChar);
            Members.Direction = ParameterDirection.Input; Members.Value = t.Members;

            SqlParameter CreateTime = new SqlParameter("CreateTime", SqlDbType.DateTime);
            CreateTime.Direction = ParameterDirection.Input; CreateTime.Value = DateTime.Parse(t.CreateTime);

            SqlParameter Version = new SqlParameter("Version", SqlDbType.Int);
            Version.Direction = ParameterDirection.Input; Version.Value = t.Version;

         
            SqlParameter[] sparray = new SqlParameter[]
                  {
                    GroupID
                   ,Name
                   ,CreatorID
                   ,Announce
                   ,Members
                   ,CreateTime
                   ,Version
                  };
            SqlServerProvider ssp = new SqlServerProvider();

            try
            {
                if (ssp.SPExcuteNoneQuery("spupdategroup", sparray, out outvalues))//更新成功
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

        public List<GGUser> GetAllUser()
        {
            List<GGUser> listUsers = new List<GGUser>();

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
                        UserContact uc = null; Hospital hs = null;
                        if (dr["HospitalID"] == DBNull.Value || dr["ProfessionTitle"] == DBNull.Value)//可能是客服或者管理员
                        {
                            uc = new UserContact(dr["PersonName"].ToString(), dr["MobilePhone"].ToString(), dr["Email"].ToString());

                        }
                        else
                        {
                            int hospid = int.Parse(dr["HospitalID"].ToString());
                            int title = int.Parse(dr["ProfessionTitle"].ToString());
                            if (iHospDic.Count > 0)//有东西;
                            {
                                 hs = iHospDic[hospid];//通过id获取Hospital对象
                            }
                             uc = new UserContact(dr["PersonName"].ToString(), dr["MobilePhone"].ToString(), dr["Email"].ToString(),
                              (EProfessionTitle)title, hospid);
                             uc.Introduction = dr["Introduction"].ToString();
                        }
                          GGUser   user = new GGUser(dr["UserID"].ToString(), dr["PasswordMD5"].ToString(), dr["Name"].ToString(),
                              dr["Friends"].ToString(), dr["Signature"].ToString(), int.Parse(dr["HeadImageIndex"].ToString()),
                              dr["Groups"].ToString(), (EUserType)int.Parse(dr["UserType"].ToString()), bool.Parse(dr["IsActivited"].ToString()), uc, hs, (DateTime.Parse(dr["CreateTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss")),(dr["ActivitedByUserID"].ToString()));
                          user.CheckType = (CheckType)int.Parse(dr["CheckType"].ToString());
                          user.Version = int.Parse(dr["Version"].ToString());//添加版本
                          user.IsChecking = bool.Parse(dr["IsChecking"].ToString());
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

        public List<GGGroup> GetAllGroup()
        {
            List<GGGroup> list = new List<GGGroup>();
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<GGGroup> accesser = scope.NewOrmAccesser<GGGroup>();
                list = accesser.GetAll();
                scope.Commit();
            }
            return list;
        }

        

        public bool ChangeUserGroups(string userID, string groups)
        {
            //using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            //{
            //    IOrmAccesser<GGUser> accesser = scope.NewOrmAccesser<GGUser>();
            //    accesser.Update(new ColumnUpdating(GGUser._Groups, groups), new Filter(GGUser._UserID, userID));
            //    scope.Commit();
            //}
            bool result = false;
            Dictionary<string, object> outvalues = null;
            SqlParameter UserID = new SqlParameter("UserID", SqlDbType.NVarChar);
            UserID.Direction = ParameterDirection.Input; UserID.Value = userID;

            SqlParameter Groups = new SqlParameter("Groups", SqlDbType.NVarChar);
            Groups.Direction = ParameterDirection.Input; Groups.Value = groups;

            SqlParameter[] sparray = new SqlParameter[]
                  {
                    UserID
                   ,Groups
                  };
            SqlServerProvider ssp = new SqlServerProvider();

            try
            {
                if (ssp.SPExcuteNoneQuery("spupdategroups", sparray, out outvalues))//更新成功
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

        public void UpdateGroupInfo(GGGroup t)
        {
            this.UpdateGroup(t);
        }

        public GGUser GetUser(string userID)
        {
             Dictionary<string, object> outVals = new Dictionary<string, object>();
            DataSet ds;  GGUser user = null;
     
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

                        UserContact uc = null; Hospital hs = null;
                        if (dr["HospitalID"] == DBNull.Value || dr["ProfessionTitle"] == DBNull.Value)//可能是客服或者管理员
                        {
                            uc = new UserContact(dr["PersonName"].ToString(), dr["MobilePhone"].ToString(), dr["Email"].ToString());

                        }
                        else
                        {
                            int hospid = int.Parse(dr["HospitalID"].ToString());
                            int title = int.Parse(dr["ProfessionTitle"].ToString());
                            if (iHospDic.Count > 0)//有东西;
                            {
                                 hs = iHospDic[hospid];//通过id获取Hospital对象
                            }
                             uc = new UserContact(dr["PersonName"].ToString(), dr["MobilePhone"].ToString(), dr["Email"].ToString(),
                              (EProfessionTitle)title, hospid);
                             uc.Introduction = dr["Introduction"].ToString();
                        }
                             user = new GGUser(dr["UserID"].ToString(), dr["PasswordMD5"].ToString(), dr["Name"].ToString(),
                              dr["Friends"].ToString(), dr["Signature"].ToString(), int.Parse(dr["HeadImageIndex"].ToString()),
                              dr["Groups"].ToString(), (EUserType)int.Parse(dr["UserType"].ToString()), bool.Parse(dr["IsActivited"].ToString()), uc, hs);
                             user.CheckType = (CheckType)int.Parse(dr["CheckType"].ToString());
                             user.Version = int.Parse(dr["Version"].ToString());//添加版本
                             user.IsChecking = bool.Parse(dr["IsChecking"].ToString());
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
        //public GGUser GetUser(string userID)
        //{
        //    GGUser user = null;
        //    using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
        //    {
        //        IOrmAccesser<GGUser> accesser = scope.NewOrmAccesser<GGUser>();
        //        user = accesser.GetOne(userID);
        //        scope.Commit();
        //    }
        //    return user;
        //}

        public string GetUserPassword(string userID)
        {
            object pwd = null;
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<GGUser> accesser = scope.NewOrmAccesser<GGUser>();
                pwd = accesser.GetColumnValue(userID, GGUser._PasswordMD5);
                scope.Commit();
            }
            if (pwd == null)
            {
                return null;
            }
            return pwd.ToString();
        }

        public GGGroup GetGroup(string groupID)
        {
            GGGroup group = null;
            using (TransactionScope scope = this.transactionScopeFactory.NewTransactionScope())
            {
                IOrmAccesser<GGGroup> accesser = scope.NewOrmAccesser<GGGroup>();
                group = accesser.GetOne(groupID);
                scope.Commit();
            }
            return group;
        }
         public byte[] GetSingleBitmapsbypath(ReadingPicture rp)
        {
            byte[] result;
            try
             {
	             if (!File.Exists(rp.PicturePath))
	             {
	                     return new byte[0];
	             }
                 else
                 {
                         Bitmap bm = new Bitmap(rp.PicturePath);
                         using( MemoryStream ms=new MemoryStream())
                         {
                             rp.FileType = rp.FileType.ToLower();
                             switch (rp.FileType)
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
        public List<byte[]> GetSmallBitmapsbypath(ReadingPicture rp, int piccount)
        {
            List<byte[]> listMaps = new List<byte[]>();
            try
            {
                if (!File.Exists(rp.PicturePath))
                {
                    return listMaps;//为空
                }
                else
                {
                    string pathfix = rp.PicturePath.Substring(0, rp.PicturePath.Length - 2);
                    for (int i = 0; i < piccount; i++)
                    {
                        Bitmap bm = new Bitmap(pathfix + "-" + (i + 1).ToString());
                        Image small = ThumbnailMaker.MakeThumbnail(bm, 200, 200, ThumbnailMode.UsrHeightWidthBound);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            rp.FileType = rp.FileType.ToLower();
                            switch (rp.FileType)
                            {
                                case "jpeg":
                                case "jpg":
                                    small.Save(ms, ImageFormat.Jpeg);
                                    break;
                                case "png":
                                    small.Save(ms, ImageFormat.Png);
                                    break;
                                case "bmp":
                                    small.Save(ms, ImageFormat.Bmp);
                                    break;
                                case "gif":
                                    small.Save(ms, ImageFormat.Gif);
                                    break;
                                default: break;
                            }
                            if (ms == null)
                            {
                                return null;
                            }
                            else
                                listMaps.Add(ms.ToArray());
                        }


                    }
                }
            }
            catch (System.Exception ex)
            {
                listMaps = null;
            }
            return listMaps;

        }
        public List<byte[]> GetBitmapsbypath(ReadingPicture rp,int piccount)
        {
            List<byte[]> listMaps = new List<byte[]>();
            try
             {
	             if (!File.Exists(rp.PicturePath))
	             {
	                            return null;
	             }
                 else
                 {
                     string pathfix = rp.PicturePath.Substring(0, rp.PicturePath.Length - 2);
                     for (int i = 0; i < piccount; i++)
                     {
                         Bitmap bm = new Bitmap(pathfix + "-"+(i + 1).ToString());
                       
                         using( MemoryStream ms=new MemoryStream())
                         {
                             rp.FileType = rp.FileType.ToLower();
                             switch (rp.FileType)
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
                                 listMaps.Add(ms.ToArray());
                         }
                        

                     }
                 }
             }
             catch (System.Exception ex)
             {
                 listMaps = null;
             }
            return listMaps;
            
        }
        public MedicalReading GetMedicalReadingByGuid(string  gid)
        {
            MedicalReading mr=null;
             Dictionary<string, object> outVals = new Dictionary<string, object>();
            DataSet ds;
            try
            {
                SqlServerProvider ssp = new SqlServerProvider();
                  SqlParameter sqmdgid = new SqlParameter("MedicalReadingID", gid);

                  SqlParameter[] parms = { sqmdgid };
                ds = ssp.SPGetDataSet("spgetmedicalreadingbygid", parms, out outVals);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //foreach (DataRow dr in ds.Tables[0].Rows)
                    DataRow dr = ds.Tables[0].Rows[0];//选取第一行；
                    {
                        try
                        {
                            mr = new MedicalReading(dr["MedicalReadingID"].ToString(), dr["UserIDFrom"].ToString(), dr["UserIDTo"].ToString(), (DateTime.Parse(dr["CreatedTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"))
                                , (EReadingStatus)int.Parse(dr["ReadingStatus"].ToString()), int.Parse(dr["MedicalPictureCount"].ToString()), dr["MedicalPictrues"].ToString()
                                , bool.Parse(dr["IsRejected"].ToString()), dr["RejectedReason"].ToString());
                            if (mr != null)
                            {
                                if (!String.IsNullOrEmpty(mr.MedicalPictrues))
                                {
                                    string[] picsstr = mr.MedicalPictrues.Remove(mr.MedicalPictrues.Length - 1).Split(';');//获取图片编号列表
                                    foreach (string str in picsstr)
                                    {
                                        Guid gidpic = new Guid(str);
                                        DataSet dspic;
                                        SqlParameter sqgid = new SqlParameter("ReadingPictureID", gidpic);
                                        SqlParameter[] picparams = {sqgid
                                                               };
                                        dspic = ssp.SPGetDataSet("spgetreadingpicturesbyguid", picparams, out outVals);
                                        if (dspic.Tables[0].Rows.Count > 0)
                                        {
                                            foreach (DataRow drpic in dspic.Tables[0].Rows)
                                            {
                                                ReadingPicture rp = new ReadingPicture((drpic["ReadingPictureID"].ToString()), drpic["PicturePath"].ToString(), int.Parse(drpic["SamplesTypeID"].ToString())
                                                    , int.Parse(drpic["DyedMethodID"].ToString()), int.Parse(drpic["ZoomID"].ToString()), int.Parse(drpic["ClientPictureMarksCount"].ToString()), drpic["ClientPictureMarks"].ToString()
                                                    , int.Parse(drpic["ExpertPictureMarksCount"].ToString()), drpic["ExpertPictureMarks"].ToString(),drpic["FileType"].ToString(),
                                                    drpic["ClientNote"].ToString(), drpic["ExpertConclusion"].ToString());
                                                if (!String.IsNullOrEmpty(rp.ClientPictureMarks))
                                                {
                                                    string[] clientmarksstr = rp.ClientPictureMarks.Remove(rp.ClientPictureMarks.Length - 1).Split(';');//获取客户标注编号列表

                                                    foreach (string clientstr in clientmarksstr)
                                                    {
                                                        Guid clientmarkgid = new Guid(clientstr);
                                                        DataSet dsclient;
                                                        SqlParameter sqclientmarkgid = new SqlParameter("PictureMarkID", clientmarkgid);
                                                        SqlParameter[] clientparams = {sqclientmarkgid
                                                               };
                                                        dsclient = ssp.SPGetDataSet("spgetpicturemarksbyguid", clientparams, out outVals);
                                                        if (dsclient != null && dsclient.Tables[0].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow drclient in dsclient.Tables[0].Rows)
                                                            {
                                                                PictureMark pmclient = new PictureMark(drclient["PictureMarkID"].ToString()
                                                                       ,float.Parse(drclient["PictureScale"].ToString()), int.Parse(drclient["RotateCount"].ToString()), drclient["Remark"].ToString(),
                                                                        drclient["MarkLocation"].ToString(), drclient["MarkVision"].ToString(), drclient["MarkColor"].ToString());
                                                                if (pmclient != null)
                                                                {
                                                                    if (rp.ListMarks == null)
                                                                        rp.ListMarks = new List<PictureMark>();
                                                                    rp.ListMarks.Add(pmclient);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                if (!String.IsNullOrEmpty(rp.ExpertPictureMarks))
                                                {
                                                    string[] expertmarksstr = rp.ExpertPictureMarks.Remove(rp.ExpertPictureMarks.Length - 1).Split(';');//获取客户标注编号列表

                                                    foreach (string expertstr in expertmarksstr)
                                                    {
                                                        Guid expertmarkgid = new Guid(expertstr);
                                                        DataSet dsexpert;
                                                        SqlParameter sqexpertmarkgid = new SqlParameter("PictureMarkID", expertmarkgid);
                                                        SqlParameter[] expertparams = {sqexpertmarkgid
                                                               };
                                                        dsexpert = ssp.SPGetDataSet("spgetpicturemarksbyguid", expertparams, out outVals);
                                                        if (dsexpert != null && dsexpert.Tables[0].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow drexpert in dsexpert.Tables[0].Rows)
                                                            {
                                                                PictureMark pmexpert = new PictureMark((drexpert["PictureMarkID"].ToString())
                                                                     , int.Parse(dr["PictureScale"].ToString())
                                                                        , int.Parse(drexpert["RotateCount"].ToString()), drexpert["Remark"].ToString(),
                                                                          drexpert["MarkLocation"].ToString(), drexpert["MarkVision"].ToString(), drexpert["MarkColor"].ToString());
                                                                if (pmexpert != null)
                                                                {
                                                                    if (rp.ListExpertMarks == null)
                                                                        rp.ListExpertMarks = new List<PictureMark>();
                                                                    rp.ListExpertMarks.Add(pmexpert);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }//标签列表结束
                                                if (mr.ListPics == null)
                                                {
                                                    mr.ListPics = new List<ReadingPicture>();
                                                }
                                                mr.ListPics.Add(rp);
                                            }//图片列表结束

                                        }

                                    }
                                }

                            }
                           
                        }
                        catch (System.Exception ex)
                        {
                            mr= null;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                mr = null;
            }
            return mr;
        }
        public  List<MedicalReading> GetSomeMedicalReading(List<string> listguids)
        {
            List<MedicalReading> list = new List<MedicalReading>();
            foreach (string guid in listguids)
            {
                MedicalReading mr = GetMedicalReadingByGuid(guid);
                list.Add(mr);
            }
            return list;
        }
        public List<string> GetClientOrExpertMDGuids(string id, bool isexpert) 
        {
            List<string> list = null;
            SqlParameter spuserid = new SqlParameter("UserID", id);
            SqlParameter spisexpert = new SqlParameter("IsExpert", isexpert);
            SqlParameter[] parms = { spuserid,spisexpert };
            SqlServerProvider ssp = new SqlServerProvider();
           if(ssp.SPGetRecordValue("spgetclientorexpertmdguids", parms,"MedicalReadingID",out list))
           {
               return list;
           }
           return list;
        }
        //获取阅片列表进行缓存
       public IList<MedicalReading> GetAllMedicalReading()
        {
            IList<MedicalReading> listMedicalReading = new List<MedicalReading>();
            Dictionary<string, object> outVals = new Dictionary<string, object>();
            DataSet ds;
            try
            {
                SqlServerProvider ssp = new SqlServerProvider();
                SqlParameter[] parms = { };
                ds = ssp.SPGetDataSet("spgetmedicalreadings", parms, out outVals);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        try
                        {
                            MedicalReading md = new MedicalReading(dr["MedicalReadingID"].ToString(), dr["UserIDFrom"].ToString(), dr["UserIDTo"].ToString(), DateTime.Parse(dr["CreatedTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss")
                                , (EReadingStatus)int.Parse(dr["ReadingStatus"].ToString()), int.Parse(dr["MedicalPictureCount"].ToString()), dr["MedicalPictrues"].ToString()
                                , bool.Parse(dr["IsRejected"].ToString()), dr["RejectedReason"].ToString());
                            if (md != null)
                            {
                                //if (md.ReadingStatus == EReadingStatus.Completed)
                                //{
                                //    string ha = ";";
                                //}
                                if (!String.IsNullOrEmpty(md.MedicalPictrues))
                                {
                                    string[] picsstr = md.MedicalPictrues.Remove(md.MedicalPictrues.Length - 1).Split(';');//获取图片编号列表
                                    foreach (string str in picsstr)
                                    {
                                        Guid gidpic = new Guid(str);
                                        DataSet dspic;
                                        SqlParameter sqgid = new SqlParameter("ReadingPictureID", gidpic);
                                        SqlParameter[] picparams = {sqgid
                                                               };
                                        dspic = ssp.SPGetDataSet("spgetreadingpicturesbyguid", picparams, out outVals);
                                        if (dspic.Tables[0].Rows.Count > 0)
                                        {
                                            foreach (DataRow drpic in dspic.Tables[0].Rows)
                                            {
                                                ReadingPicture rp = new ReadingPicture((drpic["ReadingPictureID"].ToString()), drpic["PicturePath"].ToString(), int.Parse(drpic["SamplesTypeID"].ToString())
                                                    , int.Parse(drpic["DyedMethodID"].ToString()), int.Parse(drpic["ZoomID"].ToString()), int.Parse(drpic["ClientPictureMarksCount"].ToString()), drpic["ClientPictureMarks"].ToString()
                                                    , int.Parse(drpic["ExpertPictureMarksCount"].ToString()), drpic["ExpertPictureMarks"].ToString(),drpic["FileType"].ToString(),
                                                      drpic["ClientNote"].ToString(), drpic["ExpertConclusion"].ToString());
                                                if (!String.IsNullOrEmpty(rp.ClientPictureMarks))
                                                {
                                                    string[] clientmarksstr = rp.ClientPictureMarks.Remove(rp.ClientPictureMarks.Length - 1).Split(';');//获取客户标注编号列表

                                                    foreach (string clientstr in clientmarksstr)
                                                    {
                                                        Guid clientmarkgid = new Guid(clientstr);
                                                        DataSet dsclient;
                                                        SqlParameter sqclientmarkgid = new SqlParameter("PictureMarkID", clientmarkgid);
                                                        SqlParameter[] clientparams = {sqclientmarkgid
                                                               };
                                                        dsclient = ssp.SPGetDataSet("spgetpicturemarksbyguid", clientparams, out outVals);
                                                        if (dsclient != null && dsclient.Tables[0].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow drclient in dsclient.Tables[0].Rows)
                                                            {
                                                                PictureMark pmclient = new PictureMark((drclient["PictureMarkID"].ToString())
                                                                     , float.Parse(drclient["PictureScale"].ToString())
                                                                        , int.Parse(drclient["RotateCount"].ToString()), drclient["Remark"].ToString(),
                                                                          drclient["MarkLocation"].ToString(), drclient["MarkVision"].ToString(), drclient["MarkColor"].ToString());
                                                                if (pmclient != null)
                                                                {
                                                                    if(rp.ListMarks==null)
                                                                    rp.ListMarks = new List<PictureMark>();
                                                                    rp.ListMarks.Add(pmclient);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                if (!String.IsNullOrEmpty(rp.ExpertPictureMarks))
                                                {
                                                    string[] expertmarksstr = rp.ExpertPictureMarks.Remove(rp.ExpertPictureMarks.Length - 1).Split(';');//获取客户标注编号列表

                                                    foreach (string expertstr in expertmarksstr)
                                                    {
                                                        Guid expertmarkgid = new Guid(expertstr);
                                                        DataSet dsexpert;
                                                        SqlParameter sqexpertmarkgid = new SqlParameter("PictureMarkID", expertmarkgid);
                                                        SqlParameter[] expertparams = {sqexpertmarkgid
                                                               };
                                                        dsexpert = ssp.SPGetDataSet("spgetpicturemarksbyguid", expertparams, out outVals);
                                                        if (dsexpert != null && dsexpert.Tables[0].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow drexpert in dsexpert.Tables[0].Rows)
                                                            {
                                                                PictureMark pmexpert = new PictureMark((drexpert["PictureMarkID"].ToString())
                                                                     , float.Parse(drexpert["PictureScale"].ToString()),int.Parse(drexpert["RotateCount"].ToString()), drexpert["Remark"].ToString(),
                                                                          drexpert["MarkLocation"].ToString(), drexpert["MarkVision"].ToString(), drexpert["MarkColor"].ToString());
                                                                if (pmexpert != null)
                                                                {
                                                                    if(rp.ListExpertMarks==null)
                                                                    rp.ListExpertMarks = new List<PictureMark>();
                                                                    rp.ListExpertMarks.Add(pmexpert);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }//标签列表结束
                                                if (md.ListPics == null)
                                                {
                                                    md.ListPics = new List<ReadingPicture>();
                                                }
                                                md.ListPics.Add(rp);
                                            }//图片列表结束
                                           
                                        }

                                    }
                                }
                              
                            }
                            if(md!=null)
                            listMedicalReading.Add(md);//将md添加进来
                        }
                        catch (System.Exception ex)
                        {
                            return null;
                        }
                       
                    }
                }

            }
            catch (Exception ex)
            {
                listMedicalReading = null;
            }
            return listMedicalReading;
        }
       
        public List<GGUser> GetAllExperts()
       {
             List<GGUser> listEpxerts=new List<GGUser>();
             Dictionary<string, object> outVals = new Dictionary<string, object>();
            DataSet ds;
            try
            {
                SqlServerProvider ssp = new SqlServerProvider();
               // SqlParameter sqmdgid = new SqlParameter("MedicalReadingID", gid);

                SqlParameter[] parms = {  };
                ds = ssp.SPGetDataSet("spgetallexperts", parms, out outVals);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        try
                        {
                            UserContact uc = null; Hospital hs = null;
                        if (dr["HospitalID"] == DBNull.Value || dr["ProfessionTitle"] == DBNull.Value)//可能是客服或者管理员
                        {
                            uc = new UserContact(dr["PersonName"].ToString(), dr["MobilePhone"].ToString(), dr["Email"].ToString());

                        }
                        else
                        {
                            int hospid = int.Parse(dr["HospitalID"].ToString());
                            int title = int.Parse(dr["ProfessionTitle"].ToString());
                            if (iHospDic.Count > 0)//有东西;
                            {
                                 hs = iHospDic[hospid];//通过id获取Hospital对象
                            }
                             uc = new UserContact(dr["PersonName"].ToString(), dr["MobilePhone"].ToString(), dr["Email"].ToString(),
                              (EProfessionTitle)title, hospid);
                             uc.Introduction = dr["Introduction"].ToString();
                        }
                              GGUser   user = new GGUser(dr["UserID"].ToString(), dr["PasswordMD5"].ToString(), dr["Name"].ToString(),
                              dr["Friends"].ToString(), dr["Signature"].ToString(), int.Parse(dr["HeadImageIndex"].ToString()),
                              dr["Groups"].ToString(), (EUserType)int.Parse(dr["UserType"].ToString()), bool.Parse(dr["IsActivited"].ToString()), uc, hs);
                              user.CheckType = (CheckType)int.Parse(dr["CheckType"].ToString());
                              user.Version = int.Parse(dr["Version"].ToString());//添加版本
                              user.IsChecking = bool.Parse(dr["IsChecking"].ToString());
                              if (dr["HeadImageData"] != null && dr["HeadImageData"].ToString() != "")
                                user.HeadImageData = (byte[])dr["HeadImageData"];  
                            if (user != null)
                                {
                                    listEpxerts.Add(user);
                                }
                        }
                        catch (System.Exception ex)
                        {
                            return null;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return listEpxerts;
       }
       
       public bool UpdateMedicalReading(string id, List<ReadingPicture> listpics, out List<ReadingPicture> listnew,out string updatetime)
        {
            bool isupdateok = false; listnew = new List<ReadingPicture>(); updatetime = null;
            Dictionary<string, object> outvalues = null;
            
            foreach (ReadingPicture rp in listpics)
            {
                StringBuilder sb = new StringBuilder();
                if (rp.ListExpertMarks != null)
                {
                    
                    foreach (PictureMark pm in rp.ListExpertMarks)
                    {
                        Guid gid = InsertPitureMark(pm);//插入专家标注
                        if (gid==Guid.Empty)
                        {
                            return false;//插入失败
                        }
                        else
                        {
                            //   count++;//客户标注个数
                            sb.Append(gid.ToString() + ";");
                        }
                    }
                }
                 //   rp.ExpertPictureMarksCount = rp.ListExpertMarks.Count;//把这个信息更新至表中
                    rp.ExpertPictureMarks = sb.ToString();
                    
                    SqlParameter ReadingPictureID = new SqlParameter("ReadingPictureID", SqlDbType.UniqueIdentifier);
                    ReadingPictureID.Direction = ParameterDirection.Input; ReadingPictureID.Value = new Guid(rp.ReadingPictureID);

                    SqlParameter ExpertPictureMarksCount = new SqlParameter("ExpertPictureMarksCount", SqlDbType.Int);
                    ExpertPictureMarksCount.Direction = ParameterDirection.Input; ExpertPictureMarksCount.Value = rp.ExpertPictureMarksCount;

                    SqlParameter ExpertPictureMarks = new SqlParameter("ExpertPictureMarks", SqlDbType.NVarChar);
                    ExpertPictureMarks.Direction = ParameterDirection.Input; ExpertPictureMarks.Value = rp.ExpertPictureMarks;


                    SqlParameter ExpertConclusion = new SqlParameter("ExpertConclusion", SqlDbType.NText);
                    ExpertConclusion.Direction = ParameterDirection.Input; ExpertConclusion.Value = rp.ExpertConclusion;

                   
                    SqlParameter[] sparray = new SqlParameter[]
                  {
                    ReadingPictureID,
                    ExpertPictureMarksCount,
                    ExpertPictureMarks
                    ,ExpertConclusion
                  };
                    SqlServerProvider ssp = new SqlServerProvider();

                    try
                    {
                        if (!ssp.SPExcuteNoneQuery("spupdatemedicalexpertmarks", sparray, out outvalues))//更新成功
                        {
                            return false;
                        }
                        else
                         listnew.Add(rp);//添加更新后的rp;
                    }
                    catch (System.Exception ex)
                    {
                        return false;
                    }
                 }
             
             //更新完专家标注之后，更新阅片状态为已完成
            
            SqlParameter MedicalReadingID = new SqlParameter("MedicalReadingID", SqlDbType.UniqueIdentifier);
            MedicalReadingID.Direction = ParameterDirection.Input; MedicalReadingID.Value = new Guid(id);
            SqlParameter UpdateTime = new SqlParameter("UpdateTime", SqlDbType.DateTime);
            UpdateTime.Direction = ParameterDirection.Output; 
            SqlParameter[] sparrays = new SqlParameter[]
                  {
                    MedicalReadingID
                    ,UpdateTime
                  };
            SqlServerProvider ssps = new SqlServerProvider();

            try
            {
                if (ssps.SPExcuteNoneQuery("spupdatemedicalcompleted", sparrays, out outvalues))//更新成功
                {
                    updatetime = DateTime.Parse(outvalues["UpdateTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    if (updatetime != null)
                    isupdateok = true;
                }
            }
            catch (System.Exception ex)
            {
                isupdateok= false;
            }
            return isupdateok;
           
        }
        //已接收
       public bool UpdateMedicalReading(string id,out string updatetime)
        {
            bool isupdateok = false; updatetime = null;
            Dictionary<string, object> outvalues = null;
            SqlParameter MedicalReadingID = new SqlParameter("MedicalReadingID", SqlDbType.UniqueIdentifier);
            MedicalReadingID.Direction = ParameterDirection.Input; MedicalReadingID.Value = new Guid(id);

            SqlParameter UpdateTime = new SqlParameter("UpdateTime", SqlDbType.DateTime);
            UpdateTime.Direction = ParameterDirection.Output; 
            SqlParameter[] sparray = new SqlParameter[]
                  {
                    MedicalReadingID
                    ,UpdateTime
                  };
            SqlServerProvider ssp = new SqlServerProvider();

            try
            {
                if (ssp.SPExcuteNoneQuery("spupdatemedicalreceived", sparray, out outvalues))//更新成功
                {
                    updatetime = DateTime.Parse(outvalues["UpdateTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    if(updatetime!=null)
                        isupdateok = true;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return isupdateok;
        }
        //更新数据库已拒绝
      public bool UpdateMedicalReading(string id, string reason,out string updatetime)
      {
          bool isupdateok = false; updatetime = null;
          Dictionary<string, object> outvalues = null;
          SqlParameter MedicalReadingID=new SqlParameter("MedicalReadingID",SqlDbType.UniqueIdentifier);
                MedicalReadingID.Direction=ParameterDirection.Input;MedicalReadingID.Value=new Guid(id);
          
           SqlParameter RejectedReason=new SqlParameter("RejectedReason",SqlDbType.NVarChar);
                RejectedReason.Direction=ParameterDirection.Input;RejectedReason.Value=reason;
                SqlParameter UpdateTime = new SqlParameter("UpdateTime", SqlDbType.DateTime);
                UpdateTime.Direction = ParameterDirection.Output;
          SqlParameter[] sparray = new SqlParameter[]
                  {
                    MedicalReadingID
                   ,RejectedReason
                   ,UpdateTime
                  };
          SqlServerProvider ssp = new SqlServerProvider();

          try
          {
              if (ssp.SPExcuteNoneQuery("spupdatemedicalrejectedreason", sparray, out outvalues))//更新成功
              {
                  updatetime = DateTime.Parse(outvalues["UpdateTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                  if (updatetime != null)
                   isupdateok=true;
              }
          }
          catch (System.Exception ex)
          {
              return false;
          }
          return isupdateok;
      }
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
        public bool CheckUserResult(GGUser t, string servicerID,bool isexpert)
      {
          bool result = false; int usertype = 3;//客户
          Dictionary<string, object> outvalues = null;
          if (isexpert)
          {
              usertype = 3;//专家
          }
          else
          {
              usertype = 2;
          }
          SqlParameter UserID = new SqlParameter("UserID", SqlDbType.NVarChar);
          UserID.Direction = ParameterDirection.Input; UserID.Value = t.UserID;

          SqlParameter UserType = new SqlParameter("UserType", SqlDbType.Int);
          UserType.Direction = ParameterDirection.Input; UserType.Value = usertype;

          SqlParameter Friends = new SqlParameter("Friends", SqlDbType.NVarChar);
          Friends.Direction = ParameterDirection.Input; Friends.Value = t.Friends;

          SqlParameter IsActivited = new SqlParameter("IsActivited", SqlDbType.Bit);
          IsActivited.Direction = ParameterDirection.Input; IsActivited.Value = true;

          SqlParameter ServicerID = new SqlParameter("ServicerID", SqlDbType.NVarChar);//传递客服人员的userID,获取其OID
          ServicerID.Direction = ParameterDirection.Input; ServicerID.Value = servicerID;

          SqlParameter PasswordMD5 = new SqlParameter("PasswordMD5", SqlDbType.NVarChar);
          PasswordMD5.Direction = ParameterDirection.Input; PasswordMD5.Value = t.PasswordMD5;

          SqlParameter HeadImageIndex = new SqlParameter("HeadImageIndex", SqlDbType.Int);
          HeadImageIndex.Direction = ParameterDirection.Input; HeadImageIndex.Value = t.HeadImageIndex;

          SqlParameter PersonName = new SqlParameter("PersonName", SqlDbType.NVarChar);
          PersonName.Direction = ParameterDirection.Input; PersonName.Value = t.UserContact.PersonName;

          SqlParameter Email = new SqlParameter("Email", SqlDbType.NVarChar);
          Email.Direction = ParameterDirection.Input; Email.Value = t.UserContact.Email;

          SqlParameter MobilePhone = new SqlParameter("MobilePhone", SqlDbType.NVarChar);
          MobilePhone.Direction = ParameterDirection.Input; MobilePhone.Value = t.UserContact.MobilePhone;



          SqlParameter ProfessionTitle = new SqlParameter("ProfessionTitle", SqlDbType.Int);
          ProfessionTitle.Direction = ParameterDirection.Input; ProfessionTitle.Value = (int)t.UserContact.ProfessionTitle;

          SqlParameter HospitalID = new SqlParameter("HospitalID", SqlDbType.Int);
          HospitalID.Direction = ParameterDirection.Input; HospitalID.Value = t.UserContact.HospitalID;

          SqlParameter[] sparray = new SqlParameter[]
                  {
                    UserID
                    ,UserType
                    ,Friends
                    ,IsActivited
                    ,ServicerID
                   ,PasswordMD5
                   ,HeadImageIndex
		           ,PersonName
                   ,Email
		           ,MobilePhone
                   ,ProfessionTitle
                   ,HospitalID
                  };
         
          SqlServerProvider ssp = new SqlServerProvider();

          try
          {
              if (ssp.SPExcuteNoneQuery("spinsertcheckuser", sparray, out outvalues))//更新成功
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
        //下拉获取最新的阅片列表进行缓存
        public List<MedicalReading> GetLastUpdateMedicalReadings(string userid,bool isexpert,int readingstatus, DateTime lastupdatetime)
        {
            List<MedicalReading> listMedicalReading = new List<MedicalReading>();
            Dictionary<string, object> outVals = new Dictionary<string, object>();
            DataSet ds;
            try
            {
                SqlServerProvider ssp = new SqlServerProvider();

                SqlParameter UserID = new SqlParameter("UserID", SqlDbType.NVarChar);
                UserID.Direction = ParameterDirection.Input; UserID.Value = userid;

                SqlParameter IsExpert = new SqlParameter("IsExpert", SqlDbType.Bit);
                IsExpert.Direction = ParameterDirection.Input; IsExpert.Value = isexpert;

                SqlParameter Readingstatus = new SqlParameter("Readingstatus", SqlDbType.Int);
                Readingstatus.Direction = ParameterDirection.Input; Readingstatus.Value = readingstatus;

                SqlParameter Lastupdatetime = new SqlParameter("Lastupdatetime", SqlDbType.DateTime);
                Lastupdatetime.Direction = ParameterDirection.Input; Lastupdatetime.Value = lastupdatetime;


                SqlParameter[] parms = { UserID,IsExpert,Readingstatus,Lastupdatetime };
                ds = ssp.SPGetDataSet("spgetnewmedicalreadingbylastupdatetime", parms, out outVals);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        try
                        {
                            MedicalReading md = new MedicalReading(dr["MedicalReadingID"].ToString(), dr["UserIDFrom"].ToString(), dr["UserIDTo"].ToString(), DateTime.Parse(dr["CreatedTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss")
                                , (EReadingStatus)int.Parse(dr["ReadingStatus"].ToString()), int.Parse(dr["MedicalPictureCount"].ToString()), dr["MedicalPictrues"].ToString()
                                , bool.Parse(dr["IsRejected"].ToString()), dr["RejectedReason"].ToString());
                            if (md != null)
                            {
                                //if (md.ReadingStatus == EReadingStatus.Completed)
                                //{
                                //    string ha = ";";
                                //}
                                if (!String.IsNullOrEmpty(md.MedicalPictrues))
                                {
                                    string[] picsstr = md.MedicalPictrues.Remove(md.MedicalPictrues.Length - 1).Split(';');//获取图片编号列表
                                    foreach (string str in picsstr)
                                    {
                                        Guid gidpic = new Guid(str);
                                        DataSet dspic;
                                        SqlParameter sqgid = new SqlParameter("ReadingPictureID", gidpic);
                                        SqlParameter[] picparams = {sqgid
                                                               };
                                        dspic = ssp.SPGetDataSet("spgetreadingpicturesbyguid", picparams, out outVals);
                                        if (dspic.Tables[0].Rows.Count > 0)
                                        {
                                            foreach (DataRow drpic in dspic.Tables[0].Rows)
                                            {
                                                ReadingPicture rp = new ReadingPicture((drpic["ReadingPictureID"].ToString()), drpic["PicturePath"].ToString(), int.Parse(drpic["SamplesTypeID"].ToString())
                                                    , int.Parse(drpic["DyedMethodID"].ToString()), int.Parse(drpic["ZoomID"].ToString()), int.Parse(drpic["ClientPictureMarksCount"].ToString()), drpic["ClientPictureMarks"].ToString()
                                                    , int.Parse(drpic["ExpertPictureMarksCount"].ToString()), drpic["ExpertPictureMarks"].ToString(), drpic["FileType"].ToString(),
                                                      drpic["ClientNote"].ToString(), drpic["ExpertConclusion"].ToString());
                                                if (!String.IsNullOrEmpty(rp.ClientPictureMarks))
                                                {
                                                    string[] clientmarksstr = rp.ClientPictureMarks.Remove(rp.ClientPictureMarks.Length - 1).Split(';');//获取客户标注编号列表

                                                    foreach (string clientstr in clientmarksstr)
                                                    {
                                                        Guid clientmarkgid = new Guid(clientstr);
                                                        DataSet dsclient;
                                                        SqlParameter sqclientmarkgid = new SqlParameter("PictureMarkID", clientmarkgid);
                                                        SqlParameter[] clientparams = {sqclientmarkgid
                                                               };
                                                        dsclient = ssp.SPGetDataSet("spgetpicturemarksbyguid", clientparams, out outVals);
                                                        if (dsclient != null && dsclient.Tables[0].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow drclient in dsclient.Tables[0].Rows)
                                                            {
                                                                PictureMark pmclient = new PictureMark((drclient["PictureMarkID"].ToString())
                                                                     , float.Parse(drclient["PictureScale"].ToString())
                                                                        , int.Parse(drclient["RotateCount"].ToString()), drclient["Remark"].ToString(),
                                                                          drclient["MarkLocation"].ToString(), drclient["MarkVision"].ToString(), drclient["MarkColor"].ToString());
                                                                if (pmclient != null)
                                                                {
                                                                    if (rp.ListMarks == null)
                                                                        rp.ListMarks = new List<PictureMark>();
                                                                    rp.ListMarks.Add(pmclient);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                if (!String.IsNullOrEmpty(rp.ExpertPictureMarks))
                                                {
                                                    string[] expertmarksstr = rp.ExpertPictureMarks.Remove(rp.ExpertPictureMarks.Length - 1).Split(';');//获取客户标注编号列表

                                                    foreach (string expertstr in expertmarksstr)
                                                    {
                                                        Guid expertmarkgid = new Guid(expertstr);
                                                        DataSet dsexpert;
                                                        SqlParameter sqexpertmarkgid = new SqlParameter("PictureMarkID", expertmarkgid);
                                                        SqlParameter[] expertparams = {sqexpertmarkgid
                                                               };
                                                        dsexpert = ssp.SPGetDataSet("spgetpicturemarksbyguid", expertparams, out outVals);
                                                        if (dsexpert != null && dsexpert.Tables[0].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow drexpert in dsexpert.Tables[0].Rows)
                                                            {
                                                                PictureMark pmexpert = new PictureMark((drexpert["PictureMarkID"].ToString())
                                                                     , float.Parse(drexpert["PictureScale"].ToString()), int.Parse(drexpert["RotateCount"].ToString()), drexpert["Remark"].ToString(),
                                                                          drexpert["MarkLocation"].ToString(), drexpert["MarkVision"].ToString(), drexpert["MarkColor"].ToString());
                                                                if (pmexpert != null)
                                                                {
                                                                    if (rp.ListExpertMarks == null)
                                                                        rp.ListExpertMarks = new List<PictureMark>();
                                                                    rp.ListExpertMarks.Add(pmexpert);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }//标签列表结束
                                                if (md.ListPics == null)
                                                {
                                                    md.ListPics = new List<ReadingPicture>();
                                                }
                                                md.ListPics.Add(rp);
                                            }//图片列表结束

                                        }

                                    }
                                }

                            }
                            if (md != null)
                                listMedicalReading.Add(md);//将md添加进来
                        }
                        catch (System.Exception ex)
                        {
                            return null;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                listMedicalReading = null;
            }
            return listMedicalReading;
        }
        //上拉获取更多的阅片列表进行缓存
        public List<MedicalReading> GetMoreMedicalReadings(string userid, bool isexpert, int readingstatus, DateTime foremostTime, int currPage, int pageSize)
        {
            List<MedicalReading> listMedicalReading = new List<MedicalReading>();
            Dictionary<string, object> outVals = new Dictionary<string, object>();
            DataSet ds;
            try
            {
                SqlServerProvider ssp = new SqlServerProvider();

                SqlParameter UserID = new SqlParameter("UserID", SqlDbType.NVarChar);
                UserID.Direction = ParameterDirection.Input; UserID.Value = userid;

                SqlParameter IsExpert = new SqlParameter("IsExpert", SqlDbType.Bit);
                IsExpert.Direction = ParameterDirection.Input; IsExpert.Value = isexpert;

                SqlParameter Readingstatus = new SqlParameter("Readingstatus", SqlDbType.Int);
                Readingstatus.Direction = ParameterDirection.Input; Readingstatus.Value = readingstatus;

                SqlParameter ForemostTime = new SqlParameter("ForemostTime", SqlDbType.DateTime);
                ForemostTime.Direction = ParameterDirection.Input; ForemostTime.Value = foremostTime;

                SqlParameter CurrPage = new SqlParameter("CurrPage", SqlDbType.Int);
                CurrPage.Direction = ParameterDirection.Input; CurrPage.Value = currPage;

                SqlParameter PageSize = new SqlParameter("PageSize", SqlDbType.Int);
                PageSize.Direction = ParameterDirection.Input; PageSize.Value = pageSize;


                SqlParameter[] parms = { UserID, IsExpert, Readingstatus, ForemostTime,CurrPage,PageSize };
                ds = ssp.SPGetDataSet("spgetmedicalreadingbypage", parms, out outVals);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        try
                        {
                            MedicalReading md = new MedicalReading(dr["MedicalReadingID"].ToString(), dr["UserIDFrom"].ToString(), dr["UserIDTo"].ToString(), DateTime.Parse(dr["CreatedTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss")
                                , (EReadingStatus)int.Parse(dr["ReadingStatus"].ToString()), int.Parse(dr["MedicalPictureCount"].ToString()), dr["MedicalPictrues"].ToString()
                                , bool.Parse(dr["IsRejected"].ToString()), dr["RejectedReason"].ToString());
                            if (md != null)
                            {
                                //if (md.ReadingStatus == EReadingStatus.Completed)
                                //{
                                //    string ha = ";";
                                //}
                                if (!String.IsNullOrEmpty(md.MedicalPictrues))
                                {
                                    string[] picsstr = md.MedicalPictrues.Remove(md.MedicalPictrues.Length - 1).Split(';');//获取图片编号列表
                                    foreach (string str in picsstr)
                                    {
                                        Guid gidpic = new Guid(str);
                                        DataSet dspic;
                                        SqlParameter sqgid = new SqlParameter("ReadingPictureID", gidpic);
                                        SqlParameter[] picparams = {sqgid
                                                               };
                                        dspic = ssp.SPGetDataSet("spgetreadingpicturesbyguid", picparams, out outVals);
                                        if (dspic.Tables[0].Rows.Count > 0)
                                        {
                                            foreach (DataRow drpic in dspic.Tables[0].Rows)
                                            {
                                                ReadingPicture rp = new ReadingPicture((drpic["ReadingPictureID"].ToString()), drpic["PicturePath"].ToString(), int.Parse(drpic["SamplesTypeID"].ToString())
                                                    , int.Parse(drpic["DyedMethodID"].ToString()), int.Parse(drpic["ZoomID"].ToString()), int.Parse(drpic["ClientPictureMarksCount"].ToString()), drpic["ClientPictureMarks"].ToString()
                                                    , int.Parse(drpic["ExpertPictureMarksCount"].ToString()), drpic["ExpertPictureMarks"].ToString(), drpic["FileType"].ToString(),
                                                      drpic["ClientNote"].ToString(), drpic["ExpertConclusion"].ToString());
                                                if (!String.IsNullOrEmpty(rp.ClientPictureMarks))
                                                {
                                                    string[] clientmarksstr = rp.ClientPictureMarks.Remove(rp.ClientPictureMarks.Length - 1).Split(';');//获取客户标注编号列表

                                                    foreach (string clientstr in clientmarksstr)
                                                    {
                                                        Guid clientmarkgid = new Guid(clientstr);
                                                        DataSet dsclient;
                                                        SqlParameter sqclientmarkgid = new SqlParameter("PictureMarkID", clientmarkgid);
                                                        SqlParameter[] clientparams = {sqclientmarkgid
                                                               };
                                                        dsclient = ssp.SPGetDataSet("spgetpicturemarksbyguid", clientparams, out outVals);
                                                        if (dsclient != null && dsclient.Tables[0].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow drclient in dsclient.Tables[0].Rows)
                                                            {
                                                                PictureMark pmclient = new PictureMark((drclient["PictureMarkID"].ToString())
                                                                     , float.Parse(drclient["PictureScale"].ToString())
                                                                        , int.Parse(drclient["RotateCount"].ToString()), drclient["Remark"].ToString(),
                                                                          drclient["MarkLocation"].ToString(), drclient["MarkVision"].ToString(), drclient["MarkColor"].ToString());
                                                                if (pmclient != null)
                                                                {
                                                                    if (rp.ListMarks == null)
                                                                        rp.ListMarks = new List<PictureMark>();
                                                                    rp.ListMarks.Add(pmclient);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                if (!String.IsNullOrEmpty(rp.ExpertPictureMarks))
                                                {
                                                    string[] expertmarksstr = rp.ExpertPictureMarks.Remove(rp.ExpertPictureMarks.Length - 1).Split(';');//获取客户标注编号列表

                                                    foreach (string expertstr in expertmarksstr)
                                                    {
                                                        Guid expertmarkgid = new Guid(expertstr);
                                                        DataSet dsexpert;
                                                        SqlParameter sqexpertmarkgid = new SqlParameter("PictureMarkID", expertmarkgid);
                                                        SqlParameter[] expertparams = {sqexpertmarkgid
                                                               };
                                                        dsexpert = ssp.SPGetDataSet("spgetpicturemarksbyguid", expertparams, out outVals);
                                                        if (dsexpert != null && dsexpert.Tables[0].Rows.Count > 0)
                                                        {
                                                            foreach (DataRow drexpert in dsexpert.Tables[0].Rows)
                                                            {
                                                                PictureMark pmexpert = new PictureMark((drexpert["PictureMarkID"].ToString())
                                                                     , float.Parse(drexpert["PictureScale"].ToString()), int.Parse(drexpert["RotateCount"].ToString()), drexpert["Remark"].ToString(),
                                                                          drexpert["MarkLocation"].ToString(), drexpert["MarkVision"].ToString(), drexpert["MarkColor"].ToString());
                                                                if (pmexpert != null)
                                                                {
                                                                    if (rp.ListExpertMarks == null)
                                                                        rp.ListExpertMarks = new List<PictureMark>();
                                                                    rp.ListExpertMarks.Add(pmexpert);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }//标签列表结束
                                                if (md.ListPics == null)
                                                {
                                                    md.ListPics = new List<ReadingPicture>();
                                                }
                                                md.ListPics.Add(rp);
                                            }//图片列表结束

                                        }

                                    }
                                }

                            }
                            if (md != null)
                                listMedicalReading.Add(md);//将md添加进来
                        }
                        catch (System.Exception ex)
                        {
                            return null;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                listMedicalReading = null;
            }
            return listMedicalReading;
        }
    }
   
}
