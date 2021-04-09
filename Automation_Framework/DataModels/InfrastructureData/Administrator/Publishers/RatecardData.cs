using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Administrator.Publishers
{
    public class RatecardData
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("publisher")]
        public string Publisher { get; set; }
        [JsonProperty("importRatecardFileName")]
        public string ImportRatecardFileName { get; set; }
    }
}
