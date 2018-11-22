using System;
using System.Collections.Generic;
using DataRabbit;
using System.Drawing;
using System.Text;
namespace RemoteReading.Core
{
	[Serializable]
	public partial class PictureMark 
	{
      
        #region 构造方法
        public PictureMark()
        {
            this.PictureMarkID = Guid.NewGuid().ToString();
        }

        //这个画矩形
        public PictureMark(string gid,  float picscale,
            int rotatecount, string remark,
            string markloacations,string markvision,string colors)
        {
            this.PictureMarkID = gid;
            this.m_PictureScale = picscale;
            this.m_Remark = remark;
            this.rotateCount = rotatecount;
            this.MarkLocation = markloacations;
            this.MarkVision = markvision;
            this.MarkColor = colors;
        }
        #endregion

       
       #region Property

      

        #region PictureMarkID
        private string m_PictureMarkID ; 
		public string  PictureMarkID
		{
			get
			{
				return this.m_PictureMarkID ;
			}
			set
			{
				this.m_PictureMarkID = value ;
			}
		}
		#endregion
	
		#region PictureScale
		private float m_PictureScale = 1.0f ;
      

		public float PictureScale
		{
			get
			{
				return this.m_PictureScale ;
			}
			set
			{
				this.m_PictureScale = value ;
			}
		}
		#endregion

   
		#region MarkColor
        private string m_MarkLocation = "0.5,0.5" ;
        public void setM_MarkLocation(float[] mm_MarkLocation)//将浮点型数组转成字符串进行传输；
        {
            StringBuilder sbBuilder = new StringBuilder();
            this.mm_MarkLocation = mm_MarkLocation;
            for (int i = 0; i < 2; i++)
            {

                sbBuilder.Append(mm_MarkLocation[i] + ",");

            }
            sbBuilder.Remove(sbBuilder.Length - 1,1);
            this.m_MarkLocation = sbBuilder.ToString();
        }
        public string MarkLocation
        {
            get { return m_MarkLocation; }
            set 
            { 
                m_MarkLocation = value;
                parse(this.m_MarkLocation, 3);
            }
        }
        private string m_MarkColor = "255,255,0,0";
        public void setM_MarkColor(int[] mm_MarkColor)
        {
            StringBuilder sbBuilder = new StringBuilder();
            this.mm_MarkColor = mm_MarkColor;
            for (int i = 0; i < 4; i++)
            {

                sbBuilder.Append(mm_MarkColor[i] + ",");

            }
            sbBuilder.Remove(sbBuilder.Length - 1,1);//删除最后一个，
            this.m_MarkColor = sbBuilder.ToString();
        }
        public string MarkColor
        {
            get
            {
                return this.m_MarkColor;
            }
            set
            {
                this.m_MarkColor = value;
                parse(this.m_MarkColor, 1);//解析MarkColor
            }
        }
		#endregion

        #region MarkVision
        private string m_MarkVision = "0.5,0.5";
        public string MarkVision
        {
            get
            {
                return this.m_MarkVision;
            }
            set
            {
                this.m_MarkVision = value;
                parse(this.m_MarkVision, 2);//解析MarkColor
            }
        }
        private void parse(string markstr, int type)
        {
            string[] parseStrings = markstr.Split(',');
            if (type == 1)//表示解析Color
            {
                if (parseStrings.Length == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        this.mm_MarkColor[i] = int.Parse(parseStrings[i]);
                    }
                }
            }
            else if (type == 2)
            {//解析ViSion
                if (parseStrings.Length == 2)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        this.mm_MarkVision[i] = float.Parse(parseStrings[i]);
                    }
                }
            }
            else if (type == 3)
            {//解析MarkLocation
                if (parseStrings.Length == 2)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        this.mm_MarkLocation[i] = float.Parse(parseStrings[i]);
                    }
                }
            }
        }
      
        //非DB字段，不会被传输；
        private float[] mm_MarkLocation = { 0.5f, 0.5f };//标记相对位置;

        public float[] M_MarkLocation//只有只读属性就不被传输了
        {
            get { return mm_MarkLocation; }
            
        }
        private int[] mm_MarkColor = { 255, 255, 0, 0 }; //标记画笔颜色  argb：0-255 (默认红色)

        public int[] M_MarkColor
        {
            get { return mm_MarkColor; }
           
        }
        private float[] mm_MarkVision = { 0.5f, 0.5f };//标记可视区别中心位置

        public float[] M_MarkVision
        {
            get { return mm_MarkVision; }
            
        }
        public void setM_MarkVision(float[] mm_MarkVision)
        {
            StringBuilder sbBuilder = new StringBuilder();
            this.mm_MarkVision = mm_MarkVision;
            for (int i = 0; i < 2; i++)
            {

                sbBuilder.Append(mm_MarkVision[i] + ",");

            }
            sbBuilder.Remove(sbBuilder.Length - 1,1);//删除最后一个，
            this.m_MarkVision = sbBuilder.ToString();
        }
        
        #endregion

        #region Remark
        private string m_Remark = "";
        private int rotateCount;

        public int RotateCount
        {
            get { return rotateCount; }
            set { rotateCount = value; }
        }
        public string Remark
        {
            get
            {
                return this.m_Remark;
            }
            set
            {
                this.m_Remark = value;
            }
        }
        #endregion
	    #endregion
	 
	}
}
