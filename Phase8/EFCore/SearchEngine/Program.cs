using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using SearchEngine.Interfaces;

namespace SearchEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContextOptionsBuilder<InvertedIndexContext> builder = new();
            var connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
            if (connectionString != null)
                builder.UseSqlServer(connectionString);
            else
                builder.UseInMemoryDatabase("InvertedIndex");
            if(args.Length > 0){
                var invertedIndex = new InvertedIndex(new InvertedIndexContext(builder.Options));
                Console.WriteLine("add files");
                invertedIndex.TokenizeFiles(new FileReader().ReadingFiles(args[0]));
                Console.WriteLine("finish adding file");
            }
            else{
                var invertedIndex = new InvertedIndex(new InvertedIndexContext(builder.Options));
                var searchEngine = new SearchEngine(new ConsoleReader(),new ConsoleWriter(),invertedIndex);
                Console.WriteLine("start engine");
                searchEngine.Run();
                Console.WriteLine("end engine");
            }
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
