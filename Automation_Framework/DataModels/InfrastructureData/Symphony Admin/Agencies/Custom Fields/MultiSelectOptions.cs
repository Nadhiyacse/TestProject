using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies.Custom_Fields
{
    public class MultiSelectOptions
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
