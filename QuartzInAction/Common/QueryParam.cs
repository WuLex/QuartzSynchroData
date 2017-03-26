using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuartzInAction.Common
{
    /// <summary>
    /// 分页存储过程查询参数类
    /// </summary>
    public class QueryParam
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryParam()
            : this(1, int.MaxValue)
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_mPageIndex">当前页码</param>
        /// <param name="_mPageSize">每页记录数</param>
        public QueryParam(int _mPageIndex, int _mPageSize)
        {
            _PageIndex = _mPageIndex;
            _PageSize = _mPageSize;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_mOrderType">排序类型 1:降序 其它为升序</param>
        /// <param name="_mPageIndex">当前页码</param>
        /// <param name="_mPageSize">每页记录数</param>
        public QueryParam(int _mOrderType, int _mPageIndex, int _mPageSize)
        {
            _OrderType = _mOrderType;
            _PageIndex = _mPageIndex;
            _PageSize = _mPageSize;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_mWhere">查询条件 需带Where</param>
        /// <param name="_mOrderType">排序类型 1:降序 其它为升序</param>
        /// <param name="_mPageIndex">当前页码</param>
        /// <param name="_mPageSize">每页记录数</param>
        public QueryParam(string _mWhere, int _mOrderType,
            int _mPageIndex, int _mPageSize)
        {
            _Where = _mWhere;
            _OrderType = _mOrderType;
            _PageIndex = _mPageIndex;
            _PageSize = _mPageSize;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_mWhere">查询条件 需带Where</param>
        /// <param name="_mOrderfld">排序字段</param>
        /// <param name="_mOrderType">排序类型 1:降序 其它为升序</param>
        /// <param name="_mPageIndex">当前页码</param>
        /// <param name="_mPageSize">每页记录数</param>
        public QueryParam(string _mWhere, string _mOrderfld, int _mOrderType,
            int _mPageIndex, int _mPageSize)
        {
            _Where = _mWhere;
            _Orderfld = _mOrderfld;
            _OrderType = _mOrderType;
            _PageIndex = _mPageIndex;
            _PageSize = _mPageSize;
        }



        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_mTableName">表名</param>
        /// <param name="_mReturnFields">返回字段</param>
        /// <param name="_mWhere">查询条件 需带Where</param>
        /// <param name="_mOrderfld">排序字段</param>
        /// <param name="_mOrderType">排序类型 1:降序 其它为升序</param>
        /// <param name="_mPageIndex">当前页码</param>
        /// <param name="_mPageSize">每页记录数</param>
        public QueryParam(string _mTableName, string _mReturnFields,
            string _mWhere, string _mOrderfld,
            int _mOrderType, int _mPageIndex, int _mPageSize)
        {
            _TableName = _mTableName;
            _ReturnFields = _mReturnFields;
            _Where = _mWhere;
            _Orderfld = _mOrderfld;
            _OrderType = _mOrderType;
            _PageIndex = _mPageIndex;
            _PageSize = _mPageSize;
        }



        #region "Private Variables"
        private string _TableName;
        private string _ReturnFields;
        private string _Where;
        private string _Orderfld;
        private int _OrderType = 1;
        private int _PageIndex = 1;
        private int _PageSize = int.MaxValue;
        #endregion

        #region "Public Variables"

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                _TableName = value;
            }

        }



        /// <summary>
        /// 返回字段
        /// </summary>
        public string ReturnFields
        {
            get
            {
                return _ReturnFields;
            }
            set
            {
                _ReturnFields = value;
            }
        }




        /// <summary>
        /// 查询条件 需带Where
        /// </summary>
        public string Where
        {
            get
            {
                return _Where;
            }
            set
            {
                _Where = value;
            }
        }





        /// <summary>
        /// 排序字段
        /// </summary>
        public string Orderfld
        {
            get
            {
                return _Orderfld;
            }
            set
            {
                _Orderfld = value;
            }
        }


        /// <summary>
        /// 排序类型 1:降序 其它为升序
        /// </summary>
        public int OrderType
        {
            get
            {
                return _OrderType;
            }
            set
            {
                _OrderType = value;
            }
        }


        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex
        {
            get
            {
                return _PageIndex;
            }
            set
            {
                _PageIndex = value;
            }

        }


        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = value;
            }
        }
        #endregion
    }
}
