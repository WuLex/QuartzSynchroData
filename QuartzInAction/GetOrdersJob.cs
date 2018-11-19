using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Quartz;
using QuartzInAction.Common;
using QuartzInAction.DBUtility;
using QuartzInAction.Model;
using QuartzInAction.SqlServerDAL;
using QuartzInAction.DAL;


namespace QuartzInAction
{
    public class GetOrdersJob : IJob
    {
        #region IJob 成员

        public void Execute(IJobExecutionContext context)
        {
            //数据中心所在数据库
            string allOrderConStr = ConfigurationManager.ConnectionStrings["WuConstr"].ToString();

            Loger.WriteFile(DateTime.Now + ":开始执行数据处理!");

            #region

            IList<DBConfigModel> dbConfigLists = new DBConfigDAL().GetEntities(" Valid=1 ", allOrderConStr);
            if (dbConfigLists.Count > 0)
            {
                foreach (DBConfigModel dbConfigModel in dbConfigLists)
                {
                    //数据库名称
                    string dbName = dbConfigModel.DBName;
                    //编号
                    string dbNum = dbConfigModel.DBNum;
                    //抽取订单间隔
                    int GetOrderdt = dbConfigModel.GetOrderDT;
                    //判断是否阿里数据库服务器
                    string serverFlag = dbConfigModel.Remark1;

                    string conStr = string.Empty;
                    if (!string.IsNullOrEmpty(serverFlag) && serverFlag.Equals("1"))
                    {
                        conStr =string.Format("Data Source=XXXXX;Initial Catalog={0};Persist Security Info=True;User ID=db1;Password=11111;",dbName);
                    }
                    else if (!string.IsNullOrEmpty(serverFlag) && serverFlag.Equals("2"))
                    {
                        conStr =string.Format("Data Source=XXXXX,3433;Initial Catalog={0};Persist Security Info=True;User ID=db2;Password=11111;",dbName);
                    }
                    else
                    {
                        conStr =string.Format("server=.;User ID=sa;Password=1111111;database={0};Connection Reset=true;Pooling=False;",dbName);
                    }
                    //根据dbNum删除本地对应的订单
                    //根据conStr，GetOrderdt,获取对应数据库,范围时间内的订单表，录入本地数据库
                    try
                    {
                        #region 订单批处理

                        string ordersql = string.Format(" dbNum={0} and DATEDIFF(day,OrdersDt,getDate()) < {1} ", dbNum, GetOrderdt);
                        bool flag = new OrderDAL().DeleteEntity(ordersql, allOrderConStr); //删除数据中心这一批的订单
                        if (flag)
                        {
                            ordersql = string.Format(" DATEDIFF(day,OrdersDt,getDate()) < {0} ", GetOrderdt);
                            IList<OrderModel> orderList = new OrderDAL().GetEntities(ordersql, conStr);
                            //获取各服务器相同条件下的一批订单，因为订单数据(如 订单状态，收货人电话)是不断变化，只能不断轮询拉取，更新到数据中心
                            Loger.WriteFile(conStr + "-----订单数:" + orderList.Count);
                            for (int i = 0; i < orderList.Count; i++)
                            {
                                var mOrders = new OrderModel();

                                mOrders.ANum = orderList[i].ANum;
                                mOrders.Address = orderList[i].Address;
                                mOrders.AreaKey = orderList[i].AreaKey;
                                mOrders.DBNum = dbNum; //区分订单来自哪个服务器，读取DBConfig表里服务器编号字段
                                mOrders.CarriagePrice = orderList[i].CarriagePrice;
                                mOrders.Consignee = orderList[i].Consignee;
                                mOrders.CourierCompanies = orderList[i].CourierCompanies;
                                mOrders.CourierNum = orderList[i].CourierNum;
                                mOrders.CPhone = orderList[i].CPhone;
                                mOrders.Differentiate = orderList[i].Differentiate;
                                mOrders.DifferentiateDt = orderList[i].DifferentiateDt;
                                mOrders.GroupId = orderList[i].GroupId;
                                mOrders.MId = orderList[i].MId;
                                mOrders.ONum = orderList[i].ONum; //订单号
                                mOrders.OrdersDt = orderList[i].OrdersDt;
                                mOrders.PayDt = orderList[i].PayDt;
                                mOrders.PayMessageContent = orderList[i].PayMessageContent;
                                mOrders.PayNo = orderList[i].PayNo;
                                mOrders.PayPrice = orderList[i].PayPrice;
                                mOrders.PayResult = orderList[i].PayResult;
                                mOrders.PNum = orderList[i].PNum;
                                mOrders.Price = orderList[i].Price;
                                mOrders.Reason = orderList[i].Reason;
                                mOrders.Remark = orderList[i].Remark;

                                mOrders.ReturnEndDt = orderList[i].ReturnEndDt;
                                mOrders.ReturnRemark = orderList[i].ReturnRemark;
                                mOrders.RNum = orderList[i].RNum;
                                mOrders.ShipDt = orderList[i].ShipDt;
                                mOrders.SpecificationID = orderList[i].SpecificationID;
                                mOrders.Status = orderList[i].Status;
                                mOrders.VerificationCode = orderList[i].VerificationCode;
                                mOrders.Remark1 = orderList[i].Remark1;
                                mOrders.Remark2 = orderList[i].Remark2;
                                mOrders.Remark3 = orderList[i].Remark3;
                                mOrders.Remark4 = orderList[i].Remark4;
                                mOrders.Remark5 = orderList[i].Remark5;


                                bool insertFlag = new OrderDAL().Add(mOrders, allOrderConStr); //录入数据中心
                                string courierNum = mOrders.CourierNum;
                                //主----从表2个，
                                //订单对应的快递状态从表,有正确单号就处理从表，默认单号或者非法单号就不处理从表
                                //订单对应的具体物流信息从表
                                if (insertFlag && !string.IsNullOrEmpty(courierNum) && !courierNum.Equals("11111111111") &&
                                    !courierNum.Equals("22222222111"))
                                {
                                    #region 物流批处理

                                    string delStatusSql = string.Format(" nu='{0}' ", courierNum);
                                    //根据courierNum号删除本地快递状态
                                    bool del = new ExpressStatusDAL().DeleteEntity(delStatusSql, allOrderConStr);
                                    //获取快递状态 order by id 
                                    string getStatusSql =
                                        string.Format("select * from ExpressStatus where nu='{0}' order by id  ",
                                            courierNum);
                                    DataTable ExpressStatusDt = SqlServerHelper.ExecuteDt(getStatusSql, conStr);
                                    ExpressStatusDt.Columns.Add("dbNum", typeof(string));
                                    foreach (DataRow dr in ExpressStatusDt.Rows)
                                    {
                                        dr["dbNum"] = dbNum;
                                    }

                                    //批量插入快递状态表---汇总
                                    SqlServerHelper.ExecuteInsert(ExpressStatusDt, "ExpressStatus", allOrderConStr);

                                    IList<ExpressStatusModel> deliveryList =
                                        ExpressStatusDt.ToLists<ExpressStatusModel>();
                                    int count = deliveryList.Count;
                                    string delInfoSql = string.Empty;
                                    if (count > 0)
                                    {
                                        var sArr = new int[count];
                                        for (int m = 0; m < count; m++)
                                        {
                                            sArr[m] = deliveryList[m].Id;
                                        }
                                        string condition = String.Join(",", sArr);
                                        delInfoSql = string.Format(" DId in ({0}) and dbNum='{1}' ", condition,
                                            dbNum);

                                        //根据Did号删除本地物流信息
                                        del = new LogisticsDAL().DeleteEntity(delInfoSql, allOrderConStr);
                                    }
                                    else
                                    {
                                        delInfoSql = string.Format(" DId in (null)");
                                    }

                                    //根据快递单号得到物流信息
                                    string getInfoSql =
                                        string.Format(
                                            "select *  from [LData] where DId = (select max(Id) as Id from ExpressStatus where Nu ='{0}' ) order by LDTime desc ",
                                            courierNum);
                                    DataTable LDataDt = SqlServerHelper.ExecuteDt(getInfoSql, conStr);
                                    LDataDt.Columns.Add("dbNum", typeof(string));
                                    foreach (DataRow dr in LDataDt.Rows)
                                    {
                                        dr["dbNum"] = dbNum;
                                    }
                                    //批量插入物流信息---汇总
                                    SqlServerHelper.ExecuteInsert(LDataDt, "LData", allOrderConStr);

                                    #endregion
                                }
                            }
                        }

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Loger.WriteFile(conStr + "发送信息异常," + "原因:" + ex.Message);
                        string EmailTitle = "订单汇总ERROR";
                        string EmailBody = conStr + "异常原因:" + ex.Message;
                        //new SendMailHelper().SendMail("827937686@qq.com", "Wu", null, EmailTitle, EmailBody);
                    }
                }
            }

            #endregion

            Loger.WriteFile(DateTime.Now + ":完成数据处理(单次间隔)!");
        }

        #endregion
    }
}