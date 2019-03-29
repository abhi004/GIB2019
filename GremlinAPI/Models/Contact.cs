
using GremlinAPI.Convertor;
using Newtonsoft.Json;

namespace GremlinAPI.Models
{

    [JsonConverter(typeof(GremlinGraphResponseConvertor))]
    public class Contact : EntityBase<string>
    {
        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "age")]
        public int Age { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
    }
}
