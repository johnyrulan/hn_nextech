using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HNNextech.Service.Interfaces;
using HNNextech.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HNNextech.Web.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsApiController : ControllerBase
    {
        private IHackerNewsApiService _hackerNewsApiService;

        public NewsApiController(IHackerNewsApiService hackerNewsApiService)
        {
            _hackerNewsApiService = hackerNewsApiService;
        }

        [HttpGet]
        [Route("latesthackernewsstories")]
        [ResponseCache(Duration = 60)]
        public async Task<List<HackerNewsStory>> GetLatestHackerNewsStories()
        {
            var hackerNewsStories = await _hackerNewsApiService.FetchNewestStories();

            return hackerNewsStories;
        }
    }
}