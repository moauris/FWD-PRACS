using System;
using System.Xml.Xsl;

namespace XsltPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load("Data/Source1Transform.xslt");
            transform.Transform("Data/Source1.xml", "Data/Source1Output.html");
            Console.WriteLine("Program ended, exiting.");
        }
    }
}
