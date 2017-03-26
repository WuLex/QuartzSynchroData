
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using QuartzInAction.DBUtility;
using QuartzInAction.Model;

namespace QuartzInAction.DAL
{
    public class DBConfigDAL
    {

        /// <summary>
        /// DBConfigModel实体类 
        /// </summary>
        protected QuartzInAction.Model.DBConfigModel Populate_BankList(IDataReader reader, Dictionary<string, string> fileds, string _conStr)
        {
            QuartzInAction.Model.DBConfigModel model = new QuartzInAction.Model.DBConfigModel();

            // 
            if (fileds.ContainsKey("DBName") && !Convert.IsDBNull(reader["DBName"]))
                model.DBName = !string.IsNullOrEmpty(reader["DBName"].ToString()) ? reader["DBName"].ToString().Trim() : string.Empty;

            // 
            if (fileds.ContainsKey("DBNum") && !Convert.IsDBNull(reader["DBNum"]))
                model.DBNum = !string.IsNullOrEmpty(reader["DBNum"].ToString()) ? reader["DBNum"].ToString().Trim() : string.Empty;


            if (fileds.ContainsKey("DBName") && !Convert.IsDBNull(reader["DBName"]))
                model.DBName = !string.IsNullOrEmpty(reader["DBName"].ToString()) ? reader["DBName"].ToString().Trim() : string.Empty;

            if (fileds.ContainsKey("GetOrderDT") && !Convert.IsDBNull(reader["GetOrderDT"]))
            {
                int getOrdersDt = 0;
                if (int.TryParse(reader["GetOrderDT"].ToString(), out getOrdersDt))
                    model.GetOrderDT = getOrdersDt;
            }

            if (fileds.ContainsKey("Valid") && !Convert.IsDBNull(reader["Valid"]))
            {
                int valid = 0;
                if (int.TryParse(reader["Valid"].ToString(), out valid))
                    model.Valid = valid;
            }

            #region 备用字段
            if (fileds.ContainsKey("Remark1") && !Convert.IsDBNull(reader["Remark1"]))
                model.Remark1 = string.IsNullOrEmpty(reader["Remark1"].ToString()) ? string.Empty : reader["Remark1"].ToString().Trim();
            if (fileds.ContainsKey("Remark2") && !Convert.IsDBNull(reader["Remark2"]))
                model.Remark2 = string.IsNullOrEmpty(reader["Remark2"].ToString()) ? string.Empty : reader["Remark2"].ToString().Trim();
            if (fileds.ContainsKey("Remark3") && !Convert.IsDBNull(reader["Remark3"]))
                model.Remark3 = string.IsNullOrEmpty(reader["Remark3"].ToString()) ? string.Empty : reader["Remark3"].ToString().Trim();
            if (fileds.ContainsKey("Remark4") && !Convert.IsDBNull(reader["Remark4"]))
                model.Remark4 = string.IsNullOrEmpty(reader["Remark4"].ToString()) ? string.Empty : reader["Remark4"].ToString().Trim();
            if (fileds.ContainsKey("Remark5") && !Convert.IsDBNull(reader["Remark5"]))
                model.Remark5 = string.IsNullOrEmpty(reader["Remark5"].ToString()) ? string.Empty : reader["Remark5"].ToString().Trim();
            #endregion

            return model;
        }


        /// <summary>
        /// 根据条件，返回IList
        /// </summary>
        /// <param name="sqlWhere">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public IList<QuartzInAction.Model.DBConfigModel> GetEntities(string sql, string _conStr, params SqlParameter[] parameters)
        {
            SqlServerHelper.PopulateDelegate<QuartzInAction.Model.DBConfigModel> entities = new SqlServerHelper.PopulateDelegate<QuartzInAction.Model.DBConfigModel>(this.Populate_BankList);

            return SqlServerHelper.GetEntities<QuartzInAction.Model.DBConfigModel>(entities, sql, _conStr, parameters);
        }

        /// <summary>
        /// 根据条件，返回IList
        /// </summary>
        /// <param name="sqlWhere">sql条件</param>
        /// <returns>IList</returns>
        public IList<DBConfigModel> GetEntities(string sqlWhere, string _conStr)
        {
            SqlServerHelper.PopulateDelegate<QuartzInAction.Model.DBConfigModel> entities = new SqlServerHelper.PopulateDelegate<QuartzInAction.Model.DBConfigModel>(this.Populate_BankList);

            string sql = string.Format("select * from  DBConfig ");
            if (sqlWhere.Trim() != "")
            {
                sql += string.Format(" where {0} ", sqlWhere);
            }
            return SqlServerHelper.GetEntities<QuartzInAction.Model.DBConfigModel>(entities, sql, _conStr);
        }


    }
}

