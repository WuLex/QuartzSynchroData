using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using QuartzInAction.DBUtility;

namespace QuartzInAction.SqlServerDAL
{
    /// <summary>
    /// 物流信息数据实现类
    /// </summary>
    public class LogisticsDAL  
    {
        /// <summary>
        /// 将记录集转为LogisticsModel实体类 (LogisticsModel)
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <param name="fileds">字段名列表</param>
        /// <returns>LogisticsModel</returns>
        protected QuartzInAction.Model.LogisticsModel Populate_LogisticsModel(IDataReader reader, Dictionary<string, string> fileds, string _conStr)
        {
            QuartzInAction.Model.LogisticsModel model = new QuartzInAction.Model.LogisticsModel();
            //物流信息内容
            if (fileds.ContainsKey("Context") && !Convert.IsDBNull(reader["Context"]))
                model.Context = reader["Context"].ToString();
            //查询时间
            if (fileds.ContainsKey("LDTime") && !Convert.IsDBNull(reader["LDTime"]))
            {
                DateTime lDTime;
                if (DateTime.TryParse(reader["LDTime"].ToString(), out lDTime))
                    model.LDTime = lDTime;
            }

            return model;
        }

        /// <summary>
        /// 根据条件，返回IList
        /// </summary>
        /// <param name="sqlWhere">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public IList<QuartzInAction.Model.LogisticsModel> GetEntities(string sql,string _conStr, params SqlParameter[] parameters)
        {
            SqlServerHelper.PopulateDelegate<QuartzInAction.Model.LogisticsModel> entities =
                        new SqlServerHelper.PopulateDelegate<QuartzInAction.Model.LogisticsModel>(this.Populate_LogisticsModel);

            return SqlServerHelper.GetEntities<QuartzInAction.Model.LogisticsModel>(entities, sql, _conStr, parameters);
        }

        /// <summary>
        /// 根据快递单号得到物流信息
        /// </summary>
        /// <returns></returns>
        public System.Collections.Generic.IList<QuartzInAction.Model.LogisticsModel> GetEntities(string nu, string _conStr)
        {
            if (string.IsNullOrEmpty(nu))
                return new System.Collections.Generic.List<QuartzInAction.Model.LogisticsModel>();
            string sql = "select Context,convert(datetime,LDTime) as LDTime  from [LData] ";
            sql += " where DId = (select max(Id) as Id from DeliveryStatus where Nu = @Nu ) ";
            sql += " order by LDTime desc ";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(){ParameterName = "@Nu", SqlDbType = SqlDbType.VarChar, Size = 50, Value = nu }
            };

            return GetEntities(sql, _conStr, parameters);
        }


        /// <summary>
        /// 添加数据到快递公司送货状态最新消息数据表
        /// </summary>
        /// <param name="id">快递公司送货状态表Id</param>
        /// <param name="entity">最新消息数据实体</param>
        /// <returns></returns>
        public QuartzInAction.Model.LogisticsModel AddEntity(int id, QuartzInAction.Model.LogisticsModel entity, string _conStr)
        {
            StringBuilder sql = new StringBuilder(100);
            sql.Append(" INSERT INTO [LData](DId,Context,LDTime,LDFTime,Status,AreaCode,AreaName) ");
            sql.Append(" VALUES (@DId,@Context,@LDTime,@LDFTime,@Status,@AreaCode,@AreaName) ");
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(){ParameterName = "@DId", SqlDbType = SqlDbType.Int, Value = id },
                new SqlParameter(){ParameterName = "@Context", SqlDbType = SqlDbType.NVarChar, Size = 2000, Value = (entity.Context != null ? entity.Context : string.Empty) },
                new SqlParameter(){ParameterName = "@LDTime", SqlDbType = SqlDbType.VarChar, Size = 100, Value = (entity.LDTime != null ? entity.LDTime :DateTime.Now) },
                new SqlParameter(){ParameterName = "@LDFTime", SqlDbType = SqlDbType.VarChar, Size = 100, Value = (entity.LDFTime != null ? entity.LDFTime : string.Empty) },
                new SqlParameter(){ParameterName = "@Status", SqlDbType = SqlDbType.VarChar, Size = 50, Value = (entity.Status != null ? entity.Status : string.Empty) },
                new SqlParameter(){ParameterName = "@AreaCode", SqlDbType = SqlDbType.VarChar, Size = 50, Value = (entity.AreaCode != null ? entity.AreaCode : string.Empty) },
                new SqlParameter(){ParameterName = "@AreaName", SqlDbType = SqlDbType.VarChar, Size = 500, Value = (entity.AreaName != null ? entity.AreaName : string.Empty) }
            };
            try
            {
                if (SqlServerHelper.ExecuteNonQuery(sql.ToString(), parameters, _conStr) > 0)
                    return entity;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sqlWhere">sql条件</param>
        /// <returns></returns>
        public bool DeleteEntity(string sqlWhere,string _conStr)
        {
            bool b = false;
            try
            {
                SqlServerHelper.ExecuteNonQuery(String.Format(" DELETE FROM [LData] WHERE {0}", sqlWhere), null, _conStr);
                b = true;
            }
            catch (Exception ex)
            { }
            return b;
        }


    }
}
