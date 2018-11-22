using System;
using System.Collections.Generic;
using DataRabbit;

namespace RemoteReading.Core
{
	[Serializable]
	public partial class Zoom 
	{
	    #region Force Static Check
		public const string TableName = "Zoom" ;
		public const string _ZoomID = "ZoomID" ;
		public const string _ZoomName = "ZoomName" ;
		public const string _Version = "Version" ;
	    #endregion
	 
	    #region Property
	
		#region ZoomID
		private System.Int32 m_ZoomID = 0 ; 
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
	
		#region ZoomName
		private System.String m_ZoomName = "" ; 
		public System.String ZoomName
		{
			get
			{
				return this.m_ZoomName ;
			}
			set
			{
				this.m_ZoomName = value ;
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

        public static List<string> getZoomNameList()
        {

            List<string> listZoomNameList = new List<string>();
            listZoomNameList.Add("低倍镜");
            listZoomNameList.Add("高倍镜");
            listZoomNameList.Add("油倍镜");
            listZoomNameList.Add("其他");

            return listZoomNameList;
        }
		
		#region ToString 
		public override string ToString()
		{
			return this.ZoomID.ToString()  + " " + this.ZoomName.ToString() ;
		}
		#endregion
	}
}
