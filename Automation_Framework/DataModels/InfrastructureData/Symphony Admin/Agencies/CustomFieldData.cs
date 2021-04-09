using System.Collections.Generic;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies.Custom_Fields;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies
{
    public class CustomFieldData
    {
        [JsonProperty("applyTo")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CustomFieldApplyTo ApplyTo { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
        [JsonProperty("characterLimit")]
        public int CharacterLimit { get; set; }
        [JsonProperty("defaultText")]
        public string DefaultText { get; set; }
        [JsonProperty("multiSelectOptions")]
        public List<MultiSelectOptions> MultiSelectOptions { get; set; }
        [JsonProperty("resolution")]
        public Resolution Resolution { get; set; }
        [JsonProperty("mandatory")]
        public bool Mandatory { get; set; }
    }
}