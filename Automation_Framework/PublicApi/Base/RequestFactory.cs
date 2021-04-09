using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation_Framework.PublicApi.Base.Serialization;
using RestSharp;

namespace Automation_Framework.PublicApi.Base
{
    public class RequestFactory
    {
        public RequestFactory()
        {
            Serializer = JsonSerializer.Instance;
        }

        private JsonSerializer Serializer;

        private void SetDataFormat(IRestRequest request, string dateFormat = "yyyy/MM/dd H:mm:ss UTC")
        {
            request.RequestFormat = DataFormat.Json;
            Serializer.DateFormat = dateFormat;
            request.JsonSerializer = Serializer;
        }

        public IRestRequest GetRequest(string resource, Method method)
        {
            var request = new RestRequest(resource, method);
            SetDataFormat(request);
            return request;
        }

        public IRestRequest GetRequest(Method method)
        {
            var request = new RestRequest(method);
            SetDataFormat(request);
            return request;
        }

        public IRestRequest GetRequest(Method method, string dateFormat)
        {
            var request = new RestRequest(method);
            SetDataFormat(request, dateFormat);
            return request;
        }

        public IRestRequest GetRequest(string resource, Method method, string dateFormat)
        {
            var request = new RestRequest(resource, method);
            SetDataFormat(request, dateFormat);
            return request;
        }
    }
}