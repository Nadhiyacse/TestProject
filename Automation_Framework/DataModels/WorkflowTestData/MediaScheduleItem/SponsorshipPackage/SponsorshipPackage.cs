using System.Collections.Generic;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Common;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.SponsorshipPackage
{
    public class SponsorshipPackage
    {
        [JsonProperty("isImportedItem")]
        public bool IsImportedItem { get; set; }
        [JsonProperty("sponsorshipPackageName")]
        public string SponsorshipPackageName { get; set; }
        [JsonProperty("publisher")]
        public string Publisher { get; set; }
        [JsonProperty("isDuplicateReplaceItem")]
        public bool IsDuplicateReplaceItem { get; set; }
        [JsonProperty("singlePlacements")]
        public List<SinglePlacement.SinglePlacement> SinglePlacements { get; set; }
        [JsonProperty("editData")]
        public EditData EditData { get; set; }
    }
}