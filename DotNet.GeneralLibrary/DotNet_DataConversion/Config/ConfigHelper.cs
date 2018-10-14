using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace DotNet_DataConversion
{
    /// <summary>
    /// 配置文件帮助类--
    /// 读取当前应用程序配置文件，读取指定路径下配置文件--
    /// 2018.10.15
    /// </summary>
    public static class ConfigHelper
    {
        ///<summary> 
        ///返回*.exe.config文件中appSettings配置节的value项  
        ///</summary> 
        ///<param name="strKey"></param> 
        ///<returns></returns> 
        public static string GetAppConfig(string strKey)
        {
            string file = System.Windows.Forms.Application.ExecutablePath;

            var config = ConfigurationManager.OpenExeConfiguration(file);

            var s = config.FilePath;

            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                if (key == strKey)
                {
                    return config.AppSettings.Settings[strKey].Value.ToString();
                }
            }
            return null;
        }

        ///<summary>  
        ///在*.exe.config文件中appSettings配置节增加一对键值对  
        ///</summary>  
        ///<param name="newKey"></param>  
        ///<param name="newValue"></param>  
        public static void UpdateAppConfig(string newKey, string newValue)
        {
            string file = System.Windows.Forms.Application.ExecutablePath;

            var config = ConfigurationManager.OpenExeConfiguration(file);

            bool exist = false;

            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                if (key == newKey)
                {
                    exist = true;
                }
            }

            if (exist)
            {
                config.AppSettings.Settings.Remove(newKey);
            }

            config.AppSettings.Settings.Add(newKey, newValue);

            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// 依据连接串名字connectionName返回数据连接字符串  
        /// </summary>
        /// <param name="connectionName">连接串名称</param>
        /// <returns></returns>
        public static string GetConnectionStringConfig(string connectionName)
        {
            var file = Application.ExecutablePath;

            var config = ConfigurationManager.OpenExeConfiguration(file);

            var connectionString = config.ConnectionStrings.ConnectionStrings[connectionName].ConnectionString.ToString();

            return connectionString;
        }

        /// <summary>
        /// 更新连接字符串（磁盘value值不变）
        /// </summary>
        /// <param name="newName">连接字符串名称</param>
        /// <param name="newConfigString">连接字符串内容</param>
        /// <param name="newProviderName">数据提供程序名称</param>
        public static void UpdateConnectionStringsConfig(string newName, string newConfigString, string newProviderName)
        {
            //指定config文件渎职

            var file = Application.ExecutablePath;

            var config = ConfigurationManager.OpenExeConfiguration(file);

            //记录该连接字符串是否已经存在

            var exist = false;

            //如果要更改的连接字符串已经存在

            if (config.ConnectionStrings.ConnectionStrings[newName] != null)
            {
                exist = true;
            }

            //如果连接串已存在 首先删除它

            if (exist)
            {
                config.ConnectionStrings.ConnectionStrings.Remove(newName);
            }

            //新建一个连接字符串实例

            var mySetting = new ConnectionStringSettings(newName, newConfigString, newProviderName);

            //将新的连接串添加到配置文件中

            config.ConnectionStrings.ConnectionStrings.Add(mySetting);

            //保存对配置文件所做的更改

            config.Save(ConfigurationSaveMode.Modified);

            //强制重新载入配置文件的ConnectionStrings配置节

            ConfigurationManager.RefreshSection("connectionStrings");
        }

        /// <summary>
        /// 打开默认的配置文件；
        /// </summary>
        public static Configuration GetConfiguration()
        {
            string configFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Connection.config");

            return GetConfiguration(configFile);
        }

        /// <summary>
        /// 打开指定的配置文件；
        /// </summary>
        public static Configuration GetConfiguration(string configFile)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();

            fileMap.ExeConfigFilename = configFile;

            return ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }
    }
}
