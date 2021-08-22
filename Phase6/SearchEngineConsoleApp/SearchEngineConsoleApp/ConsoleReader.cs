using System;
using SearchEngine.Interfaces;

namespace SearchEngineConsoleApp
{
    public class ConsoleReader : IInputReader
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}