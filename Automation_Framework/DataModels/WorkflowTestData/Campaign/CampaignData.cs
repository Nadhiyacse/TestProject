using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.Campaign
{
    public class CampaignData
    {
        [JsonProperty("detailsData")]
        public CampaignDetailsData DetailsData { get; set; }
        [JsonProperty("exchangeRatesData")]
        public CampaignExchangeRatesData ExchangeRatesData { get; set; }
        [JsonProperty("budgetData")]
        public CampaignBudgetData BudgetData { get; set; }
        [JsonProperty("strategyData")]
        public CampaignStrategyData StrategyData { get; set; }
    }
}