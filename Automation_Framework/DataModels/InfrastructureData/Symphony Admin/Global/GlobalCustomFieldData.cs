using Automation_Framework._3._Pages.Symphony.Common.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Global
{
    public class GlobalCustomFieldData
    {
        [JsonProperty("applyTo")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CustomFieldApplyTo ApplyTo { get; set; }
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CustomFieldType Type { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}