using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Integration.DataMapping
{
    public class ExternalApplicationData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("types")]
        public List<Type> Types { get; set; }
    }
}
