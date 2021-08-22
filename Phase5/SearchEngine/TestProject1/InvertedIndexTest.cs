using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using SearchEngine;
using SearchEngine.Interfaces;
using Xunit;

namespace TestProject1
{
    public class InvertedIndexTest
    {
        private const string Path = "../../../TestDocs/DocsForTest";

        
        private Dictionary<string, string> ReturningDictionary()
        {
            var toReturn = new Dictionary<string, string>
            {
                {"firstFile", "Hello Everyone This Is Just For Test Hello! a cat is here"}
            };
            return toReturn;
        }
        
        
        [Fact]
        public void InvertedIndexSimpleTest()
        {
            IFileReader fileReader = Substitute.For<IFileReader>();
            fileReader.ReadingFiles(Path).Returns(ReturningDictionary());
            InvertedIndex invertedIndex = (InvertedIndex)new InvertedIndex().TokenizeFiles(fileReader.ReadingFiles(Path));

            Assert.Contains("firstFile", invertedIndex.TokenizedWords["hello"]);
            Assert.False(invertedIndex.TokenizedWords.ContainsKey("ali"));
        }
        
        
        [Fact]
        public void QueryTestSimpleTest_ForStableState_CheckingFirstFileForWord (){
            IFileReader fileReader = Substitute.For<IFileReader>();
            fileReader.ReadingFiles(Path).Returns(ReturningDictionary());
            InvertedIndex invertedIndex = (InvertedIndex)new InvertedIndex().TokenizeFiles(fileReader.ReadingFiles(Path));

            UserInput userInput = new UserInput("everyone");
            SortedSet<string> files = invertedIndex.Query(userInput);
            Assert.False(!files.Any());
            Assert.Equal("firstFile", files.ToList()[0]);
            Assert.Single(files);
        }
        
        
        [Fact]
        public void QueryTest_ForStableState_CheckingEmptyResult (){
            IFileReader fileReader = Substitute.For<IFileReader>();
            fileReader.ReadingFiles(Path).Returns(ReturningDictionary());
            InvertedIndex invertedIndex = (InvertedIndex)new InvertedIndex().TokenizeFiles(fileReader.ReadingFiles(Path));

            UserInput userInput = new UserInput("everyone +hello everyone -cat");
            var files = invertedIndex.Query(userInput);
            Assert.True(!files.Any());
        }
        
        
        [Fact]
        public void QueryTest_CheckingSimpleQuery (){
            IFileReader fileReader = Substitute.For<IFileReader>();
            fileReader.ReadingFiles(Path).Returns(ReturningDictionary());
            InvertedIndex invertedIndex = (InvertedIndex)new InvertedIndex().TokenizeFiles(fileReader.ReadingFiles(Path));

            UserInput userInput = new UserInput("dog");
            var files = invertedIndex.Query(userInput);
            Assert.True(!files.Any());
        }
        
        
        [Fact]
        public void PositiveTest (){
            IFileReader fileReader = Substitute.For<IFileReader>();
            fileReader.ReadingFiles(Path).Returns(ReturningDictionary());
            InvertedIndex invertedIndex = (InvertedIndex)new InvertedIndex().TokenizeFiles(fileReader.ReadingFiles(Path));

            UserInput userInput = new UserInput("+elephant");
            var files = invertedIndex.Query(userInput);
            Assert.True(!files.Any());
        }
        
        
        [Fact]
        public void MinusTest (){
            IFileReader fileReader = Substitute.For<IFileReader>();
            fileReader.ReadingFiles(Path).Returns(ReturningDictionary());
            IInvertedIndex invertedIndex = new InvertedIndex().TokenizeFiles(fileReader.ReadingFiles(Path));

            UserInput userInput = new UserInput("-elephant");
            var files = invertedIndex.Query(userInput);
            Assert.True(!files.Any());
        }
        
        
        [Fact]
        public void EmptyTest (){
            IFileReader fileReader = Substitute.For<IFileReader>();
            fileReader.ReadingFiles(Path).Returns(ReturningDictionary());
            IInvertedIndex invertedIndex = new InvertedIndex().TokenizeFiles(fileReader.ReadingFiles(Path));

            UserInput userInput = new UserInput(" ");
            var files = invertedIndex.Query(userInput);
            Assert.True(!files.Any());
        }
        
        [Fact]
        public void TestSimpleInvertedIndex()
        {
            InvertedIndex invertedIndex = new InvertedIndex();
            invertedIndex.TokenizeFiles(new FileReader().ReadingFiles("../../../TestDocs/testDocs"));
            var queryResult = invertedIndex.Query(new UserInput("Cause +People -pretty"));
            var expected = new SortedSet<string>() {"59631.txt", "59635.txt", "59639.txt", "59648.txt"};
            Assert.Equal(expected, queryResult);
        }
        
        
        [Fact]
        public void TestJustAndQuery()
        {
            InvertedIndex invertedIndex = new InvertedIndex();
            invertedIndex.TokenizeFiles(new FileReader().ReadingFiles("../../../TestDocs/testDocs"));
            var result = invertedIndex.Query(new UserInput("Cause People pretty"));
            Assert.True(!result.Any());
        }
        
        
        [Fact]
        public void TestJustOrQuery()
        {
            InvertedIndex invertedIndex = new InvertedIndex();
            invertedIndex.TokenizeFiles(new FileReader().ReadingFiles("../../../TestDocs/testDocs"));
            var queryResult = invertedIndex.Query(new UserInput("+Cause +People +pretty"));
            var expected = new SortedSet<string>() 
                {"59631.txt", "59632.txt", "59633.txt", "59635.txt", "59637.txt", "59639.txt", "59648.txt"};
            Assert.Equal(expected,queryResult);
        }
        
        
        [Fact]
        public void TestJustRemoveQuery()
        {
            InvertedIndex invertedIndex = new InvertedIndex();
            invertedIndex.TokenizeFiles(new FileReader().ReadingFiles("../../../TestDocs/testDocs"));
            var result = invertedIndex.Query(new UserInput("-Cause -People -pretty"));
            Assert.True(!result.Any());
        }
        
        
        [Fact]
        public void TestEmptyQuery()
        {
            InvertedIndex invertedIndex = new InvertedIndex();
            invertedIndex.TokenizeFiles(new FileReader().ReadingFiles("../../../TestDocs/testDocs"));
            var result = invertedIndex.Query(new UserInput(""));
            Assert.True(!result.Any());
        }
        
        
        [Fact]
        public void TestLongQuery()
        {
            InvertedIndex invertedIndex = new InvertedIndex();
            invertedIndex.TokenizeFiles(new FileReader().ReadingFiles("../../../TestDocs/testDocs"));
            var result = invertedIndex.Query(new UserInput("thisWordIsTooLongAndThereShouldBeNoMatchDocInOurDatabase"));
            Assert.True(!result.Any());
        }
    }
}