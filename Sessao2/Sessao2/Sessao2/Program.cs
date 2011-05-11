using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Sessao2
{
    internal class Comparador : IEqualityComparer<object>
    {
        public bool Equals(object x, object y)
        {
            DirectoryInfo di1 = x as DirectoryInfo;
            DirectoryInfo di2 = x as DirectoryInfo;
            
            if (di1 != null && di2 != null)
            {
                return di1.FullName == di2.FullName;
            }
            else
            {
                return x.Equals(y);
            }
        }

        public int GetHashCode(object obj)
        {
            DirectoryInfo di = obj as DirectoryInfo;

            if (di != null)
            {
                return di.FullName.GetHashCode();
            }
            else
            {
                return obj.GetHashCode();
            }
        }
    }

    class Program
    {
        static Dictionary<object, string> dictionary = new Dictionary<object, string>(new Comparador());
        
        static void Main(string[] args)
        {
            var di = new DirectoryInfo(@"c:\program files");
            
            GetPropertiesHtmLv3(di);
            Console.ReadLine();
        }

        static void GetProperties(object obj)
        {
            Type t = obj.GetType();

            foreach (var prop in t.GetProperties())
            {
                Console.WriteLine(String.Format("Nome: {0} - Valor: {1}", prop.Name, prop.GetValue(obj, null)));
            }
        }

        static string GetPropertiesHTML(object obj)
        {
            Type t = obj.GetType();
            string fullPath = Environment.CurrentDirectory + "\\" + Guid.NewGuid() + ".html";
            StreamWriter writer = new StreamWriter(fullPath);
            
            writer.WriteLine("<html>");
            writer.WriteLine("<head>");
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
            writer.WriteLine("<table border=\"1\">");
            writer.WriteLine("<th>Nome</th>");
            writer.WriteLine("<th>Valor</th>");
            
            foreach (var prop in t.GetProperties())
            {
                writer.WriteLine("<tr>");
                writer.Write("<td>");
                writer.Write(prop.Name);
                writer.Write("</td>");
                writer.Write("<td>");
                writer.Write(prop.GetValue(obj, null));
                writer.WriteLine("</td>");
            }

            writer.WriteLine("</table>");
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
            writer.Close();

            return fullPath;
        }

        static string GetPropertiesHtmLv3(object obj)
        {
            if (obj != null)
            {
                Type t = obj.GetType();
                string fullPath = Environment.CurrentDirectory + "\\temp\\" + Guid.NewGuid() + ".html";
                StreamWriter writer = new StreamWriter(fullPath);

                writer.WriteLine("<html>");
                writer.WriteLine("<head>");
                writer.WriteLine("</head>");
                writer.WriteLine("<body>");
                writer.WriteLine("<table border=\"1\">");
                writer.WriteLine("<th>Nome</th>");
                writer.WriteLine("<th>Valor</th>");

                foreach (var prop in t.GetProperties())
                {
                    writer.WriteLine("<tr>");
                    writer.Write("<td>");
                    writer.Write(prop.Name);
                    writer.Write("</td>");
                    writer.Write("<td>");

                    if (prop.PropertyType.IsPrimitive || prop.PropertyType == typeof(String) || prop.PropertyType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Count() == 0)
                    {
                        writer.Write(prop.GetValue(obj, null).ToString());
                    }
                    else
                    {
                        if (prop.Name != "Root" && prop.Name != "Parent" && prop.Name != "Date" && prop.Name != "Now" && prop.Name != "UtcNow" && prop.Name != "TimeOfDay" && prop.Name != "Today")
                        {
                            if (prop.GetValue(obj, null) != null)
                            {
                                if (!dictionary.ContainsKey(prop.GetValue(obj, null)))
                                {
                                    string filePath = GetPropertiesHtmLv3(prop.GetValue(obj, null));
                                    dictionary.Add(prop.GetValue(obj, null), filePath);
                                    writer.Write("<a href=\"");
                                    writer.Write(filePath);
                                    writer.Write("\">");
                                    writer.Write(prop.GetValue(obj, null).ToString());
                                    writer.WriteLine("</a>");
                                }
                                else
                                {
                                    writer.Write("<a href=\"");
                                    writer.Write(dictionary[prop.GetValue(obj, null)]);
                                    writer.Write("\">");
                                    writer.Write(prop.GetValue(obj, null).ToString());
                                    writer.WriteLine("</a>");
                                }   
                            }
                        }
                    }

                    writer.WriteLine("</td>");
                }

                writer.WriteLine("</table>");
                writer.WriteLine("</body>");
                writer.WriteLine("</html>");
                writer.Close();
                return fullPath; 
            }

            return String.Empty;
        }
    }
}