using System.Collections.Generic;
using Automation_Framework.DataModels.CommonData;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Publishers
{
    public class PublisherData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("secondaryLanguageName")]
        public string SecondaryLanguageName { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("isSubscriber")]
        public bool IsSubscriber { get; set; }
        [JsonProperty("isRestrictAgencyAccess")]
        public bool IsRestrictAgencyAccess { get; set; }
        [JsonProperty("taxNumber")]
        public string TaxNumber { get; set; }
        [JsonProperty("agencyVendorCommission")]
        public string AgencyVendorCommission { get; set; }
        [JsonProperty("logoFilePath")]
        public string LogoFilePath { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("enablePublisherOverride")]
        public bool EnablePublisherOverride { get; set; }
        [JsonProperty("contactName")]
        public string ContactName { get; set; }
        [JsonProperty("contactEmail")]
        public string ContactEmail { get; set; }
        [JsonProperty("contactTelephone")]
        public string ContactTelephone { get; set; }
        [JsonProperty("contactFax")]
        public string ContactFax { get; set; }
        [JsonProperty("taxMedia")]
        public bool TaxMedia { get; set; }
        [JsonProperty("access")]
        public PublisherAccessData Access { get; set; }
        [JsonProperty("users")]
        public List<UserData> Users { get; set; }
        [JsonProperty("sites")]
        public List<SiteData> Sites { get; set; }
        [JsonProperty("locations")]
        public List<LocationData> Locations { get; set; }
        [JsonProperty("formats")]
        public List<FormatData> Formats { get; set; }
        [JsonProperty("locationFormatMappings")]
        public List<LocationFormatMappingData> LocationFormatMappings { get; set; }
        [JsonProperty("restrictAgencyAccessList")]
        public List<RestrictAgencyAccessData> RestrictAgencyAccessList { get; set; }
    }
}