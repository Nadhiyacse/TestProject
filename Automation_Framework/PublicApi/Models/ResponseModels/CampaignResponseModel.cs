﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Automation_Framework.PublicApi.Models.ResponseModels
{
    public class CampaignResponseModel
    {
        [JsonProperty("createdDateUtc")]
        public string CreatedDate { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("updatedDate")]
        public string UpdatedDate { get; set; }
    }
}
