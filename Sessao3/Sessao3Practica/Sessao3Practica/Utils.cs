using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Sessao3Practica
{
    public class Helpers
    {
        public static void ProcessFiles(DirectoryInfo rootFolder, Func<FileInfo, bool> pred, Action<FileInfo> action)
        {
            var rootFiles = rootFolder.GetFiles();

            foreach (var rootFile in rootFiles)
            {
                if (pred(rootFile))
                {
                    action(rootFile);
                }
            }

            var directories = rootFolder.GetDirectories();

            foreach (var directoryInfo in directories)
            {
                ProcessFiles(directoryInfo, pred, action);
            }
        }
    }
}