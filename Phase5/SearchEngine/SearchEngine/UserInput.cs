using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchEngine
{
    public class UserInput : IUserInput
    {
        private readonly SortedSet<string> _andInputs = new SortedSet<string>();
        private readonly SortedSet<string> _orInputs = new SortedSet<string>();
        private readonly SortedSet<string> _removeInputs = new SortedSet<string>();

        public UserInput(string input)
        {
            SortedSet<string> userInputTokens = TokenizeUserInput(input);

            foreach (string token in userInputTokens) {
                if (token.StartsWith("+"))
                    _orInputs.Add(token.Substring(1));
                else if (token.StartsWith("-"))
                    _removeInputs.Add(token.Substring(1));
                else
                    _andInputs.Add(token);
            }
            _andInputs = StringUtils.ProcessRawTokens(_andInputs);
            _orInputs = StringUtils.ProcessRawTokens(_orInputs);
            _removeInputs = StringUtils.ProcessRawTokens(_removeInputs);
        }

        public SortedSet<string> GetAndInputs()
        {
            return _andInputs;
        }

        public SortedSet<string> GetOrInputs()
        {
            return _orInputs;
        }

        public SortedSet<string> GetRemoveInputs()
        {
            return _removeInputs;
        }

        private SortedSet<string> TokenizeUserInput(string input)
        {
            SortedSet<string> tokens = new SortedSet<string>();
            var splitInput = Regex.Split(input, "\\s+");
            splitInput.Select(s => s.ToLower()).ToList().ForEach(token => tokens.Add(token));
            return tokens;
        }
    }
}