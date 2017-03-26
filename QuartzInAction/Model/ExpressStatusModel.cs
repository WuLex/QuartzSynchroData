using System;
using System.ComponentModel;

namespace QuartzInAction.Model
{
    /// <summary>
    /// 快递跟踪状态
    /// </summary>
	[Serializable]
	public class ExpressStatusModel
	{
        /// <summary>
		/// 
		/// </summary>
        [DisplayName("Id")]
        public int Id { get; set; }
		
        /// <summary>
        /// 轮询状态
		/// </summary>
        [DisplayName("轮询状态")]
        public string Status { get; set; }
		
        /// <summary>
        /// 轮询状态相关消息 如:3天查无结果，60天无变化
		/// </summary>
        [DisplayName("轮询状态相关消息")]
        public string OMessage { get; set; }
		
        /// <summary>
        /// 快递单当前状态
		/// </summary>
        [DisplayName("快递单当前状态")]
        public string State { get; set; }
		
        /// <summary>
        /// 是否签收标记
		/// </summary>
        [DisplayName("是否签收标记")]
        public string Ischeck { get; set; }
		
        /// <summary>
        /// 快递公司编码,一律用小写字母
		/// </summary>
        [DisplayName("快递公司编码")]
        public string LCom { get; set; }
		
        /// <summary>
        /// 快递单号
		/// </summary>
        [DisplayName("快递单号")]
        public string Nu { get; set; }
		 
	}
}

