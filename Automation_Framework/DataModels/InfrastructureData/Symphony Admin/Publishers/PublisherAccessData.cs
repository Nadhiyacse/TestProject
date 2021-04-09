using System.Collections.Generic;
using Automation_Framework.DataModels.CommonData;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Publishers
{
    public class PublisherAccessData
    {
        [JsonProperty("mediaScheduleExports")]
        public List<AccessControlledItem> MediaScheduleExports { get; set; }
        [JsonProperty("insertionOrderExports")]
        public List<AccessControlledItem> InsertionOrderExports { get; set; }
        [JsonProperty("languages")]
        public List<AccessControlledItem> Languages { get; set; }
    }
}