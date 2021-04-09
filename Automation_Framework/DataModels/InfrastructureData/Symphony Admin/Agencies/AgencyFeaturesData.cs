using System.Collections.Generic;
using Automation_Framework.DataModels.CommonData;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies.Agency_Features;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies
{
    public class AgencyFeaturesData
    {
        [JsonProperty("mediaScheduleItemSettings")]
        public List<Checkbox> MediaScheduleItemSettings { get; set; }
        [JsonProperty("mediaScheduleExports")]
        public List<Checkbox> MediaScheduleExports { get; set; }
        [JsonProperty("serivceFeeOptions")]
        public List<Checkbox> ServiceFeeOptions { get; set; }
        [JsonProperty("trafficking")]
        public List<Checkbox> Trafficking { get; set; }
        [JsonProperty("insertionOrders")]
        public List<Checkbox> InsertionOrders { get; set; }
        [JsonProperty("emailNotifications")]
        public EmailNotificationsData EmailNotifications { get; set; }
        [JsonProperty("billing")]
        public List<Checkbox> Billing { get; set; }
        [JsonProperty("mediaInsights")]
        public List<Checkbox> MediaInsights { get; set; }
        [JsonProperty("campaignSettings")]
        public List<Checkbox> CampaignSettings { get; set; }
        [JsonProperty("mediaScheduleApproval")]
        public MediaScheduleApprovalData MediaScheduleApproval { get; set; }
        [JsonProperty("ratecardManagement")]
        public List<Checkbox> RatecardManagement { get; set; }
        [JsonProperty("forecastingMetrics")]
        public ForecastingMetricsSettingsData ForecastingMetrics { get; set; }
        [JsonProperty("costDefaultSetting")]
        public string CostDefaultSetting { get; set; }
        [JsonProperty("administratorRoles")]
        public List<Checkbox> AdministratorRoles { get; set; }
    }
}