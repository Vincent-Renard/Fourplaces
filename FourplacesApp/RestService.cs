using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using TD.Api.Dtos;
using System.Console;

namespace FourplacesApp
{
    public class RestService
    {
        private readonly String getPlacesURI = "https://td-api.julienmialon.com/places";
        private HttpClient client;

        public RestService()
        {
            client = new HttpClient{ MaxResponseContentBufferSize = 256000 };

        }
        public async System.Threading.Tasks.Task<List<PlaceItem>> GetListPlacesAsync() {
  
            List<PlaceItem> toRet = new List<PlaceItem>();

          var uri = new Uri(string.Format(this.getPlacesURI, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    toRet = JsonConvert.DeserializeObject<List<PlaceItem>>(content);
                }
            }
            catch (Exception ex)
            {
            }

            return toRet;

        }
    }
}
