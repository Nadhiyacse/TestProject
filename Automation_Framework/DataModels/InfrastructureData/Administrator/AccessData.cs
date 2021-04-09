using System.Collections.Generic;
using Automation_Framework.DataModels.CommonData;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Administrator
{
    public class AccessData
    {
        [JsonProperty("mediaScheduleExports")]
        public List<AccessControlledItem> MediaScheduleExports { get; set; }
        [JsonProperty("insertionOrderExports")]
        public List<AccessControlledItem> InsertionOrderExports { get; set; }
        [JsonProperty("billingExports")]
        public List<AccessControlledItem> BillingExports { get; set; }
        [JsonProperty("traffickingExports")]
        public List<AccessControlledItem> TraffickingExports { get; set; }
        [JsonProperty("publisherExports")]
        public List<AccessControlledItem> PublisherExports { get; set; }
        [JsonProperty("dataMapping")]
        public List<AccessControlledItem> DataMapping { get; set; }
        [JsonProperty("thirdPartyAdServers")]
        public List<AccessControlledItem> ThirdPartyAdServers { get; set; }
        [JsonProperty("fourthPartyTracking")]
        public List<AccessControlledItem> FourthPartyTracking { get; set; }
        [JsonProperty("languages")]
        public List<AccessControlledItem> Languages { get; set; }
        [JsonProperty("mediaScheduleViews")]
        public List<AccessControlledItem> MediaScheduleViews { get; set; }
        [JsonProperty("purchaseTypes")]
        public List<AccessControlledItem> PurchaseTypes { get; set; }
        [JsonProperty("billingAllocations")]
        public List<AccessControlledItem> BillingAllocations { get; set; }
    }
}
