using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.Campaign
{
    public class CampaignBudgetData
    {
        [JsonProperty("budgetData")]
        public List<BudgetData> BudgetData { get; set; }
    }

    public class BudgetData
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("purchaseOrder")]
        public string PurchaseOrder { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("onlineMediaBudget")]
        public string OnlineMediaBudget { get; set; }
    }
}