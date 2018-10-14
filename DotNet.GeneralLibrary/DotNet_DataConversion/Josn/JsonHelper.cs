using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DotNet_DataConversion
{
    /// <summary>
    /// json帮助类
    /// 常用解析json、生成json对象
    /// 2018.10.14
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// json字符串生成匿名对象
        /// </summary>
        /// <param name="jsonDatas">json字符串</param>
        /// <returns></returns>
        public static dynamic DynamicObject(this string jsonDatas)
        {
            return JsonConvert.DeserializeObject<dynamic>(jsonDatas);
        }

        public readonly static string DataPath;

        static JsonHelper()
        {
            var workPath = new DirectoryInfo(Path.GetDirectoryName(typeof(JsonHelper).Assembly.Location));

            DataPath = Path.Combine(workPath.FullName, "Data");
        }

        public static TModel Read<TModel>() where TModel : new()
        {
            var name = string.Format("{0}-{1}", typeof(TModel).Assembly.Modules.First().MetadataToken, typeof(TModel).MetadataToken);

            var file = Path.Combine(DataPath, string.Format("{0}.json", name));

            if (!File.Exists(file))
            {
                return default(TModel);
            }

            try
            {
                var json = File.ReadAllText(file);

                return Newtonsoft.Json.JsonConvert.DeserializeObject<TModel>(json);
            }
            catch (Exception ex)
            {
                return default(TModel);
            }
        }

        public static bool Save<TModel>(TModel model) where TModel : new()
        {
            if (!Directory.Exists(DataPath))
            {
                Directory.CreateDirectory(DataPath);
            }

            var name = string.Format("{0}-{1}", typeof(TModel).Assembly.Modules.First().MetadataToken
                , typeof(TModel).MetadataToken);

            var file = Path.Combine(DataPath, string.Format("{0}.json", name));

            if (model == null)
            {
                if (File.Exists(file))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                return true;
            }

            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                File.WriteAllText(file, json);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

