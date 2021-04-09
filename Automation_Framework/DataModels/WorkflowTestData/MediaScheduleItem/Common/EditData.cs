using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Common
{
    public class EditData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("goal")]
        public string Goal { get; set; }
        [JsonProperty("rate")]
        public string Rate { get; set; }
    }
}
