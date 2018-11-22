using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
namespace RemoteReading
{
    public partial class frmPicAdjust : Form
    {
        public frmPicAdjust()
        {
            InitializeComponent();
        }
        public Image ig;
        public string PicOldPath;
        private void frmPicAdjust_Load(object sender, EventArgs e)
        {
            ptbOlePic.Image = ig;
            ptbNewPic.Image = ig;
        }

        public static Bitmap KiLighten(Bitmap b, int degree)//���ȵ���
        {
            if (b == null)
            {
                return null;
            }
            //ȷ����Сֵ�����ֵ
            if (degree < -255) degree = -255;
            if (degree > 255) degree = 255;
            try
            {
                //ȷ��ͼ��Ŀ�͸�
                int width = b.Width;
                int height = b.Height;

                int pix = 0;
                //LockBits��Bitmap�������ڴ���
                BitmapData data = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                //unsafe
                //{
                //    //pָ���ַ
                //    byte* p = (byte*)data.Scan0;//8λ�޷�������
                //    int offset = data.Stride - width * 3;
                //    for (int y = 0; y < height; y++)
                //    {
                //        for (int x = 0; x < width; x++)
                //        {
                //            // ����ָ��λ�����ص�����
                //            for (int i = 0; i < 3; i++)
                //            {
                //                pix = p[i] + degree;
                //                if (degree < 0) p[i] = (byte)Math.Max(0, pix);
                //                if (degree > 0) p[i] = (byte)Math.Min(255, pix);
                //            } // i
                //            p += 3;
                //        } // x
                //        p += offset;
                //    } // y
                //}
                b.UnlockBits(data);//���ڴ��н������

                return b;
            }
            catch
            {
                return null;
            }

        }

        public static Bitmap KiContrast(Bitmap b, int degree)//�Աȶȵ���
        {
            if (b == null)
            {
                return null;
            }

            if (degree < -100) degree = -100;
            if (degree > 100) degree = 100;

            try
            {

                double pixel = 0;
                double contrast = (100.0 + degree) / 100.0;
                contrast *= contrast;
                int width = b.Width;
                int height = b.Height;
                BitmapData data = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                //unsafe
                //{
                //    byte* p = (byte*)data.Scan0;
                //    int offset = data.Stride - width * 3;
                //    for (int y = 0; y < height; y++)
                //    {
                //        for (int x = 0; x < width; x++)
                //        {
                //            // ����ָ��λ�����صĶԱȶ�
                //            for (int i = 0; i < 3; i++)
                //            {
                //                pixel = ((p[i] / 255.0 - 0.5) * contrast + 0.5) * 255;
                //                if (pixel < 0) pixel = 0;
                //                if (pixel > 255) pixel = 255;
                //                p[i] = (byte)pixel;
                //            } // i
                //            p += 3;
                //        } // x
                //        p += offset;
                //    } // y
                //}
                b.UnlockBits(data);
                return b;
            }
            catch
            {
                return null;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(ptbOlePic.Image);
            Bitmap bp = KiLighten(b, trackBar1.Value);
            ptbNewPic.Image = bp;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            Bitmap t = new Bitmap(ptbOlePic.Image);
            Bitmap bp = KiContrast(t,trackBar3.Value);
            ptbNewPic.Image = bp;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Single LS=1;
            if (trackBar2.Value < 10)
            {
                LS = Convert.ToSingle(trackBar2.Value*0.1);
            }
            if (trackBar2.Value == 10)
            {
                LS = 1;
            }
            else
            {
                if (trackBar2.Value > 10)
                {
                    LS = Convert.ToSingle(trackBar2.Value-10);
                }
            }
            int pwidth = ig.Width;
            int pheight = ig.Height;
            ptbNewPic.Width = Convert.ToInt32(pwidth*LS);
            ptbNewPic.Height = Convert.ToInt32(pheight * LS);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = ptbNewPic.CreateGraphics();
            saveFileDialog1.Filter = "BMP|*.bmp|JPEG|*.jpeg|GIF|*.gif|PNG|*.png";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string picPath = saveFileDialog1.FileName;
                string picType = picPath.Substring(picPath.LastIndexOf(".") + 1, (picPath.Length - picPath.LastIndexOf(".") - 1));
                switch (picType)
                {
                    case "bmp":
                        Bitmap bt = new Bitmap(ptbNewPic.Image);
                        Bitmap mybmp = new Bitmap(bt, ig.Width, ig.Height);
                        mybmp.Save(picPath, ImageFormat.Bmp); break;
                    case "jpeg":
                        Bitmap bt1 = new Bitmap(ptbNewPic.Image);
                        Bitmap mybmp1 = new Bitmap(bt1, ptbNewPic.Width, ptbNewPic.Height);
                        mybmp1.Save(picPath, ImageFormat.Jpeg); break;
                    case "gif":
                        Bitmap bt2 = new Bitmap(ptbNewPic.Image);
                        Bitmap mybmp2 = new Bitmap(bt2, ptbNewPic.Width, ptbNewPic.Height);
                        mybmp2.Save(picPath, ImageFormat.Gif); break;
                    case "png":
                        Bitmap bt3 = new Bitmap(ptbNewPic.Image);
                        Bitmap mybmp3 = new Bitmap(bt3, ptbNewPic.Width, ptbNewPic.Height);
                        mybmp3.Save(picPath, ImageFormat.Png); break;
                }
            }
        }
    }
}