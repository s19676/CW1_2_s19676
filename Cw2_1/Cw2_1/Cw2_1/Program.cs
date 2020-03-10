using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Cw2
{
    class Program
    {
        public static void ErrorLogging(Exception ex)
        {
            string logpath = @"C:\Users\s19676\Desktop\log.txt";
            if (!File.Exists(logpath))
            {
                File.Create(logpath).Dispose();
            }

            StreamWriter sw = File.AppendText(logpath);
            sw.WriteLine("START" + DateTime.Now);
            sw.WriteLine("Error Message" + ex.Message);
            sw.WriteLine("END" + DateTime.Now);
            sw.Close();

        }
        static void Main(string[] args)
        {
            try
            {
                string path = Console.ReadLine();//C:\Users\s19676\Desktop\dane.csv
                string xmlpath = Console.ReadLine();//C:\Users\s19676\Desktop\
                string format = Console.ReadLine();//xml 

                if (File.Exists(path) && Directory.Exists(xmlpath))
                {

                    string[] source = File.ReadAllLines("path");
                    XElement xml = new XElement("Root",
                        from str in source
                        let fields = str.Split(',')
                        select new XElement("Student",
                            new XAttribute("Student_IndexNumber", fields[4]),
                            new XElement("fname", fields[0]),
                            new XElement("lname", fields[1]),
                            new XElement("birthdate", fields[5]),
                            new XElement("e-mails", fields[6]),
                            new XElement("mothername", fields[7]),
                            new XElement("fathername", fields[8]),
                            new XElement("studies",
                                new XElement("name", fields[2]),
                                new XElement("mode", fields[3])


                            )
                        )
                    );
                    xml.Save(String.Concat(xmlpath + "result.xml"));
                }
                else
                {
                    if (!File.Exists(path))
                    {
                        throw new Exception("Filde does not exist");
                    }
                    if (!Directory.Exists(xmlpath))
                    {
                        throw new Exception("Directory does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogging(ex);
            }
        }
    }
}
