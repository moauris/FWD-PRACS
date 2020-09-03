using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace LINQtoXMLpractice
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunLinqExample.Run();
            Convert_OSInfo.Run();

            //XELement 与 string 的对等性检验
            /*
            XElement xml = new XElement("Persons");

            XElement p1 = new XElement("Person", new XElement("name", "Momi"));
            XElement p2 = new XElement("Person", new XElement("name", "Momi"));
            

            

            Console.WriteLine("检验p1是否等于p2:");
            Console.WriteLine(p1 == p2);

            xml.Add(p1);
            xml.Add(p1);
            Console.WriteLine(xml);
            Console.WriteLine("现在xml对象第一、第二节点都是p1, 检验节点1是否等于节点2:");
            Console.WriteLine(xml.Elements().ToArray()[0] == xml.Elements().ToArray()[1]);
            */

        }

    }

}
