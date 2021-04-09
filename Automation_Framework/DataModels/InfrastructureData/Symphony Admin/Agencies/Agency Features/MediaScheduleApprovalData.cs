using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies.Agency_Features
{
    public class MediaScheduleApprovalData
    {
        [JsonProperty("enableApprovalWorkflow")]
        public bool EnableApprovalWorkflow { get; set; }
        [JsonProperty("isApprovalActionsEnabled")]
        public bool IsApprovalActionsEnabled { get; set; }
        [JsonProperty("exportDocument")]
        public string ExportDocument { get; set; }
        [JsonProperty("enableContactEmailNotification")]
        public bool EnableContactEmailNotification { get; set; }
        [JsonProperty("requireDocumentUpload")]
        public bool RequireDocumentUpload { get; set; }
        [JsonProperty("actionsNotRequiringReapproval")]
        public List<ActionsNotRequiringReapproval> ActionsNotRequiringReapproval { get; set; }
    }
}