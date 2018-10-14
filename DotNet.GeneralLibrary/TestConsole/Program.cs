using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using DotNet_DataConversion.Xml;
using DotNet_DataConversion;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] pars)
        {
            Console.WriteLine("你好，世界!");
            StudyJosn.Main4();
            //StudyXml.Main2();
        }
    }
    class MyTest
    {

    }
    class XMLTest : MyTest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
    class JsonTest : MyTest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public char Sex { get; set; }
    }
}