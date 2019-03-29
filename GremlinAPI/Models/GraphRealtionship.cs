using Newtonsoft.Json;
using System.ComponentModel;

namespace GremlinAPI.Models
{
    public class GraphRealtionship <T>
    {
        [JsonProperty(PropertyName = "SourceNode")]
        public T SourceNode { get; set; }

        [JsonProperty(PropertyName = "DestinationNode")]
        public T DestinationNode { get; set; }

        [JsonProperty(PropertyName = "EnumConstant", Required = Required.AllowNull)]
        public GraphRelationship EnumConstant { get; set; }

        public enum GraphRelationship
        {
            [Description("KNOWS")]
            KNOWS,
            [Description("WORK_WITH")]
            WORK_WITH,
            [Description("MANAGES")]
            MANAGES
        }
    }
}
