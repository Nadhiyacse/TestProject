using System.Collections.Generic;
using Automation_Framework.DataModels.CommonData;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.FeatureToggles;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Global;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Publishers;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Vendors;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin
{
    public class SymphonyAdminGenericData
    {
        [JsonProperty("publishers")]
        public List<PublisherData> Publishers { get; set; }
        [JsonProperty("vendors")]
        public List<VendorData> Vendors { get; set; }
        [JsonProperty("featureToggles")]
        public List<FeatureTogglesData> FeatureToggles { get; set; }
        [JsonProperty("globalCustomFields")]
        public List<GlobalCustomFieldData> GlobalCustomFields { get; set; }
    }
}