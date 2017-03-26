using System;
using System.Web;
using System.Text.RegularExpressions;

namespace QuartzInAction.Common
{
    public class Utils
    {
        #region 获取时间戳
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>  
        /// 获取当前时间戳  
        /// </summary>  
        /// <param name="bflag">为真时获取10位时间戳,为假时获取13位时间戳.</param>  
        /// <returns></returns>  
        public static string GetTimeStamp(bool bflag = true)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string ret = string.Empty;
            if (bflag)
                ret = Convert.ToInt64(ts.TotalSeconds).ToString();
            else
                ret = Convert.ToInt64(ts.TotalMilliseconds).ToString();

            return ret;
        }  
        #endregion

        #region 返回json格式数据
        /// <summary>
        /// 返回json格式数据
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string JsonFormat(object o)
        {
            string str = string.Empty;
            if (o == null) str = "{\"d\": \"" + string.Empty + "\"}";
            else
            {
                if (o is int || o is decimal || o is double)
                    str = "{\"d\": " + o + "}";
                //如果不单独判断，返回的形式是 {"d": True } 或 {"d": False }, js会报转换错误
                else if (o is bool)
                {
                    bool b = (bool)o;
                    if (b)
                        str = "{\"d\": true }";
                    else
                        str = "{\"d\": false }";
                }
                else
                    str = "{\"d\": \"" + o + "\"}";
            }
            return str;
        }
        #endregion

        #region 判断数据库字段赋值是否存在值
        /// <summary>
        /// 判断日期是否存在值
        /// </summary>
        public static object DateHasValue(DateTime? temp)
        {
            if (temp.HasValue)
                return temp;
            else
                return DBNull.Value;
        }
        /// <summary>
        /// 判断Int是否存在值
        /// </summary>
        public static object IntHasValue(int? temp)
        {
            if (temp.HasValue)
                return temp;
            else
                return DBNull.Value;
        }

        /// <summary>
        /// 判断Decimal是否存在值
        /// </summary>
        public static object DecimalHasValue(Decimal? temp)
        {
            if (temp.HasValue)
                return temp;
            else
                return DBNull.Value;
        }

        #endregion

        #region 获取appSettings里的值
        /// <summary>
        /// 获取appSettings里的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetAppSettings(string key)
        {
            if (System.Configuration.ConfigurationManager.AppSettings[key] == null)
                return string.Empty;
            else
                return System.Configuration.ConfigurationManager.AppSettings[key].ToString();
        }
        #endregion

    }
}
