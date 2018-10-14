using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DotNet_DataConversion
{
    /// <summary>
    /// XML帮助类--
    /// 操作Xml--
    /// 2018.10.14
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// 2.8.0 XML字符串转为实体对象
        /// </summary>
        /// <typeparam name="T">实体对象类型</typeparam>
        /// <param name="xmlcontent">Xml字符</param>
        /// <returns></returns>
        public static T XMLToEntity<T>(String xmlcontent)
        {
            if (string.IsNullOrEmpty(xmlcontent) || string.IsNullOrEmpty(xmlcontent.Trim()))
            {
                return default(T);
            }
            Type t = typeof(T);
            //创建一个实体对象
            T model = (T)Activator.CreateInstance(t);
            //读取xml
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlcontent);//加载xml
            XmlNodeList xxList = xml.GetElementsByTagName(t.Name); //取得节点名为row的XmlNode集合
            string xmlvalue = String.Empty;
            foreach (XmlNode xxNode in xxList)
            {
                XmlNodeList childList = xxNode.ChildNodes; //取得row下的子节点集合
                foreach (XmlNode xmlson in childList)
                {
                    PropertyInfo property = t.GetProperty(xmlson.Name);
                    if (!property.CanWrite)
                    {
                        continue;
                    }
                    xmlvalue = xmlson.InnerText.Replace("&amp;", "&").Replace("&lt;", "<");
                    Object value = Convert.ChangeType(xmlvalue, property.PropertyType);
                    property.SetValue(model, value, null);

                }
            }
            return model;
        }

        /// <summary>
        /// 2.9.0  XML文件转为实体集合 
        /// </summary>
        /// <typeparam name="T">实体对象类型</typeparam>
        /// <param name="content">xml字符</param>
        public static List<T> XMLToEntityList<T>(String content)
        {
            Type t = typeof(T);
            List<T> list = new List<T>();
            //读取xml
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(content);//加载xml
            XmlNodeList xxList = xml.GetElementsByTagName(t.Name); //取得节点名为row的XmlNode集合
            String xmlvalue = String.Empty;
            foreach (XmlNode xxNode in xxList)
            {
                XmlNodeList childList = xxNode.ChildNodes; //取得row下的子节点集合
                //创建一个实体对象
                T model = (T)Activator.CreateInstance(t);
                foreach (XmlNode xmlson in childList)
                {
                    PropertyInfo property = t.GetProperty(xmlson.Name);
                    xmlvalue = xmlson.InnerText.Replace("&amp;", "&").Replace("&lt;", "<");
                    Object value = Convert.ChangeType(xmlvalue, property.PropertyType);
                    property.SetValue(model, value, null);
                }
                list.Add(model);
            }
            return list;

        }

        /// <summary>
        /// 2.10.0 检查该字符串是否为空 (包含去空格检查)
        /// </summary>
        public static bool IsNullOrEmpty(String content)
        {
            if (String.IsNullOrEmpty(content))
            {
                return true;
            }
            if (string.IsNullOrEmpty(content.Trim()))
            {
                return true;
            }
            return false;
        }


        //-----------------------------对象处理------------------------------------//


        /// <summary>
        /// 3.3.0 实体转为XML文件
        /// </summary>
        /// <typeparam name="T">实体对象类型</typeparam>
        /// <param name="model">实体</param>
        public static string EntityToXML<T>(T model)
        {
            Type t = typeof(T);
            PropertyInfo[] propertys = t.GetProperties();

            StringBuilder sb = new StringBuilder(1000);
            //文件头
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            //根节点
            sb.AppendLine("<" + t.Name + ">");
            foreach (PropertyInfo item in propertys)
            {
                if (!item.CanRead)
                {
                    continue;
                }
                if (!item.CanWrite)
                {
                    continue;
                }
                Object val = item.GetValue(model, null);
                if (val == null)
                {
                    continue;
                }
                sb.Append("  <" + item.Name + ">");
                sb.Append(val.ToString().Replace("&", "&amp;").Replace("<", "&lt;"));
                sb.AppendLine("</" + item.Name + ">");
            }
            sb.AppendLine("</" + t.Name + ">");
            return sb.ToString();
        }

        /// <summary>
        /// 3.3.0 实体集合转为XML文件
        /// </summary>
        /// <typeparam name="T">实体对象类型</typeparam>
        /// <param name="models">实体对象集合</param>
        public static string EntityListToXML<T>(List<T> models)
        {
            Type t = typeof(T);
            PropertyInfo[] proparrey = t.GetProperties();

            StringBuilder sb = new StringBuilder(1000);
            //文件头
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            //根节点
            sb.AppendLine("<" + t.Name + "s>");
            foreach (T item in models)
            {
                sb.AppendLine("  <" + t.Name + ">");
                foreach (var property in proparrey)
                {
                    sb.Append("    <" + property.Name + ">");
                    sb.Append(property.GetValue(item, null).ToString().Replace("&", "&amp;").Replace("<", "&lt;"));
                    sb.AppendLine("</" + property.Name + ">");
                }
                sb.AppendLine("  </" + t.Name + ">");
            }
            sb.AppendLine("</" + t.Name + "s>");
            return sb.ToString();
        }
    }
}
