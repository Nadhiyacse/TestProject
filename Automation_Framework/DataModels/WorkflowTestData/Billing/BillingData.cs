using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.Billing
{
    public class BillingData
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("billingItems")]
        public List<BillingItem> BillingItems { get; set; }
    }

    public class BillingItem 
    {
        [JsonProperty("publisher")]
        public string Publisher { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("plannedTotals")]
        public string PlannedTotals { get; set; }
        [JsonProperty("billingAllocationMethod")]
        public string BillingAllocationMethod { get; set; }
        [JsonProperty("isOverride")]
        public bool isOverride { get; set; }

        public bool Equals(BillingItem otherItem)
        {
            if (otherItem == null)
                return false;
                
            return Publisher == otherItem.Publisher && Currency == otherItem.Currency 
                && PlannedTotals == otherItem.PlannedTotals &&
                BillingAllocationMethod == otherItem.BillingAllocationMethod && isOverride == otherItem.isOverride;
        }
    }
}
