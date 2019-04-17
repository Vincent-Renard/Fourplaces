using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Dtos;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;

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
        public LoginRequest LoginUser { get; set; }
        public UserItem UserItem { get; set; }


        public RestService()
        {
            client = new HttpClient();
            Tokens = new LoginResult();
            LoginUser = new LoginRequest();
            UserItem = new UserItem();
        }
        public async Task GetRoot()
        {
            Console.WriteLine("RS GetRoot");
            var uri = new Uri(string.Format(this.serviceURI + _rootURI, string.Empty));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
        }

     
        public async Task<int> PostImgAsync(MediaFile pic)
        {
            Console.WriteLine("RS PostImgAsync");
            client = new HttpClient();
            //byte[] imageData = await client.GetByteArrayAsync("https://bnetcmsus-a.akamaihd.net/cms/blog_header/x6/X6KQ96B3LHMY1551140875276.jpg");

            //byte[] imageData = File.ReadAllBytes(pic.Path);
            var memoryStream = new MemoryStream();
            pic.GetStream().CopyTo(memoryStream);
            byte[] imageData = memoryStream.ToArray();
          

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://td-api.julienmialon.com/images");
            request.Headers.Authorization = new AuthenticationHeaderValue(Tokens.TokenType, Tokens.AccessToken);

            MultipartFormDataContent requestContent = new MultipartFormDataContent();

            var imageContent = new ByteArrayContent(imageData);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

            // Le deuxième paramètre doit absolument être "file" ici sinon ça ne fonctionnera pas
            requestContent.Add(imageContent, "file", "file.jpg");

            request.Content = requestContent;

            HttpResponseMessage response = await client.SendAsync(request);

            string result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) {
                ImageItem toRet = JsonConvert.DeserializeObject<Response<ImageItem>>(result).Data;
                Console.WriteLine("Image uploaded! ");
                return toRet.Id;
            }
            else
            {
                Console.WriteLine(response.ReasonPhrase);
                Console.WriteLine("Image NOT uploaded! ");
                return 0;
            }
        }
        public async Task<List<PlaceItemSummary>> GetListPlacesAsync()
        {
            Console.WriteLine("RS GetListPlacesAsync");
            client = new HttpClient();
            List<PlaceItemSummary> toRet = new List<PlaceItemSummary>();

            var uri = new Uri(string.Format(this.serviceURI + _placesURI, string.Empty));

            try
            {

                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    toRet = JsonConvert.DeserializeObject<Response<List<PlaceItemSummary>>>(content).Data;
                }
            }
            catch (Exception ex) { Console.WriteLine("EROORRR " + ex.Message); }

            foreach (PlaceItemSummary place in toRet)
            {
                place.ImageSourceURL = GetImage(place.ImageId);
            }

            return toRet;

        }

        public async Task<LoginResult> Signin(RegisterRequest user)
        {
            Console.WriteLine("RS Signin");
            string tmp = string.Format(serviceURI + _loginRegisterURI, string.Empty);

            var uri = new Uri(tmp);

            string json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            client = new HttpClient();
            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {

                string rep = await response.Content.ReadAsStringAsync();
                Response<LoginResult> toks = JsonConvert.DeserializeObject<Response<LoginResult>>(rep);
                Tokens = toks.Data;
            }
            else Console.WriteLine("EROORRR Sign   ");



            return Tokens;
        }

        public async Task<bool> Login(LoginRequest log_user)
        {
            Console.WriteLine("RS Login");
            var uri = new Uri(string.Format(this.serviceURI + this._loginURI, string.Empty));
            var json = JsonConvert.SerializeObject(log_user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string rep = await response.Content.ReadAsStringAsync();
                Tokens = JsonConvert.DeserializeObject<Response<LoginResult>>(rep).Data;
            }
            else
            {
                return false;
            }
            LoginUser = log_user;
            UserItem = await GetMe();

            return true;
        }

        internal async Task RefreshToken()
        {
            Console.WriteLine("RS RefreshToken");
            RefreshRequest request = new RefreshRequest
            {
                RefreshToken = Tokens.RefreshToken
            };
            Response<LoginResult> toks = null;
            var uri = new Uri(string.Format(this.serviceURI + this._loginRefreshURI, string.Empty));
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var rep = await response.Content.ReadAsStringAsync();
                toks = JsonConvert.DeserializeObject<Response<LoginResult>>(rep);
            }
            Tokens = toks.Data;

        }
        public async Task<UserItem> GetMe()
        {
            Console.WriteLine("RS GetMe");
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
                    if (toRet.ImageId != null)
                    {
                        toRet.Image = GetImage(toRet.ImageId);
                    }
                   
                }
            }
            catch (Exception ex) { Console.WriteLine("EROORRR " + ex.Message); }

            UserItem = toRet;
            return toRet;

        }


        public async Task<UserItem> PatchMe(UpdateProfileRequest patch_user)
        {
            Console.WriteLine("RS PatchMe");
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

            UserItem = retour;
            return retour;


        }

        public async Task<UserItem> PatchPassword(string updatePassword)
        {

            Console.WriteLine("RS PatchPassword");
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
        public string GetImage(int? idImg)
        {
            return serviceURI + _imagesURI + "/" + idImg;
        }

        public async Task<Response> PostPlaceAsync(CreatePlaceRequest placeRequest)
        {
            Console.WriteLine("RS PostPlaceAsync");
            Console.WriteLine("T " + placeRequest.Title);
            Console.WriteLine("D " + placeRequest.Description);
            Console.WriteLine("L " + placeRequest.Longitude);
            Console.WriteLine("l " + placeRequest.Latitude);
            Console.WriteLine("imgid "+placeRequest.ImageId);
            await RefreshToken();

            Response retour = null;
            string tmp = string.Format(serviceURI + _placesURI, string.Empty);
            Console.WriteLine("url: "+tmp);
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
                Console.WriteLine("Place envoyée");
            }
            else
            {
                Console.WriteLine("Place PAS envoyée");
                Console.WriteLine(response.ReasonPhrase);
            }

            return retour;



        }

        public async Task<PlaceItem> GetPlace(int idPlace)
        {
            Console.WriteLine("RS GetPlace "+idPlace);
            client = new HttpClient();
            var uri = new Uri(string.Format(serviceURI + _placesURI + "/" + idPlace, string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);
            PlaceItem place = new PlaceItem();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                place = JsonConvert.DeserializeObject<Response<PlaceItem>>(content).Data;
            }
            place.ImageSourceURL = GetImage(place.ImageId);
            return place;

        }


        public async Task<Response> PostCommentAsync(int idPlace, CreateCommentRequest commentRequest)
        {
            Console.WriteLine("RS PostCommentAsync");
            await RefreshToken();
            client = new HttpClient();
            Uri uri = new Uri(string.Format(serviceURI + _placesURI + "/" + idPlace + _commentsURI, string.Empty));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Tokens.TokenType, Tokens.AccessToken);
            var json = JsonConvert.SerializeObject(commentRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(uri, content);
            var r = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<Response>(r);

            return resp;
        }
    }
}
