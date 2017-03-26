
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using QuartzInAction.Model;
using QuartzInAction.DBUtility;
namespace QuartzInAction.SqlServerDAL
{
    public class ExpressStatusDAL
	{
        protected QuartzInAction.Model.ExpressStatusModel Populate_Orders(IDataReader reader, Dictionary<string, string> fileds, string _conStr)
        {
            QuartzInAction.Model.ExpressStatusModel model = new QuartzInAction.Model.ExpressStatusModel();

            //Id
            if (fileds.ContainsKey("Id") && !Convert.IsDBNull(reader["Id"]))
            {
                int Id = 0;
                if (int.TryParse(reader["Id"].ToString(), out Id))
                    model.Id = Id;
            }

            if (fileds.ContainsKey("Status") && !Convert.IsDBNull(reader["Status"]))
                model.Status = !string.IsNullOrEmpty(reader["Status"].ToString()) ? reader["Status"].ToString().Trim() : string.Empty;

            if (fileds.ContainsKey("OMessage") && !Convert.IsDBNull(reader["OMessage"]))
                model.OMessage = !string.IsNullOrEmpty(reader["OMessage"].ToString()) ? reader["OMessage"].ToString().Trim() : string.Empty;

            if (fileds.ContainsKey("State") && !Convert.IsDBNull(reader["State"]))
                model.State = !string.IsNullOrEmpty(reader["State"].ToString()) ? reader["State"].ToString().Trim() : string.Empty;
           
            if (fileds.ContainsKey("Ischeck") && !Convert.IsDBNull(reader["Ischeck"]))
                model.Ischeck = !string.IsNullOrEmpty(reader["Ischeck"].ToString()) ? reader["Ischeck"].ToString().Trim() : string.Empty;

            if (fileds.ContainsKey("Nu") && !Convert.IsDBNull(reader["Nu"]))
                model.Nu = !string.IsNullOrEmpty(reader["Nu"].ToString()) ? reader["Nu"].ToString().Trim() : string.Empty;

            return model;
        }

        public IList<ExpressStatusModel> GetEntities(string sqlwhere, string _conStr)
        {

            SqlServerHelper.PopulateDelegate<ExpressStatusModel> entities = new SqlServerHelper.PopulateDelegate<ExpressStatusModel>(this.Populate_Orders);
            string sql = string.Format("select * from ExpressStatus ");
            if (sqlwhere.Trim() != "")
            {
                sql += string.Format(" where {0} ", sqlwhere);
            }

            return SqlServerHelper.GetEntities<ExpressStatusModel>(entities, sql, _conStr);

        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sqlWhere">sql条件</param>
        /// <returns></returns>
        public bool DeleteEntity(string sqlWhere, string _conStr)
        {
            bool b = false;
            try
            {
                SqlServerHelper.ExecuteNonQuery(String.Format(" DELETE  ExpressStatus  WHERE {0}", sqlWhere), null, _conStr);
                b = true;
            }
            catch (Exception ex)
            {

                Common.Loger.WriteFile("DeleteEntity", ex.ToString());
            }
            return b;
        }

	}
}

