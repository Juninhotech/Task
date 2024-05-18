using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Task.Model
{
    public class QuestionType
    {
        [Key]
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
    }
}
