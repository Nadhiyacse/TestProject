using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.Billing
{
    public class CustomBillingMonthData
    {
        [JsonProperty("month")]
        public int Month { get; set; }
        [JsonProperty("isLastMonth")]
        public bool IsLastMonth { get; set; }
        [JsonProperty("planned")]
        public string Planned { get; set; }
        [JsonProperty("firstParty")]
        public string FirstParty { get; set; }
        [JsonProperty("actual")]
        public string Actual { get; set; }
    }
}
