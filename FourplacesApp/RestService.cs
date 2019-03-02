using System;
using System.Collections.Generic;
using System.Net.Http;
using Model.Dtos;
using Newtonsoft.Json;

namespace FourplacesApp
{
    public class RestService
    {
        private readonly String serviceURI = "https://td-api.julienmialon.com";
        private readonly String getPlacesURI = "/places";
        private HttpClient client;

        public RestService()
        {
            client = new HttpClient{ MaxResponseContentBufferSize = 256000 };

        }
        public async System.Threading.Tasks.Task<List<PlaceItem>> GetListPlacesAsync() {
  
            List<PlaceItem> toRet = new List<PlaceItem>();

          var uri = new Uri(string.Format(this.serviceURI+this.getPlacesURI, string.Empty));

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
