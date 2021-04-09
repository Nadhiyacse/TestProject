using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies.Agency_Features
{
    public class ActionsNotRequiringReapproval
    {
        [JsonProperty("tab")]
        public string Tab { get; set; }
        [JsonProperty("elements")]
        public List<string> Elements { get; set; }
    }
}
