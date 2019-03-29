using Newtonsoft.Json;


namespace GIB2019.Contract
{
  public abstract class  EntityBase<TId>
    {
        [JsonProperty(PropertyName = "id")]
        public TId Id { get; set; }
        [JsonProperty(PropertyName = "name[0]._value")]
        public string Name { get; set; }
    }
}
