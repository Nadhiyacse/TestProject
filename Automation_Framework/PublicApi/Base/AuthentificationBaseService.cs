using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation_Framework.PublicApi.Base.BaseClient;

namespace Automation_Framework.PublicApi.Base
{
    public class AuthentificationBaseService : BaseService
    {
        public AuthentificationBaseService()
        {
            RestClientFactory.Instance.Create(GetType(), ConfigurationManager.AppSettings["IdentityServerUrl"]);
        }
    }
}
