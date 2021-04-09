using System.Collections.Generic;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies.Classifications;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies
{
    public class ClassificationData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("technicalName")]
        public string TechnicalName { get; set; }
        [JsonProperty("isMandatory")]
        public bool IsMandatory { get; set; }
        [JsonProperty("isAppliedtoFutureClients")]
        public bool IsAppliedtoFutureClients { get; set; }
        [JsonProperty("isAppliedToCurrentClients")]
        public bool IsAppliedToCurrentClients { get; set; }
        [JsonProperty("displayStyle")]
        public string DisplayStyle { get; set; }
        [JsonProperty("subcategoryLabel")]
        public string SubcategoryLabel { get; set; }
        [JsonProperty("groups")]
        public List<GroupItem> Groups { get; set; }
    }
}