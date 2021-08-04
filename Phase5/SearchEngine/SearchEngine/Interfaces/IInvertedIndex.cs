using System.Collections.Generic;

namespace SearchEngine
{
    public interface IInvertedIndex
    {
        IInvertedIndex tokenizeFiles(Dictionary<string, string> allDocuments);
        SortedSet<string> tokenize(string st);
        SortedSet<string> query(UserInput input);
        SortedSet<string> removeWordFromResult(string word, SortedSet<string> result);
        SortedSet<string> addWordToResult(string word, SortedSet<string> result);
        SortedSet<string> andWordWithResult(string word, SortedSet<string> result);
        SortedSet<string> getDocsContainWord(string word);
    }
}