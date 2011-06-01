using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections;

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
                if (x is DateTime && y is DateTime)
                {
                    DateTime xDT = (DateTime) x;
                    DateTime yDT = (DateTime) y;

                    return xDT.Day == yDT.Day && xDT.Month == yDT.Month && xDT.Year == yDT.Year;
                }
                else
                {
                    return x.Equals(y);
                }
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
                if (obj is DateTime)
                {
                    DateTime objDt = (DateTime) obj;
                    return String.Format("{0}{1}{2}", objDt.Day, objDt.Month, objDt.Year).GetHashCode();
                }
                else
                {
                    return obj.GetHashCode();   
                }
            }
        }
    }

    class Program
    {
        static Dictionary<object, string> dictionary = new Dictionary<object, string>(new Comparador());
        static Dictionary<Type, IPropertiesResolver> typePropertiesMapper = new Dictionary<Type, IPropertiesResolver>();
        
        static void Main(string[] args)
        {
            DirectoryInfo di = new DirectoryInfo(@"c:\program files");
            typePropertiesMapper.Add(typeof(DirectoryInfo), new DirectoryInfoResolver(di));
            
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

                //if (typePropertiesMapper.ContainsKey(t))
                //{
                //    foreach (KeyValuePair<string, object> propMap in typePropertiesMapper[t].GetPropertiesMap())
                //    {
                //        writer.WriteLine("<tr>");
                //        writer.Write("<td>");
                //        writer.Write(propMap.Key);
                //        writer.Write("</td>");
                //        writer.Write("<td>");

                //        if (propMap.Value.GetType().IsPrimitive || propMap.Value.GetType() == typeof(String) || propMap.Value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Count() == 0)
                //        {
                //            writer.Write(propMap.Value.ToString());
                //        }
                //        else
                //        {
                //            if (propMap.Value != null)
                //            {

                //                if (!dictionary.ContainsKey(propMap.Value))
                //                {
                //                    string filePath = GetPropertiesHtmLv3(propMap.Value);

                //                    writer.Write("<a href=\"");
                //                    writer.Write(filePath);
                //                    writer.Write("\">");
                //                    writer.Write(propMap.Value.ToString());
                //                    writer.WriteLine("</a>");
                //                }
                //                else
                //                {
                //                    writer.Write("<a href=\"");
                //                    writer.Write(dictionary[propMap.Value]);
                //                    writer.Write("\">");
                //                    writer.Write(propMap.Value.ToString());
                //                    writer.WriteLine("</a>");
                //                }
                //            }
                //        }

                //        writer.WriteLine("</td>");
                //    }
                //}


                if (typeof(IEnumerable).IsAssignableFrom(obj.GetType()))
                {
                    var enumerator = ((IEnumerable) obj).GetEnumerator();

                    while (enumerator.MoveNext())
                    {
                        var currentElement = enumerator.Current;

                        if (!dictionary.ContainsKey(currentElement))
                        {
                            string filePath = GetPropertiesHtmLv3(currentElement);

                            writer.Write("<a href=\"");
                            writer.Write(filePath);
                            writer.Write("\">");
                            writer.Write(currentElement.ToString());
                            writer.WriteLine("</a>");
                            writer.WriteLine("</br>");
                        }
                        else
                        {
                            writer.Write("<a href=\"");
                            writer.Write(dictionary[currentElement]);
                            writer.Write("\">");
                            writer.Write(currentElement.ToString());
                            writer.WriteLine("</a>");
                            writer.WriteLine("</br>");
                        }
                    }
                }

                dictionary.Add(obj, fullPath);

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
                        if (prop.GetValue(obj, null) != null)
                        {

                            if (!dictionary.ContainsKey(prop.GetValue(obj, null)))
                            {
                                string filePath = GetPropertiesHtmLv3(prop.GetValue(obj, null));

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