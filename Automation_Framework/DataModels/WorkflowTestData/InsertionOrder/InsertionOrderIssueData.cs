using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.InsertionOrder
{
    public class InsertionOrderIssueData
    {
        [JsonProperty("agencyBuyer")]
        public string AgencyBuyer { get; set; }
        [JsonProperty("agencyContact")]
        public string AgencyContact { get; set; }
        [JsonProperty("publisherContact")]
        public string PublisherContact { get; set; }
        [JsonProperty("uploadDocuments")]
        public string UploadDocuments { get; set; }
        [JsonProperty("isClientApproved")]
        public bool IsClientApproved { get; set; }
    }
}
