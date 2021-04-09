using System.Collections.Generic;
using Automation_Framework.DataModels.CommonData;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies.Agency_Features
{
    public class EmailNotificationsData
    {
        [JsonProperty("role")]
        public string Role { get; set; }
        [JsonProperty("roleNotifications")]
        public List<Checkbox> RoleNotifications { get; set; }
    }
}