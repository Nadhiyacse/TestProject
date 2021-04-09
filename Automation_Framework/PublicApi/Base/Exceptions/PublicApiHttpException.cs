using System;
using System.Net;
using RestSharp;

namespace Automation_Framework.PublicApi.Base.Exceptions
{
    public class PublicApiHttpException : Exception
    {
        private readonly IRestResponse response;
        private readonly HttpStatusCode code;
        private readonly string baseUrl;

        public PublicApiHttpException(IRestResponse response, HttpStatusCode code, string baseUrl)
        {
            this.response = response;
            this.code = code;
            this.baseUrl = baseUrl;
        }

        public override string Message
        {
            get
            {
                var requestInfo = $"{response.Request.Method} {baseUrl}{response.Request.Resource}";
                var responseInfo = $"Expected response code: {code}, actual response code: {response.StatusCode}{Environment.NewLine}{response.Content}";

                return $"{Environment.NewLine}{DateTime.Now:G}: {requestInfo} {responseInfo}";
            }
        }
    }
}