using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataProvider;
using RemoteReading.Core;
using System.Data.SqlClient;
namespace RemoteReadingManagement
{
    public partial class UserManagement : BaseForm
    {
        public UserManagement()
        {
            InitializeComponent();
        }

        private void UserManagement_Load(object sender, EventArgs e)
        {
            Dictionary<string, object> outVals = new Dictionary<string, object>();
            DataSet ds;
            SqlServerProvider ssp = new SqlServerProvider();
            SqlParameter[] parms = { };
            ds = ssp.SPGetDataSet("spgetallusers", parms, out outVals);
            this.dgv.AutoGenerateColumns = false;
            this.dgv.DataSource = ds.Tables[0].DefaultView;
        
            
           
        }

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e == null || e.Value == null || !(sender is DataGridView))
                return;
            DataGridView view = (DataGridView)sender;

            try
            {
                if (view.Columns[e.ColumnIndex].DataPropertyName == "UserType")
                {
                    int val = Convert.ToInt32(e.Value);
                    switch (val)
                    {
                        case 0:
                            e.Value = "管理员";
                            break;
                        case 1:
                            e.Value = "客服人员";
                            break;
                        case 2:
                            e.Value = "客户";
                            break;
                        case 3:
                            e.Value = "专家";
                            break;

                    }
                    e.FormattingApplied = true;
                }
                if (view.Columns[e.ColumnIndex].DataPropertyName == "ProfessionTitle")
                {
                    if (e.Value == DBNull.Value)
                    {
                        return;
                    }
                    int val = Convert.ToInt32(e.Value);
                    switch (val)
                    {
                        case 0:
                            e.Value = "医师";
                            break;
                        case 1:
                            e.Value = "主治医师";
                            break;
                        case 2:
                            e.Value = "副主任医师";
                            break;
                        case 3:
                            e.Value = "主任医师";
                            break;

                    }
                    e.FormattingApplied = true;
                }
            }
            catch (System.Exception ex)
            {
                e.FormattingApplied = false;
                MessageBox.Show(ex.ToString());
            }
         }

        private void dgv_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgv_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //添加行号 
            using (SolidBrush b = new SolidBrush(dgv.RowHeadersDefaultCellStyle.ForeColor))
            {
                string linenum = e.RowIndex.ToString();
                int linen = 0;
                linen = Convert.ToInt32(linenum) + 1;
                string line = linen.ToString();
                e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 5);
                SolidBrush B = new SolidBrush(Color.Red);
            }
        }
    }
}
