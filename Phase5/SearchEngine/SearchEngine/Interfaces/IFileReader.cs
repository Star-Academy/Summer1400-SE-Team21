using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface IFileReader
    {
        Dictionary<string, string> ReadingFiles(string folderName);
        void ListFilesForFolder(string path);
    }
}