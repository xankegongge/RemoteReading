using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;
using RemoteReading.Core;
namespace RemoteReadingManagement
{
    public partial class MainForm : BaseForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            UserManagement um = new UserManagement();
            um.ShowDialog();
           
        }
    }
}
