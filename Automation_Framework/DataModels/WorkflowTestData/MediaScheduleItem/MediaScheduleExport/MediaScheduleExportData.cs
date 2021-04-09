using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Export
{
    public class MediaScheduleExportData
    {
        [JsonProperty("managerApprovalEmail")]
        public string ManagerApprovalEmail { get; set; }
    }
}
