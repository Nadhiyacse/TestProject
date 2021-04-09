using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.Billing
{
    public class CustomBillingItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("billedGoal")]
        public string BilledGoal { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("customBillingMonths")]
        public List<CustomBillingMonthData> CustomBillingMonths { get; set; }
        [JsonProperty("plannedTotals")]
        public string PlannedTotals { get; set; }
        [JsonProperty("firstPartyTotals")]
        public string FirstPartyTotals { get; set; }
        [JsonProperty("actualTotals")]
        public string ActualTotals { get; set; }
    }
}
