using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace RemoteReading.Server
{
    public class ThumbnailMaker
    {
        //private static ThumbnailMaker instance;
        //public static ThumbnailMaker Instance
        //{
        //    get
        //    {
        //        lock (typeof(ThumbnailMaker))
        //        {
        //            if (instance == null)
        //                instance = new ThumbnailMaker();
        //            return instance;
        //        }
        //    }
        //}


        public static void CreateDirectory(string path)
        {
            if (path == "") return;
            string head = path.Substring(0, path.IndexOf("\\"));  //d:
            string weibu = path.Substring(head.Length + 1);  //       \1\2
            string hpath = head;
            while (weibu.IndexOf("\\") != -1)
            {
                string p = hpath + "\\" + weibu.Substring(0, weibu.IndexOf("\\"));
                hpath = p;
                if (!Directory.Exists(p))
                    Directory.CreateDirectory(p);
                int ix = weibu.IndexOf("\\") + 1;
                weibu = weibu.Substring(ix);
            }

        }

        /// <summary>
        /// 制作图片的缩略图
        /// </summary>
        /// <param name="originalImage">原图</param>
        /// <param name="width">缩略图的宽（像素）</param>
        /// <param name="height">缩略图的高（像素）</param>
        /// <param name="mode">缩略方式</param>
        /// <returns>缩略图</returns>
        /// <remarks>
        ///        <paramref name="mode"/>：
        ///            <para>HW：指定的高宽缩放（可能变形）</para>
        ///            <para>HWO：指定高宽缩放（可能变形）（过小则不变）</para>
        ///            <para>W：指定宽，高按比例</para>
        ///            <para>WO：指定宽（过小则不变），高按比例</para>
        ///            <para>H：指定高，宽按比例</para>
        ///            <para>HO：指定高（过小则不变），宽按比例</para>
        ///            <para>CUT：指定高宽裁减（不变形）</para>
        /// </remarks>
        public static Image MakeThumbnail(Image originalImage, int width, int height, ThumbnailMode mode)
        {
            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;


            switch (mode)
            {
                case ThumbnailMode.UsrHeightWidth: //指定高宽缩放（可能变形）
                    break;
                case ThumbnailMode.UsrHeightWidthBound: //指定高宽缩放（可能变形）（过小则不变）
                    if (originalImage.Width <= width && originalImage.Height <= height)
                    {
                        return originalImage;
                    }
                    if (originalImage.Width < width)
                    {
                        towidth = originalImage.Width;
                    }
                    if (originalImage.Height < height)
                    {
                        toheight = originalImage.Height;
                    }
                    break;
                case ThumbnailMode.UsrWidth: //指定宽，高按比例
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case ThumbnailMode.UsrWidthBound: //指定宽（过小则不变），高按比例
                    if (originalImage.Width <= width)
                    {
                        return originalImage;
                    }
                    else
                    {
                        toheight = originalImage.Height * width / originalImage.Width;
                    }
                    break;
                case ThumbnailMode.UsrHeight: //指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case ThumbnailMode.UsrHeightBound: //指定高（过小则不变），宽按比例
                    if (originalImage.Height <= height)
                    {
                        return originalImage;
                    }
                    else
                    {
                        towidth = originalImage.Width * height / originalImage.Height;
                    }
                    break;
                case ThumbnailMode.Cut: //指定高宽裁减（不变形）
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            Image bitmap = new Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            //g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                        new Rectangle(x, y, ow, oh),
                        GraphicsUnit.Pixel);
            g.Dispose();
            return bitmap;
        }

        /// <summary>
        /// 制作图片的缩略图
        /// </summary>
        /// <param name="originalStream">原图</param>
        /// <param name="thumbnailPath">保存缩略图的路径</param>
        /// <param name="width">缩略图的宽（像素）</param>
        /// <param name="height">缩略图的高（像素）</param>
        /// <param name="mode">缩略方式，参见<seealso cref="MakeThumbnail(Image, int, int, string)"/></param>
        public static void MakeThumbnail(Stream originalStream, string thumbnailPath, int width, int height, ThumbnailMode mode)
        {
            Image originalImage = Image.FromStream(originalStream);
            try
            {
                MakeThumbnail(originalImage, thumbnailPath, width, height, mode);
            }
            finally
            {
                originalImage.Dispose();
            }
        }

        /// <summary>
        /// 制作图片的缩略图
        /// </summary>
        /// <param name="originalImage">原图</param>
        /// <param name="thumbnailPath">保存缩略图的路径</param>
        /// <param name="width">缩略图的宽（像素）</param>
        /// <param name="height">缩略图的高（像素）</param>
        /// <param name="mode">缩略方式，参见<seealso cref="MakeThumbnail(Image, int, int, string)"/></param>
        public static void MakeThumbnail(Image originalImage, string thumbnailPath, int width, int height, ThumbnailMode mode)
        {
            Image bitmap = MakeThumbnail(originalImage, width, height, mode);
            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, ImageFormat.Jpeg);
            }
            finally
            {
                bitmap.Dispose();
            }
        }

        /// <summary>
        /// 制作图片的缩略图
        /// </summary>
        /// <param name="originalImagePath">原图的路径</param>
        /// <param name="thumbnailPath">保存缩略图的路径</param>
        /// <param name="width">缩略图的宽（像素）</param>
        /// <param name="height">缩略图的高（像素）</param>
        /// <param name="mode">缩略方式，参见<seealso cref="MakeThumbnail(Image, int, int, string)"/></param>
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, ThumbnailMode mode)
        {
            Image originalImage = Image.FromFile(originalImagePath);
            try
            {
                MakeThumbnail(originalImage, thumbnailPath, width, height, mode);
            }
            finally
            {
                originalImage.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mode"></param>
        public static byte[] MakeThumbnail(byte[] img, int width, int height, ThumbnailMode mode)
        {
            Stream byteStream = new MemoryStream(img);

            Image originalImage = Image.FromStream(byteStream);

            try
            {
                Image _img = MakeThumbnail(originalImage, width, height, mode);

                MemoryStream imgStream = new MemoryStream();

                _img.Save(imgStream, ImageFormat.Jpeg);

                return imgStream.ToArray();
            }
            finally
            {
                originalImage.Dispose();
            }
        }
        /// <summary>
        /// 产生高清缩略图 固定大小
        /// </summary>
        /// <param name="original_image_file">源文件</param>
        /// <param name="object_width">缩略图宽度</param>
        /// <param name="object_height">缩略图高度</param>
        public static void MakeHighQualityThumbnail(string original_image_file, string output, int object_width, int object_height)
        {


            int actual_width = 0;
            int actual_heigh = 0;
            string outputfilename = output; //original_image_file + ".jpg";

            System.Drawing.Bitmap original_image = new Bitmap(original_image_file);//读取源文件           
            actual_width = original_image.Width;
            actual_heigh = original_image.Height;

            Bitmap img = new Bitmap(object_width, object_height);
            img.SetResolution(108f, 108f);
            Graphics gdiobj = Graphics.FromImage(img);
            gdiobj.CompositingQuality = CompositingQuality.HighQuality;
            gdiobj.SmoothingMode = SmoothingMode.HighQuality;
            gdiobj.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gdiobj.PixelOffsetMode = PixelOffsetMode.HighQuality;

            gdiobj.FillRectangle(new SolidBrush(Color.White), 0, 0, object_width, object_height);
            Rectangle destrect = new Rectangle(0, 0, object_width, object_height);

            gdiobj.DrawImage(original_image, destrect, 0, 0, actual_width, actual_heigh, GraphicsUnit.Pixel);

            System.Drawing.Imaging.EncoderParameters ep = new System.Drawing.Imaging.EncoderParameters(1);
            ep.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)100);

            System.Drawing.Imaging.ImageCodecInfo ici = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders()[0];

            if (ici != null)
            {
                if (File.Exists(outputfilename))
                    File.Delete(outputfilename);
                img.Save(outputfilename, ici, ep);

            }
            else
            {
                img.Save(outputfilename, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mode"></param>
        public static byte[] MakeThumbnail(byte[] img, int width, int height)
        {
            return MakeThumbnail(img, width, height, ThumbnailMode.UsrHeightBound);
        }
    }

    public enum ThumbnailMode
    {
        UsrHeightWidth,
        UsrHeightWidthBound,
        UsrWidth,
        UsrWidthBound,
        UsrHeight,
        UsrHeightBound,
        Cut,
        NONE,
    }
}
