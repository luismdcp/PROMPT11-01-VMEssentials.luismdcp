using System;
using System.IO;

namespace Sessao3Practica
{
    class Program
    {
        static TimeSpan intervalo = new TimeSpan(5, 0, 0, 0);
        private static long size = 5;

        static void Main(string[] args)
        {
            Func<FileInfo, bool> pred = new Func<FileInfo, bool>(PredicateImplementation);
            Action<FileInfo> action = new Action<FileInfo>(ActionImplementation);
            DirectoryInfo di = new DirectoryInfo(@"c:\Intel");

            Helpers.ProcessFiles(di, pred, action);
            Console.ReadLine();
        }

        public static bool PredicateImplementation(FileInfo fi)
        {
            return ((DateTime.Now - fi.LastWriteTime) < intervalo) && (fi.Length > size);
        }

        public static void ActionImplementation(FileInfo fi)
        {
            Console.WriteLine(String.Format("Pathname: {0} | LastChange: {1} | Size: {2}", fi.FullName, fi.LastWriteTime.ToShortDateString(), fi.Length));
        }
    }
}