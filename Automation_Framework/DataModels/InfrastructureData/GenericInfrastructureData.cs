using Automation_Framework.DataModels.CommonData;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData
{
    public class GenericInfrastructureData : ITestData
    {
        [JsonProperty("symphonyAdminLoginUserData")]
        public LoginUserData SymphonyAdminLoginUserData { get; set; }
        [JsonProperty("symphonyAdminData")]
        public SymphonyAdminGenericData SymphonyAdminData { get; set; }
    }
}