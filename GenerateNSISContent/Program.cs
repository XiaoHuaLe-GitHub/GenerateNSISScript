using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GenerateNSISContent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("请输入路径：");
            string baseDir = Console.ReadLine();
            if (Directory.Exists(baseDir))
            {
                var allDirs = Directory.GetDirectories(baseDir).ToList();

                foreach (var dir in allDirs)
                {
                    _GenerateDirContent(baseDir, dir);
                }
                __WriteDirNSISContent(baseDir, baseDir);
            }
            else
            {
                Console.WriteLine("路径不存在，已返回");
            }

            Console.ReadLine();
        }

        private static void _GenerateDirContent(string baseDirName,string dirName)
        {
            var _allDirNames = Directory.GetDirectories(dirName).ToList();
            if (_allDirNames.Count<=0)
            {
               __WriteDirNSISContent(baseDirName, dirName);
            }
            else
            {
                foreach (var _dirName in _allDirNames)
                {
                    _GenerateDirContent(baseDirName, _dirName);
                }

                __WriteDirNSISContent(baseDirName, dirName);
            }
        }

        private static void __WriteDirNSISContent(string baseDirName, string dirName)
        {
            StringBuilder stringBuilder = new StringBuilder("SetOutPath \"" + dirName.Replace(baseDirName, "$INSTDIR") + "\"\n");

            var allFiles = Directory.GetFiles(dirName, "*.*");
            foreach (var files in allFiles)
            {
                stringBuilder.AppendLine("File \"" + files + "\"");
            }

            if (allFiles.Length > 0)
            {
                Console.Write(stringBuilder.ToString());
                File.AppendAllText("NSIS.txt", stringBuilder.ToString());
            }
        }
    }
}
