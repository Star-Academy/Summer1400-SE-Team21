using System;
using System.IO;
using SearchEngine;
using Xunit;

namespace TestProject1
{
    public class ManagerTest
    {
        [Fact]
        public void TestRunManager()
        {
            StringWriter writer = new StringWriter();
            Console.SetOut(writer);

            StringReader reader = new StringReader("hahaha\nexit");
            Console.SetIn(reader);

            string[] args = new string[0];
            new Manager().Run(args);
            
            Assert.Equal("using In Memory\nstart engine\nno doc found\nend engine\n", writer.ToString().Replace("\r",""));
        }
        
        
        [Fact]
        public void TestProgram()
        {
            StringWriter writer = new StringWriter();
            Console.SetOut(writer);

            StringReader reader = new StringReader("hahaha12\nexit");
            Console.SetIn(reader);

            string[] args = new string[1];
            args[0] = "../../../../TestProject1/TestDocs/Doc";
            Program.Main(args);

            Assert.Equal("using", writer.ToString().Substring(0, 5));
        }
        
    }
}