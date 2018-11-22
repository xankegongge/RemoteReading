using System.Data;
/// <summary>
///数据访问基类
///每一个实体的访问类都从本类继承出去
/// </summary>
namespace DataProvider
{
    abstract public class DataProvider
    {
        abstract protected bool ExecuteCommand(string cmdText);
        abstract public int GetRecordNumber(string cmdText);
        abstract public bool GetRecordValue(string cmdText, string[] getvaluefieldnames, out object[] returnvalues);
        abstract public bool GetRecordValue(string cmdText, string getvaluefieldname, out object returnvalue);
        public int GetRecordNumber(string tablename, string fieldname, object value)
        {
            string str_sql = "select * from [" + tablename + "]";
            str_sql += " where " + fieldname + "=";
            if (value is string)
                str_sql += "'" + value.ToString() + "'";
            if (value is int)
                str_sql += value.ToString();
            return GetRecordNumber(str_sql);
        }
        public int GetRecordNumber(string tablename, string fieldname1, object value1, string fieldname2, object value2, string logic)
        {
            string str_sql = "select * from [" + tablename + "]";
            str_sql += " where " + fieldname1 + "=";
            if (value1 is string)
                str_sql += "'" + value1.ToString() + "'";
            if (value1 is int)
                str_sql += value1.ToString();
            str_sql += " " + logic + " " + fieldname2 + "=";
            if (value2 is string)
                str_sql += "'" + value2.ToString() + "'";
            if (value2 is int)
                str_sql += value2.ToString();
            return GetRecordNumber(str_sql);
        }
        abstract public int GetRecordColNumber(string tablename);
        abstract public bool ExcuteSql(string cmdText);
        abstract public bool GetDateSet(string cmdText, out DataSet dat);
        //abstract public bool GetRecordValues(string cmdText, string[] getvaluefieldnames, out object[] returnvalues);
        public bool GetRecordValue(string tablename, string fieldname, object value, string getvaluefieldmame, out object returnvalue)
        {
            string str_sql = "select * from [" + tablename + "]";
            str_sql += " where " + fieldname + "=";
            if (value is string)
                str_sql += "'" + value.ToString() + "'";
            if (value is int)
                str_sql += value.ToString();
            return GetRecordValue(str_sql, getvaluefieldmame, out returnvalue);

        }
        public bool GetRecordValue(string tablename, string fieldname, object value, string[] getvaluefieldnames, out object[] returnvalues)
        {
            string str_sql = "select * from [" + tablename + "]";
            str_sql += " where " + fieldname + "=";
            if (value is string)
                str_sql += "'" + value.ToString() + "'";
            if (value is int)
                str_sql += value.ToString();
            return GetRecordValue(str_sql, getvaluefieldnames, out returnvalues);
        }
        public bool AddRecord(string tablename, object[] values)
        {
            int i;
            string str_sql = "insert into [" + tablename + "] values(";
            for (i = 0; i < values.Length; i++)
            {
                if (values[i] is string)
                    str_sql += "'" + values[i].ToString() + "'";
                if (values[i] is int)
                    str_sql += values[i].ToString();
                str_sql += (i >= values.Length - 1 ? ")" : ",");
            }
            return ExcuteSql(str_sql);
        }
        public bool UpdateRecord(string tablename, string fieldname, object value, string updatefieldname, object updatevalue)
        {
            string str_sql = "update [" + tablename + "]";
            str_sql += " set " + updatefieldname + "=";
            if (updatevalue is string)
                str_sql += "'" + updatevalue.ToString() + "'";
            if (updatevalue is int)
                str_sql += updatevalue.ToString();
            str_sql += " where " + fieldname + "=";
            if (value is string)
                str_sql += "'" + value.ToString() + "'";
            if (value is int)
                str_sql += value.ToString();
            return ExcuteSql(str_sql);
        }
        public bool DeleteRecord(string tablename, string fieldname, object value)
        {
            string str_sql = "delete from [" + tablename + "]";
            str_sql += " where " + fieldname + "=";
            if (value is string)
                str_sql += "'" + value.ToString() + "'";
            if (value is int)
                str_sql += value.ToString();
            return ExcuteSql(str_sql);
        }
       
    }
}