using System.Collections.Generic;

namespace WebApi.Services
{
    public interface IInvertedIndexService
    {
        List<string> Query(string query);
        void AddDocument(string name, string content);
        void AddDocument(Dictionary<string, string> fileContents);
    }
}