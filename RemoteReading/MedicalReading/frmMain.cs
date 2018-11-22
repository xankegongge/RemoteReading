//#define Debug
//#define Release
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using ESBasic;
using ESPlus.Rapid;
using ESPlus.Serialization;
using ESPlus.Core;
using ESPlus.Application;
using JustLib;
using ESBasic.ObjectManagement.Managers;
using ESBasic.ObjectManagement.Forms;
using JustLib.NetworkDisk.Passive;
using JustLib.NetworkDisk;
using System.Reflection;
using RemoteReading.Core;
using System.Threading;
using ESBasic.Helpers;
namespace RemoteReading
{
    public partial class frmMain : BaseForm
    {

        private EUserType usertype = EUserType.NormalClient;
   //     private EReadingStatus readingStatus = EReadingStatus.UnProcessed;
        //private GlobalUserCache globalUserCache; //缓存用户资料
        //private FormManager<string, ChatForm> chatFormManager;
        //private INDiskOutter nDiskOutter; 
        private MedicalReading curMR;
        private MainForm main;
        SynchronizationContext m_SyncContext = null;

        public frmMain()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.picFirstSize = new Size(this.pictureBox1.Width, this.pictureBox1.Height);
            this.picFirstLocation = this.pictureBox1.Location;
            this.TopMost = false;
            this.DialogResult = DialogResult.Abort;
            m_SyncContext = SynchronizationContext.Current;////获取UI线程同步上下文

        }

       
        
        //增加globaluserCache,ndiskoutter formmananger
        public frmMain(IRapidPassiveEngine rapid, MainForm main, GGUser cur, GGUser tar, MedicalReading mr):this()
        {
            this.main = main;
            main.UpdateMREvent += new MainForm.LoadHandler(UpdateMR);//注册事件
            this.usertype = cur.UserType;
            this.rapidPassiveEngine = rapid;
            if (mr == null)//如果是新建阅片
            {
                this.userIDFrom = cur.UserID;
                this.userIDTo = tar.UserID;
                this.curMR = new MedicalReading();
               // curMR.ListPics = dicPics;
                this.Text = "  向专家：" + tar.PersonName + "申请阅片";
             //   this.skinPanelExpert.Enabled = false;
                this.btnEnterTalk.Visible = false;//让其消失
                this.skinPanelExpert.Visible = false;//
                this.DialogResult = DialogResult.Abort;//默认是这个，提交成功后才会
            }
            else//
            {

                   // this.readingState = mr.ReadingStatus;//获取订单状态
                   // this.usertype = cur.UserType;
                   // this.readingStatus = mr.ReadingStatus;
                    this.curMR =new MedicalReading(mr);
                    this.userIDFrom = mr.UserIDFrom;
                    this.userIDTo = mr.UserIDTo;

                    this.loadPicThread = new Thread(new ThreadStart(this.ThreadProcSafePost));
                    this.loadPicThread.Start();

            }

            //this.rapidPassiveEngine.ConnectionInterrupted += new CbGeneric(rapidPassiveEngine_ConnectionInterrupted);//预订断线事件
            //this.rapidPassiveEngine.BasicOutter.BeingPushedOut += new CbGeneric(BasicOutter_BeingPushedOut);
            //this.rapidPassiveEngine.RelogonCompleted += new CbGeneric<LogonResponse>(rapidPassiveEngine_RelogonCompleted);//预订重连成功事件  
           // this.rapidPassiveEngine.MessageReceived += new CbGeneric<string, int, byte[], string>(rapidPassiveEngine_MessageReceived);
            //   this.rapidPassiveEngine.SendMessage(null, InformationTypes.GetAllMedicalReading, null, null);
        }
       // private string curMedicalReadingID;
        
        //此构造函数用于读取阅片订单信息
        

        #region 自定义方法
        public int Pindex;
        bool isload = false;
        void UpdateMR(MedicalReading mr)
        {
            //curMR = mr;
            CopyMedicalReadingNoImage(mr);
            LoadMedicalReading(mr);
        }

        private void ThreadProcSafePost()
        {
            //...执行线程任务

            //在线程中更新UI（通过UI线程同步上下文m_SyncContext）
            m_SyncContext.Post(LoadMedicalReading,this.curMR);
            //...执行线程其他任务
        }

        private void CopyMedicalReadingNoImage(MedicalReading newMr)
        {
            curMR.MedicalReadingID = newMr.MedicalReadingID;//ID和图片信息即可
            curMR.ReadingStatus = newMr.ReadingStatus;
            curMR.MedicalPictureCount = newMr.MedicalPictureCount;
            curMR.MedicalPictrues = newMr.MedicalPictrues;
            curMR.UserIDFrom = newMr.UserIDFrom;
            curMR.UserFrom = newMr.UserFrom;
            curMR.UserIDTo = newMr.UserIDTo;
            curMR.UserTo = newMr.UserTo;
            curMR.RejectedReason = newMr.RejectedReason;
            curMR.IsRejected = newMr.IsRejected;
            for (int i = 0; i < newMr.ListPics.Count; i++)
            {
                 curMR.ListPics[i].ReadingPictureID = newMr.ListPics[i].ReadingPictureID;
                curMR.ListPics[i].SamplesTypeID = newMr.ListPics[i].SamplesTypeID;
                curMR.ListPics[i].ZoomID = newMr.ListPics[i].ZoomID;
                curMR.ListPics[i].DyedMethodID = newMr.ListPics[i].DyedMethodID;
                curMR.ListPics[i].PicturePath = newMr.ListPics[i].PicturePath;
                curMR.ListPics[i].ListExpertMarks = newMr.ListPics[i].ListExpertMarks;
                curMR.ListPics[i].ListMarks = newMr.ListPics[i].ListMarks;
                curMR.ListPics[i].FileType = newMr.ListPics[i].FileType;
                curMR.ListPics[i].ExpertPictureMarks = newMr.ListPics[i].ExpertPictureMarks;
                curMR.ListPics[i].ExpertPictureMarksCount = newMr.ListPics[i].ExpertPictureMarksCount;
                curMR.ListPics[i].ClientPictureMarks = newMr.ListPics[i].ClientPictureMarks;
                curMR.ListPics[i].ClientPictureMarksCount = newMr.ListPics[i].ClientPictureMarksCount;
                curMR.ListPics[i].ClientNote = newMr.ListPics[i].ClientNote;
                curMR.ListPics[i].ExpertConclusion = newMr.ListPics[i].ExpertConclusion;
                
            }
                curMR.CreatedTime = newMr.CreatedTime;
        }
        void  LoadMedicalReading(Object ob)//加载阅片信息
        {
            MedicalReading mr = (MedicalReading)ob;
            if (this.InvokeRequired)
            {
                this.Invoke(new CbGeneric<Object>(this.LoadMedicalReading));
            }
            else
            {
	            isload=true;//表示是装载，
	            try
	            {
                  //  AllUnable();//除了ListBox以及CBMark\以及按钮可以选择，其他只能看

                    //ControlState();
                    if (listBox1.Items.Count > 0)//每次进来都需要清空List
                    {
                        listboxPath.Clear();
                        dicPics.Clear();
                        listBox1.Items.Clear();
                        listBox1.SelectedIndex = curlistBoxSelectedIndex = -1;
                    }


                    // List<ReadingPicture> listReadPics = mr.ListPics;
                    int picCount = 1;
                    if (curMR.ListPics != null)//如果有图片
                    {

                        foreach (ReadingPicture rp in curMR.ListPics)
                        {
                            if (rp.Imagebyte != null)
                            {
                                string picnum = "第" + (picCount++).ToString() + "张图片";
                                this.listBox1.Items.Add(picnum);
                                this.dicPics.Add(new KeyValuePair<string, ReadingPicture>(picnum, rp));
                                this.listboxPath.Add(picnum);
                            }

                        }
                        this.listBox1.SelectedIndex = 0;//默认加载第一项;
                       // LoadPic();

                    }
	                 }
	                 catch (System.Exception ex)
	                 {
	                     MessageBox.Show(ex.ToString());
	                 }
	           
           } 
 
        }
       

        
        public void ControlState()
        {
            if (isload)//如果是加载
            {
                ToolStatusUnable();//其他控件都不能使用,除了放大，缩小与旋转，标记选择功能
                ClearCb();
                this.toolStripButtonAddSomePic.Enabled = false;//查看的时候不能添加图片
                this.toolStripButtonAddPic.Enabled = false;//
                this.toolStripButton2.Enabled = this.toolStripButton3.Enabled= true;
                this.toolStripButtonZoomBig.Enabled = true;
                this.toolStripButtonZoomsmall.Enabled = true;
                this.cbExpertMarks.Enabled = this.cbMarks.Enabled = true;
                this.cbDyedMethod.Enabled = this.cbZoom.Enabled = this.cbSampleType.Enabled = false;
                this.btnClientNote.Text = "查看备注";
                this.btnExpertConclusion.Text = "查看结论";
                switch (curMR.ReadingStatus)
                {
                    case EReadingStatus.UnProcessed: this.Text = "此阅片状态：未处理";
                        {
                           
                            if (this.usertype == EUserType.Expert)
                            {
                                this.btnEnterTalk.Visible = this.btnSubmit.Visible = true;
                                this.skinPanelExpert.Visible = true;
                                this.btnEnterTalk.Text = "拒绝";
                                this.btnSubmit.Text = "接收";//
                               
                            }
                            else//其他角色都隐藏按钮
                            {
                                this.btnEnterTalk.Visible = this.btnSubmit.Visible = false;
                            }
                        }
                        break;
                    case EReadingStatus.Processing: this.Text = "此阅片状态：正在处理"; 
                        {
                            this.skinPanelExpert.Visible = true;
                            if (this.usertype == EUserType.Expert)//专家准备提交结论
                            {
                                this.contextMenuStrip1.Enabled = true;
                                this.btnEnterTalk.Visible = this.btnSubmit.Visible = true;
                                this.btnEnterTalk.Text = "对话";
                                this.btnSubmit.Text = "提交服务器";//
                                // this.btnSubmit.Tag = "sumbitPointer";
                                this.btnExpertConclusion.Text = "输入结论";
                            }
                            else//其他角色都隐藏按钮
                            {
                                this.btnEnterTalk.Visible = true;//可以对话
                                this.btnSubmit.Visible = false;//不能提交，只能给专家来提交；
                                this.contextMenuStrip1.Enabled = false;
                            }
                        }
                        break;
                    case EReadingStatus.Rejected: this.Text = "此阅片状态：已拒绝";
                        {

                            this.btnEnterTalk.Visible = true;
                            this.btnSubmit.Visible = false;
                            this.btnEnterTalk.Text = "查看拒绝理由";
                        }
                        break;
                    case EReadingStatus.Completed: this.Text = "此阅片状态：已完成";
                        {
                            this.btnEnterTalk.Visible = false;
                            this.btnSubmit.Visible = false;
                            this.btnEnterTalk.Text = "查看拒绝理由";
                        } break;
                }
            }
            else//如果是新建;
            {
                if (listBox1.Items.Count == 0)
                {
                    ToolStatusUnable();
                    this.btnSubmit.Visible = false;
                    this.btnEnterTalk.Visible = false;
                   // this.btnExpertConclusion.Visible = this.btnClientNote.Visible = false;//都隐藏;
                }
                else//新建且，listbox有东西
                {
                    ToolStatusEnable();
                    this.btnClientNote.Text = "备注";
                   // this.btnExpertConclusion.Visible = this.btnClientNote.Visible = true;//都隐藏;
                    this.btnSubmit.Visible = true;
                }
                ClearCb();
            }
            rotateCount = 0;//每次加载新照片，这些都要清零
            this.offsetx = this.offsety = 0;//偏移量
            this.rect.Width = 0;//清空标记
            normalscale = 1;
        }
        public void ToolStatusUnable()
        {
            contextMenuStrip1.Enabled = false;
            //设为桌面背景ToolStripMenuItem1.Enabled = false;
            //转换为ToolStripMenuItem.Enabled = false;
            //删除ToolStripMenuItem1.Enabled = false;
            //重命名ToolStripMenuItem1.Enabled = false;
            //另存为ToolStripMenuItem1.Enabled = false;
            toolStripButton2.Enabled = false;
            toolStripButton3.Enabled = false;
            toolStripButton4.Enabled = false;
            //打印ToolStripMenuItem1.Enabled = false;
            //图片特效ToolStripMenuItem.Enabled = false;
            //图片调节ToolStripMenuItem.Enabled = false;
          
            //图片文字ToolStripMenuItem.Enabled = false;
            toolStripButton7.Enabled = false;
            toolStripButtonZoomBig.Enabled = false;
            toolStripButtonZoomsmall.Enabled = false;
            this.cbMarks.Enabled = this.cbDyedMethod.Enabled = this.cbSampleType.Enabled = this.cbZoom.Enabled = false;
           // ClearCb();
       //     this.btnSubmit.Enabled = false;
          //  this.contextMenuStrip1.Enabled = false;
           // this.btnSubmit.BackColor = Color.DarkGray;
        }

        private void ClearCb()
        {
            this.cbMarks.Items.Clear();
         
            this.cbExpertMarks.Items.Clear();
            this.cbDyedMethod.Items.Clear();
            this.cbSampleType.Items.Clear();
            this.cbZoom.Items.Clear();
         
            this.lblMarkCount.Text = "0";
            this.lblExpertMarksCount.Text = "0";
        }
        public void ToolStatusEnable()
        {
            contextMenuStrip1.Enabled = true;
            //设为桌面背景ToolStripMenuItem1.Enabled = true;
            //转换为ToolStripMenuItem.Enabled = true;
            //删除ToolStripMenuItem1.Enabled = true;
            //重命名ToolStripMenuItem1.Enabled = true;
            //另存为ToolStripMenuItem1.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = true;
            toolStripButton4.Enabled = true;
            //打印ToolStripMenuItem1.Enabled = true;
            //图片特效ToolStripMenuItem.Enabled = true;
            //图片调节ToolStripMenuItem.Enabled = true;
         
            //图片文字ToolStripMenuItem.Enabled = true;
            toolStripButton7.Enabled = true;
            toolStripButtonZoomBig.Enabled = true;
            toolStripButtonZoomsmall.Enabled = true;
            this.cbMarks.Enabled = this.cbDyedMethod.Enabled = this.cbSampleType.Enabled = this.cbZoom.Enabled = true;
         //   this.btnSubmit.Enabled = true;
       //     this.btnSubmit.BackColor = Color.LimeGreen;
            //this.skinPanelClient.Enabled = true;
        
        }
        #endregion

        //#region 调用API
        //[DllImport("user32.dll", EntryPoint = "SystemParametersInfoA")]
        //static extern Int32 SystemParametersInfo(Int32 uAction, Int32 uParam, string lpvparam, Int32 fuwinIni);
        //private const int SPI_SETDESKWALLPAPER = 20;
        //#endregion

        private Size picFirstSize;
        private Point picFirstLocation;
        #region 窗体加载
        private void frmMain_Load(object sender, EventArgs e)
        {
          //  this.Text += this.userIDFrom+",欢迎您";
            this.TopMost = true;
            this.Focus();
            string resourceDir = AppDomain.CurrentDomain.BaseDirectory + "Resource\\";
            this.Icon = ImageHelper.ConvertToIcon(global::RemoteReading.Properties.Resources.medicalreading, 64);
            this.panel1.MouseWheel += new MouseEventHandler(pictureBox1_MouseWheel);
         
            this.Cursor = Cursors.Default;
            if (!isload)
            {
                ControlState();//如果是新建
            }
            
           // SetPictureLocation();
            //加载桌面的图片文件夹;
#if (Debug)
            selectedDirPath = @"C:\Users\xankes\Desktop\medical";
            DirectoryInfo DInfo = new DirectoryInfo(selectedDirPath);
            FileSystemInfo[] FSInfo = DInfo.GetFileSystemInfos();
            for (int i = 0; i < FSInfo.Length; i++)
            {
                string FileType = FSInfo[i].ToString().Substring(FSInfo[i].ToString().LastIndexOf(".") + 1, (FSInfo[i].ToString().Length - FSInfo[i].ToString().LastIndexOf(".") - 1));
                FileType = FileType.ToLower();
                if (FileType == "jpg" || FileType == "png" || FileType == "bmp" || FileType == "gif" || FileType == "jpeg")
                {

                    string picpath = selectedDirPath + "\\" + FSInfo[i].ToString();

                    if (!this.listboxPath.Contains(picpath))//不能添加相同
                    {
                        this.currentPic = new ReadingPicture();
                        this.currentPic.FileType = FileType;
                        // Image im = Image.FromFile(picpath);
                        Bitmap bm = new Bitmap(picpath);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            switch (FileType)
                            {
                                case "jpeg":
                                case "jpg":
                                    bm.Save(ms, ImageFormat.Jpeg);
                                    break;
                                case "png":
                                    bm.Save(ms, ImageFormat.Png);
                                    break;
                                case "bmp":
                                    bm.Save(ms, ImageFormat.Bmp);
                                    break;
                                case "gif":
                                    bm.Save(ms, ImageFormat.Gif);
                                    break;
                                default: break;
                            }

                           // this.currentPic.Image = bm;//保存流对象，完美
                            this.currentPic.Imagebyte = ms.ToArray();
                            // currentPic.Stream = bm;//将图片对象摄入期中
                            this.dicPics.Add(picpath, currentPic);
                            listBox1.Items.Add(FSInfo[i].ToString());
                            this.listboxPath.Add(picpath);//同步记录文件路径信息
                            
                        }
                    }
                }
            }
            sum = listBox1.Items.Count.ToString();
            if (listBox1.Items.Count > 0)
            {
                this.toolStripStatusLabel3.Text = sum;
                this.listBox1.SelectedIndexChanged -= new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
                this.listBox1.SelectedIndex = 0;//默认选择第1个并加载;
                this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
                LoadPic();
            }
#endif
        }


        #endregion

        #region 工具栏
        string selectedDirPath;
        public string sum;
        private void toolStripButton1_Click(object sender, EventArgs e)//工具栏中的“打开”
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                //listBox1.Items.Clear();
                selectedDirPath = folderBrowserDialog1.SelectedPath;
                DirectoryInfo DInfo = new DirectoryInfo(selectedDirPath);
                FileSystemInfo[] FSInfo = DInfo.GetFileSystemInfos();
                for (int i = 0; i < FSInfo.Length; i++)
                {
                    string FileType = FSInfo[i].ToString().Substring(FSInfo[i].ToString().LastIndexOf(".") + 1, (FSInfo[i].ToString().Length - FSInfo[i].ToString().LastIndexOf(".") - 1));
                    FileType = FileType.ToLower();
                    if (FileType == "jpg" || FileType == "png" || FileType == "bmp" || FileType == "gif" || FileType == "jpeg")
                    {
                        
                        string picpath = selectedDirPath + "\\" + FSInfo[i].ToString();
                    
                        if (!this.listboxPath.Contains(picpath))//不能添加相同
                        {
                            this.currentPic = new ReadingPicture();
                            this.currentPic.FileType=FileType;
                           // Image im = Image.FromFile(picpath);
                            Bitmap bm = new Bitmap(picpath);
                            using (MemoryStream ms = new MemoryStream())
                            {
                                switch (FileType)
                                {
                                    case "jpeg":
                                    case "jpg":
                                        bm.Save(ms, ImageFormat.Jpeg);
                                        break;
                                    case "png":
                                        bm.Save(ms, ImageFormat.Png);
                                        break;
                                    case "bmp":
                                        bm.Save(ms, ImageFormat.Bmp);
                                        break;
                                    case "gif":
                                        bm.Save(ms, ImageFormat.Gif);
                                        break;
                                    default: break;
                                }

                                //  this.currentPic.Image = bm;//保存流对象，完美
                                this.currentPic.Imagebyte = ms.ToArray();
                                // currentPic.Stream = bm;//将图片对象摄入期中
                                this.dicPics.Add(picpath, currentPic);
                                listBox1.Items.Add(FSInfo[i].ToString());
                                this.listboxPath.Add(picpath);//同步记录文件路径信息
                                
                            }
                        }
                    }
                }
                sum = listBox1.Items.Count.ToString();
                if (listBox1.Items.Count> 0)
                {
                    this.toolStripStatusLabel3.Text = sum;
                    this.listBox1.SelectedIndexChanged -= new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
                    this.listBox1.SelectedIndex = 0;//默认选择第1个并加载;
                    this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
                    LoadPic();
                }
               
            }
        }
        void DisplaySum()//显示1/4
        {
            this.toolStripStatusLabel1.Text = (this.listBox1.SelectedIndex+1).ToString();//显示当前选择
            this.toolStripStatusLabel3.Text = (this.listBox1.Items.Count).ToString();//总数
        }
        private void toolStripButton2_Click(object sender, EventArgs e)//刷新按钮
        {
            if (listBox1.Items.Count == 0)
            {
                
                ToolStatusUnable();
                this.pictureBox1.Image = null;
                this.curlistBoxSelectedIndex = this.listBox1.SelectedIndex = -1;//默认选择第并加载;
            }
            else
            {
                //  ToolStatusEnable();
              //  this.curlistBoxSelectedIndex = this.listBox1.SelectedIndex = this.listBox1.Items.Count - 1;//默认选择添加的那张图片进行加载;     
                //       this.curlistBoxSelectedIndex = this.listBox1.SelectedIndex = 0;//默认选择第1个并加载;
                LoadPic();//加载图片
              
               // this.toolStripStatusLabel1.Text = "1";//显示当前选择
            }
         //   this.sum = this.listBox1.Items.Count.ToString();//
            //this.toolStripStatusLabel3.Text = sum;//显示总数

            
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
          //  SavePictrue();//保存图片,才可以打印
            this.TopMost = false;
            打印ToolStripMenuItem_Click(sender, e);
        }

       
        //private void 状态栏ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (状态栏ToolStripMenuItem.CheckState == CheckState.Checked)
        //    {
        //        状态栏ToolStripMenuItem.CheckState = CheckState.Unchecked;
        //        statusStrip1.Visible = false;
        //    }
        //    else
        //    {
        //        状态栏ToolStripMenuItem.CheckState = CheckState.Checked;
        //        statusStrip1.Visible = true;
        //    }
        //}

        //private void 工具栏ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (工具栏ToolStripMenuItem.CheckState == CheckState.Checked)
        //    {
        //        工具栏ToolStripMenuItem.CheckState = CheckState.Unchecked;
        //        toolStrip1.Visible = false;
        //    }
        //    else
        //    {
        //        工具栏ToolStripMenuItem.CheckState = CheckState.Checked;
        //        toolStrip1.Visible = true;
        //    }
        //}
        //private void 图片信息ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (图片信息ToolStripMenuItem.CheckState == CheckState.Checked)
        //    {
        //        图片信息ToolStripMenuItem.CheckState = CheckState.Unchecked;
        //        this.toolStripStatusLabel7.Visible = false;
        //    }
        //    else
        //    {
        //        图片信息ToolStripMenuItem.CheckState = CheckState.Checked;
        //        this.toolStripStatusLabel7.Visible = true;
        //    }
        //}
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            删除ToolStripMenuItem_Click(sender, e);
        }
        private void toolStripButton5_Click(object sender, EventArgs e)//工具栏“向上”
        {
            try
            {
                if (listBox1.SelectedIndex != 0)
                {
                    listBox1.SetSelected(listBox1.SelectedIndex - 1, true);
                }
            }
            catch
            { }
        }
        private void toolStripButton6_Click(object sender, EventArgs e)//工具栏“向下”
        {
            try
            {
                if (listBox1.SelectedIndex < listBox1.Items.Count - 1)
                {
                    listBox1.SetSelected(listBox1.SelectedIndex + 1, true);
                }
            }
            catch
            { }
        }
        private void 图片特效ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                frmSpecialEfficacy special = new frmSpecialEfficacy();
                special.ig = pictureBox1.Image;
                special.ShowDialog();
            }
        }
        private int rotateCount=0;//旋转次数
        private void toolStripButton7_Click(object sender, EventArgs e)//旋转
        {
            this.rotateCount++;
            RotatePic();
          //  this.rect.Width = this.rect.Height = 0;
            this.offsetx = this.offsety = 0;
            this.pictureBox1.Invalidate();//
        }
        Bitmap bitmap;
        private void RotatePic()
        {
            if (bitmap == null)
                bitmap = GetSourceMap(dicPics[filePath].Imagebyte);
            bitmap.RotateFlip(RotateFlipType.Rotate90FlipXY);
            if (pictureBox1.Image != null)
                pictureBox1.Image.Dispose();
            pictureBox1.Image =new Bitmap(bitmap);
            this.pictureBox1.Refresh();
        }
        #endregion

        #region 常用事件
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                return;
            }
            int printWidth = printDocument1.DefaultPageSettings.PaperSize.Width;
            int printHeight = printDocument1.DefaultPageSettings.PaperSize.Height;
            if (Convert.ToInt32(PictureWidth) <= printWidth)
            {
                float x = (printWidth - Convert.ToInt32(PictureWidth)) / 2;
                float y = (printHeight - Convert.ToInt32(Pictureheight)) / 2;
                e.Graphics.DrawImage(pictureBox1.Image, x, y, Convert.ToInt32(PictureWidth), Convert.ToInt32(Pictureheight));
            }
            else
            {
                if (Convert.ToInt32(PictureWidth) < Convert.ToInt32(Pictureheight))
                {
                    Bitmap bitmap = new Bitmap(pictureBox1.Image);
                    bitmap.RotateFlip(RotateFlipType.Rotate90FlipXY);
                    PictureBox pb = new PictureBox();
                    pb.Image = bitmap;
                    Single a = printWidth / Convert.ToSingle(Pictureheight);
                    e.Graphics.DrawImage(pb.Image, 0, 0, Convert.ToSingle(Pictureheight) * a, Convert.ToSingle(PictureWidth) * a);
                }
                else
                {
                    Single a = printWidth / Convert.ToSingle(PictureWidth);
                    e.Graphics.DrawImage(pictureBox1.Image, 0, 0, Convert.ToSingle(PictureWidth) * a, Convert.ToSingle(Pictureheight) * a);
                }

            }
        }
      

        public Bitmap image1;
        public string filePath;
        public string PictureWidth;
        public string Pictureheight;
        public double SelectFileSize;
        public List<string> listboxPath = new List<string>();
        int curlistBoxSelectedIndex = -1;
        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (curlistBoxSelectedIndex == this.listBox1.SelectedIndex)
                {
                    return;//没有变化
                }
                if (listBox1.SelectedIndex == -1)
                {
                    //this.listBox1.SelectedIndex = curlistBoxSelectedIndex;
                    return;
                }

                LoadPic();//加载图片
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.pictureBox1.Invalidate();
        }
        private void LoadPic()
        {
            ControlState();//根据订单与身份状态来改变控件状态
            curlistBoxSelectedIndex = this.listBox1.SelectedIndex;//记录当前选项
            filePath = this.listboxPath[listBox1.SelectedIndex];
            if (this.dicPics.Keys.Contains(filePath))
            {
               if(pictureBox1.Image!=null)
                this.pictureBox1.Image.Dispose();//先释放

                this.pictureBox1.Location = this.picFirstLocation;//最初的位置；
                image1 = GetSourceMap(dicPics[filePath].Imagebyte);
                PictureWidth = image1.Width.ToString();
                Pictureheight = image1.Height.ToString();   
                this.toolStripStatusLabel7.Text =  "分辨率：" + PictureWidth + "×" + Pictureheight;
                toolStripStatusLabel3.Text = sum;
                toolStripStatusLabel1.Text = Convert.ToString(listBox1.SelectedIndex + 1);
                this.pictureBox1.Size = this.picFirstSize;//最初的大小；
                this.pictureBox1.Location = this.picFirstLocation;//最初的位置：
                pictureBox1.Image = image1;
                GetZoomImage();
            }
           
            DisplaySum();//显示照片选择项以及总数；
            //加载之后，新的filePath路径
            if (dicPics.ContainsKey(filePath))
            {
                this.cbZoom.Items.AddRange(Zoom.getZoomNameList().ToArray());
                this.cbDyedMethod.Items.AddRange(DyedMethod.getDyedMethodNameList().ToArray());
                this.cbSampleType.Items.AddRange(SamplesType.getSampleTypeNameList().ToArray());
                currentPic = dicPics[filePath];
                if (currentPic.DyedMethodID != -1)
                    this.cbDyedMethod.Text = this.cbDyedMethod.Items[currentPic.DyedMethodID].ToString();
                if (currentPic.SamplesTypeID != -1)
                    this.cbSampleType.Text = this.cbSampleType.Items[currentPic.SamplesTypeID].ToString();
                if (currentPic.ZoomID != -1)
                    this.cbZoom.Text = this.cbZoom.Items[currentPic.ZoomID].ToString();
                //如果有客户端标签，则加载
                if (currentPic.ListMarks != null)
                {
                    List<PictureMark> pmseleted = currentPic.ListMarks;
                    foreach (PictureMark pm in pmseleted)
                    {
                        cbMarks.Items.Add(pm.Remark);
                    }
                    this.lblMarkCount.Text = pmseleted.Count.ToString();
                }
                else
                {
                    this.lblMarkCount.Text = "0";
                }
                //如果专家有标签，则加载专家标记
                if (currentPic.ListExpertMarks != null)
                {
                    List<PictureMark> pmseleted = currentPic.ListExpertMarks;
                    foreach (PictureMark pm in pmseleted)
                    {
                        this.cbExpertMarks.Items.Add(pm.Remark);
                    }
                    this.lblExpertMarksCount.Text = pmseleted.Count.ToString();
                }
                else
                {
                    this.lblExpertMarksCount.Text = "0";
                }
            }

            else
            {
                //添加完成后，生成新的阅片对象
                // this.currentPic = new ReadingPicture();
                this.lblMarkCount.Text = "0";
            }

        }
       
        #endregion

        #region 文件菜单

        private void 更改目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
        }

        private void 刷新ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e);
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //private void 设为桌面背景ToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    设为桌面背景ToolStripMenuItem_Click(sender, e);
        //}

        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            删除ToolStripMenuItem_Click(sender, e);
        }

        //private void 重命名ToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    重命名ToolStripMenuItem_Click(sender, e);
        //}

        private void 打印ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            打印ToolStripMenuItem_Click(sender, e);
        }

        private void bMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fName = filePath.Substring(filePath.LastIndexOf("\\") + 1, (filePath.LastIndexOf(".") - filePath.LastIndexOf("\\") - 1));
            string Opath = filePath.Remove(filePath.LastIndexOf("\\"));
            FileInfo file = new FileInfo(filePath);
            if (file.Extension.ToLower() != ".bmp")
            {
                string Npath;
                if (Opath.Length == 4)
                {
                    Npath = Opath;
                }
                else
                {
                    Npath = Opath + "\\";
                }
                //if (this.pictureBox1.Image != null)
                //    this.pictureBox1.Image.Dispose();
                Bitmap bt = new Bitmap(pictureBox1.Image);
                bt.Save(Npath + fName + ".bmp", ImageFormat.Bmp);
                FileInfo fi = new FileInfo(filePath);
                fi.Delete();
                toolStripButton2_Click(sender, e);
            }
        }

        private void gIFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fName = filePath.Substring(filePath.LastIndexOf("\\") + 1, (filePath.LastIndexOf(".") - filePath.LastIndexOf("\\") - 1));
            string Opath = filePath.Remove(filePath.LastIndexOf("\\"));
            FileInfo file = new FileInfo(filePath);
            if (file.Extension.ToLower() != ".gif")
            {
                string Npath;
                if (Opath.Length == 4)
                {
                    Npath = Opath;
                }
                else
                {
                    Npath = Opath + "\\";
                }
                Bitmap bt = new Bitmap(pictureBox1.Image);
                bt.Save(Npath + fName + ".gif", ImageFormat.Gif);
                FileInfo fi = new FileInfo(filePath);
                fi.Delete();
                toolStripButton2_Click(sender, e);
            }
        }

        private void jPGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fName = filePath.Substring(filePath.LastIndexOf("\\") + 1, (filePath.LastIndexOf(".") - filePath.LastIndexOf("\\") - 1));
            string Opath = filePath.Remove(filePath.LastIndexOf("\\"));
            FileInfo file = new FileInfo(filePath);
            if (file.Extension.ToLower() != ".jpg" || file.Extension.ToLower() != ".jpeg")
            {
                string Npath;
                if (Opath.Length == 4)
                {
                    Npath = Opath;
                }
                else
                {
                    Npath = Opath + "\\";
                }
                Bitmap bt = new Bitmap(pictureBox1.Image);
                bt.Save(Npath + fName + ".Jpeg", ImageFormat.Jpeg);
                FileInfo fi = new FileInfo(filePath);
                fi.Delete();
                toolStripButton2_Click(sender, e);
            }
        }

        private void pNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fName = filePath.Substring(filePath.LastIndexOf("\\") + 1, (filePath.LastIndexOf(".") - filePath.LastIndexOf("\\") - 1));
            string Opath = filePath.Remove(filePath.LastIndexOf("\\"));
            FileInfo file = new FileInfo(filePath);
            if (file.Extension.ToLower() != ".png")
            {
                string Npath;
                if (Opath.Length == 4)
                {
                    Npath = Opath;
                }
                else
                {
                    Npath = Opath + "\\";
                }
                Bitmap bt = new Bitmap(pictureBox1.Image);
                bt.Save(Npath + fName + ".Png", ImageFormat.Png);
                FileInfo fi = new FileInfo(filePath);
                fi.Delete();
                toolStripButton2_Click(sender, e);
            }
        }

        private void 图片调节ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPicAdjust picadjust = new frmPicAdjust();
            picadjust.ig = pictureBox1.Image;
            picadjust.PicOldPath = filePath;
            picadjust.ShowDialog();
        }
        private void 退出CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("退出系统？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void 幻灯片放映ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialog2.SelectedPath;
                frmSlide slide = new frmSlide();
                slide.Ppath = path;
                slide.ShowDialog();
            }
        }

        #endregion

        #region 右键菜单
        //private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string path = filePath.Remove(2, 1);
        //        System.Collections.Specialized.StringCollection files = new System.Collections.Specialized.StringCollection();
        //        files.Add(path);
        //        Clipboard.SetFileDropList(files);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (this.listBox1.SelectedIndex == -1)
                {
                    return;
                }
                if (MessageBox.Show("确定要移除吗？(不会删除源文件)", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    if (this.pictureBox1.Image != null) this.pictureBox1.Image.Dispose();
                    //File.Delete(listboxPath[this.listBox1.SelectedIndex]);
                    this.listBox1.SelectedIndexChanged -= new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
                    
                    this.dicPics.Remove(filePath);//移除当前图片
                    this.listboxPath.RemoveAt(this.curlistBoxSelectedIndex);
                    this.listBox1.Items.RemoveAt(curlistBoxSelectedIndex);
                    toolStripButton2_Click(sender, e);
                    this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e);
        }
        //private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    string fName = filePath.Substring(filePath.LastIndexOf("\\") + 1, (filePath.LastIndexOf(".") - filePath.LastIndexOf("\\") - 1));
        //    string fType = filePath.Substring(filePath.LastIndexOf(".") + 1, (filePath.Length - filePath.LastIndexOf(".") - 1));
        //    frmRename rename = new frmRename();
        //    rename.filename = fName;
        //    rename.filepath = filePath;
        //    rename.filetype = fType;
        //    rename.ShowDialog();
        //}

        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        //private void 设为桌面背景ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //获取指定图片的扩展名
        //    string SFileType = filePath.Substring(filePath.LastIndexOf(".") + 1, (filePath.Length - filePath.LastIndexOf(".") - 1));
        //    //将扩展名转换成小写
        //    SFileType = SFileType.ToLower();
        //    //获取文件名
        //    string SFileName = filePath.Substring(filePath.LastIndexOf("\\") + 1, (filePath.LastIndexOf(".") - filePath.LastIndexOf("\\") - 1));
        //    //如果图片的类型是bmp则调用API中的方法将其设置为桌面背景
        //    if (SFileType == "bmp")
        //    {
        //        SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, filePath, 1);
        //    }
        //    else
        //    {
        //        string SystemPath = Environment.SystemDirectory;//获取系统路径
        //        string path = SystemPath + "\\" + SFileName + ".bmp";
        //        FileInfo fi = new FileInfo(path);
        //        if (fi.Exists)
        //        {
        //            fi.Delete();
        //            PictureBox pb = new PictureBox();
        //            pb.Image = Image.FromFile(filePath);
        //            pb.Image.Save(SystemPath + "\\" + SFileName + ".bmp", ImageFormat.Bmp);
        //        }
        //        else
        //        {
        //            PictureBox pb = new PictureBox();
        //            pb.Image = Image.FromFile(filePath);
        //            pb.Image.Save(SystemPath + "\\" + SFileName + ".bmp", ImageFormat.Bmp);
        //        }
        //        SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, 1);
        //    }
        //}

        public Bitmap CopyBitmap(Bitmap source)
        {
            int depth = Bitmap.GetPixelFormatSize(source.PixelFormat);
            if (depth != 8 && depth != 24 && depth != 32)
            {
                return null;
            }
            Bitmap destination = new Bitmap(source.Width, source.Height, source.PixelFormat);
            BitmapData source_bitmapdata = null;
            BitmapData destination_bitmapdata = null;
            try
            {
                source_bitmapdata = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadWrite,
                                                source.PixelFormat);
                destination_bitmapdata = destination.LockBits(new Rectangle(0, 0, destination.Width, destination.Height), ImageLockMode.ReadWrite,
                                                destination.PixelFormat);
                //unsafe
                //{
                //    byte* source_ptr = (byte*)source_bitmapdata.Scan0;
                //    byte* destination_ptr = (byte*)destination_bitmapdata.Scan0;
                //    for (int i = 0; i < (source.Width * source.Height * (depth / 8)); i++)
                //    {
                //        *destination_ptr = *source_ptr;
                //        source_ptr++;
                //        destination_ptr++;
                //    }
                //}
                source.UnlockBits(source_bitmapdata);
                destination.UnlockBits(destination_bitmapdata);
                return destination;
            }
            catch
            {
                destination.Dispose();
                return null;
            }
        }
        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = "BMP|*.bmp|JPEG|*.jpeg|GIF|*.gif|PNG|*.png";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string picPath = saveFileDialog1.FileName;
                    string picType = picPath.Substring(picPath.LastIndexOf(".") + 1, (picPath.Length - picPath.LastIndexOf(".") - 1));
                    switch (picType)
                    {
                        case "bmp":
                            Bitmap bt = GetSourceMap(dicPics[filePath].Imagebyte);
                            bt.Save(picPath, ImageFormat.Bmp); bt.Dispose();break;
                        case "jpeg":
                            Bitmap bt1 = GetSourceMap(dicPics[filePath].Imagebyte);
                            bt1.Save(picPath, ImageFormat.Jpeg);bt1.Dispose(); break;
                        case "gif":
                            Bitmap bt2 = GetSourceMap(dicPics[filePath].Imagebyte);
                            bt2.Save(picPath, ImageFormat.Gif); bt2.Dispose();break;
                        case "png":
                            Bitmap bt3 = GetSourceMap(dicPics[filePath].Imagebyte);
                            bt3.Save(picPath, ImageFormat.Png);bt3.Dispose(); break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void 另存为ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            另存为ToolStripMenuItem_Click(sender, e);
        }
        #endregion

       
        private void 图片文字ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                frmWater water = new frmWater();
                water.ig = pictureBox1.Image;
                water.FPath = filePath;
                water.ShowDialog();
            }
        }


        private void toolStripButton8_Click(object sender, EventArgs e)
        {
           // this.rect.Width = this.rect.Height = 0;
            ZoomPic(5);
        }
     
        private Bitmap GetSourceMap(byte [] info)
        {
            Bitmap bmp;
            using (MemoryStream ms = new MemoryStream(info))
            {
                bmp = new Bitmap(ms);
            }
            return bmp;
        }
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "图片文件|*.bmp;*.jpg;*.jpeg;*.gif;*.png"; //过滤文件类型
            fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);//设定初始目录
            fd.ShowReadOnly = false; //设定文件是否只读
            fd.Multiselect = false;
            DialogResult r = fd.ShowDialog();
            if (r == DialogResult.OK)
            {
                 if (!this.listboxPath.Contains(fd.FileName))
                {
                    this.currentPic = new ReadingPicture();
                     string filetype=fd.FileName.Substring(fd.FileName.LastIndexOf(".") + 1, (fd.FileName.Length - fd.FileName.LastIndexOf(".") - 1));
                   
                     Bitmap bm = new Bitmap(fd.FileName);
                    currentPic.FileType = filetype.ToLower();//保存后缀;
                    using (MemoryStream ms = new MemoryStream())
                    {

                        switch (currentPic.FileType)
                        {
                            case "jpeg":
                            case "jpg":
                                bm.Save(ms, ImageFormat.Jpeg);
                                break;
                            case "png":
                                bm.Save(ms, ImageFormat.Png);
                                break;
                            case "bmp":
                                bm.Save(ms, ImageFormat.Bmp);
                                break;
                            case "gif":
                                bm.Save(ms, ImageFormat.Gif);
                                break;
                            default: break;
                        }

                        //this.currentPic.Image = bm;//
                        this.currentPic.Imagebyte = ms.ToArray();//保存流字节
                        // currentPic.Image = bm;//将图片对象摄入期中
                        // Image im = Image.FromFile(fd.FileName);
                        // Bitmap bmp = new Bitmap(fd.FileName);
                        //currentPic.Image=bmp;//设置ReadingPicture的image属性
                        this.dicPics.Add(fd.FileName, currentPic);//每次成功添加后
                        this.listBox1.Items.Add(fd.SafeFileName); //添加文件名，后续处理
                        this.listboxPath.Add(fd.FileName);//记录完整文件信息；
                        this.sum = this.listBox1.Items.Count.ToString();//自增1;
                        this.toolStripStatusLabel3.Text = sum;
                       
                    }
                }
                this.listBox1.SelectedIndexChanged -= new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
                this.curlistBoxSelectedIndex=this.listBox1.SelectedIndex = this.listBox1.Items.Count-1;//默认选择添加的那张图片进行加载;     
                LoadPic();
                this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
            }
        }
      
        int x, y;//鼠标按下的屏幕坐标
        bool isMoveable = false;

        private Point curMarkLocation;//鼠标按下时的相对于图片的坐标;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            CaculateMarkLocationImageXY();//转为图片坐标;
            if (curMarkLocation.X <= 0 ||curMarkLocation.X>this.imageW|| curMarkLocation.Y <= 0||curMarkLocation.Y > imageH)
            {
                this.pictureBox1.ContextMenuStrip = null;
            }
            else
            {
                this.pictureBox1.ContextMenuStrip = this.contextMenuStrip1;
            }
            if (e.Button == MouseButtons.Right)
            {
                this.toolStripMenuItemMousePointer.Text = "当前位置：" + curMarkLocation.ToString();
                //if (isMarking)//如果是在标记
                //{
                //    isMarking = false;
                //}

            }
            else
            {
                rect = new Rectangle(e.X, e.Y, 0, 0);
                x = e.X;
                y = e.Y;
                isMoveable = true;
                this.panel1.Focus();
              
            }
            this.pictureBox1.Invalidate();
        }
         private void CaculateMarkLocationPictureBoxXY()
        { 
            if (imageW > imageH)//图片的长度大于宽度
            {
                int gapH = (this.pictureBox1.Height - this.imageH) / 2;
                //转为图片坐标系
                curMarkLocation = new Point(curMarkLocation.X, curMarkLocation.Y + gapH);
            }
            else
            {
                int gapW = (this.pictureBox1.Width - this.imageW) / 2;
                curMarkLocation = new Point(curMarkLocation.X +gapW, curMarkLocation.Y);

            }
        }
        private void CaculateMarkLocationImageXY()
        {
            curMarkLocation = this.pictureBox1.PointToClient(Control.MousePosition);
            if (imageW > imageH)//图片的长度大于宽度
            {
                int gapH = (this.pictureBox1.Height - this.imageH) / 2;
                //转为图片坐标系
                curMarkLocation = new Point(curMarkLocation.X, curMarkLocation.Y - gapH);
            }
            else
            {
                int gapW = (this.pictureBox1.Width - this.imageW) / 2;
                curMarkLocation = new Point(curMarkLocation.X - gapW, curMarkLocation.Y);

            }
        }
      //  private bool isMarking=false;
        private int offsetx, offsety = 0;
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left&&isMarking)
            //{
            //    rect = new Rectangle(rect.Left, rect.Top, e.X - rect.Left, e.Y - rect.Top);
            //    this.pictureBox1.Invalidate();
            //}
           
            if (isMoveable == true)
            {
               int ex  = e.X - x;
               offsetx += ex;
                int ey= e.Y - y;
                offsety += ey;
                pictureBox1.Location = new Point(pictureBox1.Left + ex, pictureBox1.Top + ey);
              
            }
           
        }
       
        private IDictionary<string,ReadingPicture> dicPics=new Dictionary<string,ReadingPicture>();
        ReadingPicture currentPic;
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMoveable = false;
            //Point p = this.pictureBox1.Location;
            //if (rect.Width>0&&isMarking)//如果正在计算
            //{
            //    rotateCount = rotateCount % 4;
            //    this.recWidth = rect.Width;//所画矩形宽度
            //    this.recHeight = rect.Height;//所画矩形高度;
            //    this.pictureBox1.Invalidate();
            //}
        }

        private void CreatePictureMark(frmRemark frmremark)
        {
            List<PictureMark> listMarks = new List<PictureMark>();
            string remark = frmremark.GetRemark();
            PointF markvison = CaculateMarkVision();
            PointF markLocation = CaculateMarkLocation();
            PictureMark pm = new PictureMark();
            pm.setM_MarkColor(new int[] { 255, 255, 0, 0 });
            pm.setM_MarkLocation(new float[] { markLocation.X, markLocation.Y });
            pm.setM_MarkVision(new float[] { markvison.X, markvison.Y });
            pm.PictureScale = normalscale;
            pm.Remark = frmremark.GetRemark();
            //PictureMark pm = new PictureMark(this.pictureBox1.Location.X, this.pictureBox1.Location.Y, this.pictureBox1.Width, this.pictureBox1.Height,
            //    rect.X, rect.Y, recWidth, recHeight, EDrawingType.Rectangle, remark, rotateCount);
            listMarks.Add(pm);

            currentPic = dicPics[filePath];//获取当前图片对象，
            if (currentPic.ListMarks == null)//如果客户图片中标记列表为空
            {
                if (this.usertype == EUserType.NormalClient)
                {
                    currentPic.ListMarks = listMarks;//

                }

            }
            else//如果不为空
            {
                if (this.usertype == EUserType.NormalClient)
                    currentPic.ListMarks.Add(pm);

            }
            if (currentPic.ListExpertMarks == null)
            {
                if (this.usertype == EUserType.Expert)

                    currentPic.ListExpertMarks = listMarks;

            }
            else
            {
                if (this.usertype == EUserType.Expert)
                    currentPic.ListExpertMarks.Add(pm);
            }

            if (this.usertype == EUserType.NormalClient)
            {
                this.cbMarks.Items.Add(pm.Remark);
                this.lblMarkCount.Text = currentPic.ListMarks.Count.ToString();//显示当前图片标记总数

            }
            else if (this.usertype == EUserType.Expert)
            {
                this.lblExpertMarksCount.Text = currentPic.ListExpertMarks.Count.ToString();
                this.cbExpertMarks.Items.Add(pm.Remark);
            }
            if (currentPic.ListMarks != null)
                currentPic.ClientPictureMarksCount = currentPic.ListMarks.Count;
            if (currentPic.ListExpertMarks != null)
                currentPic.ExpertPictureMarksCount = currentPic.ListExpertMarks.Count;
        }

        private PointF CaculateMarkLocation()
        {
            PointF marklocation = new PointF();
            marklocation.X = (float)curMarkLocation.X / imageW;
            marklocation.Y = (float)curMarkLocation.Y / imageH;
           return marklocation; 
        }

        private PointF CaculateMarkVision()
        {
            PointF vision = new PointF();
            float vx = (float)this.imageW / 2 - (float)offsetx;//可视区域中心点得到图片坐标系的x

            float vy = (float)this.imageH / 2 - (float)offsety;//
            vision.X = vx / this.imageW;
            vision.Y = vy / this.imageH;
            return vision;
        }
        int nt = 0;
       
       
        public void SetPictureLocation()//居中放大;
        {
            int w = (this.panel1.Width - pictureBox1.Width) / 2;
            int h = (this.panel1.Height - pictureBox1.Height) / 2;
            Point p = new Point();
            p.X = w + offsetx;
            p.Y = h + offsety;
            pictureBox1.Location = p;
        }
        private float normalscale = 1.0f;
        private float minscale=1f;//最小缩放倍数;
        private float maxscale=5f;//最大缩放倍数
       
        private void ResizePic(int c)
        {
            //if (filePath != "")
            //{
            //    if (c > 0)
            //    {
            //        timer1.Start();

            //    }
            //    else
            //    {
            //        timer2.Start();

            //    }

            //    窗体宽高作为参照，所以窗体宽高不能变，可以考虑用其他作参照物，窗体和图片一起放大缩小

            //}

        }
        private void frmMain_Resize(object sender, EventArgs e)
        {
           // this.rect.Width = 0;//取消标记
            SetPictureLocation();
        }
        private void ZoomPic(int delta)
        {
            //this.rect.Width = this.rect.Height = 0;
            if (delta >0)
            {
                    normalscale *= 1.1f;
                    if (normalscale > maxscale)
                    {
                        normalscale = maxscale;
                    }
                   
            }
            else if (delta < 0)
            {
                    normalscale *= 0.9f;
                    if (normalscale < minscale)
                    {
                        normalscale = minscale;
                    }
                    
            }
            float w = this.picFirstSize.Width * normalscale; //每次縮小 10%  
            float h = this.picFirstSize.Height * normalscale;
            this.pictureBox1.Size = Size.Ceiling(new SizeF(w, h));
            GetZoomImage();
            SetPictureLocation();
        }

        private void GetZoomImage()
        {
            PropertyInfo _ImageRectanglePropert = this.pictureBox1.GetType().GetProperty("ImageRectangle", BindingFlags.Instance | BindingFlags.NonPublic);
            Rectangle g = (Rectangle)_ImageRectanglePropert.GetValue(this.pictureBox1, null);
            imageW = g.Width;
            imageH = g.Height;
        }
   
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            this.rect.Width = 0;
            if (listboxPath.Count > 0 && listBox1.SelectedIndex >= 0)
            {
                nt = 0;
                timer1.Stop();
                timer2.Stop();
                ZoomPic(e.Delta);
            }
           

        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            this.rect.Width  = 0;
            ZoomPic(-5);
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            this.panel1.Focus();
        }

        private void 标记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmRemark frmremark = new frmRemark();
            Point p = Control.MousePosition;
            p.Y += 20;
            p.X -= 20;
            frmremark.Location = new Point(p.X, p.Y);
            DialogResult dr = frmremark.ShowDialog();
            if (dr == DialogResult.OK)
            {
                CreatePictureMark(frmremark);
                CaculateMarkLocationPictureBoxXY();//转换坐标
                CaculateRect();
            }
            //else
            //    this.rect.Width = 0;//取消未完成标记框
          
            this.pictureBox1.Invalidate();
        }
        private Rectangle rect = new Rectangle();
        private int recWidth;//矩形宽度
        private int recHeight;//矩形长度
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
        //   if (isMarking)
            {
                using (Pen pen = new Pen(Color.FromArgb(curMarkColor[1],curMarkColor[2],curMarkColor[3]), 2))
                {
                    e.Graphics.DrawRectangle(pen, rect);
                }
            }
     
        }
    

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox1.Focus();
        }

        private void cbSampleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
         //   this.currentPic = this.dicPics[filePath];
            this.currentPic.SamplesTypeID = this.cbSampleType.SelectedIndex;//选择项

        }

        private void cbDyedMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
           // this.currentPic = this.dicPics[filePath];
            this.currentPic.DyedMethodID = this.cbDyedMethod.SelectedIndex;
        }

        private void cbZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.currentPic = this.dicPics[filePath];
            this.currentPic.ZoomID = this.cbZoom.SelectedIndex;

        }
        private IRapidPassiveEngine rapidPassiveEngine;
        private void SendUpdateMedicalReading(byte[] info)//专家或用户更新阅片基础信息
        {
            try
            {
                 this.rapidPassiveEngine.SendMessage(null, InformationTypes.SendUpdateMedicalReading, info, this.curMR.MedicalReadingID);//发送List<ReadingPicture>与MedicalReadingID；
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("出错!" + ex.ToString());
            }
        }
        private void SendBlobThread(byte[] blob)
        {
           try
            {
               // string userfrom = this.rapidPassiveEngine.CurrentUserID;
	             this.rapidPassiveEngine.CustomizeOutter.SendBlob(null, InformationTypes.SendMedicalReading, blob, 2048);
             }
           catch (System.Exception ex)
           {
               MessageBox.Show("出错!" + ex.ToString());
           }
        }
        IAsyncResult ir;
      
        private string userIDFrom;
        private string userIDTo;
        private void HandleSentResult(bool succeed, object tag)
        {
            
            if (!succeed)
            {
                MessageBox.Show("因为网络原因，刚才的消息尚未发送成功！");
            }
        }
       
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //首先判断每张照片是否都已经填写标本信息
            Button btn = (Button)sender;
            if (this.usertype==EUserType.Expert&&btn.Text == "接收")
            {
                if (MessageBox.Show("确定要接收吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    this.rapidPassiveEngine.SendMessage(null, InformationTypes.SendMedicalReadingReceived, null, curMR.MedicalReadingID);
                    //if (MessageBox.Show("接收成功") == DialogResult.OK)//没有得到服务器的回复，可能会有bug；
                    //{

                    //    this.curMR.ReadingStatus = EReadingStatus.Processing;
                    //    // this.ControlState();
                    //   // this.readingStatus = EReadingStatus.Processing;
                    //    this.LoadPic();
                    //    //      MedicalReading mr = new MedicalReading(this.curMR);
                    //    this.main.UpdateMedicalReading(this.curMR);//本地更新
                    //    return;
                    //}
                }
            }
            else   if (this.usertype==EUserType.NormalClient&&!this.isload&&btn.Text == "提交服务器") //客户端提交申请
            { 
                List<ReadingPicture> listPic = new List<ReadingPicture>();
                if (this.listBox1.Items.Count < 1)
                {
                    return;
                }
                if (MessageBox.Show("确定要提交吗？","提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                
                    foreach (KeyValuePair<string, ReadingPicture> kvrp in dicPics)
                    {
                   
                        if (kvrp.Value.SamplesTypeID == -1 || kvrp.Value.DyedMethodID == -1 || kvrp.Value.ZoomID == -1)
                        {
                            MessageBox.Show("每张图片标本信息必须填写，亲!");
                            return;
                        }
                            //接下来填充图片列表对象的图片属性
                        listPic.Add(kvrp.Value);//获取图片对象
                   
                    }
                   
                    curMR.ListPics = listPic;
                    //发送者，接受者ID,图片列表
                    this.mdSubmit = new MedicalReading(userIDFrom, userIDTo, EReadingStatus.UnProcessed, this.dicPics.Keys.Count, listPic);
                    //填充完毕后，发送至服务器
                    this.mdSubmit.MedicalReadingID = curMR.MedicalReadingID;
                    byte[] info = CompactPropertySerializer.Default.Serialize(this.mdSubmit);

                    FrmStatus fs = new FrmStatus();
                    fs.Show("正在提交，请稍后", true);
                        SendBlobThread(info);
                   
                }
#if Debug
            userIDFrom="100001";
            userIDTo="100002";
#endif
                        
            }
        else   if (isload&&this.curMR.ReadingStatus==EReadingStatus.Processing&& btn.Text == "提交服务器")//已有阅片，在正在处理的状态，进行阅片修改提交
            {
                if (MessageBox.Show("提交服务器吗？", "完成工作", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    if (this.usertype == EUserType.Expert)//如果是专家提交修改阅片，只能添加专家标签信息；不能修改其他
                    {
                        //if(this.curMR.ListPics)
                        List<ReadingPicture> rplist = new List<ReadingPicture>();

                        foreach (ReadingPicture rp in this.curMR.ListPics)
                        {
                            if (rp.ExpertConclusion == null||rp.ExpertConclusion=="")
                            {
                                MessageBox.Show("每张图片都需要您下结论");
                                return;
                            }
                            ReadingPicture newrp = new ReadingPicture(rp);//除了图片进行基础信息克隆
                            rplist.Add(newrp);
                        }

                        byte[] info = CompactPropertySerializer.Default.Serialize(rplist);
                        FrmStatus fs = new FrmStatus();
                        fs.Show("正在提交，请稍后", true);
                            SendUpdateMedicalReading(info);
                        
                        //if (MessageBox.Show("提交成功") == DialogResult.OK)
                        //{
                        //    MedicalReading update = new MedicalReading(this.curMR);
                        //  //  this.readingStatus = EReadingStatus.Completed;
                        //    update.ReadingStatus = EReadingStatus.Completed;//更改状态;
                            
                        // //   this.ControlState();
                        //    this.LoadPic();
                        //    //MedicalReading mr = new MedicalReading(this.curMR);//完整信息克隆
                        //    this.main.UpdateMedicalReading(update);
                        //}
                    }
                    else if (this.usertype == EUserType.NormalClient)//如果是客户
                    {
                        

                    }
                    

                    
                }
                
            }
        }

        private void splitContainer1_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            this.pictureBox1.Invalidate();//重画图像；
        }
       
        private void btnEnterTalk_Click(object sender, EventArgs e)
        {
            Button  btn=(Button)sender;
            if (btn.Text == "对话"&&(this.usertype==EUserType.NormalClient ||this.usertype==EUserType.Expert))
            {
                ChatForm form;
                if (this.usertype == EUserType.NormalClient)
                {
                     form = this.main.GetChatForm(this.userIDTo);
                }
                else
                {
                     form = this.main.GetChatForm(this.userIDFrom);
                }
                form.Show();
                form.Focus();
            }
                else if(btn.Text=="查看拒绝理由")
            {
                frmRejectedReason frm = new frmRejectedReason();
                frm.SetRejectedReason(this.curMR.RejectedReason);
                frm.ShowDialog();
            }
            else if (btn.Text == "拒绝")//
            {
                if (this.usertype == EUserType.Expert)//只有专家才可以拒绝
                {
                    frmRejectedReason frmreject = new frmRejectedReason();
                   
                    if (frmreject.ShowDialog() == DialogResult.OK)
                    {  
                        string rejectedreason = frmreject.GetRejectedReason() ;
                        string complex = rejectedreason + ";" + this.curMR.MedicalReadingID;
                        //curMR.RejectedReason = rejectedreason;
                        //curMR.IsRejected = true;
                       
                 //       byte[] reason = ESPlus.Serialization.CompactPropertySerializer.Default.Serialize(rejectedreason);
                        this.rapidPassiveEngine.SendMessage(null, InformationTypes.SendMedicalReadingRejectedReason, null, complex);
                        //if(MessageBox.Show("提交拒绝信息成功")==DialogResult.OK)
                        //{
                        //    this.curMR.ReadingStatus=EReadingStatus.Rejected;
                        // //   this.readingStatus = EReadingStatus.Rejected;
                        //    LoadPic();
                        //    this.curMR.IsRejected=true;
                        //    this.curMR.RejectedReason=rejectedreason;
                        //    this.main.UpdateMedicalReading(this.curMR);//本地更新
                        //}
                    }
                }
            }
        }

        private void cbMarks_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           // this.currentPic = this.dicPics[filePath];//获取当前图片
            this.offsetx = this.offsety = 0;
            string remarkselected = this.cbMarks.SelectedItem.ToString();
            foreach (PictureMark pm in currentPic.ListMarks)
            {
                if (remarkselected == pm.Remark)
                {
                    LoadPictureMark(pm);
                         break;
                }
            }
            this.pictureBox1.Invalidate();//重画控件
        }

        private bool LoadPictureMark(PictureMark pm)
        {
            try
            {
                float[] marklocation = pm.M_MarkLocation;
	            float[] markvision = pm.M_MarkVision;
	            curMarkColor = pm.M_MarkColor;
	           // float scale = pm.PictureScale;
                normalscale = pm.PictureScale;//更新现在的放大倍数;
                float w = this.picFirstSize.Width * normalscale;
                float h = this.picFirstSize.Height * normalscale;
	            this.pictureBox1.Size = Size.Ceiling(new SizeF(w, h));
	            GetZoomImage();
	            float markx = marklocation[0] * this.imageW;
	            float marky = marklocation[1] * this.imageH;
	            curMarkLocation = new Point((int)markx, (int)marky);
	            CaculateMarkLocationPictureBoxXY();//将标记点转为PictureBox坐标；
	            CaculateRect();
	            //通过markVison计算出offset
	            float markvisionx = markvision[0] * this.imageW;
	            float markvisiony = markvision[1] * this.imageH;
	            offsetx = (int)((float)this.imageW / 2 - markvisionx);
	            offsety = (int)((float)this.imageH / 2 - markvisiony);
	            SetPictureLocation();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("加载标签失败!" + ex.ToString());
                return false;
            }
            return true;
        }

        private Size recFirst = new Size(40, 30);
        private void CaculateRect()
        {
            //float recW = recFirst.Width * normalscale;
            //float recH = recFirst.Height * normalscale;

            Point recloca = new Point(curMarkLocation.X - recFirst.Width/2, curMarkLocation.Y - recFirst.Height/2);
            this.rect = new Rectangle(recloca, recFirst);//标记位置
        }

       

        private void cbExpertMarks_SelectedIndexChanged(object sender, EventArgs e)
        {
           // this.currentPic = this.dicPics[filePath];//获取当前图片
            this.offsetx = this.offsety = 0;
            string remarkselected = this.cbExpertMarks.SelectedItem.ToString();

            foreach (PictureMark pm in currentPic.ListExpertMarks)
            {
                if (remarkselected == pm.Remark)
                {
                    LoadPictureMark(pm);//加载标签;
                }
            }
            this.pictureBox1.Invalidate();//重画控件
        }





        private MedicalReading mdSubmit;
        private int[] curMarkColor=new int[]{255,255,0,0};
        private int imageW;
        private int imageH;
    //    private EReadingStatus readingState=EReadingStatus.UnProcessed;
        public  MedicalReading GetSubmitMD()
        {
          
                return mdSubmit;
        }

        private void btnExpertConclusion_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == "查看结论")//查看
            {
                frmRemark note = new frmRemark();
                note.StartPosition = FormStartPosition.CenterParent;
                note.setLableName("专家结论");
                note.SetRemark(currentPic.ExpertConclusion);
                note.setIsLoad(true);//设置为加载状态，不能清空，不能编辑;
                note.ShowDialog();

            }
            else if (btn.Text == "输入结论" && this.usertype == EUserType.Expert)//只有用户可以备注
            {
                frmRemark note = new frmRemark();
                note.StartPosition = FormStartPosition.CenterParent;
                note.setLableName("专家结论:");
                note.SetRemark(currentPic.ExpertConclusion);
                if (note.ShowDialog() == DialogResult.OK)
                {
                    string expertcon = note.GetRemark();
                    currentPic.ExpertConclusion = expertcon;
                }
            }
          
        }

        private void btnClientNote_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == "查看备注")//查看
            {
                frmRemark note = new frmRemark();
                note.StartPosition = FormStartPosition.CenterParent;
                note.setLableName("用户备注:");
                note.SetRemark(currentPic.ClientNote);
                note.setIsLoad(true);//设置为加载状态，不能清空，不能编辑;
                note.ShowDialog();
                
            }
            else if (btn.Text == "备注" && this.usertype == EUserType.NormalClient)//只有用户可以备注
            {
                frmRemark note = new frmRemark();
                note.StartPosition = FormStartPosition.CenterParent;
                note.setLableName("用户备注:");
               
                 note.SetRemark(currentPic.ClientNote);
                if (note.ShowDialog() == DialogResult.OK)
                {
                    string clientnote = note.GetRemark();
                    currentPic.ClientNote = clientnote;
                }
            }
            
           
        }

        private void frmMain_Click(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.Focus();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
         //   System.Environment.Exit(System.Environment.ExitCode);
            timer1.Dispose();
            timer2.Dispose();

           
        }
        public void LoadImage()
        {
           
        }
        private Thread loadPicThread;
        private void frmMain_Shown(object sender, EventArgs e)
        {


        }

        private void toolStripButton3_DoubleClick(object sender, EventArgs e)
        {

        }

        private void frmMain_DoubleClick(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.Focus();
        }

        private void printPreviewDialog1_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            this.printPreviewDialog1.Focus();
        }
    }
}