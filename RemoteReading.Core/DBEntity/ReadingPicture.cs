using System;
using System.Collections.Generic;
using DataRabbit;
using System.Drawing;
using System.IO;
namespace RemoteReading.Core
{
	[Serializable]
	public partial class ReadingPicture 
	{
	   

        #region 构造方法
        public ReadingPicture()
        {
            this.ReadingPictureID = Guid.NewGuid().ToString();
        }
        public ReadingPicture(ReadingPicture rp)
        {
            this.ReadingPictureID = rp.ReadingPictureID;
            this.SamplesTypeID = rp.SamplesTypeID;
            this.ZoomID = rp.ZoomID;
            this.DyedMethodID = rp.DyedMethodID;
            this.PicturePath = rp.PicturePath;
            this.ListExpertMarks = rp.listExpertMarks;
            this.ListMarks = rp.listMarks;
            this.FileType = rp.FileType;
            this.ExpertPictureMarks = rp.ExpertPictureMarks;
            this.ExpertPictureMarksCount = rp.ExpertPictureMarksCount;
            this.ClientPictureMarks = rp.ClientPictureMarks;
            this.ClientPictureMarksCount = rp.ClientPictureMarksCount;
            this.ClientNote = rp.ClientNote;
            this.ExpertConclusion = rp.ExpertConclusion;
        }
        public ReadingPicture(string  readingid, string picpath, int samplestype, int dyemethod, int zoom)
        {
            this.ReadingPictureID = readingid;
            this.PicturePath = picpath;
            this.SamplesTypeID = samplestype;
            this.DyedMethodID = dyemethod;
            this.ZoomID = zoom;

        }

        public ReadingPicture(string  readingid, string picpath, int samplestype, int dyemethod, int zoom
            , int clientMarksCount, string clientMarks, int expertMarksCount, string expertsMarks,string filetype
            ,string note,string con)
            : this( readingid, picpath, samplestype, dyemethod, zoom)
        {
            this.ClientPictureMarksCount = clientMarksCount;
            this.ClientPictureMarks = clientMarks;
            this.ExpertPictureMarksCount = expertMarksCount;
            this.ExpertPictureMarks = expertsMarks;
            this.FileType = filetype;
            this.ExpertConclusion=con;
            this.ClientNote = note; 
        }
        #endregion

        #region Property 数据库字段

        #region ReadingPictureID
        private string  m_ReadingPictureID  ; 
		public string  ReadingPictureID
		{
			get
			{
				return this.m_ReadingPictureID ;
			}
			set
			{
				this.m_ReadingPictureID = value ;
			}
		}
		#endregion
	
		#region PicturePath
		private System.String m_PicturePath = "" ; 
		public System.String PicturePath
		{
			get
			{
				return this.m_PicturePath ;
			}
			set
			{
				this.m_PicturePath = value ;
			}
		}
		#endregion
	
		#region DyedMethodID
		private System.Int32 m_DyedMethodID = -1; 
		public System.Int32 DyedMethodID
		{
			get
			{
				return this.m_DyedMethodID ;
			}
			set
			{
				this.m_DyedMethodID = value ;
			}
		}
		#endregion
	
		#region SamplesTypeID
		private System.Int32 m_SamplesTypeID =-1 ; 
		public System.Int32 SamplesTypeID
		{
			get
			{
				return this.m_SamplesTypeID ;
			}
			set
			{
				this.m_SamplesTypeID = value ;
			}
		}
		#endregion
	
		#region ZoomID
		private System.Int32 m_ZoomID =-1 ; 
		public System.Int32 ZoomID
		{
			get
			{
				return this.m_ZoomID ;
			}
			set
			{
				this.m_ZoomID = value ;
			}
		}
		#endregion

        #region 文件后缀
        public string FileType { get; set; }
        #endregion

        #region ClientPictureMarksCount
        private System.Int32 m_ClientPictureMarksCount = 0 ; 
		public System.Int32 ClientPictureMarksCount
		{
			get
			{
				return this.m_ClientPictureMarksCount ;
			}
			set
			{
				this.m_ClientPictureMarksCount = value ;
			}
		}
		#endregion
	
		#region ClientPictureMarks
		private System.String m_ClientPictureMarks = "" ; 
		public System.String ClientPictureMarks
		{
			get
			{
				return this.m_ClientPictureMarks ;
			}
			set
			{
				this.m_ClientPictureMarks = value ;
			}
		}
		#endregion
	
		#region ExpertPictureMarksCount
		private System.Int32 m_ExpertPictureMarksCount = 0 ; 
		public System.Int32 ExpertPictureMarksCount
		{
			get
			{
				return this.m_ExpertPictureMarksCount ;
			}
			set
			{
				this.m_ExpertPictureMarksCount = value ;
			}
		}
		#endregion
	
		#region ExpertPictureMarks
		private System.String m_ExpertPictureMarks = "" ; 
		public System.String ExpertPictureMarks
		{
			get
			{
				return this.m_ExpertPictureMarks ;
			}
			set
			{
				this.m_ExpertPictureMarks = value ;
			}
		}


		#endregion
        private String m_ExpertConclusion;
        #region ExpertConclusion
        public String ExpertConclusion
        {
            get { return m_ExpertConclusion; }
            set { m_ExpertConclusion = value; }
        }
        #endregion

        #region ClientNote
        private String m_ClientNote;
        public String ClientNote
        {
            get { return m_ClientNote; }
            set { m_ClientNote = value; }
        }
        #endregion

        #endregion

        #region 非DB字段

        #region 客户标签列表
        private List<PictureMark> listMarks;
        public List<PictureMark> ListMarks
        {
            get { return listMarks; }
            set { listMarks = value; }
        }
        #endregion

        #region 专家标签列表
        private List<PictureMark> listExpertMarks;//专家的标记
        public List<PictureMark> ListExpertMarks
        {
            get { return listExpertMarks; }
            set { listExpertMarks = value; }
        }
        #endregion

        #region 图片字节
        private byte[] imagebyte;

        public byte[] Imagebyte
        {
            get { return this.imagebyte; }
            set { imagebyte = value; }
        }
        #endregion

      
        #endregion
		
		#region ToString 
		public override string ToString()
		{
			return this.ReadingPictureID.ToString()  ;
		}
		#endregion

       
    }
}
