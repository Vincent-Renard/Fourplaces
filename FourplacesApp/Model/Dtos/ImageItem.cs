using Newtonsoft.Json;

namespace Model.Dtos
{
    public class ImageItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}