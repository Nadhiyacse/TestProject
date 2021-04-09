using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Common;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.SinglePlacement
{
    public class SinglePlacement
    {
        [JsonProperty("isAutomatedGuaranteedItem")]
        public bool IsAutomatedGuaranteedItem { get; set; }
        [JsonProperty("isItemGoingToConvertFromRfpToAg")]
        public bool IsItemGoingToConvertFromRfpToAg { get; set; }
        [JsonProperty("isImportedItem")]
        public bool IsImportedItem { get; set; }
        [JsonProperty("isDuplicateReplaceItem")]
        public bool IsDuplicateReplaceItem { get; set; }
        [JsonProperty("isCancelledItem")]
        public bool IsCancelledItem { get; set; }
        [JsonProperty("editData")]
        public EditData EditData { get; set; }
        [JsonProperty("inventoryProvider")]
        public string InventoryProvider { get; set; }
        [JsonProperty("detailTabData")]
        public DetailTabData DetailTabData { get; set; }
        [JsonProperty("classificationsTabData")]
        public ClassificationTabData ClassificationsTabData { get; set; }
        [JsonProperty("flightingTabData")]
        public FlightingTabData FlightingTabData { get; set; }
        [JsonProperty("trafficTabData")]
        public TrafficTabData TrafficTabData { get; set; }
        [JsonProperty("costsTabData")]
        public CostsTabData CostsTabData { get; set; }
        [JsonProperty("forecastTabData")]
        public ForecastTabData ForecastTabData { get; set; }
        [JsonProperty("moreTabData")]
        public MoreTabData MoreTabData { get; set; }
    }
}