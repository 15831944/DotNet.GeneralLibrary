using DotNet_DataConversion;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    static class StudyJosn
    {
        public static void Main1()
        {
            //1~测试json

            var writeJson = "{\"City\":\"上海\",\"ID\":\"45\"}";

            var dyObject = JsonConvert.DeserializeObject<dynamic>(writeJson);

            System.Console.WriteLine(dyObject.City);

            System.Console.WriteLine(dyObject.ID);

            var outClass = new OutClass();

            var jTest = JsonConvert.DeserializeObject<JosnTest>(writeJson);

            var njTest = JsonConvert.DeserializeObject<NewJsonTest>(writeJson);

            outClass.JosnTest = JsonConvert.DeserializeObject<JosnTest>(writeJson);

            outClass.NewJsonTest = JsonConvert.DeserializeObject<NewJsonTest>(writeJson);

            System.Console.ReadKey();
        }
        public static void Main2()
        {
            //单个json解析
            string jsonText = "{\"Zone\":\"海淀\",\"Zone_en\":\"haidian\"}";

            var jo = JsonConvert.DeserializeObject<JObject>(jsonText);

            string zone = jo["海淀"].ToString();

            string zone_en = jo["haidian"].ToString();

            //嵌套json解析

            string data = "{\"data\":[{\"totol\":\"100\", \"Time\":\"2018-03-01\"}]}";

            var q = JsonConvert.DeserializeObject<Test>(data);

            //数组json解析
        }
        public static void Main3()
        {
            string jsonText = " {\"companyID\":\"15\",\"employees\":[{\"firstName\":\"Bill\",\"lastName\":\"Gates\"},{\"firstName\":\"George\",\"lastName\":\"Bush\"}],\"manager\":[{\"salary\":\"6000\",\"age\":\"23\"},{\"salary\":\"8000\",\"age\":\"26\"}]} ";

            Console.WriteLine(jsonText);

            RootObject rb = JsonConvert.DeserializeObject<RootObject>(jsonText);

            Console.WriteLine(rb.companyID);

            Console.WriteLine(rb.employees[0].firstName);

            foreach (Manager ep in rb.manager)
            {
                Console.WriteLine(ep.age);
            }

            Console.ReadKey();
        }
        public static void Main4()
        {
            var rpath = Path.Combine(new DirectoryInfo(typeof(StudyJosn).Assembly.Location).Parent.FullName, "a.json");

            var result = File.ReadAllText(rpath,Encoding.Default);

            var jp = JsonConvert.DeserializeObject<JsonParser>(result);//result为上面的Json数据

            var list = jp.data;

            list = jp.data;

            var sb = new StringBuilder();

            foreach (Data data in list)
            {
                sb.Append(data.time + "\t");

                sb.Append(data.context + "\r\n\r\n");

                Console.WriteLine(sb.ToString());
            }
        }

    }

    public class JsonParser
    {
        public int status;
        public int errCode;
        public string message;
        public string html;
        public int mailTo;
        public string tel;
        public string expTextName;
        public IList<Data> data;
    }

    public class Data
    {
        public string time;
        public string context;
    }


    public class Employees
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class Manager
    {
        public string salary { get; set; }
        public string age { get; set; }
    }
    public class RootObject
    {
        public string companyID { get; set; }
        public List<Employees> employees { get; set; }
        public List<Manager> manager { get; set; }
    }


    public class Test
    {
        public string code { get; set; }
        public string message { get; set; }
        public IList<data> data { get; set; }
    }

    public class data
    {
        public string totol { get; set; }
        public string Time { get; set; }
    }

    class JObject
    {
        private string _zone { get; set; }

        private string _zone_en { get; set; }

        public string Zone
        {
            get
            {
                return _zone;
            }
            set
            {
                _zone = value;
            }
        }
        public string Zone_en
        {
            get
            {
                return _zone_en;
            }
            set
            {
                _zone_en = value;
            }
        }

        public string this[string str]
        {
            get
            {
                if (str.Equals(_zone))
                {
                    return _zone;
                }
                if (str.Equals(_zone_en))
                {
                    return _zone_en;
                }

                return string.Empty;
            }
        }
    }
    class JosnTest
    {
        public string City { get; set; } = "简易";
    }
    class NewJsonTest
    {
        public int ID { get; set; } = 21;
    }
    class OutClass
    {
        public JosnTest JosnTest { get; set; }
        public NewJsonTest NewJsonTest { get; set; }
    }
}
