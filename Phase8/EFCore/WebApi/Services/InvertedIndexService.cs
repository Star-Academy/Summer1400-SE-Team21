using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SearchEngine;

namespace WebApi.Services
{
    public class InvertedIndexService : IInvertedIndexService
    {
        private InvertedIndex _invertedIndex;

        public InvertedIndexService()
        {
            var builder = new DbContextOptionsBuilder<InvertedIndexContext>();
            var connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
            if (connectionString != null)
                builder.UseSqlServer(connectionString);
            else
                builder.UseInMemoryDatabase("InvertedIndex");
            var context = new InvertedIndexContext(builder.Options);
            _invertedIndex = new InvertedIndex(context,new Tokenizer());
        }
        public List<string> Query(string query)
        {
            var userInput = new UserInput(query);
            return _invertedIndex.Query(userInput).ToList();
        }

        public void AddDocument(string name, string content)
        {
            _invertedIndex.AddDocument(name,content);
        }

        public void AddDocument(Dictionary<string, string> fileContents)
        {
            _invertedIndex.AddDocuments(fileContents);
        }
    }
}