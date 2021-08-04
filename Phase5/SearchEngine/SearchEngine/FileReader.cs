using System;
using System.Collections.Generic;
using System.IO;

namespace SearchEngine
{
    public class FileReader : IFileReader
    {
        private Dictionary<string, string> _fileNameToItsStuffs = new Dictionary<string, string>();

        public Dictionary<string, string> ReadingFiles(string folderName)
        {
            ListFilesForFolder(folderName);
            return _fileNameToItsStuffs;
        }

        public void ListFilesForFolder(string path)
        {
            try
            {
                foreach (string file in Directory.EnumerateFileSystemEntries(path))
                {
                    FileAttributes attr = File.GetAttributes(file);

                    if (attr.HasFlag(FileAttributes.Directory))
                        ListFilesForFolder(file);
                    else
                    {
                        string[] splited = file.Split('\\');
                        string fileName = splited[splited.Length - 1];
                        string[] lines = File.ReadAllLines(file);
                        string stuffs = "";
                        foreach (string line in lines)
                            stuffs += line + " ";
                        stuffs = stuffs.Trim();
                        _fileNameToItsStuffs.Add(fileName, stuffs);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Path is not valid");
            }
        }
        
        
    }
}