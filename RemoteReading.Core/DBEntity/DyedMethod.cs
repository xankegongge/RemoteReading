using System;
using System.Collections.Generic;
using DataRabbit;

namespace RemoteReading.Core
{
	[Serializable]
	public partial class DyedMethod 
	{
	    #region Force Static Check
		public const string TableName = "DyedMethod" ;
		public const string _DyedMethodID = "DyedMethodID" ;
		public const string _DyedMethodName = "DyedMethodName" ;
		public const string _Version = "Version" ;
	    #endregion
	 
	    #region Property
	
		#region DyedMethodID 染色方法ID
		private System.Int32 m_DyedMethodID = 0 ; 
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
	
		#region DyedMethodName 染色方法名称
		private System.String m_DyedMethodName = "" ; 
		public System.String DyedMethodName
		{
			get
			{
				return this.m_DyedMethodName ;
			}
			set
			{
				this.m_DyedMethodName = value ;
			}
		}
		#endregion

        #region Version版本号
        private System.Int32 m_Version = 0; 
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
        public static List<string> getDyedMethodNameList()
        {

            List<string> listDyedMethodList = new List<string>();
            listDyedMethodList.Add("革兰染色");
            listDyedMethodList.Add("抗酸染色");
            listDyedMethodList.Add("荧光染色");
            listDyedMethodList.Add("瑞式染色");
            listDyedMethodList.Add("其他");
            return listDyedMethodList;
        }
		#region ToString 
		public override string ToString()
		{
			return this.DyedMethodID.ToString()  + " " + this.DyedMethodName.ToString() ;
		}
		#endregion
	}
}
