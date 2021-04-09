using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Automation_Framework.PublicApi.Models.ResponseModels
{
    public class AuthenticationResponseModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
