using System.Collections.Generic;
using Automation_Framework.DataModels.InfrastructureData.Administrator.Campaigns;
using Automation_Framework.DataModels.InfrastructureData.Administrator.Details;
using Automation_Framework.DataModels.InfrastructureData.Administrator.Publishers;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Administrator
{
    public class AgencyAdministratorData
    {
        [JsonProperty("featureSettings")]
        public FeatureSettingsData FeatureSettingsData { get; set; }
        [JsonProperty("clients")]
        public List<ClientData> Clients { get; set; }
        [JsonProperty("access")]
        public AccessData Access { get; set; }
        [JsonProperty("ioAdmin")]
        public IOAdminData IOAdminData { get; set; }
        [JsonProperty("campaigns")]
        public CampaignsData Campaigns { get; set; }
        [JsonProperty("publishers")]
        public PublishersData PublishersData { get; set; }
    }
}
