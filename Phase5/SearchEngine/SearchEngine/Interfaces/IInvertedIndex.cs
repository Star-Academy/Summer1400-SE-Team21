using System.Collections.Generic;

namespace SearchEngine
{
    public interface IInvertedIndex
    {
        InvertedIndex TokenizeFiles(Dictionary<string, string> allDocuments);
        SortedSet<string> Tokenize(string st);
        SortedSet<string> Query(UserInput input);
        SortedSet<string> RemoveWordFromResult(string word, SortedSet<string> result);
        SortedSet<string> AddWordToResult(string word, SortedSet<string> result);
        SortedSet<string> AndWordWithResult(string word, SortedSet<string> result);
        SortedSet<string> GetDocsContainWord(string word);
    }
}