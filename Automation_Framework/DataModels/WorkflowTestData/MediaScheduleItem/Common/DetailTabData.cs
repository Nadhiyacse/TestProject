using System.Collections.Generic;
using Automation_Framework.DataModels.CommonData;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Common
{
    public class DetailTabData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("autoGenerateName")]
        public bool AutoGenerateName { get; set; }
        [JsonProperty("publisher")]
        public string Publisher { get; set; }
        [JsonProperty("site")]
        public string Site { get; set; }
        [JsonProperty("supplier")]
        public string Supplier { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("format")]
        public string Format { get; set; }
        [JsonProperty("width")]
        public string Width { get; set; }
        [JsonProperty("height")]
        public string Height { get; set; }
        [JsonProperty("creativeType")]
        public string CreativeType { get; set; }
        [JsonProperty("package")]
        public string Package { get; set; }
        [JsonProperty("devices")]
        public List<string> Devices { get; set; }
        [JsonProperty("landingPage")]
        public string LandingPage { get; set; }
        [JsonProperty("targetingType")]
        public string TargetingType { get; set; }
        [JsonProperty("targetingOptions")]
        public List<string> TargetingOptions { get; set; }
        [JsonProperty("targetingComments")]
        public string TargetingComments { get; set; }
        [JsonProperty("frequencyCapImpressions")]
        public string FrequencyCapImpressions { get; set; }
        [JsonProperty("frequencyCapDuration")]
        public string FrequencyCapDuration { get; set; }
        [JsonProperty("frequencyCapInterval")]
        public string FrequencyCapInterval { get; set; }
        [JsonProperty("startDate")]
        public string StartDate { get; set; }
        [JsonProperty("endDate")]
        public string EndDate { get; set; }
        [JsonProperty("clientAgreement")]
        public string ClientAgreement { get; set; }
        [JsonProperty("purchaseType")]
        public string PurchaseType { get; set; }
        [JsonProperty("goal")]
        public string Goal { get; set; }
        [JsonProperty("autoCalculateField")]
        public string AutoCalculateField { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("exchangeRate")]
        public string ExchangeRate { get; set; }
        [JsonProperty("ratecardRate")]
        public string RatecardRate { get; set; }
        [JsonProperty("ratecardCost")]
        public string RatecardCost { get; set; }
        [JsonProperty("baseRate")]
        public string BaseRate { get; set; }
        [JsonProperty("baseCost")]
        public string BaseCost { get; set; }
        [JsonProperty("netRate")]
        public string NetRate { get; set; }
        [JsonProperty("netCost")]
        public string NetCost { get; set; }
        [JsonProperty("shareOfVoice")]
        public string ShareOfVoice { get; set; }
        [JsonProperty("isDefaultRate")]
        public bool IsDefaultRate { get; set; }
        [JsonProperty("customFields")]
        public List<CustomFieldData> CustomFields { get; set; }
    }
}