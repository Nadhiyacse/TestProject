using System;
using System.Linq.Expressions;
using System.Net;
using Automation_Framework.PublicApi.Base.BaseClient;
using RestSharp;

namespace Automation_Framework.PublicApi.Base.Executor
{
    public class ApiExecutor
    {
        private ApiClient currentClient;

        private readonly HttpStatusCode currentCode;

        public ApiExecutor(HttpStatusCode currentCode = HttpStatusCode.OK)
        {
            this.currentCode = currentCode;
        }

        public IRestResponse<TResponse> Execute<TService, TResponse>(Expression<Func<TService, IRestRequest>> requestBuilder) where TService : new() where TResponse : new()
        {
            var action = requestBuilder.Compile();
            var newService = new TService();
            currentClient = RestClientFactory.Instance.CurrentClient;
            var request = action(newService);
            return currentClient.Execute<TResponse>(request, currentCode);
        }

        public IRestResponse Execute<TService>(Expression<Func<TService, IRestRequest>> requestBuilder) where TService : new()
        {
            var action = requestBuilder.Compile();
            var newService = new TService();
            currentClient = RestClientFactory.Instance.CurrentClient;
            var request = action(newService);
            return currentClient.Execute(request, currentCode);
        }
    }
}
