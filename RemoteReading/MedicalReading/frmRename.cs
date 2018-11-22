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
    public partial class frmRename : Form
    {
        public frmRename()
        {
            InitializeComponent();
        }
        public string filename;
        public string filepath;
        public string filetype;
        private void frmRename_Load(object sender, EventArgs e)
        {
            textBox1.Text = filename;
            textBox2.Text = filename;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("请输入文件名称", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string path = filepath.Remove(filepath.LastIndexOf("\\"));
                    string newPath;
                    if (path.Length == 4)
                    {
                        newPath = path;
                    }
                    else
                    {
                        newPath = path + "\\";
                    }
                    string newfilepath = newPath + textBox2.Text.Trim() + "." + filetype;
                    FileInfo fi = new FileInfo(newfilepath);
                    if (fi.Exists)
                    {
                        MessageBox.Show("文件名存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        File.Move(filepath, newfilepath);
                        File.Delete(filepath);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}