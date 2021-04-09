using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies.Custom_Fields
{
    public class Resolution
    {
        [JsonProperty("width")]
        public string Width { get; set; }
        [JsonProperty("height")]
        public string Height { get; set; }
    }
}
