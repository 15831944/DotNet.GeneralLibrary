using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DotNet_DataConversion
{
    /// <summary>
    /// 全局路径--
    /// 获取系统绝对路径、相对路径--
    /// 2018.10.15
    /// </summary>
    public static class GPath
    {
        /// <summary>
        /// 逻辑桌面并非物理文件系统桌面
        /// </summary>
        public static string Desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        /// <summary>
        /// 该程序集运行目录（相对路径）
        /// </summary>
        public static string CurrentDllRun = new DirectoryInfo(typeof(GPath).Assembly.Location).Parent.FullName;
    }
}
