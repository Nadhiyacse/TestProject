using Automation_Framework.DataModels.CommonData;
using Automation_Framework.DataModels.InfrastructureData.Administrator;
using Automation_Framework.DataModels.InfrastructureData.Integration;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData
{
    public class AgencyInfrastructureData : ITestData
    {
        [JsonProperty("agencyLoginUserData")]
        public LoginUserData AgencyLoginUserData { get; set; }
        [JsonProperty("symphonyAdminData")]
        public SymphonyAdminAgencyData SymphonyAdminData { get; set; }
        [JsonProperty("agencyAdministratorData")]
        public AgencyAdministratorData AgencyAdministratorData { get; set; }
        [JsonProperty("integrationData")]
        public IntegrationData IntegrationData { get; set; }
    }
}