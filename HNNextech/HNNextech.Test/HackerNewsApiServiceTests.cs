using HNNextech.Service;
using HNNextech.Service.Interfaces;
using HNNextech.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HNNextech.Test
{
    [TestClass]
    public class HackerNewsApiServiceTests
    {
        private IHackerNewsApiService _hackerNewsApiService;

        [TestInitialize]
        public void Initialize()
        {
            var config = ConfigurationHelper.GetTestConfiguration();
            _hackerNewsApiService = new HackerNewsApiService(new HttpClient(), config);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task HackerNewsApiService_FetchNewestStories_EmptyApiUrls()
        {
            _hackerNewsApiService = new HackerNewsApiService(new HttpClient(), ConfigurationHelper.GetEmptyConfiguration());

            await _hackerNewsApiService.FetchNewestStories();
        }

        [TestMethod]        
        public async Task HackerNewsApiService_FetchNewestStories_FetchStoriesForValidUrl()
        {           
            var stories = await _hackerNewsApiService.FetchNewestStories();

            Assert.IsTrue(stories.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(JsonReaderException))]
        public async Task HackerNewsApiService_FetchNewestStories_FetchStoriesForInvalidUrl()
        {
            _hackerNewsApiService = new HackerNewsApiService(new HttpClient(), ConfigurationHelper.GetBadTestConfiguration());

            var stories = await _hackerNewsApiService.FetchNewestStories();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task HackerNewsApiService_FetchNewestStoryIds_EmptyApiUrl()
        {
            _hackerNewsApiService = new HackerNewsApiService(new HttpClient(), ConfigurationHelper.GetEmptyConfiguration());

            await _hackerNewsApiService.FetchNewestStoryIds();
        }

        [TestMethod]
        public async Task HackerNewsApiService_FetchNewestStoryIds_FetchStoriesForValidUrl()
        {
            var storyIds = await _hackerNewsApiService.FetchNewestStoryIds();

            Assert.IsTrue(storyIds.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(JsonReaderException))]
        public async Task HackerNewsApiService_FetchNewestStoryIds_FetchStoriesForInvalidUrl()
        {
            _hackerNewsApiService = new HackerNewsApiService(new HttpClient(), ConfigurationHelper.GetBadTestConfiguration());

            var stories = await _hackerNewsApiService.FetchNewestStoryIds();            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task HackerNewsApiService_FetchStoryById_EmptyApiUrl()
        {
            _hackerNewsApiService = new HackerNewsApiService(new HttpClient(), ConfigurationHelper.GetEmptyConfiguration());

            await _hackerNewsApiService.FetchStoryById(default);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task HackerNewsApiService_FetchStoryById_EmptyId()
        {            
            await _hackerNewsApiService.FetchStoryById(default);
        }

        [TestMethod]
        public async Task HackerNewsApiService_FetchStoryById_FetchStoryForValidId()
        {
            var storyIds = await _hackerNewsApiService.FetchNewestStoryIds();

            var story = await _hackerNewsApiService.FetchStoryById(storyIds.First());

            Assert.IsNotNull(story);
        }

        [TestMethod]               
        public async Task HackerNewsApiService_FetchStoryById_FetchStoriesForInvalidUrl()
        {
            _hackerNewsApiService = new HackerNewsApiService(new HttpClient(), ConfigurationHelper.GetBadTestConfiguration());

            var story = await _hackerNewsApiService.FetchStoryById(1);

            Assert.IsNull(story);
        }

        [TestMethod]        
        public async Task HackerNewsApiService_FetchStoryById_FetchStoriesForInvalidId()
        {            
            var story = await _hackerNewsApiService.FetchStoryById(Int32.MaxValue);

            Assert.IsNull(story);
        }
    }
}
