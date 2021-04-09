using System.Collections.Generic;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin
{
    public class SymphonyAdminAgencyData
    {
        [JsonProperty("agencies")]
        public List<AgencyData> Agencies { get; set; }
    }
}