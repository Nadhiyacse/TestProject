using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Publishers
{
    public class SiteData
    {
        [JsonProperty("siteName")]
        public string SiteName { get; set; }
        [JsonProperty("secondaryLanguageName")]
        public string SecondaryLanguageName { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("primaryCategory")]
        public string PrimaryCategory { get; set; }
        [JsonProperty("secondaryCategory")]
        public string SecondaryCategory { get; set; }
        [JsonProperty("isProprietaryMediaSite")]
        public bool IsProprietaryMediaSite { get; set; }
        [JsonProperty("parentSite")]
        public string ParentSite { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
