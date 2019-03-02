using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
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
        private readonly String _loginURI = "auth/login";
        private readonly String _loginRegisterURI = "auth/register";
        private readonly String _loginRefreshURI = "auth/refresh";
        private readonly String _meURI = "/me";
        private readonly String _patchPasswordURI = "/me/password";
        private readonly String _imagesURI = "/images";
        private readonly String _commentsURI = "/images";//a mettre apres seviveuri/placesURI/{id}/
        private HttpClient client;

        public RestService()
        {
            client = new HttpClient { MaxResponseContentBufferSize = 256000 };

        }
        public async void GetRoot()
        {



            var uri = new Uri(string.Format(this.serviceURI + _rootURI, string.Empty));

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }






        }
        public async Task<ObservableCollection<PlaceItemSummary>> GetListPlacesAsync()
        {

            ObservableCollection<PlaceItemSummary> toRet = new ObservableCollection<PlaceItemSummary>();

            var uri = new Uri(string.Format(this.serviceURI + this._placesURI, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    toRet = JsonConvert.DeserializeObject<Response<ObservableCollection<PlaceItemSummary>>>(content).Data; ;
                }
            }
            catch (Exception ex) { Console.WriteLine("EROORRR " + ex.Message); }



            return toRet;

        }
        public async Task<LoginResult> Signin(RegisterRequest user)
        {
            Response<LoginResult> toks = null;
            var uri = new Uri(string.Format(this.serviceURI + this._loginRegisterURI, string.Empty));
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var rep = await response.Content.ReadAsStringAsync();
                toks = JsonConvert.DeserializeObject<Response<LoginResult>>(rep);
            }
            return toks.Data;
        }

    }
}
