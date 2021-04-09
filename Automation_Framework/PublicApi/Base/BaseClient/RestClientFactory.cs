using System;
using System.Reflection;
using Automation_Framework.PublicApi.Base.Attributes;

namespace Automation_Framework.PublicApi.Base.BaseClient
{
    public class RestClientFactory
    {
        private static RestClientFactory factory;

        public static RestClientFactory Instance
        {
            get
            {
                if (factory == null)
                {
                    factory = new RestClientFactory();
                }

                return factory;
            }
        }

        public ApiClient CurrentClient { get; private set; }

        public ApiClient Create(string resource)
        {
            return CurrentClient = new ApiClient(resource);
        }

        public ApiClient Create(Type type, string root)
        {
            var attribute = (ServiceUrlAttribute)type.GetCustomAttribute(typeof(ServiceUrlAttribute), true);
            var url = attribute == null ? root : root + attribute.Url;
            return Create(url);
        }
    }
}
