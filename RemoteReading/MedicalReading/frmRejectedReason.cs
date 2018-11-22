using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RemoteReading
{
    public partial class frmRejectedReason :BaseForm
    {
        public frmRejectedReason()
        {
            InitializeComponent();
        }

        public string GetRejectedReason()
        {
            return this.skinComboBoxReason.SelectedIndex==2?
                this.skinRichTextBoxOtherReason.Text:this.skinComboBoxReason.SelectedItem.ToString();
        }
       
        private void skbReajectedOK_Click(object sender, EventArgs e)
        {
            if (this.skinComboBoxReason.SelectedIndex==-1||(this.skinComboBoxReason.SelectedIndex==2&&this.skinRichTextBoxOtherReason.Text.Trim()==string.Empty))
            {
                MessageBox.Show("请选择拒绝理由!");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

      

        //private void frmRemark_Load(object sender, EventArgs e)
        //{
        //    this.skinComboBoxReason.Focus();
        //}

        private void skinComboBoxReason_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  CCWin.SkinControl.SkinComboBox com = (CCWin.SkinControl.SkinComboBox)sender;
            if (this.skinComboBoxReason.SelectedIndex == -1)
            {
                return;
            }
            if (this.skinComboBoxReason.SelectedIndex == 2)
            {
               // this.skinLabelOtherReason.Visible = this.skinRichTextBoxOtherReason.Visible = true;
                this.skinPanel2.Visible = true;
                this.skinRichTextBoxOtherReason.Focus();
            }
            else
            {
                this.skinPanel2.Visible = false;
              //  this.skinLabelOtherReason.Visible = this.skinRichTextBoxOtherReason.Visible = false;
            }
        }

        private void frmRejectedReason_Load(object sender, EventArgs e)
        {
            this.skinComboBoxReason.Focus();
        }



        public void SetRejectedReason(string p)
        {
            //this.skinComboBoxReason.SelectedItem = p;
            //this.skinComboBoxReason.SelectedText = p;
            this.skinComboBoxReason.Text = p;
            this.skinComboBoxReason.Enabled = false;
            this.skbReajectedOK.Visible = false;
        }
    }
}
