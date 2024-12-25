﻿using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AppRateLimiter.CheckBucketReadService.Services
{
    public class ReadService : IReadService
    {
        public async Task<string> ReadAsync(string urlInput)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            var query = new Dictionary<string, string>
            {
                ["url"] = "https://www.google.com/"
            };

            var url = "https://appratelimiterbs.azurewebsites.net/api/read?";
            var response = await client.GetAsync(QueryHelpers.AddQueryString(url, query));
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<string>(await responseBody);
        }
    }
}
