using System;
using SearchEngine;
using SearchEngine = SearchEngine.SearchEngine;

namespace SearchEngineConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            InvertedIndex index = new InvertedIndex();
            index.TokenizeFiles(new FileReader().ReadingFiles("../../../TestDocs"));
            global::SearchEngine.SearchEngine myEngine =
                new global::SearchEngine.SearchEngine(new ConsoleReader(), new ConsoleWriter(), index);
            myEngine.Run();
        }
    }
}