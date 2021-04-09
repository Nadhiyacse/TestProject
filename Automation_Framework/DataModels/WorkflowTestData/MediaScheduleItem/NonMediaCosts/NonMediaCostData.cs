using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Common;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.NonMediaCosts
{
    public class NonMediaCostData
    {
        [JsonProperty("editNonMediaCostData")]
        public EditNonMediaCostData EditNonMediaCostData { get; set; }
        [JsonProperty("costsTabData")]
        public CostsTabData CostsTabData { get; set; }
        [JsonProperty("classificationsTabData")]
        public ClassificationTabData ClassificationsTabData { get; set; }
        [JsonProperty("isCancelled")]
        public bool IsCancelled { get; set; }
        [JsonProperty("isDuplicateItem")]
        public bool IsDuplicateItem { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("startDate")]
        public string StartDate { get; set; }
        [JsonProperty("endDate")]
        public string EndDate { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("vendor")]
        public string Vendor { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("agencyCost")]
        public string AgencyCost { get; set; }
        [JsonProperty("purchaseOrderNumber")]
        public string PurchaseOrderNumber { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}
