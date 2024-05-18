using Newtonsoft.Json;
using Task.Helper;

namespace Task.DTO
{
    public class UserDTO
    {

        [JsonProperty("firstname")]
        public string Firstname { get; set; }
        [JsonProperty("lastname")]

        public string LastName { get; set; }
        [JsonProperty("email")]

        public string Email { get; set; }
        [JsonProperty("phone")]

        public string Phone { get; set; }
        [JsonProperty("nationality")]

        public string Nationality { get; set; }
        [JsonProperty("currentResidence")]

        public string CurrentResidence { get; set; }
        [JsonProperty("idNumber")]

        public string IdNumber { get; set; }
        [JsonProperty("dateOfBirth")]

        public DateTime DateOfBirth { get; set; }
        [JsonProperty("gender")]

        public string Gender { get; set; }

        public List<AdditionalQ> Questions { get; set; }
    }
}
