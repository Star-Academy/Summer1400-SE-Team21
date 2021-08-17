using System.Collections.Generic;

namespace WebApi.Services
{
    public interface IInvertedIndexService
    {
        List<string> Search(string word);
    }
}