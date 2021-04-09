using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies
{
    public class CustomLabelData
    {
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("customLabelOverrides")]
        public List<CustomLabelOverride> CustomLabelOverrides { get; set; }
    }

    public class CustomLabelOverride
    {
        [JsonProperty("labelToOverride")]
        public string LabelToOverride { get; set; }
        [JsonProperty("overrideValue")]
        public string OverrideValue { get; set; }
    }
}