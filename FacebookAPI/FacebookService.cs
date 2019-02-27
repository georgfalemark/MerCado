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




        //Denna kommer vi förmodligen inte använda, vi 
        //ska ju inte lägga ut nått på Facebook från appen
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


        internal async Task GetInfoFromFaceBookUser(string somePersonKey)
        {
            string page = "blabla"; //och stoppa in nyckeln
            var result = await Get(page);

            //nytt konto
            //ny person
            //Gav kön, plats, ålder! 


            throw new NotImplementedException();
        }


        


    }
}
