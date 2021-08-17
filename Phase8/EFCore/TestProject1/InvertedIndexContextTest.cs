using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SearchEngine;
using Xunit;

namespace TestProject1
{
    public class InvertedIndexContextTest
    {
        private InvertedIndexContext GetContext()
        {
            var builder = new DbContextOptionsBuilder<InvertedIndexContext>();
            builder.UseInMemoryDatabase("InvertedIndexContext");
            var context = new InvertedIndexContext(builder.Options);
            context.Database.EnsureDeleted();
            return context;
        }

        [Fact]
        public void TestCreateDatabase()
        {
            var context = GetContext();
            Assert.True(context.Create());
        }

        [Fact]
        public void TestDeleteDatabase()
        {
            var context = GetContext();
            Assert.False(context.Delete());
            context.Create();
            Assert.True(context.Delete());
        }

        [Fact]
        public void TestAddWordDocument()
        {
            var context = GetContext();
            context.Create();
            Word myWord = new Word() {String = "word"};
            var word = myWord.String;
            Document myDocument = new Document() {Name = "document"};
            var document = myDocument.Name;
            //
            myDocument.Words = new List<Word>();
            var words = myDocument.Words;
            words.Add(myWord);
            //
            Assert.Empty(context.Get(word));
            context.Add(word, document);
            context.Save();
            var expected = new List<string> {document };
            Assert.Equal(expected,context.Get(word));
        }

        [Fact] 
        public void TestDuplicateAdd()
        {
            var context = GetContext();
            context.Create();
            var word = "word";
            var document = "document";
            Assert.Empty(context.Get(word));
            context.Add(word, document);
            context.Add(word, document);
            context.Save();
            var expected = new List<string> {document };
            Assert.Equal(expected,context.Get(word));
        }

        [Fact]
        public void TestAddManyDocument()
        {
            var context = GetContext();
            context.Create();
            var word = "word";
            var document1 = "document1";
            var document2 = "document2";
            Assert.Empty(context.Get(word));
            context.Add(word, document1);
            context.Add(word, document2);
            context.Save();
            var expected = new List<string> {document1,document2 };
            Assert.Equal(expected,context.Get(word));
        }
    }
}