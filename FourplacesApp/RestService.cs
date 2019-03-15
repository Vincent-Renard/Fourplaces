using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Model.Dtos;
using Newtonsoft.Json;

namespace FourplacesApp
{
    public class RestService : IRestService
    {
        private readonly String serviceURI = "https://td-api.julienmialon.com";
        private readonly String _placesURI = "/places";
        private readonly String _rootURI = "/";
        private readonly String _loginURI = "/auth/login";
        private readonly String _loginRegisterURI = "/auth/register";
        private readonly String _loginRefreshURI = "/auth/refresh";
        private readonly String _meURI = "/me";
        private readonly String _patchPasswordURI = "/me/password";
        private readonly String _imagesURI = "/images";
        private readonly String _commentsURI = "/comments";//a mettre apres service uri/placesURI/{id}/
        private HttpClient client;
        private LoginResult Tokens { get; set; }
       

        public RestService()
        {
            client = new HttpClient();
            Tokens = new LoginResult();



        }
        public LoginResult GetToken()
        {
            return Tokens;
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


        public async Task<List<PlaceItemSummary>> GetListPlacesAsync()
        {

            client = new HttpClient();
            List<PlaceItemSummary> toRet = new List<PlaceItemSummary>();

            var uri = new Uri(string.Format(this.serviceURI + this._placesURI, string.Empty));

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Tokens.AccessToken);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    toRet = JsonConvert.DeserializeObject<Response<List<PlaceItemSummary>>>(content).Data; ;
                }
            }
            catch (Exception ex) { Console.WriteLine("EROORRR " + ex.Message); }



            return toRet;

         }

        public async Task<LoginResult> Signin(RegisterRequest user)
        {

            Response<LoginResult> toks = null;
            String tmp = string.Format(this.serviceURI + this._loginRegisterURI, string.Empty);

            var uri = new Uri(tmp);
 
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            client = new HttpClient();
            var response = await client.PostAsync(uri, content);   

            if (response.IsSuccessStatusCode)
            {

                var rep = await response.Content.ReadAsStringAsync();
                toks = JsonConvert.DeserializeObject<Response<LoginResult>>(rep);
                Tokens = toks.Data;
            }
            else Console.WriteLine("EROORRR Sign   ");
          
            return Tokens;
        }     

        public async Task<bool> Login(LoginRequest log_user)
        {
            Response<LoginResult> toks = null;
            var uri = new Uri(string.Format(this.serviceURI + this._loginURI, string.Empty));
            var json = JsonConvert.SerializeObject(log_user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var rep = await response.Content.ReadAsStringAsync();
                toks = JsonConvert.DeserializeObject<Response<LoginResult>>(rep);
                Tokens = toks.Data;
            }
            else
            {
                return false;
            }

            return true;
        }

        internal async void  RefreshToken()
        {
            RefreshRequest request = new RefreshRequest
            {
                RefreshToken = Tokens.RefreshToken
            };
            Response<LoginResult> toks = null;
            var uri = new Uri(string.Format(this.serviceURI + this._loginRefreshURI, string.Empty));
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var rep = await response.Content.ReadAsStringAsync();
                toks = JsonConvert.DeserializeObject<Response<LoginResult>>(rep);
            }
            Tokens = toks.Data;
            //return Tokens;
        }
        public async Task<UserItem> GetMe()
        {//TOKEN
            UserItem toRet = null;
            var uri = new Uri(string.Format(this.serviceURI + this._meURI, string.Empty));
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Tokens.AccessToken);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    toRet = JsonConvert.DeserializeObject<Response<UserItem>>(content).Data; ;
                }
            }
            catch (Exception ex) { Console.WriteLine("EROORRR " + ex.Message); }



            return toRet;

        }
      

        public Task<UserItem> PatchMe(UpdateProfileRequest patch_user)
        {
            //TOKEN
            throw new NotImplementedException();
        }

        public Task<UserItem> PatchPassword(UpdatePasswordRequest updatePassword)
        {
            //TOKEN
            throw new NotImplementedException();
        }

        public Task<string> GetImage(int idImg)
        {
            throw new NotImplementedException();
        }

        public Task<Response> PostPlace(CreatePlaceRequest placeRequest)
        {
            //TOKEN
            throw new NotImplementedException();
        }

        public async Task<PlaceItem> GetPlace(int idPlace)
        {
            client = new HttpClient();
            var uri = new Uri(string.Format(this.serviceURI + this._placesURI+"/"+idPlace, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);
            PlaceItem place = new PlaceItem();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                place = JsonConvert.DeserializeObject<Response<PlaceItem>>(content).Data;
            }
            return place;
      
        }

     
        public async Task<Response> PostCommentAsync(int idPlace, CreateCommentRequest commentRequest)
        {
            client = new HttpClient();
            var uri = new Uri(string.Format(this.serviceURI + this._placesURI + "/" + idPlace+this._commentsURI, string.Empty));
            client.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer", Tokens.AccessToken);
            var json = JsonConvert.SerializeObject(commentRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(uri, content);
            /*
              if (response.IsSuccessStatusCode)
             {

             }
             */
            var r = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<Response>(r);

            return resp;
        }
    }
}
