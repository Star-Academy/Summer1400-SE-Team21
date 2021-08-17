using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface IInvertedIndex
    {
        IInvertedIndex AddDocuments(Dictionary<string, string> allDocuments,ITokenizer tokenizer);
        SortedSet<string> Query(IUserInput input);
    }
}