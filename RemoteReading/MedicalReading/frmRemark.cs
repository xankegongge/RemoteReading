using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RemoteReading
{
    public partial class frmRemark :BaseForm
    {
        public frmRemark()
        {
            InitializeComponent();
        }
        public frmRemark(string remark):base(){
            SetRemark(remark);
        }
        public void setLableName(string name)
        {
            this.skLabel.Text = name;
        }
        public string GetRemark()
        {
            return this.tbRemark.Text;
        }
        public void  SetRemark(string remark)
        {
            if (remark == null)
                return;
            this.tbRemark.Text=remark;
        }
        private void skbtOK_Click(object sender, EventArgs e)
        {
            if (this.tbRemark.Text.Trim() == "")
            {
                MessageBox.Show("内容不能为空!");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void skbtnClear_Click(object sender, EventArgs e)
        {
            this.tbRemark.Clear();
        }

        private void frmRemark_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.Focus();
            //this.tbRemark.Focus();
        }

        public void setIsLoad(bool p)
        {
            if (p)//如果是加载状态
            {
                this.skbtnClear.Visible = this.skbtOK.Visible = false;
                this.tbRemark.ReadOnly = true;
            }
            else
            {
                this.skbtnClear.Visible = this.skbtOK.Visible = true;
                this.tbRemark.ReadOnly = false;
            }
        }
    }
}
