using Newtonsoft.Json;

namespace Automation_Framework.DataModels.CommonData
{
    public class Checkbox
    {
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("metadata")]
        public string Metadata { get; set; }
    }
}