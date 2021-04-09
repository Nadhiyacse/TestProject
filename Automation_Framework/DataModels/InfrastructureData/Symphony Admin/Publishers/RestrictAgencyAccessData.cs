using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Publishers
{
    public class RestrictAgencyAccessData
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("agencies")]
        public List<string> Agencies { get; set; }
    }
}
