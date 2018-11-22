using System;
using System.Collections.Generic;
using DataRabbit;
using JustLib;
namespace RemoteReading.Core
{
	[Serializable]
	public partial class MedicalReading 
	{
	   
        #region 非DB字段

        #region 图片列表
        private List<ReadingPicture> m_listPics;//多张图片
        public List<ReadingPicture> ListPics
        {
            get { return m_listPics; }
            set { m_listPics = value; }
        }
        #endregion

        #region  发送用户
        private GGUser m_UserFrom;
        public GGUser UserFrom
        {
            get { return m_UserFrom; }
            set { m_UserFrom = value; }
        }
        #endregion

        #region 接收用户
        private GGUser m_UserTo;
        public GGUser UserTo
        {
            get { return m_UserTo; }
            set { m_UserTo = value; }
        }
        #endregion


        #endregion

        #region 构造函数
        public MedicalReading()
        {
            this.MedicalReadingID = Guid.NewGuid().ToString();
        }
        public MedicalReading(MedicalReading newMr)//完整的克隆
        {
            this.MedicalReadingID = newMr.MedicalReadingID;//ID和图片信息即可

            this.ReadingStatus = newMr.ReadingStatus;
            this.MedicalPictureCount = newMr.MedicalPictureCount;
            this.MedicalPictrues = newMr.MedicalPictrues;
            this.UserIDFrom = newMr.UserIDFrom;
            this.UserFrom = newMr.UserFrom;
            this.UserIDTo = newMr.UserIDTo;
            this.UserTo = newMr.UserTo;
            this.RejectedReason = newMr.RejectedReason;
            this.IsRejected = newMr.IsRejected;
            this.ListPics = newMr.ListPics;
            this.CreatedTime = newMr.CreatedTime;
        }
        public MedicalReading(string userIDFrom, string userIDTo, EReadingStatus readingstatus, int piccount, List<ReadingPicture> listPic)
        {
            this.UserIDFrom = userIDFrom;
            this.UserIDTo = userIDTo;
            this.ReadingStatus = readingstatus;//未处理
            this.m_MedicalPictureCount = piccount;//图片数量;
            this.m_listPics = listPic;//图片列表;
           // this.MedicalPictureCount = listPic.Count;//自动填写图片张数；
        }
        public MedicalReading(string  gid,string userIDFrom, string userIDTo, string createtime,
            EReadingStatus readingstatus, int piccount,string pics,bool isrejected,string reason)
        {
            this.MedicalReadingID = gid ;
            this.UserIDFrom = userIDFrom;
            this.UserIDTo = userIDTo;
            this.CreatedTime = createtime;
            this.ReadingStatus = readingstatus;//未处理
            this.m_MedicalPictureCount = piccount;//图片数量;
            this.m_MedicalPictrues = pics;
            this.m_RejectedReason = reason;
            this.IsRejected = isrejected;

        }
        #endregion

        #region Property

        #region MedicalReadingID
        private string m_MedicalReadingID  ; 
		public string MedicalReadingID
		{
			get
			{
				return this.m_MedicalReadingID ;
			}
			set
			{
				this.m_MedicalReadingID = value ;
			}
		}
		#endregion
	
		#region UserIDFrom
		private System.String m_UserIDFrom = "" ; 
		public System.String UserIDFrom
		{
			get
			{
				return this.m_UserIDFrom ;
			}
			set
			{
				this.m_UserIDFrom = value ;
			}
		}
		#endregion
	
		#region UserIDTo
		private System.String m_UserIDTo = "" ; 
		public System.String UserIDTo
		{
			get
			{
				return this.m_UserIDTo ;
			}
			set
			{
				this.m_UserIDTo = value ;
			}
		}
		#endregion
	
		#region CreatedTime
		private string m_CreatedTime ; 
		public string CreatedTime
		{
			get
			{
                if (this.m_CreatedTime == null)
                {
                    this.m_CreatedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
				return this.m_CreatedTime ;
			}
			set
			{
				this.m_CreatedTime = value ;
			}
		}
		#endregion
	
		#region ReadingStatus
		private EReadingStatus m_ReadingStatus =EReadingStatus.UnProcessed ;
        public EReadingStatus ReadingStatus
		{
			get
			{
				return this.m_ReadingStatus ;
			}
			set
			{
				this.m_ReadingStatus = value ;
			}
		}
		#endregion
	
		#region MedicalPictureCount
		private System.Int32 m_MedicalPictureCount = 0 ; 
		public System.Int32 MedicalPictureCount
		{
			get
			{
				return this.m_MedicalPictureCount ;
			}
			set
			{
				this.m_MedicalPictureCount = value ;
			}
		}
		#endregion
	
		#region MedicalPictrues
		private System.String m_MedicalPictrues = "" ; 
		public System.String MedicalPictrues
		{
			get
			{
				return this.m_MedicalPictrues ;
			}
			set
			{
				this.m_MedicalPictrues = value ;
			}
		}
		#endregion

        #region 是否为拒绝
        private bool m_IsRejected = false;//是否被专家拒绝了;
        public bool IsRejected
        {
            get { return m_IsRejected; }
            set { m_IsRejected = value; }
        }
        #endregion

        #region 拒绝理由
        private string m_RejectedReason;//拒绝理由

        public string RejectedReason
        {
            get { return m_RejectedReason; }
            set { m_RejectedReason = value; }
        }
        #endregion
	    #endregion

	}
}
