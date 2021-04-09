using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.NonMediaCosts
{
    public class EditNonMediaCostData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("startDate")]
        public string StartDate { get; set; }
        [JsonProperty("endDate")]
        public string EndDate { get; set; }
        [JsonProperty("agencyCost")]
        public string AgencyCost { get; set; }
        [JsonProperty("purchaseOrderNumber")]
        public string PurchaseOrderNumber { get; set; }
    }
}
