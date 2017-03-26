using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuartzInAction.Model
{
   [Serializable]
   public class DBConfigModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { set;  get; }
        /// <summary>
        /// 数据库编号
        /// </summary>
        public string DBNum { set; get; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DBName { set; get; }
        /// <summary>
        /// 抽取订单间隔
        /// </summary>
        public int GetOrderDT { set; get; }
        /// <summary>
        /// 数据库有效无效
        /// </summary>
        public int? Valid { set; get; }
        /// <summary>
        /// 辅助字段1
        /// </summary>
        public string Remark1 { set; get; }
        /// <summary>
        /// 辅助字段2
        /// </summary>
        public string Remark2 { set; get; }
        /// <summary>
        /// 辅助字段3
        /// </summary>
        public string Remark3 { set; get; }
        /// <summary>
        /// 辅助字段4
        /// </summary>
        public string Remark4 { set; get; }
        /// <summary>
        /// 辅助字段5
        /// </summary>
        public string Remark5 { set; get; }
    }
}
