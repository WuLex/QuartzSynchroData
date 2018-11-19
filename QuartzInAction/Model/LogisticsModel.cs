using System;
using System.ComponentModel;


namespace QuartzInAction.Model
{
    /// <summary>
    /// 快递100接口的返回数据
    /// </summary>
    [Serializable]
    public class LogisticsModel
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        /// <summary>
        /// 快递公司送货状态表Id
        /// </summary>
        [DisplayName("快递公司送货状态表Id")]
        public int? DId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DisplayName("内容")]
        public string Context { get; set; }

        /// <summary>
        /// 时间，原始格式
        /// </summary>
        [DisplayName("时间")]
        public DateTime LDTime { get; set; }

        /// <summary>
        /// 格式化后时间
        /// </summary>
        [DisplayName("格式化后时间")]
        public string LDFTime { get; set; }

        /// <summary>
        /// 本数据元对应的签收状态
        /// </summary>
        [DisplayName("本数据元对应的签收状态")]
        public string Status { get; set; }

        /// <summary>
        /// 本数据元对应的行政区域的编码
        /// </summary>
        [DisplayName("本数据元对应的行政区域的编码")]
        public string AreaCode { get; set; }

        ///<summary>
        /// 本数据元对应的行政区域的名称
        /// </summary>
        [DisplayName("本数据元对应的行政区域的名称")]
        public string AreaName { get; set; }
    }
}