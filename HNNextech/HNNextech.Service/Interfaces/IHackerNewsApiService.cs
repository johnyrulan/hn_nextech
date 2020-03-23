using HNNextech.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HNNextech.Service.Interfaces
{
    public interface IHackerNewsApiService
    {
        Task<List<int>> FetchNewestStoryIds();
        Task<HackerNewsStory> FetchStoryById(int id);
        Task<List<HackerNewsStory>> FetchNewestStories();
    }
}
