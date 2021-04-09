using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Integration.DataMapping
{
    public class Publisher
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("publisher")]
        public string PublisherName { get; set; }
        [JsonProperty("sites")]
        public List<SiteData> Sites { get; set; }
    }
}
