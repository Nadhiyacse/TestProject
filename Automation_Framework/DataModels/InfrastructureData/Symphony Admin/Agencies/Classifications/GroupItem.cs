using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies.Classifications
{
    public class GroupItem
    {
        [JsonProperty("groupName")]
        public string GroupName { get; set; }
        [JsonProperty("items")]
        public List<string> Items { get; set; }
    }
}
