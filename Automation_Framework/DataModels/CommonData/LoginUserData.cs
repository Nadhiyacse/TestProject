using Newtonsoft.Json;

namespace Automation_Framework.DataModels.CommonData
{
    public class LoginUserData
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("esignature")]
        public string EsignatureFileName { get; set; }
    }
}