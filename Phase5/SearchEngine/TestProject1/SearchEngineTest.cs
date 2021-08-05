using System.Collections.Generic;
using NSubstitute;
using SearchEngine.Interfaces;
using Xunit;

namespace TestProject1
{
    public class SearchEngineTest
    {
        [Fact]
        public void TestSearchEngine()
        {
            CustomInput reader = new CustomInput("hello\nthis\nexit");
            CustomOutput writer = new CustomOutput();
            IInvertedIndex invertedIndex = GetInvertedIndex();
            SearchEngine.SearchEngine searchEngine =
                new SearchEngine.SearchEngine(reader, writer, invertedIndex);
            searchEngine.Run();
            const string expectedOutput = "1\n2\nno doc found\n";
            Assert.Equal(expectedOutput, writer.AllOutput);
        }

        [Fact]
        public void TestNoDocFound()
        {
            CustomInput reader = new CustomInput("cat\nnothing\nexit");
            CustomOutput writer = new CustomOutput();
            IInvertedIndex invertedIndex = GetInvertedIndex();
            SearchEngine.SearchEngine searchEngine =
                new SearchEngine.SearchEngine(reader, writer, invertedIndex);
            searchEngine.Run();
            const string expectedOutput = "no doc found\nno doc found\n";
            Assert.Equal(expectedOutput, writer.AllOutput);
        }

        [Fact]
        public void TestJustExit()
        {
            CustomInput reader = new CustomInput("exit");
            CustomOutput writer = new CustomOutput();
            IInvertedIndex invertedIndex = GetInvertedIndex();
            SearchEngine.SearchEngine searchEngine =
                new SearchEngine.SearchEngine(reader, writer, invertedIndex);
            searchEngine.Run();
            const string expectedOutput = "";
            Assert.Equal(expectedOutput, writer.AllOutput);
        }

        private static IInvertedIndex GetInvertedIndex()
        {
            IInvertedIndex invertedIndex = Substitute.For<IInvertedIndex>();
            invertedIndex.TokenizeFiles(Arg.Any<Dictionary<string, string>>()).Returns(invertedIndex);
            invertedIndex.Query(Arg.Any<IUserInput>()).Returns(x =>
            {
                var list = new SortedSet<string>();
                if (((IUserInput)x[0]).GetAndInputs().Contains("hello"))
                {
                    list.Add("1");
                    list.Add("2");
                }
                return list;
            });
            return invertedIndex;
            
        }
    }

    class CustomInput : IInputReader
    {
        private readonly string[] _inputs;
        private int _counter = 0;

        public CustomInput(string input)
        {
            _inputs = input.Split('\n');
        }

        public string Read()
        {
            if (_counter < _inputs.Length)
                return _inputs[_counter++];
            return "";
        }
    }

    class CustomOutput : IOutputWriter
    {
        public string AllOutput { private set; get; } = "";

        public void Write(string output)
        {
            AllOutput += output + "\n";
        }
    }
}