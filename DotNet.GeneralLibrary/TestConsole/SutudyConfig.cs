using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using DotNet_DataConversion;

namespace TestConsole
{
    class SutudyConfig
    {
        public static void Main1()
        {

            var jsonPath = ConfigHelper.GetAppConfig("jsonPath");

            ConfigHelper.UpdateAppConfig("savePath", Path.Combine(GPath.Desktop, "test.txt"));

            var testConnectString = ConfigHelper.GetConnectionStringConfig("ad");

            ConfigHelper.UpdateConnectionStringsConfig("a", "b", "c");

            var s = ConfigHelper.GetConfiguration();

            var configPath = Path.Combine(GPath.Desktop, "test.config");

            var assginConfig = ConfigHelper.GetConfiguration(configPath);

            var values = assginConfig.AppSettings.Settings["k"].Value;
        }
    }
}
