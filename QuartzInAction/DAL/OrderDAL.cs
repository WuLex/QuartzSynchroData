using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using QuartzInAction.DBUtility;
using QuartzInAction.Model;
using System.Data.SqlTypes;


namespace QuartzInAction.SqlServerDAL
{
    /// <summary>
    /// 订单数据实现类
    /// </summary>
    public class OrderDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <param name="fileds">字段名列表</param>
        /// <returns>OrderModel</returns>
        protected QuartzInAction.Model.OrderModel Populate_Orders(IDataReader reader, Dictionary<string, string> fileds,
            string _conStr)
        {
            QuartzInAction.Model.OrderModel model = new QuartzInAction.Model.OrderModel();

            //订单编号
            if (fileds.ContainsKey("ONum") && !Convert.IsDBNull(reader["ONum"]))
                model.ONum = !string.IsNullOrEmpty(reader["ONum"].ToString())
                    ? reader["ONum"].ToString().Trim()
                    : string.Empty;

            //商品编号
            if (fileds.ContainsKey("PNum") && !Convert.IsDBNull(reader["PNum"]))
                model.PNum = !string.IsNullOrEmpty(reader["PNum"].ToString())
                    ? reader["PNum"].ToString().Trim()
                    : string.Empty;

            //活动编号
            if (fileds.ContainsKey("ANum") && !Convert.IsDBNull(reader["ANum"]))
            {
                model.ANum = !string.IsNullOrEmpty(reader["ANum"].ToString())
                    ? reader["ANum"].ToString().Trim()
                    : string.Empty;
            }

            //验证码
            if (fileds.ContainsKey("VerificationCode") && !Convert.IsDBNull(reader["VerificationCode"]))
                model.VerificationCode = !string.IsNullOrEmpty(reader["VerificationCode"].ToString())
                    ? reader["VerificationCode"].ToString().Trim()
                    : string.Empty;


            //收货人姓名
            if (fileds.ContainsKey("Consignee") && !Convert.IsDBNull(reader["Consignee"]))
                model.Consignee = !string.IsNullOrEmpty(reader["Consignee"].ToString())
                    ? reader["Consignee"].ToString().Trim()
                    : string.Empty;


            //收货人手机号
            if (fileds.ContainsKey("CPhone") && !Convert.IsDBNull(reader["CPhone"]))
                model.CPhone = !string.IsNullOrEmpty(reader["CPhone"].ToString())
                    ? reader["CPhone"].ToString().Trim()
                    : string.Empty;

            //银行编号
            if (fileds.ContainsKey("DBNum") && !Convert.IsDBNull(reader["DBNum"]))
                model.DBNum = reader["DBNum"].ToString();

            //收货人地址
            if (fileds.ContainsKey("Address") && !Convert.IsDBNull(reader["Address"]))
                model.Address = !string.IsNullOrEmpty(reader["Address"].ToString())
                    ? reader["Address"].ToString().Trim()
                    : string.Empty;


            //运费组Id
            if (fileds.ContainsKey("GroupId") && !Convert.IsDBNull(reader["GroupId"]))
            {
                int groupId = 0;
                if (int.TryParse(reader["GroupId"].ToString(), out groupId))
                    model.GroupId = groupId;
            }

            //地区key
            if (fileds.ContainsKey("AreaKey") && !Convert.IsDBNull(reader["AreaKey"]))
                model.AreaKey = !string.IsNullOrEmpty(reader["AreaKey"].ToString())
                    ? reader["AreaKey"].ToString().Trim()
                    : string.Empty;

            //下单时间
            if (fileds.ContainsKey("OrdersDt") && !Convert.IsDBNull(reader["OrdersDt"]))
            {
                DateTime ordersDt;
                if (DateTime.TryParse(reader["OrdersDt"].ToString(), out ordersDt))
                    model.OrdersDt = ordersDt;
            }


            //支付时间
            if (fileds.ContainsKey("PayDt") && !Convert.IsDBNull(reader["PayDt"]))
            {
                DateTime payDt;
                if (DateTime.TryParse(reader["PayDt"].ToString(), out payDt))
                    model.PayDt = payDt;
            }


            //发货时间
            if (fileds.ContainsKey("ShipDt") && !Convert.IsDBNull(reader["ShipDt"]))
            {
                DateTime shipDt;
                if (DateTime.TryParse(reader["ShipDt"].ToString(), out shipDt))
                    model.ShipDt = shipDt;
            }


            //快递公司编码
            if (fileds.ContainsKey("CourierCompanies") && !Convert.IsDBNull(reader["CourierCompanies"]))
                model.CourierCompanies = string.IsNullOrEmpty(reader["CourierCompanies"].ToString())
                    ? string.Empty
                    : reader["CourierCompanies"].ToString().Trim();

            //快递单号
            if (fileds.ContainsKey("CourierNum") && !Convert.IsDBNull(reader["CourierNum"]))
                model.CourierNum = string.IsNullOrEmpty(reader["CourierNum"].ToString())
                    ? string.Empty
                    : reader["CourierNum"].ToString().Trim();


            //订单状态
            if (fileds.ContainsKey("Status") && !Convert.IsDBNull(reader["Status"]))
            {
                int status = 0;
                if (int.TryParse(reader["Status"].ToString(), out status))
                    model.Status = status;
            }

            //撤单区分
            //0:手动撤单。1:系统自动撤单
            if (fileds.ContainsKey("Differentiate") && !Convert.IsDBNull(reader["Differentiate"]))
            {
                int differentiate = 0;
                if (int.TryParse(reader["Differentiate"].ToString(), out differentiate))
                    model.Differentiate = differentiate;
            }

            //撤单理由
            if (fileds.ContainsKey("Reason") && !Convert.IsDBNull(reader["Reason"]))
                model.Reason = string.IsNullOrEmpty(reader["Reason"].ToString())
                    ? string.Empty
                    : reader["Reason"].ToString().Trim();

            //撤单时间
            if (fileds.ContainsKey("DifferentiateDt") && !Convert.IsDBNull(reader["DifferentiateDt"]))
            {
                DateTime differentiateDt;
                if (DateTime.TryParse(reader["DifferentiateDt"].ToString(), out differentiateDt))
                    model.DifferentiateDt = differentiateDt;
            }

            #region  退货相关字段

            //退货提交时间
            //退货完成时间
            //银行返回成功退货时间
            if (fileds.ContainsKey("ReturnEndDt") && !Convert.IsDBNull(reader["ReturnEndDt"]))
            {
                DateTime returnEndDt;
                if (DateTime.TryParse(reader["ReturnEndDt"].ToString(), out returnEndDt))
                    model.ReturnEndDt = returnEndDt;
            }

            //退货订单号
            //提交银行退货订单号（原则上：订单号最后一位加大写字母X 注：个别银行在退货时需要提交另外生成的退货订单号，不需要时不生成）
            if (fileds.ContainsKey("RNum") && !Convert.IsDBNull(reader["RNum"]))
                model.RNum = string.IsNullOrEmpty(reader["RNum"].ToString())
                    ? string.Empty
                    : reader["RNum"].ToString().Trim();

            //退货说明
            if (fileds.ContainsKey("ReturnRemark") && !Convert.IsDBNull(reader["ReturnRemark"]))
                model.ReturnRemark = string.IsNullOrEmpty(reader["ReturnRemark"].ToString())
                    ? string.Empty
                    : reader["ReturnRemark"].ToString().Trim();

            #endregion

            //订单中该商品的活动价格
            if (fileds.ContainsKey("Price") && !Convert.IsDBNull(reader["Price"]))
            {
                decimal price = 0;
                if (decimal.TryParse(reader["Price"].ToString(), out price))
                    model.Price = price;
            }

            //运费
            if (fileds.ContainsKey("CarriagePrice") && !Convert.IsDBNull(reader["CarriagePrice"]))
            {
                decimal carriagePrice = 0;
                if (decimal.TryParse(reader["CarriagePrice"].ToString(), out carriagePrice))
                    model.CarriagePrice = carriagePrice;
            }

            //实际支付金额
            if (fileds.ContainsKey("PayPrice") && !Convert.IsDBNull(reader["PayPrice"]))
            {
                decimal payPrice = 0;
                if (decimal.TryParse(reader["PayPrice"].ToString(), out payPrice))
                    model.PayPrice = payPrice;
            }

            //用户Id
            if (fileds.ContainsKey("MId") && !Convert.IsDBNull(reader["MId"]))
                model.MId = string.IsNullOrEmpty(reader["MId"].ToString())
                    ? string.Empty
                    : reader["MId"].ToString().Trim();

            //支付结果
            if (fileds.ContainsKey("PayResult") && !Convert.IsDBNull(reader["PayResult"]))
                model.PayResult = string.IsNullOrEmpty(reader["PayResult"].ToString())
                    ? string.Empty
                    : reader["PayResult"].ToString().Trim();

            //支付编号
            if (fileds.ContainsKey("PayNo") && !Convert.IsDBNull(reader["PayNo"]))
                model.PayNo = string.IsNullOrEmpty(reader["PayNo"].ToString())
                    ? string.Empty
                    : reader["PayNo"].ToString().Trim();

            //说明
            if (fileds.ContainsKey("Remark") && !Convert.IsDBNull(reader["Remark"]))
                model.Remark = string.IsNullOrEmpty(reader["Remark"].ToString())
                    ? string.Empty
                    : reader["Remark"].ToString().Trim();

            //支付成功后用户的短信通知内容
            if (fileds.ContainsKey("PayMessageContent") && !Convert.IsDBNull(reader["PayMessageContent"]))
                model.PayMessageContent = string.IsNullOrEmpty(reader["PayMessageContent"].ToString())
                    ? string.Empty
                    : reader["PayMessageContent"].ToString().Trim();

            //分库存ID
            if (fileds.ContainsKey("SpecificationID") && !Convert.IsDBNull(reader["SpecificationID"]))
            {
                int specificationid = 0;
                if (int.TryParse(reader["SpecificationID"].ToString(), out specificationid))
                    model.SpecificationID = specificationid;
            }
            // 备用字段1
            if (fileds.ContainsKey("Remark1") && !Convert.IsDBNull(reader["Remark1"]))
                model.Remark1 = string.IsNullOrEmpty(reader["Remark1"].ToString())
                    ? string.Empty
                    : reader["Remark1"].ToString().Trim();

            // 备用字段2
            if (fileds.ContainsKey("Remark2") && !Convert.IsDBNull(reader["Remark2"]))
                model.Remark2 = string.IsNullOrEmpty(reader["Remark2"].ToString())
                    ? string.Empty
                    : reader["Remark2"].ToString().Trim();

            // 备用字段3
            if (fileds.ContainsKey("Remark3") && !Convert.IsDBNull(reader["Remark3"]))
                model.Remark3 = string.IsNullOrEmpty(reader["Remark3"].ToString())
                    ? string.Empty
                    : reader["Remark3"].ToString().Trim();

            // 备用字段4
            if (fileds.ContainsKey("Remark4") && !Convert.IsDBNull(reader["Remark4"]))
                model.Remark4 = string.IsNullOrEmpty(reader["Remark4"].ToString())
                    ? string.Empty
                    : reader["Remark4"].ToString().Trim();

            // 备用字段5
            if (fileds.ContainsKey("Remark5") && !Convert.IsDBNull(reader["Remark5"]))
                model.Remark5 = string.IsNullOrEmpty(reader["Remark5"].ToString())
                    ? string.Empty
                    : reader["Remark5"].ToString().Trim();

            return model;
        }

        /// <summary>
        /// 根据条件，返回IList
        /// </summary>
        /// <param name="sqlWhere">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public IList<QuartzInAction.Model.OrderModel> GetEntities(string sql, string _conStr,
            params SqlParameter[] parameters)
        {
            SqlServerHelper.PopulateDelegate<QuartzInAction.Model.OrderModel> entities =
                new SqlServerHelper.PopulateDelegate<QuartzInAction.Model.OrderModel>(this.Populate_Orders);

            return SqlServerHelper.GetEntities<QuartzInAction.Model.OrderModel>(entities, sql, _conStr, parameters);
        }

        /// <summary>
        /// 根据用户Id,订单状态查询订单 
        /// </summary>
        /// <param name="memberId">用户Id</param>
        /// <param name="status">订单状态(字符串类型，可能为多状态，格式：1,2,3)  详细：QuartzInAction.Common.OrderStatus</param>
        /// <returns></returns>
        public System.Collections.Generic.IList<QuartzInAction.Model.OrderModel> GetEntities(string mId, string status,
            string _conStr)
        {
            string sql =
                " select * from [Orders] where MId = @MId and Status in (select col from SplitIn(@Status,',')) ";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter() {ParameterName = "@MId", SqlDbType = SqlDbType.VarChar, Size = 17, Value = mId},
                new SqlParameter() {ParameterName = "@Status", SqlDbType = SqlDbType.VarChar, Size = 15, Value = status}
            };
            return GetEntities(sql, _conStr, parameters);
        }

        /// <summary>
        /// 根据用户Id,活动编号,订单状态查询订单 
        /// 注：用户Id可为空。用户Id为空时，则根据活动编号,订单状态查询订单
        /// </summary>
        /// <param name="memberId">用户Id(注：可为空。用户Id为空时，则根据活动编号,订单状态查询订单 )</param>
        /// <param name="aNum">活动编号</param>
        /// <param name="status">订单状态(字符串类型，可能为多状态，格式：1,2,3)  详细：QuartzInAction.Common.OrderStatus</param>
        /// <returns></returns>
        public System.Collections.Generic.IList<QuartzInAction.Model.OrderModel> GetEntities(string mId, string aNum,
            string status, string _conStr)
        {
            string sql = string.Empty;
            SqlParameter[] parameters = null;
            if (!string.IsNullOrEmpty(mId))
            {
                sql =
                    " select * from [Orders] where MId = @MId and ANum = @ANum and Status in (select col from SplitIn(@Status,',')) ";
                parameters = new SqlParameter[]
                {
                    new SqlParameter() {ParameterName = "@MId", SqlDbType = SqlDbType.VarChar, Size = 17, Value = mId},
                    new SqlParameter() {ParameterName = "@ANum", SqlDbType = SqlDbType.VarChar, Size = 11, Value = aNum},
                    new SqlParameter()
                    {
                        ParameterName = "@Status",
                        SqlDbType = SqlDbType.VarChar,
                        Size = 15,
                        Value = status
                    }
                };
            }
            else
            {
                sql = " select * from [Orders] where ANum = @ANum and Status in (select col from SplitIn(@Status,',')) ";
                parameters = new SqlParameter[]
                {
                    new SqlParameter() {ParameterName = "@ANum", SqlDbType = SqlDbType.VarChar, Size = 11, Value = aNum},
                    new SqlParameter()
                    {
                        ParameterName = "@Status",
                        SqlDbType = SqlDbType.VarChar,
                        Size = 15,
                        Value = status
                    }
                };
            }

            return GetEntities(sql, _conStr, parameters);
        }

        /// <summary>
        /// 根据订单编号返回实体
        /// </summary>
        /// <param name="oNum">订单编号</param>
        /// <returns></returns>
        public QuartzInAction.Model.OrderModel GetEntity(string oNum, string _conStr)
        {
            string sql = " select * from Orders where ONum = @ONum ";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter() {ParameterName = "@ONum", SqlDbType = SqlDbType.VarChar, Size = 18, Value = oNum}
            };
            IList<QuartzInAction.Model.OrderModel> entities = GetEntities(sql, _conStr, parameters);
            if (entities.Count > 0)
            {
                return entities[0];
            }
            else
                return null;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string ONum, string _conStr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Orders ");
            strSql.Append(" where ONum=@ONum ");
            SqlParameter[] parameters =
            {
                new SqlParameter("@ONum", SqlDbType.Char, 50)
            };
            parameters[0].Value = ONum;

            int rows = SqlServerHelper.ExecuteNonQuery(strSql.ToString(), parameters, _conStr);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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
                SqlServerHelper.ExecuteNonQuery(String.Format(" DELETE FROM Orders WHERE {0}", sqlWhere), null, _conStr);
                b = true;
            }
            catch (Exception ex)
            {
                Common.Loger.WriteFile("DeleteEntity", ex.ToString());
            }
            return b;
        }

        public IList<OrderModel> GetEntities(string sqlwhere, string _conStr)
        {
            SqlServerHelper.PopulateDelegate<OrderModel> entities =
                new SqlServerHelper.PopulateDelegate<OrderModel>(this.Populate_Orders);
            string sql = string.Format("select * from Orders");
            if (sqlwhere.Trim() != "")
            {
                sql += string.Format(" where {0} ", sqlwhere);
            }

            return SqlServerHelper.GetEntities<OrderModel>(entities, sql, _conStr);
        }

        /// <summary>
        /// 根据条件，返回DataTable
        /// </summary>
        /// <param name="sqlWhere">sql条件</param>
        /// <returns>IList</returns>
        public DataTable GetDt(string sqlWhere, string _conStr)
        {
            string sql = string.Format("select * from [Orders] ");
            if (sqlWhere.Trim() != "")
                sql += string.Format(" where {0} ", sqlWhere);
            return SqlServerHelper.ExecuteDt(sql, _conStr);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(OrderModel model, string _conStr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Orders(");
            strSql.Append(
                "ONum,PNum,ANum,VerificationCode,Consignee,CPhone,DBNum,GroupId,AreaKey,Address,Remark,OrdersDt,PayDt,ShipDt,ToForecastDt,CourierCompanies,CourierNum,Status,Differentiate,Reason,DifferentiateDt,ReturnSubmitDt,ReturnEndDt,RNum,ReturnStatus,ReturnRemark,Ydifferentiate,Price,CarriagePrice,SubPrice,PayPrice,MId,Size,Color,PayResult,PayNo,Remark1,Remark2,Remark3,Remark4,Remark5,SpecificationID,PayMessageContent,MONum)");
            strSql.Append(" values (");
            strSql.Append(
                "@ONum,@PNum,@ANum,@VerificationCode,@Consignee,@CPhone,@DBNum,@GroupId,@AreaKey,@Address,@Remark,@OrdersDt,@PayDt,@ShipDt,@ToForecastDt,@CourierCompanies,@CourierNum,@Status,@Differentiate,@Reason,@DifferentiateDt,@ReturnSubmitDt,@ReturnEndDt,@RNum,@ReturnStatus,@ReturnRemark,@Ydifferentiate,@Price,@CarriagePrice,@SubPrice,@PayPrice,@MId,@Size,@Color,@PayResult,@PayNo,@Remark1,@Remark2,@Remark3,@Remark4,@Remark5,@SpecificationID,@PayMessageContent,@MONum)");
            SqlParameter[] parameters =
            {
                new SqlParameter("@ONum", SqlDbType.Char, 50),
                new SqlParameter("@PNum", SqlDbType.Char, 11),
                new SqlParameter("@ANum", SqlDbType.Char, 11),
                new SqlParameter("@VerificationCode", SqlDbType.Char, 60),
                new SqlParameter("@Consignee", SqlDbType.NVarChar, 50),
                new SqlParameter("@CPhone", SqlDbType.NVarChar, 24),
                new SqlParameter("@DBNum", SqlDbType.NVarChar, 200),
                new SqlParameter("@GroupId", SqlDbType.Int, 4),
                new SqlParameter("@AreaKey", SqlDbType.VarChar, 3),
                new SqlParameter("@Address", SqlDbType.NVarChar, 200),
                new SqlParameter("@Remark", SqlDbType.NVarChar, 500),
                new SqlParameter("@OrdersDt", SqlDbType.DateTime),
                new SqlParameter("@PayDt", SqlDbType.DateTime),
                new SqlParameter("@ShipDt", SqlDbType.DateTime),
                new SqlParameter("@ToForecastDt", SqlDbType.DateTime),
                new SqlParameter("@CourierCompanies", SqlDbType.NVarChar, 500),
                new SqlParameter("@CourierNum", SqlDbType.NVarChar, 500),
                new SqlParameter("@Status", SqlDbType.TinyInt, 1),
                new SqlParameter("@Differentiate", SqlDbType.TinyInt, 1),
                new SqlParameter("@Reason", SqlDbType.NVarChar, 500),
                new SqlParameter("@DifferentiateDt", SqlDbType.DateTime),
                new SqlParameter("@ReturnSubmitDt", SqlDbType.DateTime),
                new SqlParameter("@ReturnEndDt", SqlDbType.DateTime),
                new SqlParameter("@RNum", SqlDbType.Char, 10),
                new SqlParameter("@ReturnStatus", SqlDbType.Int, 4),
                new SqlParameter("@ReturnRemark", SqlDbType.NVarChar, 200),
                new SqlParameter("@Ydifferentiate", SqlDbType.TinyInt, 1),
                new SqlParameter("@Price", SqlDbType.Decimal, 9),
                new SqlParameter("@CarriagePrice", SqlDbType.Decimal, 9),
                new SqlParameter("@SubPrice", SqlDbType.Decimal, 9),
                new SqlParameter("@PayPrice", SqlDbType.Decimal, 9),
                new SqlParameter("@MId", SqlDbType.VarChar, 17),
                new SqlParameter("@Size", SqlDbType.VarChar, 50),
                new SqlParameter("@Color", SqlDbType.NVarChar, 50),
                new SqlParameter("@PayResult", SqlDbType.NVarChar, 1000),
                new SqlParameter("@PayNo", SqlDbType.VarChar, 50),
                new SqlParameter("@Remark1", SqlDbType.NVarChar, 200),
                new SqlParameter("@Remark2", SqlDbType.NVarChar, 200),
                new SqlParameter("@Remark3", SqlDbType.NVarChar, 200),
                new SqlParameter("@Remark4", SqlDbType.NVarChar, 200),
                new SqlParameter("@Remark5", SqlDbType.NVarChar, 200),
                new SqlParameter("@SpecificationID", SqlDbType.Int, 4),
                new SqlParameter("@PayMessageContent", SqlDbType.NVarChar, 250),
                new SqlParameter("@MONum", SqlDbType.VarChar, 100)
            };
            parameters[0].Value = model.ONum != null ? model.ONum : string.Empty;
            parameters[1].Value = model.PNum != null ? model.PNum : string.Empty;
            parameters[2].Value = model.ANum != null ? model.ANum : string.Empty;
            parameters[3].Value = model.VerificationCode != null ? model.VerificationCode : string.Empty;
            parameters[4].Value = model.Consignee != null ? model.Consignee : string.Empty;
            parameters[5].Value = model.CPhone != null ? model.CPhone : string.Empty;
            parameters[6].Value = model.DBNum != null ? model.DBNum : string.Empty;
            parameters[7].Value = model.GroupId;
            parameters[8].Value = model.AreaKey != null ? model.AreaKey : string.Empty;
            parameters[9].Value = model.Address != null ? model.Address : string.Empty;
            parameters[10].Value = model.Remark != null ? model.Remark : string.Empty;
            parameters[11].Value = QuartzInAction.Common.Utils.DateHasValue(model.OrdersDt);
            parameters[12].Value = QuartzInAction.Common.Utils.DateHasValue(model.PayDt);
            parameters[13].Value = QuartzInAction.Common.Utils.DateHasValue(model.ShipDt);
            //parameters[14].Value = QuartzInAction.Common.Utils.DateHasValue(model.ToForecastDt);
            parameters[15].Value = model.CourierCompanies != null ? model.CourierCompanies : string.Empty;
            parameters[16].Value = model.CourierNum != null ? model.CourierNum : string.Empty;
            parameters[17].Value = model.Status;
            parameters[18].Value = model.Differentiate;
            parameters[19].Value = model.Reason != null ? model.Reason : string.Empty;
            parameters[20].Value = QuartzInAction.Common.Utils.DateHasValue(model.DifferentiateDt);
            //parameters[21].Value = QuartzInAction.Common.Utils.DateHasValue(model.ReturnSubmitDt);
            parameters[22].Value = QuartzInAction.Common.Utils.DateHasValue(model.ReturnEndDt);
            parameters[23].Value = model.RNum != null ? model.RNum : string.Empty;
            // parameters[24].Value = model.ReturnStatus;
            parameters[25].Value = model.ReturnRemark != null ? model.ReturnRemark : string.Empty;
            // parameters[26].Value = model.Ydifferentiate;
            parameters[27].Value = model.Price;
            parameters[28].Value = QuartzInAction.Common.Utils.DecimalHasValue(model.CarriagePrice);
            //parameters[29].Value = QuartzInAction.Common.Utils.DecimalHasValue(model.SubPrice);
            parameters[30].Value = model.PayPrice;
            parameters[31].Value = model.MId != null ? model.MId : string.Empty;
            //parameters[32].Value = model.Size != null ? model.Size : string.Empty;
            //parameters[33].Value = model.Color != null ? model.Color : string.Empty;
            parameters[34].Value = model.PayResult != null ? model.PayResult : string.Empty;
            parameters[35].Value = model.PayNo != null ? model.PayNo : string.Empty;
            parameters[36].Value = model.Remark1 != null ? model.Remark1 : string.Empty;
            parameters[37].Value = model.Remark2 != null ? model.Remark2 : string.Empty;
            parameters[38].Value = model.Remark3 != null ? model.Remark3 : string.Empty;
            parameters[39].Value = model.Remark4 != null ? model.Remark4 : string.Empty;
            parameters[40].Value = model.Remark5 != null ? model.Remark5 : string.Empty;
            parameters[41].Value = model.SpecificationID;
            parameters[42].Value = model.PayMessageContent != null ? model.PayMessageContent : string.Empty;
            //parameters[43].Value = model.MONum != null ? model.MONum : string.Empty;

            int rows = SqlServerHelper.ExecuteNonQuery(strSql.ToString(), parameters, _conStr);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable GetOrderSummary(string anum, string _conStr)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter() {ParameterName = "@ANum", SqlDbType = SqlDbType.VarChar, Size = 18, Value = anum}
            };
            return SqlServerHelper.GetDataTableByStoredProcedure("GetOrderSummary", parameters, _conStr);
        }
    }
}