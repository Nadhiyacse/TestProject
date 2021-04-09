using System.Collections.Generic;
using Automation_Framework.DataModels.CommonData;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Common
{
    public class MoreTabData
    {
        [JsonProperty("purchaseOrder")]
        public string PurchaseOrder { get; set; }
        [JsonProperty("billingSource")]
        public string BillingSource { get; set; }
        [JsonProperty("exclusions")]
        public List<Checkbox> Exclusions { get; set; }
    }
}