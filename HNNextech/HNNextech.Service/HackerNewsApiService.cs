using HNNextech.Service.Interfaces;
using HNNextech.Service.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HNNextech.Service
{
    public class HackerNewsApiService : IHackerNewsApiService
    {
        private HttpClient _httpClient;
        private readonly string _hackerNewsNewestStoriesUrl;
        private readonly string _hackerNewsItemUrl;

        private readonly string _hackerNewsNewestStoriesUrlConfig = "HackerNews:NewestStoriesUrl";
        private readonly string _hackerNewsItemUrlConfig = "HackerNews:ItemUrl";

        public HackerNewsApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _hackerNewsNewestStoriesUrl = configuration[_hackerNewsNewestStoriesUrlConfig];
            _hackerNewsItemUrl = configuration[_hackerNewsItemUrlConfig];
        }

        public async Task<List<int>> FetchNewestStoryIds()
        {
            if (string.IsNullOrEmpty(_hackerNewsNewestStoriesUrl))
            {
                throw new ArgumentNullException("The HackerNews Newest Stories Url is required.");
            }

            var storyIds = new List<int>();

            var hackerNewsNewestStoriesResponse = await _httpClient.GetAsync(_hackerNewsNewestStoriesUrl);

            if (!hackerNewsNewestStoriesResponse.IsSuccessStatusCode)
            {
                return storyIds;
            }

            var hackerNewsNewestStoriesContent = await hackerNewsNewestStoriesResponse.Content.ReadAsStringAsync();
            storyIds = JsonConvert.DeserializeObject<List<int>>(hackerNewsNewestStoriesContent);

            return storyIds;
        }

        public async Task<HackerNewsStory> FetchStoryById(int id)
        {
            if(string.IsNullOrEmpty(_hackerNewsItemUrl))
            {
                throw new ArgumentNullException("The HackerNews Item is required.");
            }

            if(id == default || id < 0)
            {
                throw new ArgumentNullException("Please enter a valid id.");
            }

            string storyUrl = $"{_hackerNewsItemUrl}/{id.ToString()}.json";

            var hackerNewsItemResponse = await _httpClient.GetAsync(storyUrl);

            if(!hackerNewsItemResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var hackerNewsItemContent = await hackerNewsItemResponse.Content.ReadAsStringAsync();
            var hackerNewsStory = JsonConvert.DeserializeObject<HackerNewsStory>(hackerNewsItemContent);

            return hackerNewsStory;
        }

        public async Task<List<HackerNewsStory>> FetchNewestStories()
        {
            var storyIds = await FetchNewestStoryIds();

            var fetchStoryByIdTasks = storyIds.Select(id => FetchStoryById(id)).ToList();

            await Task.WhenAll(fetchStoryByIdTasks);

            var stories = fetchStoryByIdTasks.Select(task => task.Result).ToList();

            return stories;
        }
    }
}
