using System.Collections.Generic;

namespace SearchEngine
{
    public interface IUserInput
    {
        SortedSet<string> GetAndInputs();
        SortedSet<string> GetOrInputs();
        SortedSet<string> GetRemoveInputs();
    }
}