using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Model.Dtos;
using Newtonsoft.Json;

namespace FourplacesApp
{
    public class RestService
    {
        private readonly String serviceURI = "https://td-api.julienmialon.com";
        private readonly String _placesURI = "/places";
        private readonly String _rootURI = "/";
        private HttpClient client;

        public RestService()
        {
            client = new HttpClient{ MaxResponseContentBufferSize = 256000 };

        }
        public async void GetRoot()
        {

           

            var uri = new Uri(string.Format(this.serviceURI+_rootURI, string.Empty));

              var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                }
            
            




        }
        public async Task<List<PlaceItem>> GetListPlacesAsync() {
  
            List<PlaceItem> toRet = new List<PlaceItem>();

          var uri = new Uri(string.Format(this.serviceURI+this._placesURI, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    toRet = JsonConvert.DeserializeObject<List<PlaceItem>>(content);
                }
            }
            catch (Exception ex) { }



            return toRet;

        }
    }
}
