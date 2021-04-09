using Automation_Framework.DataModels.WorkflowTestData.Campaign;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Export;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem
{
    public class MediaScheduleData
    {
        [JsonProperty("mediaScheduleExportData")]
        public MediaScheduleExportData MediaScheduleExportData { get; set; }
        [JsonProperty("campaignApprovalData")]
        public CampaignApprovalData CampaignApprovalData { get; set; }
    }
}
