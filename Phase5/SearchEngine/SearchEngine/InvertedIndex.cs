using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngine
{
    public class InvertedIndex : IInvertedIndex
    {
        public Dictionary<string, SortedSet<string>> tokenizedWords { get; set; } = new Dictionary<string, SortedSet<string>>();
        
        
        public InvertedIndex TokenizeFiles(Dictionary<string, string> allDocuments)
        {
            foreach(KeyValuePair<string, string> entry in allDocuments)
            {
                string value = entry.Value;
                SortedSet<string> rawAllWords = Tokenize(value);
                SortedSet<string> allWords = StringUtils.ProcessRawTokens(rawAllWords);
                foreach (var word in allWords)
                {
                    if (tokenizedWords.ContainsKey(word))
                    {
                        tokenizedWords[word].Add(entry.Key);
                    }
                    else
                    {
                        SortedSet<string> docks = new SortedSet<string>();
                        docks.Add(entry.Key);
                        tokenizedWords.Add(word.ToLower(), docks);
                    }
                }
            }

            return this;
        }

        public SortedSet<string> Tokenize(string st)
        {
            SortedSet<string> tokens = new SortedSet<string>();
            for (int i = 0; i<st.Length; ++i)
            {
                if (!Char.IsLetter(st[i])) continue;
                StringBuilder token = new StringBuilder();
                while (i < st.Length && Char.IsLetter(st[i]))
                {
                    token.Append(Char.ToLower(st[i]));
                    i++;
                }

                tokens.Add(token.ToString());
            }

            return tokens;
        }

        public SortedSet<string> Query(UserInput input)
        {
            SortedSet<string> result = null;
            foreach (var st in input.GetAndInputs())
            {
                result = AddWordToResult(st, result);
            }
            foreach (var st in input.GetOrInputs())
            {
                result = AddWordToResult(st, result);
            }
            foreach (var st in input.GetRemoveInputs())
            {
                result = RemoveWordFromResult(st, result);
            }

            if (result != null) return result;
            else return new SortedSet<string>();
        }

        public SortedSet<string> RemoveWordFromResult(string word, SortedSet<string> result)
        {
            SortedSet<string> wordList = GetDocsContainWord(word);
            if (result == null) return new SortedSet<string>();
            result.ExceptWith(wordList);
            return result;
        }

        public SortedSet<string> AddWordToResult(string word, SortedSet<string> result)
        {
            SortedSet<string> wordList = GetDocsContainWord(word);
            if (result == null) return CloneSortedSet(wordList);
            result.UnionWith(wordList);
            return result;
        }

        public SortedSet<string> AndWordWithResult(string word, SortedSet<string> result)
        {
            SortedSet<string> wordList = GetDocsContainWord(word);
            if (result == null) return CloneSortedSet(wordList);
            result.IntersectWith(wordList);
            return result;
        }

        public SortedSet<string> GetDocsContainWord(string word)
        {
            if (tokenizedWords.ContainsKey(word)) return tokenizedWords[word];
            return new SortedSet<string>();
        }

        public SortedSet<string> CloneSortedSet(SortedSet<string> toClone)
        {
            SortedSet<string> clonable = new SortedSet<string>();
            foreach (var st in toClone)
            {
                clonable.Add(st);
            }
            return toClone;
        }
    }
}