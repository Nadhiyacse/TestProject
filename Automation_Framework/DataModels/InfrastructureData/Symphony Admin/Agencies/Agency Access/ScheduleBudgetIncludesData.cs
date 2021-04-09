using System.Collections.Generic;
using Automation_Framework.DataModels.CommonData;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies.Agency_Access
{
    public class ScheduleBudgetIncludesData
    {
        [JsonProperty("base")]
        public string Base { get; set; }
        [JsonProperty("additions")]
        public List<Checkbox> Additions { get; set; }
    }
}
