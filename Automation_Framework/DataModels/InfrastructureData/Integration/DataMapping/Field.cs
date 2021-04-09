using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Integration.DataMapping
{
    public class Field
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
