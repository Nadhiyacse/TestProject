using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Common
{
    public class TrafficTabData
    {
        [JsonProperty("platform")]
        public string Platform { get; set; }
    }
}
