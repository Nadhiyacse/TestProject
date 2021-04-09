using System.Collections.Generic;
﻿using Automation_Framework.DataModels.InfrastructureData.Integration.DataMapping;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Integration
{
    public class IntegrationData
    {
        [JsonProperty("externalAccounts")]
        public List<ExternalAccountData> ExternalAccounts { get; set; }
        [JsonProperty("dataMapping")]
        public MappingData MappingData { get; set; }
    }
}
