using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.Trafficking
{
    public class TraffickingData
    {
        [JsonProperty("adServer")]
        public string AdServer { get; set; }
        [JsonProperty("visibleItemCount")]
        public int VisibleItemCount { get; set; }
    }
}
