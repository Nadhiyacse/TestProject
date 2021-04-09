using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Integration
{
    public class ExternalAccountData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("externalAccountId")]
        public string ExternalAccountId { get; set; }
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("clientId")]
        public string ClientId { get; set; }
        [JsonProperty("clientSecret")]
        public string ClientSecret { get; set; }
        [JsonProperty("managerAccountId")]
        public string ManagerAccountId { get; set; }
        [JsonProperty("customerId")]
        public string CustomerId { get; set; }
        [JsonProperty("clientOverrideData")]
        public List<ClientOverrideData> ClientOverrideData { get; set; }
    }
}
