using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SearchEngine.Interfaces;

namespace SearchEngine
{
    public class InvertedIndex : IInvertedIndex
    {
        private readonly IDatabaseMap<string,string> _context;
        private ITokenizer _tokenizer;
        
        public InvertedIndex(IDatabaseMap<string,string> context,ITokenizer tokenizer)
        {
            _context = context;
            _tokenizer = tokenizer;
        }

        public IInvertedIndex AddDocuments(Dictionary<string, string> allDocuments)
        {
            _context.Delete();
            _context.Create();
            allDocuments.ToList().ForEach(pair =>
            {
                Console.WriteLine($"adding {pair.Key}");
                AddDocument(pair.Key,pair.Value);
            });
            
            _context.Save();
            return this;
        }

        public void AddDocument(string name, string content)
        {
            foreach (var word in StringUtils.ProcessRawTokens(_tokenizer.Tokenize(content)).ToList())
            {
                _context.Add(word,name);
            }
        }

        public SortedSet<string> Query(IUserInput input)
        {
            _context.Create();
            SortedSet<string> result = null;
            input.GetAndInputs().ToList().ForEach(st => result = AndWordWithResult(st, result));
            input.GetOrInputs().ToList().ForEach(st => result = AddWordToResult(st, result));
            input.GetRemoveInputs().ToList().ForEach(st => result = RemoveWordFromResult(st, result));
            if (result != null) return result;
            else return new SortedSet<string>();
        }

        private SortedSet<string> RemoveWordFromResult(string word, SortedSet<string> result)
        {
            List<string> wordList = GetDocsContainWord(word);
            if (result == null) return new SortedSet<string>();
            result.ExceptWith(wordList);
            return result;
        }

        private SortedSet<string> AddWordToResult(string word, SortedSet<string> result)
        {
            List<string> wordList = GetDocsContainWord(word);
            if (result == null) return CloneSortedSet(wordList);
            result.UnionWith(wordList);
            return result;
        }

        private SortedSet<string> AndWordWithResult(string word, SortedSet<string> result)
        {
            List<string> wordList = GetDocsContainWord(word);
            if (result == null) return CloneSortedSet(wordList);
            result.IntersectWith(wordList);
            return result;
        }

        public List<string> GetDocsContainWord(string word)
        {
            return _context.Get(word);
        }

        private SortedSet<string> CloneSortedSet(List<string> toClone)
        {
            SortedSet<string> cloneable = new SortedSet<string>();
            toClone.ToList().ForEach(st => cloneable.Add(st));
            return cloneable;
        }
    }
}