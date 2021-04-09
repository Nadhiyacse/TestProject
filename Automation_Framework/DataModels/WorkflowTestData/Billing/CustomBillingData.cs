using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.Billing
{
    public class CustomBillingData
    {
        [JsonProperty("costSources")]
        public string CostSources { get; set; }
        [JsonProperty("currencyType")]
        public string CurrencyType { get; set; }
        [JsonProperty("customBillingItems")]
        public List<CustomBillingItem> CustomBillingItems { get; set; }
        [JsonProperty("summaryPlannedTotals")]
        public string SummaryPlannedTotals { get; set; }
        [JsonProperty("summaryFirstPartyTotals")]
        public string SummaryFirstPartyTotals { get; set; }
        [JsonProperty("summaryActualTotals")]
        public string SummaryActualTotals { get; set; }
    }
}
