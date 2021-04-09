using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies.Agency_Access
{
    public class InventoryProviderData
    {
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("provider")]
        public string Provider { get; set; }
        [JsonProperty("billingMethod")]
        public string BillingMethod { get; set; }
        [JsonProperty("termsAndConditions")]
        public string TermsAndCondition { get; set; }
    }
}
