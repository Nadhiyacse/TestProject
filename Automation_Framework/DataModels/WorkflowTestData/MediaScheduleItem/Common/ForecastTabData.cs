using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Common
{
    public class ForecastTabData
    {
        [JsonProperty("useRate")]
        public bool UseRate { get; set; }
        [JsonProperty("impressions")]
        public int Impressions { get; set; }
        [JsonProperty("clicks")]
        public int Clicks { get; set; }
        [JsonProperty("acquisitions")]
        public int Acquisitions { get; set; }
        [JsonProperty("views")]
        public int Views { get; set; }
        [JsonProperty("clickThroughRate")]
        public decimal ClickThroughRate { get; set; }
        [JsonProperty("clickViewRate")]
        public decimal ClickViewRate { get; set; }
        [JsonProperty("viewThroughRate")]
        public decimal ViewThroughRate { get; set; }
        [JsonProperty("estImps")]
        public string EstImps { get; set; }
        [JsonProperty("estClicks")]
        public string EstClicks { get; set; }
        [JsonProperty("estAcqs")]
        public string EstAcqs { get; set; }
        [JsonProperty("estViews")]
        public string EstViews { get; set; }
        [JsonProperty("estCtrPercentage")]
        public string EstCtrPercentage { get; set; }
        [JsonProperty("estCvrPercentage")]
        public string EstCvrPercentage { get; set; }
        [JsonProperty("estVtrPercentage")]
        public string EstVtrPercentage { get; set; }
        [JsonProperty("estCpm")]
        public string EstCpm { get; set; }
        [JsonProperty("estCpc")]
        public string EstCpc { get; set; }
        [JsonProperty("estCpa")]
        public string EstCpa { get; set; }
        [JsonProperty("estCpv")]
        public string EstCpv { get; set; }
    }

    // TODO - Additional Forecasting Metrics
}