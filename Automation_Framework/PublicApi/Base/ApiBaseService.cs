using System.Configuration;
using Automation_Framework.PublicApi.Base.BaseClient;

namespace Automation_Framework.PublicApi.Base
{
    public class ApiBaseService : BaseService
    {
        public ApiBaseService()
        {
            RestClientFactory.Instance.Create(GetType(), ConfigurationManager.AppSettings["PublicApiUrl"]);
        }
    }
}
