using DotNet_DataConversion;
using DotNet_DataConversion.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class StudyXml
    {
        public static void Main1()
        {
            var xmlTest = new XMLTest() { ID = 10000, Name = "蒋勇", Age = 24 };

            var xml = XmlHelper.EntityToXML(xmlTest);

            var xmlSavePath = Path.Combine(GPath.Desktop, "xml1.xml");

            using (var wr = new StreamWriter(xmlSavePath))
            {
                wr.Write(xml);
            }

            var read = File.ReadAllText(xmlSavePath);

            Console.WriteLine(read);

            Console.ReadKey();
        }
        public static void Main2()
        {
            var jsonTest = new JsonTest() { ID = 20000, Name = "何忠浩", Age = 25, Address = "湖北省嘉鱼县何家畈", Sex = '男' };
            var jsonTest2 = new JsonTest() { ID = 20000, Name = "何忠浩1", Age = 25, Address = "湖北省嘉鱼县何家畈", Sex = '1' };
            var jsonTest3 = new JsonTest() { ID = 20000, Name = "何忠浩2", Age = 25, Address = "湖北省嘉鱼县何家畈", Sex = '2' };

            var mts = new List<JsonTest>() { jsonTest, jsonTest2, jsonTest3 };

            var xml = XmlHelper.EntityListToXML(mts);

            var xmlSavePath = Path.Combine(GPath.Desktop, "xml2.xml");

            using (var wr = new StreamWriter(xmlSavePath))
            {
                wr.Write(xml);
            }

            var read = File.ReadAllText(xmlSavePath);

            Console.WriteLine(read);

            Console.ReadKey();
        }
    }
}
