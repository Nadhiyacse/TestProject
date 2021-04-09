using System.Net;
using Automation_Framework.PublicApi.Base.Exceptions;
using RestSharp;

namespace Automation_Framework.PublicApi.Base.BaseClient
{
    public class ApiClient : RestClient
    {
        public ApiClient(string action) : base(action)
        {
        }

        public IRestResponse Execute(IRestRequest request, HttpStatusCode code = HttpStatusCode.OK)
        {
            var response = base.Execute(request);

            if (response.StatusCode != code)
            {
                throw new PublicApiHttpException(response, code, BaseUrl.ToString());
            }

            return response;
        }

        public IRestResponse<T> Execute<T>(IRestRequest request, HttpStatusCode code = HttpStatusCode.OK) where T : new()
        {
            var response = base.Execute<T>(request);

            if (response.StatusCode != code)
            {
                throw new PublicApiHttpException(response, code, BaseUrl.ToString());
            }

            return response;
        }
    }
}
