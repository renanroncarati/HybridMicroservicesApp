using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace QueryBookService.Clients
{
    public static class HttpClientProvider
    {
        public static void PostContent(string uri, List<QueryBooksService.Controllers.Books> content)
        {
            var serializedObj = JsonConvert.SerializeObject(content);
            HttpContent contentPost = new StringContent(serializedObj, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5001");

                var result = client.PostAsync(string.Format("/api{0}", uri), contentPost).Result;
                // var result = client.GetAsync("/shoppingcart").Result;
                // string resultContent = result.Content.ReadAsStringAsync().Result;

            }
        }

    }
}