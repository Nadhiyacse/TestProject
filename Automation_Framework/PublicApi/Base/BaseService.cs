namespace Automation_Framework.PublicApi.Base
{
    public class BaseService
    {
        protected RequestFactory RequestFactory;

        public BaseService()
        {
            RequestFactory = new RequestFactory();
        }
    }
}
