using System;

namespace Automation_Framework.PublicApi.Base.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ServiceUrlAttribute : Attribute
    {
        public ServiceUrlAttribute(string url)
        {
            Url = url;
        }

        public string Url { get; }
    }
}
