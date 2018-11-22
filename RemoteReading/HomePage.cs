using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RemoteReading
{
    public partial class HomePage : BaseForm
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void skbtnChating_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
                mf.Show();
        }

        private void skbtnReading_Click(object sender, EventArgs e)
        {
            frmMain fm = new frmMain();

        }
    }
}
