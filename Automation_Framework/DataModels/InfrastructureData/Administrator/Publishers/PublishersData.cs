using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Administrator.Publishers
{
    public class PublishersData
    {
        [JsonProperty("ratecard")]
        public List<RatecardData> RatecardData { get; set; }
    }
}
