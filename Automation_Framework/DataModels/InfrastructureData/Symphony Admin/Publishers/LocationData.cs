using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Publishers
{
    public class LocationData
    {
        [JsonProperty("site")]
        public string Site { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("isGlobalScope")]
        public bool IsGlobalScope { get; set; }
        [JsonProperty("agencyToScopeTo")]
        public string AgencyToScopeTo { get; set; }
    }
}
