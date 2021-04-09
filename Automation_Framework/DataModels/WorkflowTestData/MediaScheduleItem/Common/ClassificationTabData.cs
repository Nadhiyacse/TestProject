using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Common
{
    public class ClassificationTabData
    {
        [JsonProperty("classifications")]
        public List<ClassificationItem> Classifications { get; set; }
    }

    public class ClassificationItem
    {
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("option")]
        public string Option { get; set; }
    }
}