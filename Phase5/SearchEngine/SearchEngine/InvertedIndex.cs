using System.Collections.Generic;
using System.Linq;
using System.Text;
using SearchEngine.Interfaces;

namespace SearchEngine
{
    public class InvertedIndex : IInvertedIndex
    {
        public Dictionary<string, SortedSet<string>> TokenizedWords { get; } = new();


        public IInvertedIndex TokenizeFiles(Dictionary<string, string> allDocuments)
        {
            allDocuments.ToList().ForEach(pair =>
            {
                foreach (var word in StringUtils.ProcessRawTokens(Tokenize(pair.Value)).ToList())
                {
                    if (TokenizedWords.ContainsKey(word))
                    {
                        TokenizedWords[word].Add(pair.Key);
                    }
                    else
                    {
                        SortedSet<string> docks = new SortedSet<string> { pair.Key };
                        TokenizedWords.Add(word.ToLower(), docks);
                    }
                }
            });

            return this;
        }

        private static SortedSet<string> Tokenize(string st)
        {
            SortedSet<string> tokens = new SortedSet<string>();
            for (int i = 0; i < st.Length; ++i)
            {
                if (!char.IsLetter(st[i])) continue;
                StringBuilder token = new StringBuilder();
                while (i < st.Length && char.IsLetterOrDigit(st[i]))
                {
                    token.Append(char.ToLower(st[i]));
                    i++;
                }

                tokens.Add(token.ToString());
            }

            return tokens;
        }

        public SortedSet<string> Query(IUserInput input)
        {
            SortedSet<string> result = null;
            input.GetAndInputs().ToList().ForEach(st => result = AndWordWithResult(st, result));
            input.GetOrInputs().ToList().ForEach(st => result = AddWordToResult(st, result));
            input.GetRemoveInputs().ToList().ForEach(st => result = RemoveWordFromResult(st, result));
            if (result != null) return result;
            else return new SortedSet<string>();
        }

        private SortedSet<string> RemoveWordFromResult(string word, SortedSet<string> result)
        {
            SortedSet<string> wordList = GetDocsContainWord(word);
            if (result == null) return new SortedSet<string>();
            result.ExceptWith(wordList);
            return result;
        }

        private SortedSet<string> AddWordToResult(string word, SortedSet<string> result)
        {
            SortedSet<string> wordList = GetDocsContainWord(word);
            if (result == null) return CloneSortedSet(wordList);
            result.UnionWith(wordList);
            return result;
        }

        private SortedSet<string> AndWordWithResult(string word, SortedSet<string> result)
        {
            SortedSet<string> wordList = GetDocsContainWord(word);
            if (result == null) return CloneSortedSet(wordList);
            result.IntersectWith(wordList);
            return result;
        }

        private SortedSet<string> GetDocsContainWord(string word)
        {
            if (TokenizedWords.ContainsKey(word)) return TokenizedWords[word];
            return new SortedSet<string>();
        }

        private SortedSet<string> CloneSortedSet(SortedSet<string> toClone)
        {
            SortedSet<string> cloneable = new SortedSet<string>();
            toClone.ToList().ForEach(st => cloneable.Add(st));
            return cloneable;
        }
    }
}