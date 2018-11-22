using System;
using System.Collections.Generic;
using DataRabbit;
using System.Collections;
namespace RemoteReading.Core
{
	[Serializable]
	public partial class SamplesType 
	{
	    #region Force Static Check
		public const string TableName = "SamplesType" ;
		public const string _SamplesTypeID = "SamplesTypeID" ;
		public const string _SamplesName = "SamplesName" ;
		public const string _Version = "Version" ;
	    #endregion
        public SamplesType(int sampid, string name, int version)
        {
            this.m_SamplesTypeID = sampid;
            this.m_SamplesName = name;
            this.m_Version = version;
        }
	    #region Property
	
		#region SamplesTypeID
		private System.Int32 m_SamplesTypeID = 0 ; 
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
	
		#region SamplesName
		private System.String m_SamplesName = "" ; 
		public System.String SamplesName
		{
			get
			{
				return this.m_SamplesName ;
			}
			set
			{
				this.m_SamplesName = value ;
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
        public static List<string> getSampleTypeNameList()
        {

            List<string> listSampleTypeList = new List<string>();
            listSampleTypeList.Add("痰液");
            listSampleTypeList.Add("血液");
            listSampleTypeList.Add("尿液");
            listSampleTypeList.Add("脑脊液");
            listSampleTypeList.Add("粪便");
            listSampleTypeList.Add("脓液");
            listSampleTypeList.Add("分泌物");
            listSampleTypeList.Add("胸水");
            listSampleTypeList.Add("腹水");
            listSampleTypeList.Add("其他");
            return listSampleTypeList;
        }
		#region ToString 
		public override string ToString()
		{
			return this.SamplesTypeID.ToString()  + " " + this.SamplesName.ToString() ;
		}
		#endregion
	}
}
