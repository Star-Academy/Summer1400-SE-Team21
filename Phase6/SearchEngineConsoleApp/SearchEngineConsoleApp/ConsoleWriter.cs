using System;
using SearchEngine.Interfaces;

namespace SearchEngineConsoleApp
{
    public class ConsoleWriter : IOutputWriter
    {
        public void Write(string output)
        {
            Console.WriteLine(output);
        }
    }
}