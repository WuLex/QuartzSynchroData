using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace QuartzInAction.Common
{
    /// <summary>
    /// 手动记录错误日志，不用Log4Net组件
    /// </summary>
    public class Loger
    {
        /// <summary>
        ///  将日志写入指定的文件
        /// </summary>
        /// <param name="Path">文件路径，如果没有该文件，刚创建</param>
        /// <param name="type">日志类型</param>
        /// <param name="content">日志内容</param>
        public static void WriteFile(string type, string content)
        {
            string Path = AppDomain.CurrentDomain.BaseDirectory + "Log";
            if (!Directory.Exists(Path))
            {
                //若文件目录不存在 则创建
                Directory.CreateDirectory(Path);
            }
            Path += "\\" + DateTime.Now.ToString("yyMMdd") + type + ".log";
            if (!File.Exists(Path))
            {
                File.Create(Path).Close();
            }
            StreamWriter writer = new StreamWriter(Path, true, Encoding.GetEncoding("gb2312"));
            writer.WriteLine("时间：" + DateTime.Now.ToString());
            writer.WriteLine("日志信息：" + content);
            writer.WriteLine("-----------------------------------------------------------");
            writer.Close();
            writer.Dispose();
        }


        /// <summary>
        ///  将日志写入指定的文件
        /// </summary>
        /// <param name="Path">文件路径，如果没有该文件，刚创建</param>
        /// <param name="type">日志类型</param>
        /// <param name="content">日志内容</param>
        public static void WriteFile(string content)
        {
            string Path = AppDomain.CurrentDomain.BaseDirectory + "Log";
            if (!Directory.Exists(Path))
            {
                //若文件目录不存在 则创建
                Directory.CreateDirectory(Path);
            }
            Path += "\\" + DateTime.Now.ToString("yyMMdd") + ".log";
            if (!File.Exists(Path))
            {
                File.Create(Path).Close();
            }
            StreamWriter writer = new StreamWriter(Path, true, Encoding.GetEncoding("gb2312"));
            writer.WriteLine("时间：" + DateTime.Now.ToString());
            writer.WriteLine("日志信息：" + content);
            writer.WriteLine("-----------------------------------------------------------");
            writer.Close();
            writer.Dispose();
        }

        /// <summary>
        ///  将日志写入指定的文件
        /// </summary>
        /// <param name="Path">文件路径，如果没有该文件，刚创建</param>
        /// <param name="content">日志内容</param>
        public static void WriteFile(int content)
        {
            string Path = AppDomain.CurrentDomain.BaseDirectory + "Log";
            if (!Directory.Exists(Path))
            {
                //若文件目录不存在 则创建
                Directory.CreateDirectory(Path);
            }
            Path += "\\" + DateTime.Now.ToString("yyMMdd") + ".log";
            if (!File.Exists(Path))
            {
                File.Create(Path).Close();
            }
            StreamWriter writer = new StreamWriter(Path, true, Encoding.GetEncoding("gb2312"));
            writer.WriteLine("时间：" + DateTime.Now.ToString());
            writer.WriteLine("日志信息：" + content);
            writer.WriteLine("-----------------------------------------------------------");
            writer.Close();
            writer.Dispose();
        }


        /// <summary>
        ///  将日志写入指定的文件
        /// </summary>
        /// <param name="erroMsg">错误详细信息</param>
        /// <param name="source">源位置</param>
        /// <param name="fileName">文件名</param>
        public static void WriteFile(string erroMsg, string source, string stackTrace, string fileName)
        {
            string Path = AppDomain.CurrentDomain.BaseDirectory + "Log";
            if (!Directory.Exists(Path))
            {
                //若文件目录不存在 则创建
                Directory.CreateDirectory(Path);
            }
            Path += "\\" + DateTime.Now.ToString("yyMMdd") + ".log";
            if (!File.Exists(Path))
            {
                File.Create(Path).Close();
            }
            StreamWriter writer = new StreamWriter(Path, true, Encoding.GetEncoding("gb2312"));
            writer.WriteLine("时间：" + DateTime.Now.ToString());
            writer.WriteLine("文件：" + fileName);
            writer.WriteLine("源：" + source);
            writer.WriteLine("错误信息：" + erroMsg);
            writer.WriteLine("-----------------------------------------------------------");
            writer.Close();
            writer.Dispose();
        }
    }
}