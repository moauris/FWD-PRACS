using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LINQtoXMLpractice
{
    public class Convert_OSInfo
    {
        public static void Run()
        {
            XElement originalXML = XElement.Load("tbl_osinfo.xml");
            Console.WriteLine(originalXML.ToString());
            /*
            foreach (XElement child in originalXML.Elements())
            {
                //Got the OSInfo entry
                foreach (XElement chil in child.Elements())
                {
                    Console.WriteLine($"{chil.Name} = {chil.Value}");


                }
            }
            */
            XElement structuredXML = new XElement("osList");
            //生成子节点叫做 osInfo

            IEnumerable<string> osNameQuery = //不可以使用 XElement 作为 T，否则distinct 将无效化。
                (
                    from element in originalXML.Elements().Elements()
                    where element.Name == "os_name"
                    select element.Value
                 ).Distinct();
            // XElement 属于 reference type，因此虽然值一样，instance不一样时，不算distinct。
            // String 属于 value type，因此只要值一样，就算相等。

            /* 
<tbl_osinfo>
    < ID > 1 </ ID >
    < os_name > Amazon Linux </ os_name >
    < os_version > 2015.09 or later</ os_version >
    < os_architecture > 64 - bit </ os_architecture >
</ tbl_osinfo >
            */
            foreach (string item in osNameQuery)
            {
                XElement osinfoNode = new XElement("osInfo");
                osinfoNode.Add(new XAttribute("name", item));
                IEnumerable<string> osVersionQuery =
                (
                    from element in originalXML.Elements()
                    where element.Element("os_name").Value == item
                    select element.Element("os_version").Value
                 ).Distinct();
                foreach (string version in osVersionQuery)
                {
                    XElement osversionNode = new XElement("osVersion");
                    osversionNode.Add(new XAttribute("name", version));
                    IEnumerable<string> osArchitectureQuery =
                (
                    from element in originalXML.Elements()
                    where element.Element("os_name").Value == item
                    where element.Element("os_version").Value == version
                    select element.Element("os_architecture").Value
                 ).Distinct();
                    foreach (string arch in osArchitectureQuery)
                    {
                        XElement osarch = new XElement("osArchitecture");
                        osarch.Add(new XAttribute("name", arch));
                        osversionNode.Add(osarch);
                    }

                    osinfoNode.Add(osversionNode);

                }
                structuredXML.Add(osinfoNode);
            }

            Console.WriteLine(structuredXML.ToString());

            structuredXML.Save("structured_osinfo.xml");

        }

    }
}
