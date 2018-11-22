using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemoteReading.Core
{
    public partial class FrmStatus : Form
    {
        public FrmStatus()
        {
            InitializeComponent();
          
        }
        public void Show(string msg,bool a)
        {
            if(a)
            this.timer1.Enabled = true;
            lblStatus.Text = msg;
            this.Show();  
            x = this.Location.X;
            y = this.Location.Y;   
        }
        private int x; int y;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity == 0)
            {
                if(!this.IsDisposed)
                this.Close();
            }
            else
            {
                this.Opacity -=0.04;
                this.Location = new Point(x, ++y);
            }
        }

    }
}
