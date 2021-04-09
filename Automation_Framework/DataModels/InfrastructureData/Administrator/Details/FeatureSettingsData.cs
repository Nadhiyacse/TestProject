using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Administrator.Details
{
    public class FeatureSettingsData
    {
        [JsonProperty("agencyFees")]
        public AgencyFeesData AgencyFeesData { get; set; }
        [JsonProperty("costAdjustments")]
        public List<CostAdjustmentsData> CostAdjustmentsData { get; set; }
    }
}
