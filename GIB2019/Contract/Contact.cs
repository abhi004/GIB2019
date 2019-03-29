using GIB2019.Methods;
using Newtonsoft.Json;

namespace GIB2019.Contract
{
    [JsonConverter(typeof(GraphFeedConvertor))]
  public  class Contact :EntityBase<string>
    {

        [JsonProperty(PropertyName = "firstname[0]._value")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "lastname[0]._value")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "age[0]._value")]
        public int Age { get; set; }
        [JsonProperty(PropertyName = "title[0]._value")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "country[0]._value")]
        public string Country { get; set; }
        [JsonProperty(PropertyName = "city[0]._value")]
        public string City { get; set; }

    }
}
