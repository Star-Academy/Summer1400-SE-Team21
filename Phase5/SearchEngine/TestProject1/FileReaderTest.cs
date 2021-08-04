using System;
using System.Collections.Generic;
using SearchEngine;
using Xunit;

namespace TestProject1
{
    public class FileReaderTest
    {
        [Fact]
        public void ReadNotAvailableFolder() {
            FileReader fileReader = new FileReader();
            Dictionary<string, string> contents = fileReader.ReadingFiles("notAvailable");
            Dictionary<string, string> expected = new Dictionary<string, string>();
            Assert.Equal(contents, expected);
        }

        [Fact]
        public void readSimpleFolder(){
            FileReader fileReader = new FileReader();
            Dictionary<string, string> content = fileReader.ReadingFiles("../../../TestProject1/TestDocs/simpleFolder");
            Dictionary<string, string> expected = new Dictionary<string, string>();
            expected.Add("simpleFile1.txt","this is simpleFile1");
            expected.Add("simpleFile2.txt","this is simpleFile2");
            Assert.Equal(expected,content);
        }

        [Fact]
        public void readComplexFolder(){
            FileReader fileReader = new FileReader();
            Dictionary<string, string> content = fileReader.ReadingFiles("../../../TestProject1/TestDocs/complexFolder");
            Dictionary<string, string> expected = new Dictionary<string, string>();
            expected.Add("childFile1.txt","this is childFile1");
            expected.Add("childFile2.txt","this is childFile2");
            expected.Add("complexFile1.txt","this is complexFile1");
            expected.Add("complexFile2.txt","this is complexFile2");
            Assert.Equal(expected,content);
        }


        [Fact]
        public void gettingFileAndReadIt (){
            FileReader fileReader = new FileReader();
            Dictionary<string, string> files = fileReader.ReadingFiles("../../../TestProject1/TestDocs/DocsForTest");

            Assert.True(files.ContainsKey("firstFile"));
            Assert.True(files["firstFile"].StartsWith("Hello Everyone"));
        }
    }
}