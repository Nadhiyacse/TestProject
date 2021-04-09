using System.Collections.Generic;
using Automation_Framework.DataModels.CommonData;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.Campaign
{
    public class CampaignDetailsData
    {
        [JsonProperty("campaignName")]
        public string CampaignName { get; set; }
        [JsonProperty("internalId")]
        public string InternalId { get; set; }
        [JsonProperty("autoGenerateName")]
        public bool AutoGenerateName { get; set; }
        [JsonProperty("addToWatchlist")]
        public bool AddToWatchlist { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("startDate")]
        public string StartDate { get; set; }
        [JsonProperty("endDate")]
        public string EndDate { get; set; }
        [JsonProperty("timingComment")]
        public string TimingComment { get; set; }
        [JsonProperty("agencyContact")]
        public string AgencyContact { get; set; }
        [JsonProperty("creativeContact")]
        public string CreativeContact { get; set; }
        [JsonProperty("clientName")]
        public string ClientName { get; set; }
        [JsonProperty("clientId")]
        public string ClientId { get; set; }
        [JsonProperty("product")]
        public string Product { get; set; }
        [JsonProperty("parentCampaign")]
        public string ParentCampaign { get; set; }
        [JsonProperty("thirdPartyAdserver")]
        public string ThirdPartyAdserver { get; set; }
        [JsonProperty("exportMediaCostInformationToAdserver")]
        public bool ExportMediaCostInformationToAdserver { get; set; }
        [JsonProperty("fourthPartyAdserver")]
        public string FourthPartyAdserver { get; set; }
        [JsonProperty("traffickingPlacementLevelLandingPage")]
        public bool TraffickingPlacementLevelLandingPage { get; set; }
        [JsonProperty("isCostOfTrackingRate")]
        public bool IsCostOfTrackingRate { get; set; }
        [JsonProperty("landingPage")]
        public string LandingPage { get; set; }
        [JsonProperty("customFields")]
        public List<CustomFieldData> CustomFields { get; set; }
    }
}