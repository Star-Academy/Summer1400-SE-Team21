﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NSubstitute.Core;
using SearchEngine;
using SearchEngine.Interfaces;
using Xunit;

namespace TestProject1
{
    public class InvertedIndexTest
    {
        private const string Path = "../../../TestDocs/DocsForTest";
        private IDatabaseMap<string,string> _context;
        private ITokenizer _tokenizer;

        public InvertedIndexTest()
        {
            _context = new SimpleDatabase();
            _tokenizer = GetTokenizer();
        }

        private ITokenizer GetTokenizer()
        {
            ITokenizer tokenizer = Substitute.For<ITokenizer>();
            tokenizer.Tokenize(Arg.Any<string>()).Returns(s =>
            {
                var matches = Regex.Matches((string)s[0], "[a-zA-Z]+");
                SortedSet<string> set = new SortedSet<string>();
                foreach (var match in matches)
                {
                    var matchString = match.ToString();
                    if (matchString != null) set.Add(matchString);
                }

                return set;
            });
            tokenizer.Tokenize("Hello Everyone This Is Just For Test Hello! a cat is here").Returns(
                new SortedSet<string>{"Hello","Everyone","This","Is","Just","For","Test","Hello","a","cat","is","here"}
            );
            return tokenizer;
        }


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
            InvertedIndex invertedIndex = (InvertedIndex)new InvertedIndex(_context).AddDocuments(fileReader.ReadingFiles(Path),_tokenizer);

            Assert.Contains("firstFile", invertedIndex.GetDocsContainWord("hello"));
            Assert.Empty(invertedIndex.GetDocsContainWord("ali"));
        }
        
        
        [Fact]
        public void QueryTestSimpleTest_ForStableState_CheckingFirstFileForWord (){
            IFileReader fileReader = Substitute.For<IFileReader>();
            fileReader.ReadingFiles(Path).Returns(ReturningDictionary());
            InvertedIndex invertedIndex = (InvertedIndex)new InvertedIndex(_context).AddDocuments(fileReader.ReadingFiles(Path),_tokenizer);

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
            InvertedIndex invertedIndex = (InvertedIndex)new InvertedIndex(_context).AddDocuments(fileReader.ReadingFiles(Path),_tokenizer);

            UserInput userInput = new UserInput("everyone +hello everyone -cat");
            var files = invertedIndex.Query(userInput);
            Assert.True(!files.Any());
        }
        
        
        [Fact]
        public void QueryTest_CheckingSimpleQuery (){
            IFileReader fileReader = Substitute.For<IFileReader>();
            fileReader.ReadingFiles(Path).Returns(ReturningDictionary());
            InvertedIndex invertedIndex = (InvertedIndex)new InvertedIndex(_context).AddDocuments(fileReader.ReadingFiles(Path),_tokenizer);

            UserInput userInput = new UserInput("dog");
            var files = invertedIndex.Query(userInput);
            Assert.True(!files.Any());
        }
        
        
        [Fact]
        public void PositiveTest (){
            IFileReader fileReader = Substitute.For<IFileReader>();
            fileReader.ReadingFiles(Path).Returns(ReturningDictionary());
            InvertedIndex invertedIndex = (InvertedIndex)new InvertedIndex(_context).AddDocuments(fileReader.ReadingFiles(Path),_tokenizer);

            UserInput userInput = new UserInput("+elephant");
            var files = invertedIndex.Query(userInput);
            Assert.True(!files.Any());
        }
        
        
        [Fact]
        public void MinusTest (){
            IFileReader fileReader = Substitute.For<IFileReader>();
            fileReader.ReadingFiles(Path).Returns(ReturningDictionary());
            IInvertedIndex invertedIndex = new InvertedIndex(_context).AddDocuments(fileReader.ReadingFiles(Path),_tokenizer);

            UserInput userInput = new UserInput("-elephant");
            var files = invertedIndex.Query(userInput);
            Assert.True(!files.Any());
        }
        
        
        [Fact]
        public void EmptyTest (){
            IFileReader fileReader = Substitute.For<IFileReader>();
            fileReader.ReadingFiles(Path).Returns(ReturningDictionary());
            IInvertedIndex invertedIndex = new InvertedIndex(_context).AddDocuments(fileReader.ReadingFiles(Path),_tokenizer);

            UserInput userInput = new UserInput(" ");
            var files = invertedIndex.Query(userInput);
            Assert.True(!files.Any());
        }
        
        [Fact]
        public void TestSimpleInvertedIndex()
        {
            InvertedIndex invertedIndex = new InvertedIndex(_context);
            invertedIndex.AddDocuments(new FileReader().ReadingFiles("../../../TestDocs/testDocs"),_tokenizer);
            var queryResult = invertedIndex.Query(new UserInput("Cause +People -pretty"));
            var expected = new SortedSet<string>() {"59631.txt", "59635.txt", "59639.txt", "59648.txt"};
            Assert.Equal(expected, queryResult);
        }
        
        
        [Fact]
        public void TestJustAndQuery()
        {
            InvertedIndex invertedIndex = new InvertedIndex(_context);
            invertedIndex.AddDocuments(new FileReader().ReadingFiles("../../../TestDocs/testDocs"),_tokenizer);
            var result = invertedIndex.Query(new UserInput("Cause People pretty"));
            Assert.True(!result.Any());
        }
        
        
        [Fact]
        public void TestJustOrQuery()
        {
            InvertedIndex invertedIndex = new InvertedIndex(_context);
            invertedIndex.AddDocuments(new FileReader().ReadingFiles("../../../TestDocs/testDocs"),_tokenizer);
            var queryResult = invertedIndex.Query(new UserInput("+Cause +People +pretty"));
            var expected = new SortedSet<string>() 
                {"59631.txt", "59632.txt", "59633.txt", "59635.txt", "59637.txt", "59639.txt", "59648.txt"};
            Assert.Equal(expected,queryResult);
        }
        
        
        [Fact]
        public void TestJustRemoveQuery()
        {
            InvertedIndex invertedIndex = new InvertedIndex(_context);
            invertedIndex.AddDocuments(new FileReader().ReadingFiles("../../../TestDocs/testDocs"),_tokenizer);
            var result = invertedIndex.Query(new UserInput("-Cause -People -pretty"));
            Assert.True(!result.Any());
        }
        
        
        [Fact]
        public void TestEmptyQuery()
        {
            InvertedIndex invertedIndex = new InvertedIndex(_context);
            invertedIndex.AddDocuments(new FileReader().ReadingFiles("../../../TestDocs/testDocs"),_tokenizer);
            var result = invertedIndex.Query(new UserInput(""));
            Assert.True(!result.Any());
        }
        
        
        [Fact]
        public void TestLongQuery()
        {
            InvertedIndex invertedIndex = new InvertedIndex(_context);
            invertedIndex.AddDocuments(new FileReader().ReadingFiles("../../../TestDocs/testDocs"),_tokenizer);
            var result = invertedIndex.Query(new UserInput("thisWordIsTooLongAndThereShouldBeNoMatchDocInOurDatabase"));
            Assert.True(!result.Any());
        }
    }

    class SimpleDatabase : IDatabaseMap<string,string>
    {
        private Dictionary<string, SortedSet<string>> _dictionary;
        public List<string> Get(string key)
        {
            if (_dictionary.ContainsKey(key))
                return _dictionary[key].ToList();
            else
                return new List<string>();
        }

        public void Add(string key, string value)
        {
            if (_dictionary.ContainsKey(key))
                _dictionary[key].Add(value);
            else
                _dictionary.Add(key,new SortedSet<string>{value});
        }

        public bool Delete()
        {
            if (_dictionary != null)
            {
                _dictionary = null;
                return true;
            }

            return false;
        }

        public bool Create()
        {
            if (_dictionary == null)
            {
                _dictionary = new Dictionary<string, SortedSet<string>>();
                return true;
            }

            return false;
        }

        public void Save()
        {
        }
    }
}