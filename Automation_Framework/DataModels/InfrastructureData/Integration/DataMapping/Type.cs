using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Integration.DataMapping
{
    public class Type
    {
        [JsonProperty("type")]
        public string TypeName { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("fields")]
        public List<Field> Fields { get; set; }
        [JsonProperty("clients")]
        public List<ClientData> Clients { get; set; }
        [JsonProperty("publishers")]
        public List<Publisher> Publishers { get; set; }
        [JsonProperty("vendors")]
        public List<VendorData> Vendors { get; set; }
    }
}
