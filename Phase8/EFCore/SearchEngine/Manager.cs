using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using SearchEngine.Interfaces;

namespace SearchEngine
{
    public class Manager
    {
        public void Run(string[] args)
        {
            DbContextOptionsBuilder<InvertedIndexContext> builder = new();
            var connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
            if (connectionString != null)
            {
                builder.UseSqlServer(connectionString);
            }
            else
            {
                Console.WriteLine("using In Memory");
                builder.UseInMemoryDatabase("InvertedIndex");
            }

            var database = new InvertedIndexContext(builder.Options);
            if(args.Length > 0)
            {
                Index(args[0], database);
            }
            Search(database);
        }

        private void Search(IDatabaseMap<string,string> database)
        {
            var invertedIndex = new InvertedIndex(database);
            var searchEngine = new SearchEngine(new ConsoleReader(), new ConsoleWriter(), invertedIndex);
            Console.WriteLine("start engine");
            searchEngine.Run();
            Console.WriteLine("end engine");
        }

        private void Index(string filePath,IDatabaseMap<string,string> database)
        {
            var invertedIndex = new InvertedIndex(database);
            Console.WriteLine("add files");
            invertedIndex.TokenizeFiles(new FileReader().ReadingFiles(filePath));
            Console.WriteLine("finish adding file");
        }
    }
    
    class ConsoleReader : IInputReader
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }

    class ConsoleWriter : IOutputWriter
    {
        public void Write(string output)
        {
            Console.WriteLine(output);
        }
    }
}