using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class SearchController : ControllerBase
    {
        private readonly IInvertedIndexService _invertedIndex;

        public SearchController(IInvertedIndexService invertedIndex)
        {
            _invertedIndex = invertedIndex;
        }

        [HttpGet]
        public List<string> Query(string query)
        {
            return _invertedIndex.Query(query);
        }

        [HttpPost]
        public IActionResult PostDocument([FromBody] Dictionary<string, string> fileContents)
        {
            _invertedIndex.AddDocument(fileContents);
            return Ok(new
            {
                fileContents
            });
        }
    }
}