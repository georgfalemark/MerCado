using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FacebookAPI
{
    class FacebookService
    {
        private async Task<string> Get(string url)
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(url))
            using (HttpContent content = response.Content)
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                return await content.ReadAsStringAsync();
            }
        }

        private async Task<string> Post(string url)
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.PostAsync(url, null))
            using (HttpContent content = response.Content)
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                return await content.ReadAsStringAsync();
            }
        }

    }
}
