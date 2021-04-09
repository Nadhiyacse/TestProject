using System.Collections.Generic;
using Automation_Framework.DataModels.CommonData;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies.Agency_Features
{
    public class ForecastingMetricsSettingsData
    {
        [JsonProperty("defaultMetrics")]
        public Checkbox DefaultMetrics { get; set; }
        [JsonProperty("customForecastMetrics")]
        public List<Checkbox> CustomForecastingMetrics { get; set; }
    }
}