using System;
using System.Net;
using Automation_Framework.DataModels.CommonData;
using Automation_Framework.PublicApi.Base.Executor;
using Automation_Framework.PublicApi.Models.ResponseModels;
using Automation_Framework.PublicApi.Services;

namespace Automation_Framework.PublicApi.Scenarios
{
    public class AuthenticationScenario
    {
        public string GenerateAccessToken(LoginUserData loginUserData)
        {
            return new ApiExecutor(HttpStatusCode.OK).Execute<AuthenticationService, AuthenticationResponseModel>(s => s.PostAuthentication(loginUserData)).Data.AccessToken;
        }
    }
}
