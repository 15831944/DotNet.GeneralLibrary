using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DotNet_DataConversion
{
    /// <summary>
    /// 字符串帮助类--
    /// 操作字符串--
    /// 2018.10.14
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 任意字符串以UTF8格式输出
        /// </summary>
        /// <param name="value">初始字符串</param>
        /// <returns></returns>
        public static string ToUTF8(this string contents)
        {
            var byteArray = System.Text.Encoding.Default.GetBytes(contents);

            contents = Encoding.UTF8.GetString(byteArray);

            return contents;
        }
    }
}
