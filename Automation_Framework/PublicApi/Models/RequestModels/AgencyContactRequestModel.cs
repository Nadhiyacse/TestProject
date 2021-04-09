using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Automation_Framework.PublicApi.Models.RequestModels
{
    public class AgencyContactRequestModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
