using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface IInvertedIndex
    {
        IInvertedIndex TokenizeFiles(Dictionary<string, string> allDocuments);
        SortedSet<string> Query(IUserInput input);
    }
}