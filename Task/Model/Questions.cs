using Newtonsoft.Json;

namespace Task.Model
{
    public class Questions
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("question")]
        public string Question { get; set; }
    }
}
