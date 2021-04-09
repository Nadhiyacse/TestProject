using System.Collections.Generic;
using Automation_Framework.DataModels.CommonData;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies
{
    public class AgencyData
    {
        [JsonProperty("masterAgency")]
        public string MasterAgency { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("isTestAgency")]
        public bool IsTestAgency { get; set; }
        [JsonProperty("taxNumber")]
        public string TaxNumber { get; set; }
        [JsonProperty("logoFilePath")]
        public string LogoFilePath { get; set; }
        [JsonProperty("insertionOrderEnabled")]
        public bool InsertionOrderEnabled { get; set; }
        [JsonProperty("billingSource")]
        public string BillingSource { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("timeZone")]
        public string TimeZone { get; set; }
        [JsonProperty("enableAgencyOverride")]
        public bool EnableAgencyOverride { get; set; }
        [JsonProperty("contactName")]
        public string ContactName { get; set; }
        [JsonProperty("contactEmail")]
        public string ContactEmail { get; set; }
        [JsonProperty("contactTelephone")]
        public string ContactTelephone { get; set; }
        [JsonProperty("uniquePlannerBuyer")]
        public bool UniquePlannerBuyer { get; set; }
        [JsonProperty("authenticationMethod")]
        public string AuthenticationMethod { get; set; }
        [JsonProperty("access")]
        public AgencyAccessData Access { get; set; }
        [JsonProperty("users")]
        public List<UserData> Users { get; set; }
        [JsonProperty("features")]
        public AgencyFeaturesData Features { get; set; }
        [JsonProperty("customLabels")]
        public List<CustomLabelData> CustomLabels { get; set; }
        [JsonProperty("customFields")]
        public List<CustomFieldData> CustomFields { get; set; }
        [JsonProperty("classifications")]
        public List<ClassificationData> Classifications { get; set; }
    }
}