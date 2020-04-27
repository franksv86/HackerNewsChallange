using CallengeApi.Helpers;
using CallengeApi.Models;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CallengeApi.BLL
{
    public class HackerNewEntity
    {
        private IMemoryCache _cache;
        private AppSettings _appsettings;
        private IHttpClientFactory _clientFactory;
        
        public HackerNewEntity(AppSettings appsettings, IMemoryCache cache, IHttpClientFactory clientFactory) {
            _appsettings = appsettings;
            _clientFactory = clientFactory;
            _cache = cache;
        }

       

       

        public async Task<List<int>> GetLastIds()
        {
            var client = _clientFactory.CreateClient(_appsettings.HackerNewsName);

            var response = await client.GetAsync("newstories.json?print=pretty");

            if (response.IsSuccessStatusCode)
            {

                string apiResponse = await response.Content.ReadAsStringAsync();
                var listIds = JsonConvert.DeserializeObject<List<int>>(apiResponse);
                return listIds;

            }
            else
            {
                return new List<int>();
            }

        }

        public List<HackerNewsModel> GetPage(List<int> ids, int take, string search, CancellationToken cancellationToken)
        {

            List<HackerNewsModel> resultList = new List<HackerNewsModel>();
            List<HackerNewsModel> cachedList = GetAllCached();

            foreach (var id in ids)
            {

                if (cancellationToken.IsCancellationRequested)
                    return null;

                HackerNewsModel item = cachedList.FirstOrDefault(x => x.Id == id);

                if (item == null)
                {
                    item = this.GetById(id).Result;
                    
                    if (item == null)
                        continue;
                    
                    cachedList = InsertInCached(item);
                }

                if (String.IsNullOrEmpty(search))
                    resultList.Add(item);
                
                else if (item.Title.ToLower().Contains(search.ToLower()))
                    resultList.Add(item);

                if (resultList.Count == take)
                    break;
            }

            return resultList;

        }

        private async Task<HackerNewsModel> GetById(int id)
        {
            var client = _clientFactory.CreateClient(_appsettings.HackerNewsName);

            var response = await client.GetAsync($"item/{id}.json?print=pretty");

            if (response.IsSuccessStatusCode) {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<HackerNewsModel>(apiResponse);
                return item;
            }
            else
            {
                return null;
            }
        }



        private List<HackerNewsModel> GetAllCached()
        {
            List<HackerNewsModel> list = null;
            
            _cache.TryGetValue("HackerNews", out list);

            if (list == null)
                return new List<HackerNewsModel>();

            return list;
        }

        private List<HackerNewsModel> InsertInCached(HackerNewsModel model) {
            
            var list = GetAllCached();

            if (list.Any(x=> x.Id  == model.Id))
                return list;
            
            list.Add(model);
            
            _cache.Set("HackerNews", list);
            
            return list;
        }


    }
}
