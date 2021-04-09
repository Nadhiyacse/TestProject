using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Common
{
    public class CostsTabData
    {
        [JsonProperty("vendorAdjustments")]
        public List<Adjustment> VendorAdjustments { get; set; }
        [JsonProperty("clientAdjustments")]
        public List<Adjustment> ClientAdjustments { get; set; }
        [JsonProperty("costSummaries")]
        public List<CostSummary> CostSummaries { get; set; }
        [JsonProperty("cost")]
        public Cost Cost { get; set; }
    }

    public class Cost
    {
        [JsonProperty("agency")]
        public string Agency { get; set; }
        [JsonProperty("client")]
        public string Client { get; set; }
    }

    public class Adjustment
    {
        [JsonProperty("adjustmentName")]
        public string AdjustmentName { get; set; }
        [JsonProperty("baseCost")]
        public bool BaseCost { get; set; }
        [JsonProperty("rate")]
        public decimal Rate { get; set; }
        [JsonProperty("cost")]
        public decimal Cost { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class CostSummary
    {
        [JsonProperty("costSummaryName")]
        public string CostSummaryName { get; set; }
        [JsonProperty("agency")]
        public string Agency { get; set; }
        [JsonProperty("client")]
        public string Client { get; set; }
    }
}