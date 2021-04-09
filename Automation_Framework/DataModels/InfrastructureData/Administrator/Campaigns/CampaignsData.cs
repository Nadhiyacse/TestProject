using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Administrator.Campaigns
{
    public class CampaignsData
    {
        [JsonProperty("parentCampaignsData")]
        public List<ParentCampaignsData> ParentCampaignsData { get; set; }
    }
}
