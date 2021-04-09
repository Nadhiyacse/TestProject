using System.Collections.Generic;
using Automation_Framework.DataModels.CommonData;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Administrator
{
    public class ClientData
    {
        [JsonProperty("clientName")]
        public string ClientName { get; set; }
        [JsonProperty("clientLogoName")]
        public string ClientLogoName { get; set; }
        [JsonProperty("countries")]
        public List<string> Countries { get; set; }
        [JsonProperty("clientContactMarketing")]
        public string ClientContactMarketing { get; set; }
        [JsonProperty("clientContactMarketingEmail")]
        public string ClientContactMarketingEmail { get; set; }
        [JsonProperty("customFields")]
        public List<CustomFieldData> CustomFields { get; set; }
    }
}
