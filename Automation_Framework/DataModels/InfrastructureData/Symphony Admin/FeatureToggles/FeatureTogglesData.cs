using System.Collections.Generic;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.FeatureToggles
{
    public class FeatureTogglesData
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public FeatureToggle Feature { get; set; }

        [JsonProperty("environmentScopes")]
        public List<EnvironmentScope> EnvironmentScopes { get; set; }
    }
}
