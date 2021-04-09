using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Integration.DataMapping
{
    public class MappingData
    {
        [JsonProperty("externalApplications")]
        public List<ExternalApplicationData> ExternalApplications { get; set; }
    }
}
