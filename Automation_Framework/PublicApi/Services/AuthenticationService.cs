using System;
using System.Configuration;
using Automation_Framework.DataModels.CommonData;
using Automation_Framework.PublicApi.Base;
using Automation_Framework.PublicApi.Base.Attributes;
using RestSharp;
using RestSharp.Extensions;

namespace Automation_Framework.PublicApi.Services
{
    [ServiceUrl("token/")]
    public class AuthenticationService : AuthentificationBaseService
    {
        public IRestRequest PostAuthentication(LoginUserData loginUserData)
        {
            var request = RequestFactory.GetRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "Bearer VEVTVENMSUVOVDpVR0VrSkhkdg==");
            var bodyParameter = $"grant_type=password&username={loginUserData.Username.UrlEncode()}&password={loginUserData.Password.UrlEncode()}";
            request.AddParameter("application/x-www-form-urlencoded", bodyParameter, ParameterType.RequestBody);
            return request;
        }
    }
}