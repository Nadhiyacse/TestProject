using System.ComponentModel;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Administrator.Details
{
    public class CostAdjustmentsData
    {
        [JsonProperty("client")]
        public string Client { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("publisher")]
        public string Publisher { get; set; }
        [JsonProperty("adjustment")]
        public string Adjustment { get; set; }
        [JsonProperty("vendorRate")]
        public string VendorRate { get; set; }
        [DefaultValue(true)]
        [JsonProperty("allowUserOverrideVendorRate", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool AllowUserOverrideVendorRate { get; set; }
        [JsonProperty("clientRate")]
        public string ClientRate { get; set; }
        [DefaultValue(true)]
        [JsonProperty("allowUserOverrideClientRate", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool AllowUserOverrideClientRate { get; set; }
    }
}
