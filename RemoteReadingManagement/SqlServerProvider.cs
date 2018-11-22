using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 关于SqlServer的数据访问接口
/// </summary>
namespace DataProvider
{
    public class SqlServerProvider : DataProvider
    {
        private SqlConnection con;
        private SqlCommand com;
        private SqlDataReader reader;
        private SqlDataAdapter adapter;
        private SqlTransaction transaction;
        //private DataSet dataset;
        //private bool ready = false;
        private string ErrorMessage;
        public SqlServerProvider()
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.AppSettings["sql_connstr"]);
            }
            catch (SqlException e)
            {
                ErrorMessage = e.StackTrace;
            }
                
        }
        public SqlServerProvider(string Str_Connection)
        {
            try
            {
                con = new SqlConnection(Str_Connection);
            }
            catch (SqlException e)
            {
                ErrorMessage = e.StackTrace;
            }
        }
       ~SqlServerProvider()
        {
            if (con!=null&&con.State != ConnectionState.Closed)
                con.Close();
          // com.Dispose();
           //if(!reader.IsClosed)
           //reader.Close();
        }

        protected override bool ExecuteCommand(string cmdText)
        {
            bool returntemp = false;
            
            com = new SqlCommand(cmdText, con);
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                reader = com.ExecuteReader();
                returntemp = true;
            }
            catch (SqlException e)
            {
                ErrorMessage = e.StackTrace;
            }
            finally
            {
                com.Dispose();
            }
            return returntemp;
        }
        public override int GetRecordNumber(string cmdText)
        {
            int length = -1;
            if (ExecuteCommand(cmdText))
            {
                length = 0;
                while (reader.Read()) length++;
            }
            try
            {
                reader.Close();
                com.Dispose();
                con.Close();
            }
            catch (SqlException e)
            {
                ErrorMessage = e.StackTrace;
            }
            return length;
        }
        public override bool GetRecordValue(string cmdText, string getvaluefieldname, out object returnvalue)
        {
            bool returntemp = false;
            returnvalue = 0;
            if (ExecuteCommand(cmdText))
            {
                reader.Read();
                try
                {
                    returnvalue = reader.GetSqlValue(reader.GetOrdinal(getvaluefieldname));
                    returntemp = true;
                }
                catch (IndexOutOfRangeException ex)
                {
                    ErrorMessage = ex.StackTrace;

                }
            }
            try
            {
                reader.Close();
                com.Dispose();
                con.Close();
            }
            catch (SqlException e)
            {
                ErrorMessage = e.StackTrace;
            }
            return returntemp;
        }
        public override bool GetRecordValue(string cmdText, string[] getvaluefieldnames, out object[] returnvalues)
        {
            bool returntemp = false;
            int i;
            returnvalues = new object[1];
            try
            {
                Array.Resize(ref returnvalues,getvaluefieldnames.Length);
                if (ExecuteCommand(cmdText))
                {
                    if (reader.Read())
                    {
                        for (i = 0; i < getvaluefieldnames.Length; i++)
                        {
                            try
                            {
                                returnvalues[i] = reader.GetSqlValue(reader.GetOrdinal(getvaluefieldnames[i]));
                                returntemp = true;
                            }
                            catch (IndexOutOfRangeException e)
                            {
                                ErrorMessage = e.StackTrace;
                                returntemp = false;
                                break;
                            }
                        }
                    }
                    try
                    {
                        reader.Close();
                        com.Dispose();
                        con.Close();
                    }
                    catch (SqlException e)
                    {
                        ErrorMessage = e.StackTrace;
                        returntemp = false;
                    }

                }
            }
            catch (NullReferenceException e)
            {
                ErrorMessage = e.StackTrace;
                returntemp = false;
            }
            return returntemp;
        }
        public override int GetRecordColNumber(string tablename)
        {
            string str_sql = "select * from [" + tablename + "]";
            int length = -1;
            if (ExecuteCommand(str_sql))
            {
                length = reader.FieldCount;
            }
            try
            {
                reader.Close();
                com.Dispose();
                con.Close();
            }
            catch (SqlException e)
            {
                ErrorMessage = e.StackTrace;
            }
            return length;
        }
        public override bool ExcuteSql(string cmdText)
        {
            com = new SqlCommand(cmdText, con);
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                com.ExecuteNonQuery();
                com.Dispose();
                con.Close();
                return true;
            }
            catch (SqlException e)
            {
                ErrorMessage = e.StackTrace;
            }
            return false;
        }
        public  bool ExcuteSqlScalar(string cmdText,out int index)
        {
            com = new SqlCommand(cmdText, con); index = -1;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                if (com.ExecuteScalar() != null)
                    index = int.Parse(com.ExecuteScalar().ToString());
                com.Dispose();
                con.Close();
                return true;
            }
            catch (SqlException e)
            {
                ErrorMessage = e.StackTrace;
            }
            return false;
        }
       
       
        public override bool GetDateSet(string cmdText, out DataSet dat)
        {
            bool returnvalue = false;
            dat = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                adapter = new SqlDataAdapter(cmdText, con);
                adapter.Fill(dat);
                con.Close();
                returnvalue = true;
            }
            catch (SqlException e)
            {
                ErrorMessage = e.StackTrace;
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            finally
            {
                adapter.Dispose();
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return returnvalue;
        }
        
        public  bool GetDateSetTable(string cmdText, out DataSet dat ,params string[] tablenames)
        {
            bool returnvalue = false;
            dat = new DataSet();
            com =new SqlCommand (cmdText,con);
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    dat.Load(sdr, LoadOption.OverwriteChanges, tablenames);
                }
                com.Dispose();
                con.Close();
                returnvalue = true;
            }
            catch (SqlException e)
            {
                ErrorMessage = e.StackTrace;
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            return returnvalue;
        }
        public bool AddRecordCom(string cmdText, SqlParameter[] spc)
        {
            bool returntemp = false; 
            com = new SqlCommand(cmdText, con);
            foreach (SqlParameter sp in spc)
            {
                com.Parameters.Add(sp);
            }
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                com.ExecuteNonQuery();
                returntemp = true;
                com.Dispose();
                con.Close();
            }
            catch (SqlException e)
            {
                ErrorMessage = e.StackTrace;
                System.Windows.Forms.MessageBox.Show(ErrorMessage);
            }

            return returntemp;
        }
        public bool AddRecordCom(string cmdText,out int index,SqlParameter[] spc)
        {
            bool returntemp = false; index = -1;
            com = new SqlCommand(cmdText, con);
            foreach (SqlParameter sp in spc)
            {
                com.Parameters.Add(sp);
            }
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                if (com.ExecuteScalar() != null)
                    index = int.Parse(com.ExecuteScalar().ToString());
                returntemp = true;
                com.Dispose();
                con.Close();
            }
            catch (SqlException e)
            {
                ErrorMessage = e.StackTrace;
                System.Windows.Forms.MessageBox.Show(ErrorMessage);
            }
            
            return returntemp;
        }
        
        public bool GetRecordValue(string tablename, out object[] returnvalues)
        {
            bool returntemp = false;
            string str_sql = "select * from [" + tablename + "]";
            int fieldcount = this.GetRecordColNumber(tablename);
            returnvalues = new object[1];
            if (-1 == fieldcount)
                return returntemp;
            Array.Resize(ref returnvalues, fieldcount);
            if (ExecuteCommand(str_sql))
            {
                if (reader.Read())
                {
                    try
                    {
                        reader.GetSqlValues(returnvalues);
                        returntemp = true;
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        ErrorMessage = e.StackTrace;
                        returntemp = false;
                    }
                    try
                    {
                        reader.Close();
                        com.Dispose();
                        con.Close();
                    }
                    catch (SqlException e)
                    {
                        ErrorMessage = e.StackTrace;
                        returntemp = false;
                    }
                }
            }
            return returntemp;

        }
        public string GerErrorMessage()
        {
            return ErrorMessage;
        }
        
        //返回一个dataSet.根据cmdText中的存储过程名
        public int GetDataSetBySP(string cmdText, string spparamname,string spparamvalue, ref DataSet ds,string tablename)
        {
            int intResult =-1;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                transaction = con.BeginTransaction();
                com = new SqlCommand(cmdText, con);
                com.CommandType = CommandType.StoredProcedure;
                com.Transaction = transaction;//添加事务
              
                if (spparamname == "UserOID"||spparamname =="TLDGeneratorOID")
                    com.Parameters.Add("@" + spparamname, SqlDbType.UniqueIdentifier).Value =new Guid( spparamvalue); 
                else
                com.Parameters.Add("@"+spparamname, SqlDbType.Text).Value = spparamvalue;
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = com;
                intResult = adapter.Fill(ds, tablename);
                transaction.Commit();//提交事务
            }
            catch (SqlException e)
            {
              //  CommDeal.WriteErrorLog(e.ToString());//出错，开始回滚。并写入错误信息至本地
                try
                {
                    transaction.Rollback();//事务回滚
                }
                catch (Exception ex)
                {
                  //  CommDeal.WriteErrorLog(ex.ToString());                           //事务回滚失败。写入错误信息
                }
                //ErrorMessage = e.StackTrace;
                //System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            finally
            {
                transaction.Dispose();
                if (con.State == ConnectionState.Open)
                    con.Close();
                adapter.Dispose();
                com.Dispose();
                
            }
            return intResult;
        }

        
     
       
        
        public int isDupTransBySP(string strappcode, string wastename, string initdate)
        {
            int iResult =-1;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                com = new SqlCommand("dbo.sp_isduptrans", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@TransAppCode", SqlDbType.Text).Value = strappcode;
                com.Parameters.Add("@WasteName", SqlDbType.Text).Value = wastename;
                com.Parameters.Add("@InitiateDate", SqlDbType.DateTime).Value = DateTime.Parse(initdate);
                iResult = com.ExecuteNonQuery();//返回收到影响的行数，-1表示没有影响
            }

            catch (SqlException e)
            {
                ErrorMessage = e.StackTrace;
                iResult = -1;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                com.Dispose();
            }
            return iResult;
        }
        public int GetMaxTMIDInfoBySP(string tmidsubstr, out string maxtmid)
        {
            maxtmid = ""; 
            int intResult =-1; 
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                transaction = con.BeginTransaction();
                com = new SqlCommand("dbo.sp_selecttmidsbytmid", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Transaction = transaction;//添加事务
                com.Parameters.Add(@"TManifestIDSubStr", SqlDbType.Text).Value = tmidsubstr;
                reader = com.ExecuteReader();
              
                string tmpid = ""; long tmpnum; long tempmax = 0;
                while (reader.Read())//如果读出了数据
                {    
                     tmpid=reader["TManifestID"].ToString();
                     tmpnum = System.Convert.ToInt64(tmpid.ToString().Substring(8, 5));//转换为long数据
                     if (tempmax < tmpnum)
                     {
                         tempmax = tmpnum;
                     }
                }
              
                intResult = 1;//表示获取成功
                if(tempmax!=0)
                    maxtmid = tmidsubstr + (tempmax + 1).ToString().PadLeft(5, '0');//获取最大联单号+1。
                else//没有数据则表示这为今年的第一个联单号
                {
                    maxtmid = tmidsubstr + "00001";
                }
                reader.Close();//先关闭
                transaction.Commit();//提交事务
            }
            catch (SqlException e)
            {
                //CommDeal.WriteErrorLog(e.ToString());//出错，开始回滚。并写入错误信息至本地
                try
                {
                    transaction.Rollback();//事务回滚
                }
                catch (Exception ex)
                {
                    //CommDeal.WriteErrorLog(ex.ToString());                           //事务回滚失败。写入错误信息
                }
            }
            finally
            {
                transaction.Dispose();
                if (con.State == ConnectionState.Open)
                    con.Close();
                com.Dispose();
                
            }
            return intResult;
            
        }
        public DataSet SPGetDataSet(string spText, SqlParameter[] pars, out Dictionary<string, object> outvalues)
        {
            outvalues = new Dictionary<string, object>();
            DataSet returndataset=new DataSet() ; int iResult = -1;
            try
            {

                if (con.State == ConnectionState.Closed)
                    con.Open();
                transaction = con.BeginTransaction();
                com = new SqlCommand(spText, con);
                com.CommandType = CommandType.StoredProcedure;
                com.Transaction = transaction;
                foreach (SqlParameter sq in pars)
                {
                    com.Parameters.Add(sq);//添加进来
                }
                adapter = new SqlDataAdapter(com);
                iResult = adapter.Fill(returndataset);
                //iResult = com.ExecuteNonQuery();
                if (iResult > 0)
                {
                    
                    transaction.Commit();//提交事务
                    foreach (SqlParameter sq in pars)
                    {
                        if (sq.Direction == ParameterDirection.Output || sq.Direction == ParameterDirection.ReturnValue)
                        {
                            outvalues.Add(sq.ParameterName, sq.Value);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                
                if (transaction != null)
                    transaction.Rollback();//事务回滚
            }
            finally
            {
                transaction.Dispose();
                if (con.State == ConnectionState.Open)
                    con.Close();

                if (adapter != null)
                {
                    adapter.Dispose();
                }
                com.Dispose();
            }
            return returndataset;
        }

        public bool SPExcuteNoneQuery(string spText, SqlParameter []pars, out Dictionary<string, object> outvalues)
       {
           
           outvalues = new Dictionary<string,object>();
           bool returnval = false; int iResult = -1;
           try
           {
               
               if (con.State == ConnectionState.Closed)
                   con.Open();
               transaction = con.BeginTransaction();
               com = new SqlCommand(spText, con);
               com.CommandType = CommandType.StoredProcedure;
               com.Transaction = transaction;
               foreach (SqlParameter sq in pars)
               {
                   com.Parameters.Add(sq);//添加进来
               }
               iResult = com.ExecuteNonQuery();
               if (iResult > 0)
               {
                   returnval = true;
                   transaction.Commit();//提交事务
                   foreach (SqlParameter sq in pars)
                   {
                       if (sq.Direction == ParameterDirection.Output||sq.Direction==ParameterDirection.ReturnValue)
                       {
                           outvalues.Add(sq.ParameterName, sq.Value);
                       }
                     
                   }
               }
           }
            catch(Exception  ex)
           {
               returnval = false;
                if(transaction!=null)
               transaction.Rollback();//事务回滚
           }
           finally
           {
               transaction.Dispose();
               if (con.State == ConnectionState.Open)
                   con.Close();
               com.Dispose();
           }
           return returnval;
       }
    

        public int getRegionBySP(string oid, out string region)
        {
            int intResult =-1; region = "";
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                transaction = con.BeginTransaction();
                com = new SqlCommand("dbo.sp_selectregionbyuseroid", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Transaction = transaction;//添加事务
                com.Parameters.Add("@useroid", SqlDbType.UniqueIdentifier).Value = new Guid(oid);

                reader = com.ExecuteReader();
                
                if (reader.Read())//如果读出了数据
                {
                    intResult = 1;
                    region=reader["Region"].ToString();
                }
                      if(!reader.IsClosed)   reader.Close();
                transaction.Commit();
            }
            catch (SqlException e)
            {
              //  CommDeal.WriteErrorLog(e.ToString());//出错，开始回滚。并写入错误信息至本地
                try
                {
                    transaction.Rollback();//事务回滚
                }
                catch (Exception ex)
                {
                    //CommDeal.WriteErrorLog(ex.ToString());                           //事务回滚失败。写入错误信息
                }
            }
            finally
            {
            
                transaction.Dispose();
                if (con.State == ConnectionState.Open)
                    con.Close();
            //    adapter.Dispose();
                com.Dispose();
               
            }
            return intResult;
        }


        public  bool SPGetRecordValue(string spText, SqlParameter[] parms, string fieldName, out List<string> list)
        {
           
           bool returnval = true; 
           list = new List<string>();
           try
           {
              
               if (con.State == ConnectionState.Closed)
                   con.Open();
               com = new SqlCommand(spText, con);
               com.CommandType = CommandType.StoredProcedure;
               foreach (SqlParameter sq in parms)
               {
                   com.Parameters.Add(sq);//添加进来
               }
               reader = com.ExecuteReader();

              while(reader.Read())//如果读出了数据
               {
                   
                  string  gid = reader[fieldName].ToString();
                  if (gid != null)
                  {
                      list.Add(gid);
                   }
                  
               }
            
           }
           catch (Exception ex)
           {
               returnval = false;
               if (transaction != null)
                   transaction.Rollback();//事务回滚
           }
           finally
           {
            
               if (con.State == ConnectionState.Open)
                   con.Close();
               com.Dispose();
           }
           return returnval;

        }
    }
}
