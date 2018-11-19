using System;
using System.ComponentModel;


namespace QuartzInAction.Model
{
    /// <summary>
    /// 订单实体类
    /// </summary>
    [Serializable]
    public class OrderModel
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        [DisplayName("订单编号")]
        public string ONum { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        [DisplayName("商品编号")]
        public string PNum { get; set; }

        /// <summary>
        /// 活动编号
        /// </summary>
        [DisplayName("活动编号")]
        public string ANum { get; set; }

        /// <summary>
        /// 数据库编号（数据中心的数据来源哪个服务器数据库编号）
        /// </summary>
        public string DBNum { set; get; }

        /// <summary>
        /// 验证码
        /// </summary>
        [DisplayName("验证码")]
        public string VerificationCode { get; set; }

        /// <summary>
        /// 收货人信息（一般指姓名）
        /// </summary>
        [DisplayName("收货人信息")]
        public string Consignee { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        [DisplayName("收货人电话")]
        public string CPhone { get; set; }


        /// <summary>
        /// 运费组Id
        /// </summary>
        [DisplayName("运费组Id")]
        public int GroupId { get; set; }

        /// <summary>
        /// 地区key
        /// </summary>
        [DisplayName("地区key")]
        public String AreaKey { get; set; }

        /// <summary>
        /// 收货人地址
        /// </summary>
        [DisplayName("收货人地址")]
        public string Address { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [DisplayName("说明")]
        public string Remark { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        [DisplayName("下单时间")]
        public DateTime? OrdersDt { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        [DisplayName("支付时间")]
        public DateTime? PayDt { get; set; }

        /// <summary>
        /// 发货时间
        /// </summary>
        [DisplayName("发货时间")]
        public DateTime? ShipDt { get; set; }

        /// <summary>
        /// 快递公司编码
        /// </summary>
        [DisplayName("快递公司编码")]
        public string CourierCompanies { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        [DisplayName("快递单号")]
        public string CourierNum { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [DisplayName("订单状态")]
        public int Status { get; set; }

        /// <summary>
        /// 撤单区分
        /// </summary>
        [DisplayName("撤单区分")]
        public int Differentiate { get; set; }

        /// <summary>
        /// 撤单理由
        /// </summary>
        [DisplayName("撤单理由")]
        public string Reason { get; set; }

        /// <summary>
        /// 撤单时间
        /// </summary>
        [DisplayName("撤单时间")]
        public DateTime? DifferentiateDt { get; set; }


        /// <summary>
        /// 退货时间
        /// </summary>
        [DisplayName("退货时间")]
        public DateTime? ReturnEndDt { get; set; }

        /// <summary>
        /// 退货订单号
        /// </summary>
        [DisplayName("退货订单号")]
        public string RNum { get; set; }

        /// <summary>
        /// 退货说明
        /// </summary>
        [DisplayName("退货说明")]
        public string ReturnRemark { get; set; }

        /// <summary>
        /// 活动价格
        /// </summary>
        [DisplayName("活动价格")]
        public decimal? Price { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        [DisplayName("运费")]
        public decimal? CarriagePrice { get; set; }

        /// <summary>
        /// 实际支付金额
        /// </summary>
        [DisplayName("实际支付金额")]
        public decimal PayPrice { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [DisplayName("用户Id")]
        public string MId { get; set; }

        /// <summary>
        /// 支付结果
        /// </summary>
        [DisplayName("支付结果")]
        public string PayResult { get; set; }

        /// <summary>
        /// 支付编号
        /// </summary>
        [DisplayName("支付编号")]
        public string PayNo { get; set; }

        /// <summary>
        /// 分库存Id
        /// </summary>
        [DisplayName("分库存Id")]
        public int SpecificationID { get; set; }

        /// <summary>
        /// 支付成功后用户的短信通知内容
        /// </summary>
        [DisplayName("支付成功后用户的短信通知内容")]
        public string PayMessageContent { get; set; }

        /// <summary>
        /// 备用字段1
        /// </summary>
        [DisplayName("备用字段1")]
        public string Remark1 { get; set; }

        /// <summary>
        /// 备用字段2
        /// </summary>
        [DisplayName("备用字段2")]
        public string Remark2 { get; set; }

        /// <summary>
        /// 备用字段3
        /// </summary>
        [DisplayName("备用字段3")]
        public string Remark3 { get; set; }

        /// <summary>
        /// 备用字段4
        /// </summary>
        [DisplayName("备用字段4")]
        public string Remark4 { get; set; }

        /// <summary>
        /// 备用字段5
        /// </summary>
        [DisplayName("备用字段5")]
        public string Remark5 { get; set; }
    }
}