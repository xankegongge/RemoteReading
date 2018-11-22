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
        //private GlobalUserCache globalUserCache; //�����û�����
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
            m_SyncContext = SynchronizationContext.Current;////��ȡUI�߳�ͬ��������

        }

       
        
        //����globaluserCache,ndiskoutter formmananger
        public frmMain(IRapidPassiveEngine rapid, MainForm main, GGUser cur, GGUser tar, MedicalReading mr):this()
        {
            this.main = main;
            main.UpdateMREvent += new MainForm.LoadHandler(UpdateMR);//ע���¼�
            this.usertype = cur.UserType;
            this.rapidPassiveEngine = rapid;
            if (mr == null)//������½���Ƭ
            {
                this.userIDFrom = cur.UserID;
                this.userIDTo = tar.UserID;
                this.curMR = new MedicalReading();
               // curMR.ListPics = dicPics;
                this.Text = "  ��ר�ң�" + tar.PersonName + "������Ƭ";
             //   this.skinPanelExpert.Enabled = false;
                this.btnEnterTalk.Visible = false;//������ʧ
                this.skinPanelExpert.Visible = false;//
                this.DialogResult = DialogResult.Abort;//Ĭ����������ύ�ɹ���Ż�
            }
            else//
            {

                   // this.readingState = mr.ReadingStatus;//��ȡ����״̬
                   // this.usertype = cur.UserType;
                   // this.readingStatus = mr.ReadingStatus;
                    this.curMR =new MedicalReading(mr);
                    this.userIDFrom = mr.UserIDFrom;
                    this.userIDTo = mr.UserIDTo;

                    this.loadPicThread = new Thread(new ThreadStart(this.ThreadProcSafePost));
                    this.loadPicThread.Start();

            }

            //this.rapidPassiveEngine.ConnectionInterrupted += new CbGeneric(rapidPassiveEngine_ConnectionInterrupted);//Ԥ�������¼�
            //this.rapidPassiveEngine.BasicOutter.BeingPushedOut += new CbGeneric(BasicOutter_BeingPushedOut);
            //this.rapidPassiveEngine.RelogonCompleted += new CbGeneric<LogonResponse>(rapidPassiveEngine_RelogonCompleted);//Ԥ�������ɹ��¼�  
           // this.rapidPassiveEngine.MessageReceived += new CbGeneric<string, int, byte[], string>(rapidPassiveEngine_MessageReceived);
            //   this.rapidPassiveEngine.SendMessage(null, InformationTypes.GetAllMedicalReading, null, null);
        }
       // private string curMedicalReadingID;
        
        //�˹��캯�����ڶ�ȡ��Ƭ������Ϣ
        

        #region �Զ��巽��
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
            //...ִ���߳�����

            //���߳��и���UI��ͨ��UI�߳�ͬ��������m_SyncContext��
            m_SyncContext.Post(LoadMedicalReading,this.curMR);
            //...ִ���߳���������
        }

        private void CopyMedicalReadingNoImage(MedicalReading newMr)
        {
            curMR.MedicalReadingID = newMr.MedicalReadingID;//ID��ͼƬ��Ϣ����
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
        void  LoadMedicalReading(Object ob)//������Ƭ��Ϣ
        {
            MedicalReading mr = (MedicalReading)ob;
            if (this.InvokeRequired)
            {
                this.Invoke(new CbGeneric<Object>(this.LoadMedicalReading));
            }
            else
            {
	            isload=true;//��ʾ��װ�أ�
	            try
	            {
                  //  AllUnable();//����ListBox�Լ�CBMark\�Լ���ť����ѡ������ֻ�ܿ�

                    //ControlState();
                    if (listBox1.Items.Count > 0)//ÿ�ν�������Ҫ���List
                    {
                        listboxPath.Clear();
                        dicPics.Clear();
                        listBox1.Items.Clear();
                        listBox1.SelectedIndex = curlistBoxSelectedIndex = -1;
                    }


                    // List<ReadingPicture> listReadPics = mr.ListPics;
                    int picCount = 1;
                    if (curMR.ListPics != null)//�����ͼƬ
                    {

                        foreach (ReadingPicture rp in curMR.ListPics)
                        {
                            if (rp.Imagebyte != null)
                            {
                                string picnum = "��" + (picCount++).ToString() + "��ͼƬ";
                                this.listBox1.Items.Add(picnum);
                                this.dicPics.Add(new KeyValuePair<string, ReadingPicture>(picnum, rp));
                                this.listboxPath.Add(picnum);
                            }

                        }
                        this.listBox1.SelectedIndex = 0;//Ĭ�ϼ��ص�һ��;
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
            if (isload)//����Ǽ���
            {
                ToolStatusUnable();//�����ؼ�������ʹ��,���˷Ŵ���С����ת�����ѡ����
                ClearCb();
                this.toolStripButtonAddSomePic.Enabled = false;//�鿴��ʱ�������ͼƬ
                this.toolStripButtonAddPic.Enabled = false;//
                this.toolStripButton2.Enabled = this.toolStripButton3.Enabled= true;
                this.toolStripButtonZoomBig.Enabled = true;
                this.toolStripButtonZoomsmall.Enabled = true;
                this.cbExpertMarks.Enabled = this.cbMarks.Enabled = true;
                this.cbDyedMethod.Enabled = this.cbZoom.Enabled = this.cbSampleType.Enabled = false;
                this.btnClientNote.Text = "�鿴��ע";
                this.btnExpertConclusion.Text = "�鿴����";
                switch (curMR.ReadingStatus)
                {
                    case EReadingStatus.UnProcessed: this.Text = "����Ƭ״̬��δ����";
                        {
                           
                            if (this.usertype == EUserType.Expert)
                            {
                                this.btnEnterTalk.Visible = this.btnSubmit.Visible = true;
                                this.skinPanelExpert.Visible = true;
                                this.btnEnterTalk.Text = "�ܾ�";
                                this.btnSubmit.Text = "����";//
                               
                            }
                            else//������ɫ�����ذ�ť
                            {
                                this.btnEnterTalk.Visible = this.btnSubmit.Visible = false;
                            }
                        }
                        break;
                    case EReadingStatus.Processing: this.Text = "����Ƭ״̬�����ڴ���"; 
                        {
                            this.skinPanelExpert.Visible = true;
                            if (this.usertype == EUserType.Expert)//ר��׼���ύ����
                            {
                                this.contextMenuStrip1.Enabled = true;
                                this.btnEnterTalk.Visible = this.btnSubmit.Visible = true;
                                this.btnEnterTalk.Text = "�Ի�";
                                this.btnSubmit.Text = "�ύ������";//
                                // this.btnSubmit.Tag = "sumbitPointer";
                                this.btnExpertConclusion.Text = "�������";
                            }
                            else//������ɫ�����ذ�ť
                            {
                                this.btnEnterTalk.Visible = true;//���ԶԻ�
                                this.btnSubmit.Visible = false;//�����ύ��ֻ�ܸ�ר�����ύ��
                                this.contextMenuStrip1.Enabled = false;
                            }
                        }
                        break;
                    case EReadingStatus.Rejected: this.Text = "����Ƭ״̬���Ѿܾ�";
                        {

                            this.btnEnterTalk.Visible = true;
                            this.btnSubmit.Visible = false;
                            this.btnEnterTalk.Text = "�鿴�ܾ�����";
                        }
                        break;
                    case EReadingStatus.Completed: this.Text = "����Ƭ״̬�������";
                        {
                            this.btnEnterTalk.Visible = false;
                            this.btnSubmit.Visible = false;
                            this.btnEnterTalk.Text = "�鿴�ܾ�����";
                        } break;
                }
            }
            else//������½�;
            {
                if (listBox1.Items.Count == 0)
                {
                    ToolStatusUnable();
                    this.btnSubmit.Visible = false;
                    this.btnEnterTalk.Visible = false;
                   // this.btnExpertConclusion.Visible = this.btnClientNote.Visible = false;//������;
                }
                else//�½��ң�listbox�ж���
                {
                    ToolStatusEnable();
                    this.btnClientNote.Text = "��ע";
                   // this.btnExpertConclusion.Visible = this.btnClientNote.Visible = true;//������;
                    this.btnSubmit.Visible = true;
                }
                ClearCb();
            }
            rotateCount = 0;//ÿ�μ�������Ƭ����Щ��Ҫ����
            this.offsetx = this.offsety = 0;//ƫ����
            this.rect.Width = 0;//��ձ��
            normalscale = 1;
        }
        public void ToolStatusUnable()
        {
            contextMenuStrip1.Enabled = false;
            //��Ϊ���汳��ToolStripMenuItem1.Enabled = false;
            //ת��ΪToolStripMenuItem.Enabled = false;
            //ɾ��ToolStripMenuItem1.Enabled = false;
            //������ToolStripMenuItem1.Enabled = false;
            //���ΪToolStripMenuItem1.Enabled = false;
            toolStripButton2.Enabled = false;
            toolStripButton3.Enabled = false;
            toolStripButton4.Enabled = false;
            //��ӡToolStripMenuItem1.Enabled = false;
            //ͼƬ��ЧToolStripMenuItem.Enabled = false;
            //ͼƬ����ToolStripMenuItem.Enabled = false;
          
            //ͼƬ����ToolStripMenuItem.Enabled = false;
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
            //��Ϊ���汳��ToolStripMenuItem1.Enabled = true;
            //ת��ΪToolStripMenuItem.Enabled = true;
            //ɾ��ToolStripMenuItem1.Enabled = true;
            //������ToolStripMenuItem1.Enabled = true;
            //���ΪToolStripMenuItem1.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = true;
            toolStripButton4.Enabled = true;
            //��ӡToolStripMenuItem1.Enabled = true;
            //ͼƬ��ЧToolStripMenuItem.Enabled = true;
            //ͼƬ����ToolStripMenuItem.Enabled = true;
         
            //ͼƬ����ToolStripMenuItem.Enabled = true;
            toolStripButton7.Enabled = true;
            toolStripButtonZoomBig.Enabled = true;
            toolStripButtonZoomsmall.Enabled = true;
            this.cbMarks.Enabled = this.cbDyedMethod.Enabled = this.cbSampleType.Enabled = this.cbZoom.Enabled = true;
         //   this.btnSubmit.Enabled = true;
       //     this.btnSubmit.BackColor = Color.LimeGreen;
            //this.skinPanelClient.Enabled = true;
        
        }
        #endregion

        //#region ����API
        //[DllImport("user32.dll", EntryPoint = "SystemParametersInfoA")]
        //static extern Int32 SystemParametersInfo(Int32 uAction, Int32 uParam, string lpvparam, Int32 fuwinIni);
        //private const int SPI_SETDESKWALLPAPER = 20;
        //#endregion

        private Size picFirstSize;
        private Point picFirstLocation;
        #region �������
        private void frmMain_Load(object sender, EventArgs e)
        {
          //  this.Text += this.userIDFrom+",��ӭ��";
            this.TopMost = true;
            this.Focus();
            string resourceDir = AppDomain.CurrentDomain.BaseDirectory + "Resource\\";
            this.Icon = ImageHelper.ConvertToIcon(global::RemoteReading.Properties.Resources.medicalreading, 64);
            this.panel1.MouseWheel += new MouseEventHandler(pictureBox1_MouseWheel);
         
            this.Cursor = Cursors.Default;
            if (!isload)
            {
                ControlState();//������½�
            }
            
           // SetPictureLocation();
            //���������ͼƬ�ļ���;
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

                    if (!this.listboxPath.Contains(picpath))//���������ͬ
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

                           // this.currentPic.Image = bm;//��������������
                            this.currentPic.Imagebyte = ms.ToArray();
                            // currentPic.Stream = bm;//��ͼƬ������������
                            this.dicPics.Add(picpath, currentPic);
                            listBox1.Items.Add(FSInfo[i].ToString());
                            this.listboxPath.Add(picpath);//ͬ����¼�ļ�·����Ϣ
                            
                        }
                    }
                }
            }
            sum = listBox1.Items.Count.ToString();
            if (listBox1.Items.Count > 0)
            {
                this.toolStripStatusLabel3.Text = sum;
                this.listBox1.SelectedIndexChanged -= new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
                this.listBox1.SelectedIndex = 0;//Ĭ��ѡ���1��������;
                this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
                LoadPic();
            }
#endif
        }


        #endregion

        #region ������
        string selectedDirPath;
        public string sum;
        private void toolStripButton1_Click(object sender, EventArgs e)//�������еġ��򿪡�
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
                    
                        if (!this.listboxPath.Contains(picpath))//���������ͬ
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

                                //  this.currentPic.Image = bm;//��������������
                                this.currentPic.Imagebyte = ms.ToArray();
                                // currentPic.Stream = bm;//��ͼƬ������������
                                this.dicPics.Add(picpath, currentPic);
                                listBox1.Items.Add(FSInfo[i].ToString());
                                this.listboxPath.Add(picpath);//ͬ����¼�ļ�·����Ϣ
                                
                            }
                        }
                    }
                }
                sum = listBox1.Items.Count.ToString();
                if (listBox1.Items.Count> 0)
                {
                    this.toolStripStatusLabel3.Text = sum;
                    this.listBox1.SelectedIndexChanged -= new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
                    this.listBox1.SelectedIndex = 0;//Ĭ��ѡ���1��������;
                    this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
                    LoadPic();
                }
               
            }
        }
        void DisplaySum()//��ʾ1/4
        {
            this.toolStripStatusLabel1.Text = (this.listBox1.SelectedIndex+1).ToString();//��ʾ��ǰѡ��
            this.toolStripStatusLabel3.Text = (this.listBox1.Items.Count).ToString();//����
        }
        private void toolStripButton2_Click(object sender, EventArgs e)//ˢ�°�ť
        {
            if (listBox1.Items.Count == 0)
            {
                
                ToolStatusUnable();
                this.pictureBox1.Image = null;
                this.curlistBoxSelectedIndex = this.listBox1.SelectedIndex = -1;//Ĭ��ѡ��ڲ�����;
            }
            else
            {
                //  ToolStatusEnable();
              //  this.curlistBoxSelectedIndex = this.listBox1.SelectedIndex = this.listBox1.Items.Count - 1;//Ĭ��ѡ����ӵ�����ͼƬ���м���;     
                //       this.curlistBoxSelectedIndex = this.listBox1.SelectedIndex = 0;//Ĭ��ѡ���1��������;
                LoadPic();//����ͼƬ
              
               // this.toolStripStatusLabel1.Text = "1";//��ʾ��ǰѡ��
            }
         //   this.sum = this.listBox1.Items.Count.ToString();//
            //this.toolStripStatusLabel3.Text = sum;//��ʾ����

            
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
          //  SavePictrue();//����ͼƬ,�ſ��Դ�ӡ
            this.TopMost = false;
            ��ӡToolStripMenuItem_Click(sender, e);
        }

       
        //private void ״̬��ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (״̬��ToolStripMenuItem.CheckState == CheckState.Checked)
        //    {
        //        ״̬��ToolStripMenuItem.CheckState = CheckState.Unchecked;
        //        statusStrip1.Visible = false;
        //    }
        //    else
        //    {
        //        ״̬��ToolStripMenuItem.CheckState = CheckState.Checked;
        //        statusStrip1.Visible = true;
        //    }
        //}

        //private void ������ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (������ToolStripMenuItem.CheckState == CheckState.Checked)
        //    {
        //        ������ToolStripMenuItem.CheckState = CheckState.Unchecked;
        //        toolStrip1.Visible = false;
        //    }
        //    else
        //    {
        //        ������ToolStripMenuItem.CheckState = CheckState.Checked;
        //        toolStrip1.Visible = true;
        //    }
        //}
        //private void ͼƬ��ϢToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (ͼƬ��ϢToolStripMenuItem.CheckState == CheckState.Checked)
        //    {
        //        ͼƬ��ϢToolStripMenuItem.CheckState = CheckState.Unchecked;
        //        this.toolStripStatusLabel7.Visible = false;
        //    }
        //    else
        //    {
        //        ͼƬ��ϢToolStripMenuItem.CheckState = CheckState.Checked;
        //        this.toolStripStatusLabel7.Visible = true;
        //    }
        //}
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ɾ��ToolStripMenuItem_Click(sender, e);
        }
        private void toolStripButton5_Click(object sender, EventArgs e)//�����������ϡ�
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
        private void toolStripButton6_Click(object sender, EventArgs e)//�����������¡�
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
        private void ͼƬ��ЧToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                frmSpecialEfficacy special = new frmSpecialEfficacy();
                special.ig = pictureBox1.Image;
                special.ShowDialog();
            }
        }
        private int rotateCount=0;//��ת����
        private void toolStripButton7_Click(object sender, EventArgs e)//��ת
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

        #region �����¼�
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
                    return;//û�б仯
                }
                if (listBox1.SelectedIndex == -1)
                {
                    //this.listBox1.SelectedIndex = curlistBoxSelectedIndex;
                    return;
                }

                LoadPic();//����ͼƬ
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.pictureBox1.Invalidate();
        }
        private void LoadPic()
        {
            ControlState();//���ݶ��������״̬���ı�ؼ�״̬
            curlistBoxSelectedIndex = this.listBox1.SelectedIndex;//��¼��ǰѡ��
            filePath = this.listboxPath[listBox1.SelectedIndex];
            if (this.dicPics.Keys.Contains(filePath))
            {
               if(pictureBox1.Image!=null)
                this.pictureBox1.Image.Dispose();//���ͷ�

                this.pictureBox1.Location = this.picFirstLocation;//�����λ�ã�
                image1 = GetSourceMap(dicPics[filePath].Imagebyte);
                PictureWidth = image1.Width.ToString();
                Pictureheight = image1.Height.ToString();   
                this.toolStripStatusLabel7.Text =  "�ֱ��ʣ�" + PictureWidth + "��" + Pictureheight;
                toolStripStatusLabel3.Text = sum;
                toolStripStatusLabel1.Text = Convert.ToString(listBox1.SelectedIndex + 1);
                this.pictureBox1.Size = this.picFirstSize;//����Ĵ�С��
                this.pictureBox1.Location = this.picFirstLocation;//�����λ�ã�
                pictureBox1.Image = image1;
                GetZoomImage();
            }
           
            DisplaySum();//��ʾ��Ƭѡ�����Լ�������
            //����֮���µ�filePath·��
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
                //����пͻ��˱�ǩ�������
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
                //���ר���б�ǩ�������ר�ұ��
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
                //�����ɺ������µ���Ƭ����
                // this.currentPic = new ReadingPicture();
                this.lblMarkCount.Text = "0";
            }

        }
       
        #endregion

        #region �ļ��˵�

        private void ����Ŀ¼ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
        }

        private void ˢ��ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e);
        }

        private void �˳�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //private void ��Ϊ���汳��ToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    ��Ϊ���汳��ToolStripMenuItem_Click(sender, e);
        //}

        private void ɾ��ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ɾ��ToolStripMenuItem_Click(sender, e);
        }

        //private void ������ToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    ������ToolStripMenuItem_Click(sender, e);
        //}

        private void ��ӡToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ��ӡToolStripMenuItem_Click(sender, e);
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

        private void ͼƬ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPicAdjust picadjust = new frmPicAdjust();
            picadjust.ig = pictureBox1.Image;
            picadjust.PicOldPath = filePath;
            picadjust.ShowDialog();
        }
        private void �˳�CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("�˳�ϵͳ��", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void �õ�Ƭ��ӳToolStripMenuItem_Click(object sender, EventArgs e)
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

        #region �Ҽ��˵�
        //private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
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
        //        MessageBox.Show(ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        private void ɾ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (this.listBox1.SelectedIndex == -1)
                {
                    return;
                }
                if (MessageBox.Show("ȷ��Ҫ�Ƴ���(����ɾ��Դ�ļ�)", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    if (this.pictureBox1.Image != null) this.pictureBox1.Image.Dispose();
                    //File.Delete(listboxPath[this.listBox1.SelectedIndex]);
                    this.listBox1.SelectedIndexChanged -= new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
                    
                    this.dicPics.Remove(filePath);//�Ƴ���ǰͼƬ
                    this.listboxPath.RemoveAt(this.curlistBoxSelectedIndex);
                    this.listBox1.Items.RemoveAt(curlistBoxSelectedIndex);
                    toolStripButton2_Click(sender, e);
                    this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ˢ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e);
        }
        //private void ������ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    string fName = filePath.Substring(filePath.LastIndexOf("\\") + 1, (filePath.LastIndexOf(".") - filePath.LastIndexOf("\\") - 1));
        //    string fType = filePath.Substring(filePath.LastIndexOf(".") + 1, (filePath.Length - filePath.LastIndexOf(".") - 1));
        //    frmRename rename = new frmRename();
        //    rename.filename = fName;
        //    rename.filepath = filePath;
        //    rename.filetype = fType;
        //    rename.ShowDialog();
        //}

        private void ��ӡToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        //private void ��Ϊ���汳��ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //��ȡָ��ͼƬ����չ��
        //    string SFileType = filePath.Substring(filePath.LastIndexOf(".") + 1, (filePath.Length - filePath.LastIndexOf(".") - 1));
        //    //����չ��ת����Сд
        //    SFileType = SFileType.ToLower();
        //    //��ȡ�ļ���
        //    string SFileName = filePath.Substring(filePath.LastIndexOf("\\") + 1, (filePath.LastIndexOf(".") - filePath.LastIndexOf("\\") - 1));
        //    //���ͼƬ��������bmp�����API�еķ�����������Ϊ���汳��
        //    if (SFileType == "bmp")
        //    {
        //        SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, filePath, 1);
        //    }
        //    else
        //    {
        //        string SystemPath = Environment.SystemDirectory;//��ȡϵͳ·��
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
        private void ���ΪToolStripMenuItem_Click(object sender, EventArgs e)
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
                MessageBox.Show(ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ���ΪToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ���ΪToolStripMenuItem_Click(sender, e);
        }
        #endregion

       
        private void ͼƬ����ToolStripMenuItem_Click(object sender, EventArgs e)
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
            fd.Filter = "ͼƬ�ļ�|*.bmp;*.jpg;*.jpeg;*.gif;*.png"; //�����ļ�����
            fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);//�趨��ʼĿ¼
            fd.ShowReadOnly = false; //�趨�ļ��Ƿ�ֻ��
            fd.Multiselect = false;
            DialogResult r = fd.ShowDialog();
            if (r == DialogResult.OK)
            {
                 if (!this.listboxPath.Contains(fd.FileName))
                {
                    this.currentPic = new ReadingPicture();
                     string filetype=fd.FileName.Substring(fd.FileName.LastIndexOf(".") + 1, (fd.FileName.Length - fd.FileName.LastIndexOf(".") - 1));
                   
                     Bitmap bm = new Bitmap(fd.FileName);
                    currentPic.FileType = filetype.ToLower();//�����׺;
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
                        this.currentPic.Imagebyte = ms.ToArray();//�������ֽ�
                        // currentPic.Image = bm;//��ͼƬ������������
                        // Image im = Image.FromFile(fd.FileName);
                        // Bitmap bmp = new Bitmap(fd.FileName);
                        //currentPic.Image=bmp;//����ReadingPicture��image����
                        this.dicPics.Add(fd.FileName, currentPic);//ÿ�γɹ���Ӻ�
                        this.listBox1.Items.Add(fd.SafeFileName); //����ļ�������������
                        this.listboxPath.Add(fd.FileName);//��¼�����ļ���Ϣ��
                        this.sum = this.listBox1.Items.Count.ToString();//����1;
                        this.toolStripStatusLabel3.Text = sum;
                       
                    }
                }
                this.listBox1.SelectedIndexChanged -= new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
                this.curlistBoxSelectedIndex=this.listBox1.SelectedIndex = this.listBox1.Items.Count-1;//Ĭ��ѡ����ӵ�����ͼƬ���м���;     
                LoadPic();
                this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
            }
        }
      
        int x, y;//��갴�µ���Ļ����
        bool isMoveable = false;

        private Point curMarkLocation;//��갴��ʱ�������ͼƬ������;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            CaculateMarkLocationImageXY();//תΪͼƬ����;
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
                this.toolStripMenuItemMousePointer.Text = "��ǰλ�ã�" + curMarkLocation.ToString();
                //if (isMarking)//������ڱ��
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
            if (imageW > imageH)//ͼƬ�ĳ��ȴ��ڿ��
            {
                int gapH = (this.pictureBox1.Height - this.imageH) / 2;
                //תΪͼƬ����ϵ
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
            if (imageW > imageH)//ͼƬ�ĳ��ȴ��ڿ��
            {
                int gapH = (this.pictureBox1.Height - this.imageH) / 2;
                //תΪͼƬ����ϵ
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
            //if (rect.Width>0&&isMarking)//������ڼ���
            //{
            //    rotateCount = rotateCount % 4;
            //    this.recWidth = rect.Width;//�������ο��
            //    this.recHeight = rect.Height;//�������θ߶�;
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

            currentPic = dicPics[filePath];//��ȡ��ǰͼƬ����
            if (currentPic.ListMarks == null)//����ͻ�ͼƬ�б���б�Ϊ��
            {
                if (this.usertype == EUserType.NormalClient)
                {
                    currentPic.ListMarks = listMarks;//

                }

            }
            else//�����Ϊ��
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
                this.lblMarkCount.Text = currentPic.ListMarks.Count.ToString();//��ʾ��ǰͼƬ�������

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
            float vx = (float)this.imageW / 2 - (float)offsetx;//�����������ĵ�õ�ͼƬ����ϵ��x

            float vy = (float)this.imageH / 2 - (float)offsety;//
            vision.X = vx / this.imageW;
            vision.Y = vy / this.imageH;
            return vision;
        }
        int nt = 0;
       
       
        public void SetPictureLocation()//���зŴ�;
        {
            int w = (this.panel1.Width - pictureBox1.Width) / 2;
            int h = (this.panel1.Height - pictureBox1.Height) / 2;
            Point p = new Point();
            p.X = w + offsetx;
            p.Y = h + offsety;
            pictureBox1.Location = p;
        }
        private float normalscale = 1.0f;
        private float minscale=1f;//��С���ű���;
        private float maxscale=5f;//������ű���
       
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

            //    ��������Ϊ���գ����Դ����߲��ܱ䣬���Կ���������������������ͼƬһ��Ŵ���С

            //}

        }
        private void frmMain_Resize(object sender, EventArgs e)
        {
           // this.rect.Width = 0;//ȡ�����
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
            float w = this.picFirstSize.Width * normalscale; //ÿ�οsС 10%  
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

        private void ���ToolStripMenuItem_Click(object sender, EventArgs e)
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
                CaculateMarkLocationPictureBoxXY();//ת������
                CaculateRect();
            }
            //else
            //    this.rect.Width = 0;//ȡ��δ��ɱ�ǿ�
          
            this.pictureBox1.Invalidate();
        }
        private Rectangle rect = new Rectangle();
        private int recWidth;//���ο��
        private int recHeight;//���γ���
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
            this.currentPic.SamplesTypeID = this.cbSampleType.SelectedIndex;//ѡ����

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
        private void SendUpdateMedicalReading(byte[] info)//ר�һ��û�������Ƭ������Ϣ
        {
            try
            {
                 this.rapidPassiveEngine.SendMessage(null, InformationTypes.SendUpdateMedicalReading, info, this.curMR.MedicalReadingID);//����List<ReadingPicture>��MedicalReadingID��
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("����!" + ex.ToString());
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
               MessageBox.Show("����!" + ex.ToString());
           }
        }
        IAsyncResult ir;
      
        private string userIDFrom;
        private string userIDTo;
        private void HandleSentResult(bool succeed, object tag)
        {
            
            if (!succeed)
            {
                MessageBox.Show("��Ϊ����ԭ�򣬸ղŵ���Ϣ��δ���ͳɹ���");
            }
        }
       
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //�����ж�ÿ����Ƭ�Ƿ��Ѿ���д�걾��Ϣ
            Button btn = (Button)sender;
            if (this.usertype==EUserType.Expert&&btn.Text == "����")
            {
                if (MessageBox.Show("ȷ��Ҫ������", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    this.rapidPassiveEngine.SendMessage(null, InformationTypes.SendMedicalReadingReceived, null, curMR.MedicalReadingID);
                    //if (MessageBox.Show("���ճɹ�") == DialogResult.OK)//û�еõ��������Ļظ������ܻ���bug��
                    //{

                    //    this.curMR.ReadingStatus = EReadingStatus.Processing;
                    //    // this.ControlState();
                    //   // this.readingStatus = EReadingStatus.Processing;
                    //    this.LoadPic();
                    //    //      MedicalReading mr = new MedicalReading(this.curMR);
                    //    this.main.UpdateMedicalReading(this.curMR);//���ظ���
                    //    return;
                    //}
                }
            }
            else   if (this.usertype==EUserType.NormalClient&&!this.isload&&btn.Text == "�ύ������") //�ͻ����ύ����
            { 
                List<ReadingPicture> listPic = new List<ReadingPicture>();
                if (this.listBox1.Items.Count < 1)
                {
                    return;
                }
                if (MessageBox.Show("ȷ��Ҫ�ύ��","��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                
                    foreach (KeyValuePair<string, ReadingPicture> kvrp in dicPics)
                    {
                   
                        if (kvrp.Value.SamplesTypeID == -1 || kvrp.Value.DyedMethodID == -1 || kvrp.Value.ZoomID == -1)
                        {
                            MessageBox.Show("ÿ��ͼƬ�걾��Ϣ������д����!");
                            return;
                        }
                            //���������ͼƬ�б�����ͼƬ����
                        listPic.Add(kvrp.Value);//��ȡͼƬ����
                   
                    }
                   
                    curMR.ListPics = listPic;
                    //�����ߣ�������ID,ͼƬ�б�
                    this.mdSubmit = new MedicalReading(userIDFrom, userIDTo, EReadingStatus.UnProcessed, this.dicPics.Keys.Count, listPic);
                    //�����Ϻ󣬷�����������
                    this.mdSubmit.MedicalReadingID = curMR.MedicalReadingID;
                    byte[] info = CompactPropertySerializer.Default.Serialize(this.mdSubmit);

                    FrmStatus fs = new FrmStatus();
                    fs.Show("�����ύ�����Ժ�", true);
                        SendBlobThread(info);
                   
                }
#if Debug
            userIDFrom="100001";
            userIDTo="100002";
#endif
                        
            }
        else   if (isload&&this.curMR.ReadingStatus==EReadingStatus.Processing&& btn.Text == "�ύ������")//������Ƭ�������ڴ����״̬��������Ƭ�޸��ύ
            {
                if (MessageBox.Show("�ύ��������", "��ɹ���", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    if (this.usertype == EUserType.Expert)//�����ר���ύ�޸���Ƭ��ֻ�����ר�ұ�ǩ��Ϣ�������޸�����
                    {
                        //if(this.curMR.ListPics)
                        List<ReadingPicture> rplist = new List<ReadingPicture>();

                        foreach (ReadingPicture rp in this.curMR.ListPics)
                        {
                            if (rp.ExpertConclusion == null||rp.ExpertConclusion=="")
                            {
                                MessageBox.Show("ÿ��ͼƬ����Ҫ���½���");
                                return;
                            }
                            ReadingPicture newrp = new ReadingPicture(rp);//����ͼƬ���л�����Ϣ��¡
                            rplist.Add(newrp);
                        }

                        byte[] info = CompactPropertySerializer.Default.Serialize(rplist);
                        FrmStatus fs = new FrmStatus();
                        fs.Show("�����ύ�����Ժ�", true);
                            SendUpdateMedicalReading(info);
                        
                        //if (MessageBox.Show("�ύ�ɹ�") == DialogResult.OK)
                        //{
                        //    MedicalReading update = new MedicalReading(this.curMR);
                        //  //  this.readingStatus = EReadingStatus.Completed;
                        //    update.ReadingStatus = EReadingStatus.Completed;//����״̬;
                            
                        // //   this.ControlState();
                        //    this.LoadPic();
                        //    //MedicalReading mr = new MedicalReading(this.curMR);//������Ϣ��¡
                        //    this.main.UpdateMedicalReading(update);
                        //}
                    }
                    else if (this.usertype == EUserType.NormalClient)//����ǿͻ�
                    {
                        

                    }
                    

                    
                }
                
            }
        }

        private void splitContainer1_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            this.pictureBox1.Invalidate();//�ػ�ͼ��
        }
       
        private void btnEnterTalk_Click(object sender, EventArgs e)
        {
            Button  btn=(Button)sender;
            if (btn.Text == "�Ի�"&&(this.usertype==EUserType.NormalClient ||this.usertype==EUserType.Expert))
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
                else if(btn.Text=="�鿴�ܾ�����")
            {
                frmRejectedReason frm = new frmRejectedReason();
                frm.SetRejectedReason(this.curMR.RejectedReason);
                frm.ShowDialog();
            }
            else if (btn.Text == "�ܾ�")//
            {
                if (this.usertype == EUserType.Expert)//ֻ��ר�Ҳſ��Ծܾ�
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
                        //if(MessageBox.Show("�ύ�ܾ���Ϣ�ɹ�")==DialogResult.OK)
                        //{
                        //    this.curMR.ReadingStatus=EReadingStatus.Rejected;
                        // //   this.readingStatus = EReadingStatus.Rejected;
                        //    LoadPic();
                        //    this.curMR.IsRejected=true;
                        //    this.curMR.RejectedReason=rejectedreason;
                        //    this.main.UpdateMedicalReading(this.curMR);//���ظ���
                        //}
                    }
                }
            }
        }

        private void cbMarks_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           // this.currentPic = this.dicPics[filePath];//��ȡ��ǰͼƬ
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
            this.pictureBox1.Invalidate();//�ػ��ؼ�
        }

        private bool LoadPictureMark(PictureMark pm)
        {
            try
            {
                float[] marklocation = pm.M_MarkLocation;
	            float[] markvision = pm.M_MarkVision;
	            curMarkColor = pm.M_MarkColor;
	           // float scale = pm.PictureScale;
                normalscale = pm.PictureScale;//�������ڵķŴ���;
                float w = this.picFirstSize.Width * normalscale;
                float h = this.picFirstSize.Height * normalscale;
	            this.pictureBox1.Size = Size.Ceiling(new SizeF(w, h));
	            GetZoomImage();
	            float markx = marklocation[0] * this.imageW;
	            float marky = marklocation[1] * this.imageH;
	            curMarkLocation = new Point((int)markx, (int)marky);
	            CaculateMarkLocationPictureBoxXY();//����ǵ�תΪPictureBox���ꣻ
	            CaculateRect();
	            //ͨ��markVison�����offset
	            float markvisionx = markvision[0] * this.imageW;
	            float markvisiony = markvision[1] * this.imageH;
	            offsetx = (int)((float)this.imageW / 2 - markvisionx);
	            offsety = (int)((float)this.imageH / 2 - markvisiony);
	            SetPictureLocation();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("���ر�ǩʧ��!" + ex.ToString());
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
            this.rect = new Rectangle(recloca, recFirst);//���λ��
        }

       

        private void cbExpertMarks_SelectedIndexChanged(object sender, EventArgs e)
        {
           // this.currentPic = this.dicPics[filePath];//��ȡ��ǰͼƬ
            this.offsetx = this.offsety = 0;
            string remarkselected = this.cbExpertMarks.SelectedItem.ToString();

            foreach (PictureMark pm in currentPic.ListExpertMarks)
            {
                if (remarkselected == pm.Remark)
                {
                    LoadPictureMark(pm);//���ر�ǩ;
                }
            }
            this.pictureBox1.Invalidate();//�ػ��ؼ�
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
            if (btn.Text == "�鿴����")//�鿴
            {
                frmRemark note = new frmRemark();
                note.StartPosition = FormStartPosition.CenterParent;
                note.setLableName("ר�ҽ���");
                note.SetRemark(currentPic.ExpertConclusion);
                note.setIsLoad(true);//����Ϊ����״̬��������գ����ܱ༭;
                note.ShowDialog();

            }
            else if (btn.Text == "�������" && this.usertype == EUserType.Expert)//ֻ���û����Ա�ע
            {
                frmRemark note = new frmRemark();
                note.StartPosition = FormStartPosition.CenterParent;
                note.setLableName("ר�ҽ���:");
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
            if (btn.Text == "�鿴��ע")//�鿴
            {
                frmRemark note = new frmRemark();
                note.StartPosition = FormStartPosition.CenterParent;
                note.setLableName("�û���ע:");
                note.SetRemark(currentPic.ClientNote);
                note.setIsLoad(true);//����Ϊ����״̬��������գ����ܱ༭;
                note.ShowDialog();
                
            }
            else if (btn.Text == "��ע" && this.usertype == EUserType.NormalClient)//ֻ���û����Ա�ע
            {
                frmRemark note = new frmRemark();
                note.StartPosition = FormStartPosition.CenterParent;
                note.setLableName("�û���ע:");
               
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