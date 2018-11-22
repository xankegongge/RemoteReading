using System;
using System.Collections.Generic;
using System.Text;
using ESBasic;
using System.IO;
namespace RemoteReading.Core
{
    public static class GlobalConsts
    {       

        /// <summary>
        /// 普通视频采集分辨率。（长宽之和）
        /// </summary>
        public readonly static int CommonQualityVideo = 700;

        /// <summary>
        /// 高清视频采集分辨率。（长宽之和）
        /// </summary>
        public readonly static int HighQualityVideo = 1000;

        /// <summary>
        /// 全员群的ID
        /// </summary>
        public readonly static string CompanyGroupID = "#0";

        //public static byte[] StreamToBytes(MemoryStream stream)
        //{
        //    byte[] bytes = new byte[stream.Length];
        //    stream.Write(bytes, 0, stream.Length);
        //    stream.Read(bytes, 0, bytes.Length);
        //    // 设置当前流的位置为流的开始
        //    stream.Seek(0, SeekOrigin.Begin);
        //    return bytes;
        //}
    }
}
