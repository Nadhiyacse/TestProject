using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.Campaign
{
    public class CampaignExchangeRatesData
    {
        [JsonProperty("exchangeRateData")]
        public List<ExchangeRateData> ExchangeRateData { get; set; }
    }

    public class ExchangeRateData
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("baseCurrency")]
        public string BaseCurrency { get; set; }
        [JsonProperty("exchangeCurrency")]
        public string ExchangeCurrency { get; set; }
        [JsonProperty("rate")]
        public string Rate { get; set; }
    }
}