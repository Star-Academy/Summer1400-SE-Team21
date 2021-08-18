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
        public IActionResult PostDocument([FromQuery] string name, [FromBody] string content)
        {
            _invertedIndex.AddDocument(name,content);
            return Ok(new
            {
                Name = name,
                Content = content
            });
        }
    }
}