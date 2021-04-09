using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Automation_Framework.PublicApi.Models.RequestModels
{
    public class CampaignRequestModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("startDate")]
        public string StartDate { get; set; }
        [JsonProperty("endDate")]
        public string EndDate { get; set; }
        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }
        [JsonProperty("advertiser")]
        public AdvertiserRequestModel Advertiser { get; set; }
        [JsonProperty("brand")]
        public BrandRequestModel Brand { get; set; }
        [JsonProperty("agencyContact")]
        public AgencyContactRequestModel AgencyContact { get; set; }
    }
}
