using System;
using System.Collections.Generic;
using DataRabbit;

namespace RemoteReading
{
	[Serializable]
	public partial class GGGroup : IEntity<System.String>
	{
	    #region Force Static Check
		public const string TableName = "GGGroup" ;
		public const string _GroupID = "GroupID" ;
		public const string _Name = "Name" ;
		public const string _CreatorID = "CreatorID" ;
		public const string _Announce = "Announce" ;
		public const string _Members = "Members" ;
		public const string _CreateTime = "CreateTime" ;
		public const string _Version = "Version" ;
	    #endregion
	 
	    #region Property
	
		#region GroupID
		private System.String m_GroupID = "" ; 
		public System.String GroupID
		{
			get
			{
				return this.m_GroupID ;
			}
			set
			{
				this.m_GroupID = value ;
			}
		}
		#endregion
	
		#region Name
		private System.String m_Name = "" ; 
		public System.String Name
		{
			get
			{
				return this.m_Name ;
			}
			set
			{
				this.m_Name = value ;
			}
		}
		#endregion
	
		#region CreatorID
		private System.String m_CreatorID = "" ; 
		public System.String CreatorID
		{
			get
			{
				return this.m_CreatorID ;
			}
			set
			{
				this.m_CreatorID = value ;
			}
		}
		#endregion
	
		#region Announce
		private System.String m_Announce = "" ; 
		public System.String Announce
		{
			get
			{
				return this.m_Announce ;
			}
			set
			{
				this.m_Announce = value ;
			}
		}
		#endregion
	
		#region Members
		private System.String m_Members = "" ; 
		public System.String Members
		{
			get
			{
				return this.m_Members ;
			}
			set
			{
				this.m_Members = value ;
			}
		}
		#endregion
	
		#region CreateTime
		private System.DateTime m_CreateTime = DateTime.Now ; 
		public System.DateTime CreateTime
		{
			get
			{
				return this.m_CreateTime ;
			}
			set
			{
				this.m_CreateTime = value ;
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
	    public System.String GetPKeyValue()
	    {
	       return this.GroupID;
	    }
	    #endregion
		
		#region ToString 
		public override string ToString()
		{
			return this.GroupID.ToString()  + " " + this.Name.ToString() ;
		}
		#endregion
	}
}
