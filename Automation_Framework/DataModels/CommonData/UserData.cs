using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.CommonData
{
    public class UserData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("sendPassword")]
        public bool SendPassword { get; set; }
        [JsonProperty("roles")]
        public List<string> Roles { get; set; }
    }
}