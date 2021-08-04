using System.Collections.Generic;

namespace SearchEngine
{
    public interface IFileReader
    {
        Dictionary<string, string> ReadingFiles(string folderName);
        void ListFilesForFolder(string path);
    }
}