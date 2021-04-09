using System.Collections.Generic;
using Automation_Framework.DataModels.CommonData;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Common;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.PerformancePackage
{
    public class PlacementsTabData
    {
        [JsonProperty("placements")]
        public List<PlacementData> Placements { get; set; }
    }

    public class PlacementData
    {
        [JsonProperty("placementName")]
        public string PlacementName { get; set; }
        [JsonProperty("autoGenerateName")]
        public bool AutoGenerateName { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("format")]
        public string Format { get; set; }
        [JsonProperty("creativeType")]
        public string CreativeType { get; set; }
        [JsonProperty("package")]
        public string Package { get; set; }
        [JsonProperty("shareOfVoice")]
        public string ShareOfVoice { get; set; }
        [JsonProperty("devices")]
        public List<string> Devices { get; set; }
        [JsonProperty("landingPage")]
        public string LandingPage { get; set; }
        [JsonProperty("generalComments")]
        public string GeneralComments { get; set; }
        [JsonProperty("numOfExecutions")]
        public string NumOfExecutions { get; set; }
        [JsonProperty("productionComments")]
        public string ProductionComments { get; set; }
        [JsonProperty("customFields")]
        public List<CustomFieldData> CustomFields { get; set; }
        [JsonProperty("editData")]
        public EditData EditData { get; set; }
        [JsonProperty("isDuplicateReplaceItem")]
        public bool isDuplicateReplaceItem { get; set; }
    }
}
