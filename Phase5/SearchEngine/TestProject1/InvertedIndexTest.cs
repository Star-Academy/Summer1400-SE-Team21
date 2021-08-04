using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using SearchEngine;
using Xunit;

namespace TestProject1
{
    public class InvertedIndexTest
    {
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


        private Dictionary<string, string> ReturningDictionary()
        {
            var toReturn = new Dictionary<string, string>();
            toReturn.Add("firstFile", "Hello Everyone This Is Just For Test Hello! a cat is here");
            return toReturn;
        }
        
        
        [Fact]
        public void InvertedIndexSimpleTest()
        {
            IFileReader fileReader = Substitute.For<IFileReader>();
            fileReader.ReadingFiles("../../../TestDocs/DocsForTest").Returns(ReturningDictionary());
            InvertedIndex invertedIndex = new InvertedIndex().TokenizeFiles(fileReader.ReadingFiles("../../../TestDocs/DocsForTest"));

            Assert.Contains("firstFile", invertedIndex.tokenizedWords["hello"]);
            Assert.False(invertedIndex.tokenizedWords.ContainsKey("ali"));
        }
    }
}