using Newtonsoft.Json;

namespace Model.Dtos
{
    public class RefreshRequest
    {
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}