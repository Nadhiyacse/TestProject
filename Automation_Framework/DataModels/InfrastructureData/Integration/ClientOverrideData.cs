using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Integration
{
    public class ClientOverrideData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("externalAccountId")]
        public string externalAccountId { get; set; }
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
        [JsonProperty("authorisationKey")]
        public string AuthorisationKey { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("appSecret")]
        public string AppSecret { get; set; }
        [JsonProperty("appKey")]
        public string AppKey { get; set; }
        [JsonProperty("clientId")]
        public string ClientId { get; set; }
        [JsonProperty("clientSecret")]
        public string ClientSecret { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
    }
}
