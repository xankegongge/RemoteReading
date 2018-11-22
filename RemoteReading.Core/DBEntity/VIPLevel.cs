using System;
using System.Collections.Generic;
using DataRabbit;

namespace RemoteReading.Core
{
	[Serializable]
	public partial class VIPLevel : IEntity<System.Int32>
	{
	    #region Force Static Check
		public const string TableName = "VIPLevel" ;
		public const string _VIPLevelID = "VIPLevelID" ;
		public const string _LevelName = "LevelName" ;
		public const string _BirthdayPrivilege = "BirthdayPrivilege" ;
		public const string _NoADPrivilege = "NoADPrivilege" ;
		public const string _HandledPrivilege = "HandledPrivilege" ;
		public const string _LotteryPrivilege = "LotteryPrivilege" ;
		public const string _FreeConsultCount = "FreeConsultCount" ;
		public const string _Version = "Version" ;
	    #endregion
	 
	    #region Property
	
		#region VIPLevelID
		private System.Int32 m_VIPLevelID = 0 ; 
		public System.Int32 VIPLevelID
		{
			get
			{
				return this.m_VIPLevelID ;
			}
			set
			{
				this.m_VIPLevelID = value ;
			}
		}
		#endregion
	
		#region LevelName
		private System.String m_LevelName = "" ; 
		public System.String LevelName
		{
			get
			{
				return this.m_LevelName ;
			}
			set
			{
				this.m_LevelName = value ;
			}
		}
		#endregion
	
		#region BirthdayPrivilege
		private System.Boolean m_BirthdayPrivilege = false ; 
		public System.Boolean BirthdayPrivilege
		{
			get
			{
				return this.m_BirthdayPrivilege ;
			}
			set
			{
				this.m_BirthdayPrivilege = value ;
			}
		}
		#endregion
	
		#region NoADPrivilege
		private System.Boolean m_NoADPrivilege = false ; 
		public System.Boolean NoADPrivilege
		{
			get
			{
				return this.m_NoADPrivilege ;
			}
			set
			{
				this.m_NoADPrivilege = value ;
			}
		}
		#endregion
	
		#region HandledPrivilege
		private System.Boolean m_HandledPrivilege = false ; 
		public System.Boolean HandledPrivilege
		{
			get
			{
				return this.m_HandledPrivilege ;
			}
			set
			{
				this.m_HandledPrivilege = value ;
			}
		}
		#endregion
	
		#region LotteryPrivilege
		private System.Boolean m_LotteryPrivilege = false ; 
		public System.Boolean LotteryPrivilege
		{
			get
			{
				return this.m_LotteryPrivilege ;
			}
			set
			{
				this.m_LotteryPrivilege = value ;
			}
		}
		#endregion
	
		#region FreeConsultCount
		private System.Int32 m_FreeConsultCount = 0 ; 
		public System.Int32 FreeConsultCount
		{
			get
			{
				return this.m_FreeConsultCount ;
			}
			set
			{
				this.m_FreeConsultCount = value ;
			}
		}
		#endregion
	
		#region Version
		private System.Int32 m_Version = 0 ; 
		public System.Int32 Version
		{
			get
			{
				return this.m_Version ;
			}
			set
			{
				this.m_Version = value ;
			}
		}
		#endregion
	    #endregion
	 
	    #region IEntity 成员
	    public System.Int32 GetPKeyValue()
	    {
	       return this.VIPLevelID;
	    }
	    #endregion
		
		#region ToString 
		public override string ToString()
		{
			return this.VIPLevelID.ToString()  + " " + this.LevelName.ToString() ;
		}
		#endregion
	}
}
