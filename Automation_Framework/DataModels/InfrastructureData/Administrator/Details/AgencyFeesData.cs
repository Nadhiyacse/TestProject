using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Administrator.Details
{
    public class AgencyFeesData
    {
        [JsonProperty("defaultFees")]
        public List<DefaultFeesData> DefaultFeesData { get; set; }
        [JsonProperty("allowAgencyFeeRateCostOverride")]
        public bool AllowAgencyFeeRateCostOverride { get; set; }
        [JsonProperty("allowAgencyFeeBaseOverride")]
        public bool AllowAgencyFeeBaseOverride { get; set; }
    }
}
