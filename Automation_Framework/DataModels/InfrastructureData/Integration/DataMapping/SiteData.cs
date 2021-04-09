using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Integration.DataMapping
{
    public class SiteData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("fields")]
        public List<Field> Fields { get; set; }
    }
}
