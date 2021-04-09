using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Administrator.Details
{
    public class DefaultFeesData
    {
        [JsonProperty("client")]
        public string Client { get; set; }
        [JsonProperty("product")]
        public string Product { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("publisher")]
        public string Publisher { get; set; }
        [JsonProperty("site")]
        public string Site { get; set; }
        [JsonProperty("costBreakdownType")]
        public string CostBreakdownType { get; set; }
        [JsonProperty("feePercentage")]
        public string FeePercentage { get; set; }
    }
}
