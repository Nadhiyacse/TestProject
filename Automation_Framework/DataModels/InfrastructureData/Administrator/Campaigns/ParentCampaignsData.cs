using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Administrator.Campaigns
{
    public class ParentCampaignsData
    {
        [JsonProperty("client")]
        public string Client { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
