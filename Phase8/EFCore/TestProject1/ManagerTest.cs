using System;
using System.IO;
using SearchEngine;
using Xunit;

namespace TestProject1
{
    public class ManagerTest
    {
        [Fact]
        public void JustTest()
        {
            StringWriter writer = new StringWriter();
            Console.SetOut(writer);

            StringReader reader = new StringReader("hahaha\nexit");
            Console.SetIn(reader);

            string[] args = new string[0];
            new Manager().Run(args);
            
            Assert.Equal(writer.ToString(), "using In Memory\r\nstart engine\r\nno doc found\r\nend engine\r\n");
        }
        
        
        [Fact]
        public void JustTest2()
        {
            StringWriter writer = new StringWriter();
            Console.SetOut(writer);

            StringReader reader = new StringReader("hahaha\nexit");
            Console.SetIn(reader);

            string[] args = new string[1];
            args[0] = "../../../../TestProject1/TestDocs/Doc";
            Program.Main(args);

            Assert.Equal(writer.ToString().Substring(0, 5), "using");
        }
        
    }
}