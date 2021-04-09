using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Integration.DataMapping
{
    public class VendorData
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("fields")]
        public List<Field> Fields { get; set; }
    }
}
