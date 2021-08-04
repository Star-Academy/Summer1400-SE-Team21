using System.Collections.Generic;
using SearchEngine;
using Xunit;

namespace TestProject1
{
    public class UserInputTest
    {
        [Fact]
        public void SimpleInputTest()
        {
            string input = "hello this +fine -cats";
            UserInput userInput = new UserInput(input);

            SortedSet<string> expectedAndInput = new SortedSet<string> { "hello" };
            SortedSet<string> expectedOrInput = new SortedSet<string> { "fine" };
            SortedSet<string> expectedRemoveInput = new SortedSet<string> { "cat" };

            Assert.Equal(expectedAndInput, userInput.GetAndInputs());
            Assert.Equal(expectedOrInput, userInput.GetOrInputs());
            Assert.Equal(expectedRemoveInput, userInput.GetRemoveInputs());
        }
    }
}