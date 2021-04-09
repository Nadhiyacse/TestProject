using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.Campaign
{
    public class CampaignApprovalData
    {
        [JsonProperty("uploadDocumentName")]
        public string UploadDocumentName { get; set; }
        [JsonProperty("approvers")]
        public List<string> Approvers { get; set; }
    }
}
