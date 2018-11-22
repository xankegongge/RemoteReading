using System;
using System.Collections.Generic;
using DataRabbit;

namespace RemoteReading
{
	[Serializable]
	public partial class Region : IEntity<System.String>
	{
	    #region Force Static Check
		public const string TableName = "Region" ;
		public const string _RegionID = "RegionID" ;
		public const string _RegionCode = "RegionCode" ;
		public const string _RegionName = "RegionName" ;
		public const string _ParentID = "ParentID" ;
	    #endregion
	 
	    #region Property
	
		#region RegionID
		private System.Int32 m_RegionID = 0 ; 
		public System.Int32 RegionID
		{
			get
			{
				return this.m_RegionID ;
			}
			set
			{
				this.m_RegionID = value ;
			}
		}
		#endregion
	
		#region RegionCode
		private System.String m_RegionCode = "" ; 
		public System.String RegionCode
		{
			get
			{
				return this.m_RegionCode ;
			}
			set
			{
				this.m_RegionCode = value ;
			}
		}
		#endregion
	
		#region RegionName
		private System.String m_RegionName = "" ; 
		public System.String RegionName
		{
			get
			{
				return this.m_RegionName ;
			}
			set
			{
				this.m_RegionName = value ;
			}
		}
		#endregion
	
		#region ParentID
		private System.Int32 m_ParentID = 0 ; 
		public System.Int32 ParentID
		{
			get
			{
				return this.m_ParentID ;
			}
			set
			{
				this.m_ParentID = value ;
			}
		}
		#endregion
	    #endregion
	 
	    #region IEntity 成员
	    public System.String GetPKeyValue()
	    {
	       return this.RegionCode;
	    }
	    #endregion
		
		#region ToString 
		public override string ToString()
		{
			return this.RegionCode.ToString()  + " " + this.RegionName.ToString() ;
		}
		#endregion
	}
}
