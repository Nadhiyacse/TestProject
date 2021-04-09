using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Publishers
{
    public class LocationFormatMappingData
    {
        [JsonProperty("site")]
        public string Site { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("format")]
        public string Format { get; set; }
    }
}
