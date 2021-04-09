using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Publishers
{
    public class FormatData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("isGlobalScope")]
        public bool IsGlobalScope { get; set; }
        [JsonProperty("agencyToScopeTo")]
        public string AgencyToScopeTo { get; set; }
    }
}
