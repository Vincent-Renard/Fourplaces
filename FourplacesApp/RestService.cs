using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Dtos;
using Newtonsoft.Json;

namespace FourplacesApp
{
    public class RestService : IRestService
    {
        private readonly string serviceURI = "https://td-api.julienmialon.com";
        private readonly string _placesURI = "/places";
        private readonly string _rootURI = "/";
        private readonly string _loginURI = "/auth/login";
        private readonly string _loginRegisterURI = "/auth/register";
        private readonly string _loginRefreshURI = "/auth/refresh";
        private readonly string _meURI = "/me";
        private readonly string _patchPasswordURI = "/me/password";
        private readonly string _imagesURI = "/images";
        private readonly string _commentsURI = "/comments";//a mettre apres service uri/placesURI/{id}/
        private HttpClient client;
        private LoginResult Tokens { get; set; }
        public LoginRequest LoginUser { set; get; }

        public RestService()
        {
            client = new HttpClient();
            Tokens = new LoginResult();
            LoginUser = new LoginRequest();
        }

        public LoginResult Token => Tokens;
        public async Task GetRoot()
        {
            Console.WriteLine("GetRoot");
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
            Console.WriteLine("RS GetListPlacesAsync");
            client = new HttpClient();
            List<PlaceItemSummary> toRet = new List<PlaceItemSummary>();

            var uri = new Uri(string.Format(this.serviceURI + this._placesURI, string.Empty));

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Tokens.TokenType, Tokens.AccessToken);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    toRet = JsonConvert.DeserializeObject<Response<List<PlaceItemSummary>>>(content).Data;               
                    }
            }
            catch (Exception ex) { Console.WriteLine("EROORRR " + ex.Message); }

            foreach(PlaceItemSummary place in toRet)
            {
                place.ImageSourceURL = GetImage(place.ImageId);
            }

            return toRet;

        }

        public async Task<LoginResult> Signin(RegisterRequest user)
        {
            Console.WriteLine("RS Signin");
            Response<LoginResult> toks = null;
            string tmp = string.Format(this.serviceURI + this._loginRegisterURI, string.Empty);

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
            LoginUser.Password = user.Password;
            LoginUser.Email = user.Email;
            return Tokens;
        }

        public async Task<bool> Login(LoginRequest log_user)
        {
            Console.WriteLine("RS Login");
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

        internal async Task RefreshToken()
        {
            Console.WriteLine("RefreshToken");
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
            Console.WriteLine("GetMe");
            await this.RefreshToken();
            UserItem toRet = null;
            var uri = new Uri(string.Format(this.serviceURI + this._meURI, string.Empty));
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Tokens.TokenType, Tokens.AccessToken);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    toRet = JsonConvert.DeserializeObject<Response<UserItem>>(content).Data;
                }
            }
            catch (Exception ex) { Console.WriteLine("EROORRR " + ex.Message); }



            return toRet;

        }


        public async Task<UserItem> PatchMe(UpdateProfileRequest patch_user)
        {
            Console.WriteLine("PatchMe");
            //TOKEN
            await this.RefreshToken();
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Tokens.TokenType, Tokens.AccessToken);
            var uri = new Uri(string.Format(this.serviceURI + this._meURI, string.Empty));
            var json = JsonConvert.SerializeObject(patch_user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpRequestMessage message = new HttpRequestMessage(new HttpMethod("PATCH"), uri)
            {
                Content = content
            };
            var response = await client.SendAsync(message);

            UserItem retour = null;
            if (response.IsSuccessStatusCode)
            {

                var rep = await response.Content.ReadAsStringAsync();
                retour = JsonConvert.DeserializeObject<UserItem>(rep);

            }
            return retour;


        }

        public async Task<UserItem> PatchPassword(string updatePassword)
        {

            Console.WriteLine("PatchPassword");
            UpdatePasswordRequest nouveaupwd = new UpdatePasswordRequest
            {
                OldPassword = LoginUser.Password,
                NewPassword = updatePassword
            };

            //TOKEN
            await this.RefreshToken();
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Tokens.TokenType, Tokens.AccessToken);
            var uri = new Uri(string.Format(this.serviceURI + this._meURI + this._patchPasswordURI, string.Empty));
            var json = JsonConvert.SerializeObject(nouveaupwd);
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            HttpRequestMessage message = new HttpRequestMessage(new HttpMethod("PATCH"), uri)
            {
                Content = content
            };
            var response = await client.SendAsync(message);

            UserItem retour = null;
            if (response.IsSuccessStatusCode)
            {

                var rep = await response.Content.ReadAsStringAsync();
                retour = JsonConvert.DeserializeObject<UserItem>(rep);
                LoginUser.Password = nouveaupwd.NewPassword;
                /*On met a jour les Token au cas ou le pswd est lié */
                await Login(LoginUser);
            }
            return retour;
        }
        public string GetImage(int idImg)
        {
            return serviceURI + _imagesURI + "/" + idImg;
        }

        public async Task<Response> PostPlaceAsync(CreatePlaceRequest placeRequest)
        {
            Console.WriteLine("PostPlaceAsync");

            await this.RefreshToken();

            Response retour = null;
            string tmp = string.Format(this.serviceURI + this._loginRegisterURI, string.Empty);

            var uri = new Uri(tmp);

            var json = JsonConvert.SerializeObject(placeRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Tokens.TokenType, Tokens.AccessToken);
            HttpResponseMessage response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {

                var rep = await response.Content.ReadAsStringAsync();
                retour = JsonConvert.DeserializeObject<Response>(rep);

            }
            return retour;



        }

        public async Task<PlaceItem> GetPlace(int idPlace)
        {
            Console.WriteLine("GetPlace");
            client = new HttpClient();
            var uri = new Uri(string.Format(this.serviceURI + this._placesURI + "/" + idPlace, string.Empty));
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
            Console.WriteLine("PostCommentAsync");
            await this.RefreshToken();
            client = new HttpClient();
            var uri = new Uri(string.Format(this.serviceURI + this._placesURI + "/" + idPlace + this._commentsURI, string.Empty));

            // client.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer", Tokens.AccessToken);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Tokens.TokenType, Tokens.AccessToken);
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
