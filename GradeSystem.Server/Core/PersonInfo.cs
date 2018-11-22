using System;
using System.Collections.Generic;

namespace GradeSystem.Server
{
	[Serializable]
	public partial class PersonInfo 
	{
	    #region Force Static Check
		public const string TableName = "UserContact" ;
		public const string _User_ContactOID = "User_ContactOID" ;
		public const string _PersonName = "PersonName" ;
		public const string _WorkPhone = "WorkPhone" ;
		public const string _MobilePhone = "MobilePhone" ;
		public const string _Email = "Email" ;
		public const string _WorkAddress = "WorkAddress" ;
		public const string _HomeAddress = "HomeAddress" ;
		public const string _WorkPostCode = "WorkPostCode" ;
		public const string _HomePostCode = "HomePostCode" ;
		public const string _IsExpert = "IsExpert" ;
		public const string _IsMan = "IsMan" ;
		public const string _Birthday = "Birthday" ;
		public const string _ProfessionTitle = "ProfessionTitle" ;
		public const string _Hobby = "Hobby" ;
		public const string _MarriageState = "MarriageState" ;
		public const string _Education = "Education" ;
		public const string _SYS_CreatedByUserID = "SYS_CreatedByUserID" ;
		public const string _Hospital = "Hospital" ;
	    #endregion

        #region 构造方法
        public PersonInfo()
        {

        }
        public PersonInfo(string realname, string mobilephone, string email)
        {
            this.m_PersonName = realname;
            this.m_MobilePhone = mobilephone;
            this.m_Email = email;
        }
       
        #endregion

        #region Property

      
        private String m_Introduction = "";

        public String Introduction
        {
            get { return m_Introduction; }
            set { m_Introduction = value; }
        }

       
		#region PersonName
		private System.String m_PersonName = "" ; 
		public System.String PersonName
		{
			get
			{
				return this.m_PersonName ;
			}
			set
			{
				this.m_PersonName = value ;
			}
		}
		#endregion
	
		#region WorkPhone
		private System.String m_WorkPhone = "" ; 
		public System.String WorkPhone
		{
			get
			{
				return this.m_WorkPhone ;
			}
			set
			{
				this.m_WorkPhone = value ;
			}
		}
		#endregion
	
		#region MobilePhone
		private System.String m_MobilePhone = "" ; 
		public System.String MobilePhone
		{
			get
			{
				return this.m_MobilePhone ;
			}
			set
			{
				this.m_MobilePhone = value ;
			}
		}
		#endregion
	
		#region Email
		private System.String m_Email = "" ; 
		public System.String Email
		{
			get
			{
				return this.m_Email ;
			}
			set
			{
				this.m_Email = value ;
			}
		}
		#endregion
	
		#region WorkAddress
		private System.String m_WorkAddress = "" ; 
		public System.String WorkAddress
		{
			get
			{
				return this.m_WorkAddress ;
			}
			set
			{
				this.m_WorkAddress = value ;
			}
		}
		#endregion
	
		#region HomeAddress
		private System.String m_HomeAddress = "" ; 
		public System.String HomeAddress
		{
			get
			{
				return this.m_HomeAddress ;
			}
			set
			{
				this.m_HomeAddress = value ;
			}
		}
		#endregion
	
		#region WorkPostCode
		private System.String m_WorkPostCode = "" ; 
		public System.String WorkPostCode
		{
			get
			{
				return this.m_WorkPostCode ;
			}
			set
			{
				this.m_WorkPostCode = value ;
			}
		}
		#endregion
	
		#region HomePostCode
		private System.String m_HomePostCode = "" ; 
		public System.String HomePostCode
		{
			get
			{
				return this.m_HomePostCode ;
			}
			set
			{
				this.m_HomePostCode = value ;
			}
		}
		#endregion
	
		#region IsMan
		private System.Boolean m_IsMan = false ; 
		public System.Boolean IsMan
		{
			get
			{
				return this.m_IsMan ;
			}
			set
			{
				this.m_IsMan = value ;
			}
		}
		#endregion
	
		#region Birthday
		private string m_Birthday ; 
		public string Birthday
		{
			get
			{
				return this.m_Birthday ;
			}
			set
			{
				this.m_Birthday = value ;
			}
		}
		#endregion
	
		#region Hobby
		private System.String m_Hobby = "" ; 
		public System.String Hobby
		{
			get
			{
				return this.m_Hobby ;
			}
			set
			{
				this.m_Hobby = value ;
			}
		}
		#endregion
	
		#region MarriageState
		private System.String m_MarriageState = "" ; 
		public System.String MarriageState
		{
			get
			{
				return this.m_MarriageState ;
			}
			set
			{
				this.m_MarriageState = value ;
			}
		}
		#endregion
	
		#region Education
		private System.String m_Education = "" ; 
		public System.String Education
		{
			get
			{
				return this.m_Education ;
			}
			set
			{
				this.m_Education = value ;
			}
		}
		#endregion
	
	
	    #endregion
	
		
		#region ToString 
		public override string ToString()
		{
			return   this.PersonName.ToString() ;
		}
		#endregion
	}
}
