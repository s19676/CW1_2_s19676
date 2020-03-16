using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
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

        public static void ErrorLog(string err)
        {
            String logpath = @"C:\Users\s19676\Desktop\log.txt";
            if (!File.Exists(logpath))
            {
                File.Create(logpath).Dispose();
            }

            StreamWriter sw = File.AppendText(logpath);
            sw.WriteLine("Start " + DateTime.Now);
            sw.WriteLine(err);
            sw.Close();
        }

        static void Main(string[] args)
        {
            try
            {
                string path = @"C:\Users\s19676\Desktop\dane.csv";
                string xmlpath = @"C:\Users\s19676\Desktop\";
                string format = Console.ReadLine(); //xml 

                var sizeNewArray = 0;
                var indexes = new List<int>();


                if (!File.Exists(path) || !Directory.Exists(xmlpath))
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
                else
                {
                    string[] so = File.ReadAllLines(path);

                    for (var i = 0; i < so.Length; i++)
                    {
                        string[] student = so[i].Split(',');
                        if (student[0].Length == 0 ||
                            student[1].Length == 0 ||
                            student[2].Length == 0 ||
                            student[3].Length == 0 ||
                            student[4].Length == 0 ||
                            student[5].Length == 0 ||
                            student[6].Length == 0 ||
                            student[7].Length == 0 ||
                            student[8].Length == 0)
                        {
                            sizeNewArray++;
                            indexes.Add(i);
                            ErrorLog("Błąd zapisu: " + so[i]);

                        }

                    }

                    List<string> list = new List<string>();
                    var indeks = 0;

                    for (var i = 0; i < so.Length; i++)
                    {
                        if (i == indexes[indeks])
                        {
                            if (indeks >= indexes.Capacity - 1)
                                continue;
                            indeks++;
                        }
                        else
                        {
                            list.Add(so[i]);
                        }
                    }

                    List<string> uniqList = list.Distinct().ToList();
                    if (uniqList != list)
                    {
                        ErrorLog("Usunięto duplikaty!");
                    }
                    XElement xml = new XElement("Root",
                        from str in so
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
            }
            catch (Exception ex)
            {
                ErrorLogging(ex);
            }
        }
    }
}
