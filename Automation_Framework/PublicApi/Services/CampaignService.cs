using Automation_Framework.PublicApi.Base;
using Automation_Framework.PublicApi.Models.RequestModels;
using RestSharp;

namespace Automation_Framework.PublicApi.Services
{
    public class CampaignService : ApiBaseService
    {
        public IRestRequest PostCampaign(CampaignRequestModel model, string token)
        {
            var request = RequestFactory.GetRequest("v1/campaigns", Method.POST);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Content-Type", "application/json");
            request.AddBody(model);
            return request;
        }

        public IRestRequest GetCampaignIds(string token)
        {
            var request = RequestFactory.GetRequest("v1/campaignIds", Method.GET);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Content-Type", "application/json");
            return request;
        }
    }
}