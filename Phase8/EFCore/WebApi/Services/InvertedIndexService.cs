using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using SearchEngine;

namespace WebApi.Services
{
    public class InvertedIndexService : IInvertedIndexService
    {
        public List<string> Search(string word)
        {
            throw new System.NotImplementedException();
        }
    }
}