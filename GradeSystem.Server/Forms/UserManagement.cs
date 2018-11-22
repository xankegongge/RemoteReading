using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using DataProvider;
using System.Data.SqlClient;
namespace GradeSystem.Server
{
    public partial class UserManagement : BaseForm
    {
        private GlobalCache globalUserCache;
        //private List<Hospital> listHospitals;
        private IRapidServerEngine rapidServerEngine;
        private string administratorID;
        public UserManagement(GlobalCache cache, IRapidServerEngine rapid, string administratorID)
        {
            globalUserCache = cache;
            this.rapidServerEngine = rapid;
           // listHospitals = globalUserCache.GetAllHospitals();
            this.administratorID = administratorID;
            InitializeComponent();
        }
        DataTable dtSource;
       // FrmStatus msg = new FrmStatus();
        private void CheckOutData()
        {
            Dictionary<string, object> outVals = new Dictionary<string, object>();
            DataSet ds;
            //SqlServerProvider ssp = new SqlServerProvider();
           // SqlParameter[] parms = { };
            //ds = ssp.SPGetDataSet("spgetallusers", parms, out outVals);
           // this.dgv.AutoGenerateColumns = false;
            //dtSource = ds.Tables[0];
            //this.dgv.DataSource = dtSource;
          //  dgv.Sort(dgv.Columns["CreateTime"], ListSortDirection.Descending);
        }
        private void UserManagement_Load(object sender, EventArgs e)
        {
            this.dgv.AutoGenerateColumns = false;
            this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            CheckOutData();
        }
        private void ShowMsg(string message, bool isautodispear)
        {
            //if (msg.IsDisposed)
            //{
            //    msg = new FrmStatus();
            //}
            //msg.Show(message, isautodispear);
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
                    if (e.Value == DBNull.Value )
                    {
                        return;
                    }
                    int val = Convert.ToInt32(e.Value);
                    if (val == -1)
                    {
                        return;
                    }
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
                    if (e.Value == DBNull.Value )
                    {
                        return;
                    }
                    int val = Convert.ToInt32(e.Value);
                    if (val == -1)
                    {
                        return;
                    }
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
                if (view.Columns[e.ColumnIndex].DataPropertyName == "VIPLevel")
                {
                    if (e.Value == DBNull.Value )
                    {
                        return;
                    }
                    int val = Convert.ToInt32(e.Value);
                    if (val == -1)
                    {
                        return;
                    }
                    switch (val)
                    {
                        case 0:
                            e.Value = "钻石会员";
                            break;
                        case 1:
                            e.Value = "铂金会员";
                            break;
                        case 2:
                            e.Value = "黄金会员";
                            break;
                        case 3:
                            e.Value = "尊贵会员";
                            break;

                    }
                    e.FormattingApplied = true;
                }
                if (view.Columns[e.ColumnIndex].DataPropertyName == "HospitalID")
                {
                    if (e.Value == DBNull.Value)
                    {
                        return;
                    }
                    int val = Convert.ToInt32(e.Value);
                    if (val == -1)
                    {
                        return;
                    }
                    //string hosName = listHospitals[val].HospitalName;
                    //e.Value = hosName;
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
        private int iSlectedRowIndex = -1;
        private void dgv_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            iSlectedRowIndex = e.RowIndex;
           
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (iSlectedRowIndex == -1&&dgv.SelectedRows.Count==0)
            {
                MessageBox.Show("至少选中一行！");
                return;
            }
            if (ESBasic.Helpers.WindowsHelper.ShowQuery("您确定要删除吗？"))
            {
                foreach (DataGridViewRow dr in dgv.SelectedRows)
                {
                    iSlectedRowIndex = dr.Index; dr.Selected = false;
                    DeleteRow(iSlectedRowIndex);
                }
            }
           
            iSlectedRowIndex = -1;

        }
        private void DgvRefresh()
        {
            this.dgv.DataSource = dtSource;
        //    dgv.Sort(dgv.Columns["CreateTime"], ListSortDirection.Descending);
            
        }
        private void DeleteRow(int index)
        {
            try
            {
                string usertypedel=dtSource.Rows[index]["UserType"].ToString();
                if (usertypedel == "0" || usertypedel == "管理员")
                {
                    MessageBox.Show("您无此权限!");
                    return;
                }
                string deluserid=dtSource.Rows[index]["UserID"].ToString();
                if(rapidServerEngine.UserManager.IsUserOnLine(deluserid))//如果在线则踢出；
                   //rapidServerEngine.BasicController.KickOut(deluserid);
                if (globalUserCache.DeleteUser(deluserid))//删除缓存user，以及数据库中user
                {
                    dtSource.Rows.RemoveAt(index);
                    DgvRefresh();
                    ShowMsg("删除成功", true);
                }
                else
                {
                    ShowMsg("删除失败", true);
                }
            }
            catch (Exception ex)
            {
                ShowMsg("删除失败" + ex.ToString(), true);
            }
        }

        private void 添加用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUserForm rs = new AddUserForm(this.globalUserCache,administratorID);
            try
            {
	            if (rs.ShowDialog() == DialogResult.OK)//如果是确定OK
	            {
	                CheckOutData();//重新查询数据库并绑定;
	                ShowMsg("添加成功!",true);
	            }
            }
            catch (System.Exception ex)
            {
                ShowMsg("添加失败!"+ex.ToString(), true);
            }
        }

        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dgv_Sorted(object sender, EventArgs e)
        {
           
        }
        int col = 0;
        private void dgv_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columstr = dgv.Columns[e.ColumnIndex].Name;
            if (col == e.ColumnIndex)
            {
                dtSource.DefaultView.Sort = dtSource.Columns[columstr] + " DESC";
                col = -1;
            }
            else
            {
                dtSource.DefaultView.Sort = dtSource.Columns[columstr] + " ASC";
                col = e.ColumnIndex;
            }
            dtSource = dtSource.DefaultView.ToTable();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            CheckOutData();
            DgvRefresh();
        }

    }
}
