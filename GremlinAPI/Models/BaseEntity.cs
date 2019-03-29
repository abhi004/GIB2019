
namespace GremlinAPI.Models
{
    using Newtonsoft.Json;

    public abstract class EntityBase<TId>
    {
        [JsonProperty(PropertyName = "id")]
        public TId Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
