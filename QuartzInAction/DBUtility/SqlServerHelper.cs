using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Text;
using System.Data.Common;
using QuartzInAction.Common;


namespace QuartzInAction.DBUtility
{
    public class SqlServerHelper
    {
        /// <summary>
        /// 委托将DataReader转为实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <param name="Fileds">字段名列表</param>
        /// <returns></returns>
        public delegate T PopulateDelegate<T>(IDataReader dr, Dictionary<string, string> Fileds, string _conStr);

        /// <summary>
        /// 返回IList
        /// </summary>
        /// <param name="pd">委托对象</param>
        /// <param name="pp">查询字符串</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>返回记录集List</returns>
        public static IList<T> GetEntities<T>(PopulateDelegate<T> pd, string sql, string _conStr)
        {
            IList<T> lst = new List<T>();
            SqlDataReader reader = null;
            try
            {
                reader = GetDataReader(sql, null, _conStr);
                Dictionary<string, string> Fileds = new Dictionary<string, string>();
                foreach (DataRow var in reader.GetSchemaTable().Select())
                {
                    Fileds.Add(var[0].ToString(), var[0].ToString());
                }
                while (reader.Read())
                {
                    lst.Add(pd(reader, Fileds, _conStr));
                }
            }
            catch (Exception ex)
            {
                QuartzInAction.Common.Loger.WriteFile(
                    " public static IList<T> GetEntities<T>(PopulateDelegate<T> pd, string sql, string _conStr)出错！" + ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return lst;
        }

        public static IList<T> GetEntities<T>(PopulateDelegate<T> pd, string sql, string _conStr,
            params SqlParameter[] parameters)
        {
            IList<T> lst = new List<T>();
            SqlDataReader reader = null;
            try
            {
                reader = GetDataReader(sql, parameters, _conStr);
                Dictionary<string, string> Fileds = new Dictionary<string, string>();
                foreach (DataRow var in reader.GetSchemaTable().Select())
                {
                    Fileds.Add(var[0].ToString(), var[0].ToString());
                }
                while (reader.Read())
                {
                    lst.Add(pd(reader, Fileds, _conStr));
                }
            }
            catch (Exception ex)
            {
                QuartzInAction.Common.Loger.WriteFile(
                    "public static IList<T> GetEntities<T>(PopulateDelegate<T> pd, string sql, string _conStr, params SqlParameter[] parameters)出错！" +
                    ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return lst;
        }

        /// <summary>
        /// 返回IList
        /// </summary>
        /// <param name="pd">委托对象</param>
        /// <param name="queryParam">查询字符串</param>
        /// <param name="count">返回记录总数</param>
        /// <returns>返回记录集List</returns>
        public static IList<T> GetEntities<T>(PopulateDelegate<T> pd, QuartzInAction.Common.QueryParam queryParam,
            string _conStr, out int count)
        {
            List<T> lst = new List<T>();
            count = 0;
            using (SqlConnection conn = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand("SupesoftPage", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                // 设置参数
                cmd.Parameters.Add("@TableName", SqlDbType.NVarChar, 500).Value = queryParam.TableName;
                cmd.Parameters.Add("@ReturnFields", SqlDbType.NVarChar, 500).Value = queryParam.ReturnFields;
                cmd.Parameters.Add("@Where", SqlDbType.NVarChar, 500).Value = queryParam.Where;
                cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = queryParam.PageIndex;
                cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = queryParam.PageSize;
                cmd.Parameters.Add("@Orderfld", SqlDbType.NVarChar, 200).Value = queryParam.Orderfld;
                cmd.Parameters.Add("@OrderType", SqlDbType.Int).Value = queryParam.OrderType;
                // 执行
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Dictionary<string, string> Fileds = new Dictionary<string, string>();
                foreach (DataRow var in dr.GetSchemaTable().Select())
                {
                    Fileds.Add(var[0].ToString(), var[0].ToString());
                }
                while (dr.Read())
                {
                    lst.Add(pd(dr, Fileds, _conStr));
                }
                // 取记录总数 及页数
                if (dr.NextResult())
                {
                    if (dr.Read())
                    {
                        count = Convert.ToInt32(dr["RecordCount"]);
                    }
                }

                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return lst;
        }

        /// <summary>
        /// 返回IList
        /// </summary>
        /// <param name="pd">委托对象</param>
        /// <param name="queryParam">查询字符串</param>
        /// <param name="i">重载标记字段，任意数值</param>
        /// <param name="count">返回记录总数</param>
        /// <returns>返回记录集List</returns>
        public static IList<T> GetEntities<T>(PopulateDelegate<T> pd, QuartzInAction.Common.QueryParam queryParam, int i,
            out int count, string _conStr)
        {
            IList<T> lst = new List<T>();
            count = 0;
            using (SqlConnection Conn = new SqlConnection(_conStr))
            {
                StringBuilder sb = new StringBuilder();
                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr = null;
                cmd.Connection = Conn;

                int TotalRecordForPageIndex = queryParam.PageIndex*queryParam.PageSize;
                string OrderBy;
                string CutOrderBy;
                if (queryParam.OrderType == 1)
                {
                    OrderBy = " Order by " + queryParam.Orderfld.Replace(",", " desc,") + " desc ";
                    CutOrderBy = " Order by " + queryParam.Orderfld.Replace(",", " asc,") + " asc ";
                }
                else
                {
                    OrderBy = " Order by " + queryParam.Orderfld.Replace(",", " asc,") + " asc ";
                    CutOrderBy = " Order by " + queryParam.Orderfld.Replace(",", " desc,") + " desc ";
                }

                Conn.Open();
                // 取记录总数
                cmd.CommandText = string.Format("SELECT Count(1) From {0} {1}", queryParam.TableName, queryParam.Where);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Parameters.Clear();

                int CurrentPageSize = queryParam.PageSize;
                if ((count - 1)/queryParam.PageSize + 1 == queryParam.PageIndex)
                {
                    CurrentPageSize = count%queryParam.PageSize;
                    if (CurrentPageSize == 0)
                        CurrentPageSize = queryParam.PageSize;
                }
                //取记录值
                //MYSQL : sb.AppendFormat("SELECT * FROM (SELECT * FROM (SELECT  {2}	FROM {3} {4} {5} LIMIT {1} ) TB2  {6}  LIMIT {0}) TB3 {5} ", CurrentPageSize, TotalRecordForPageIndex, queryParam.ReturnFields, queryParam.TableName, queryParam.Where, OrderBy, CutOrderBy);
                sb.AppendFormat(
                    "SELECT * FROM (SELECT TOP {0} * FROM (SELECT TOP {1} {2}	FROM {3} {4} {5}) TB2	{6}) TB3 {5} ",
                    CurrentPageSize, TotalRecordForPageIndex, queryParam.ReturnFields, queryParam.TableName,
                    queryParam.Where, OrderBy, CutOrderBy);
                cmd.CommandText = sb.ToString();
                dr = cmd.ExecuteReader();

                Dictionary<string, string> Fileds = new Dictionary<string, string>();
                foreach (DataRow var in dr.GetSchemaTable().Select())
                {
                    Fileds.Add(var[0].ToString(), var[0].ToString());
                }
                while (dr.Read())
                {
                    lst.Add(pd(dr, Fileds, _conStr));
                }
                dr.Close();
                dr.Dispose();
                dr.Close();
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return lst;
        }

        /// <summary>
        /// 执行存储过程，并返回SqlDataReader对象
        /// </summary>
        /// <returns>返回SqlDataReader</returns>
        public static SqlDataReader GetDataReaderbyStoredProcedure(SqlCommand cmd, SqlConnection con,
            string storedProcedursName, IList<SqlParameter> parameters, string _conStr)
        {
            con.ConnectionString = _conStr;
            con.Open();
            cmd.CommandText = storedProcedursName;
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;

            PrepareCommand(cmd, parameters);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }


        /// <summary>
        /// 执行存储过程，并返回DataTable对象
        /// </summary>
        /// <returns>返回Table</returns>
        public static DataTable GetDataTableByStoredProcedure(string storedProcedursName, SqlParameter[] parameters,
            string _conStr)
        {
            using (SqlConnection conn = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand(storedProcedursName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);

                // 执行
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                cmd.Dispose();
                conn.Close();
                return dt;
            }
        }


        /// <summary>
        /// 执行存储过程，并返回String对象
        /// </summary>
        /// <returns>返回SqlDataReader</returns>
        public static string ExecStoredProcedure(string storedProcedursName, SqlParameter[] parameters, string _conStr)
        {
            using (SqlConnection conn = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand(storedProcedursName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);
                //设定参数的输出方向  
                cmd.Parameters.Add("@UpdateResult", SqlDbType.NVarChar, 20).Direction = ParameterDirection.Output;

                // 执行
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                string result = cmd.Parameters["@UpdateResult"].Value.ToString();

                cmd.Dispose();
                conn.Close();
                return result;
            }
        }


        /// <summary>
        /// 执行存储过程，并返回String对象，dal_coupon.cs中使用到
        /// </summary>
        /// <returns>返回SqlDataReader</returns>
        public static string ExecStoredProcedure(string storedProcedursName, string outputParameter,
            SqlParameter[] parameters, string _conStr)
        {
            using (SqlConnection conn = new SqlConnection(_conStr))
            {
                SqlCommand cmd = new SqlCommand(storedProcedursName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);

                // 执行
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                string result = cmd.Parameters[outputParameter].Value.ToString();

                cmd.Dispose();
                conn.Close();
                return result;
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, string _conStr)
        {
            using (SqlConnection connection = new SqlConnection(_conStr))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 对连接执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        public static int ExecuteNonQuery(String sql, IList<SqlParameter> parameters, string _conStr)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    PrepareCommand(cmd, parameters);
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, string _conStr, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(_conStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        QuartzInAction.Common.Loger.WriteFile(e.ToString());
                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，并返回SqlDataReader对象
        /// </summary>
        /// <returns>返回SqlDataReader</returns>
        public static SqlDataReader GetDataReader(String sql, IList<SqlParameter> parameters, string _conStr)
        {
            SqlConnection con = new SqlConnection(_conStr);
            SqlCommand cmd = new SqlCommand(sql, con);
            PrepareCommand(cmd, parameters);
            con.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// 执行SQL语句，并且返回查询出的数据
        /// </summary>
        /// <param name="sql">查询的SQL语句</param>
        /// <returns>返回的数据</returns>
        public static DataSet ExecuteDataSet(string sql, SqlParameter[] parameters, CommandType commType, string _conStr)
        {
            using (SqlConnection con = new SqlConnection(_conStr))
            {
                SqlCommand comm = new SqlCommand(sql, con);
                comm.CommandType = commType;
                if (parameters != null && parameters.Length > 0)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        comm.Parameters.Add(parameter);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataSet ds = new DataSet();
                da.Fill(ds);
                da.Dispose();
                return ds;
            }
        }

        /// <summary>
        /// 为SqlCommand准备执行语句
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText,
            SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open) //如果数据库连接为关闭状态 
                conn.Open(); //打开数据库连接 
            cmd.Connection = conn; //设置命令连接  
            cmd.CommandText = cmdText; //设置执行命令的sql语句  
            if (trans != null) //如果事务不为空  
                cmd.Transaction = trans; //设置执行命令的事务  
            cmd.CommandType = CommandType.Text; //设置解释sql语句的类型为“文本”类型（也是就说该函数不适用于存储过程）
            if (cmdParms != null) //如果参数数组不为空
            {
                //循环传入的参数数组 
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput ||
                         parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value; //获取参数的值
                    }
                    cmd.Parameters.Add(parameter); //添加参数
                }
            }
        }

        /// <summary>
        /// 为SqlCommand准备执行语句
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameters"></param>
        private static void PrepareCommand(SqlCommand cmd, IList<SqlParameter> parameters)
        {
            if (parameters != null && parameters.Count > 0)
                foreach (SqlParameter parameter in parameters)
                    cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 使用SqlBulkCopy 提交DataTable到数据
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static bool ExecuteInsert(DataTable dt, string tableName, string _conStr)
        {
            using (SqlConnection connection = new SqlConnection(_conStr))
            {
                try
                {
                    connection.Open();
                    using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(connection))
                    {
                        sqlbulkcopy.DestinationTableName = tableName;
                        sqlbulkcopy.BulkCopyTimeout = 3600;
                        sqlbulkcopy.WriteToServer(dt);
                        return true;
                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    Loger.WriteFile("SqlServerHelper", _conStr + ",出现异常:" + ex.ToString());
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>   
        /// 执行SQL语句并返回数据表   
        /// </summary>   
        /// <param name="Sqlstr">SQL语句</param>   
        /// <returns></returns>   
        public static DataTable ExecuteDt(String Sqlstr, string _conStr)
        {
            using (SqlConnection conn = new SqlConnection(_conStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(Sqlstr, conn);
                DataTable dt = new DataTable();
                conn.Open();
                da.Fill(dt);
                conn.Close();
                da.Dispose();
                return dt;
            }
        }
    }
}