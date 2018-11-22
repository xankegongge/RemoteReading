using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace RemoteReading
{
    public partial class frmSlide : Form
    {
        public frmSlide()
        {
            InitializeComponent();
        }
        public string Ppath;
        string FilePath;
        FileSystemInfo[] FSInfo;
        DirectoryInfo DInfo;
        int i = 0;
        private void frmSlide_Load(object sender, EventArgs e)
        {
            this.Text = Ppath;
            DInfo = new DirectoryInfo(Ppath);
            FSInfo = DInfo.GetFileSystemInfos();
            
            if (Ppath.Length <=4)
            {
                FilePath = Ppath;
            }
            else
            {
                FilePath = Ppath + "\\";
            }
        }
        private void GetPic()
        {
            if(i<FSInfo.Length)
            {
                string FileType = FSInfo[i].ToString().Substring(FSInfo[i].ToString().LastIndexOf(".") + 1, (FSInfo[i].ToString().Length - FSInfo[i].ToString().LastIndexOf(".") - 1));
                FileType = FileType.ToLower();
                if (FileType == "jpg" || FileType == "png" || FileType == "bmp" || FileType == "gif" || FileType == "jpeg")
                {
                    pictureBox1.Image = Image.FromFile(FilePath + FSInfo[i].ToString());
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetPic();
            i++;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (toolStripButton1.Text == "��ͣ")
            {
                toolStripButton1.Text = "����";
                timer1.Stop();
            }
            else
            {
                toolStripButton1.Text = "��ͣ";
                timer1.Start();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            i = 0;
            timer1.Start();
            toolStripButton1.Text = "��ͣ";

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}