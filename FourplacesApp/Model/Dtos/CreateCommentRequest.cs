using Newtonsoft.Json;

namespace Model.Dtos
{
	public class CreateCommentRequest
	{
		[JsonProperty("text")]
		public string Text { get; set; }
	}
}