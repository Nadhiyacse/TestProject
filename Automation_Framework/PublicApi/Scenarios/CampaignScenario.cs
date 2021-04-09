using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Automation_Framework.DataModels.CommonData;
using Automation_Framework.DataModels.WorkflowTestData.Campaign;
using Automation_Framework.PublicApi.Base.Executor;
using Automation_Framework.PublicApi.Models.RequestModels;
using Automation_Framework.PublicApi.Models.ResponseModels;
using Automation_Framework.PublicApi.Services;

namespace Automation_Framework.PublicApi.Scenarios
{
    public class CampaignScenario
    {
        private const string SHORT_DATE_FORMAT = "yyyy-MM-dd";

        public void CreateCampaign(CampaignData campaignData, LoginUserData loginUserData, string token)
        {
            CampaignRequestModel model = new CampaignRequestModel
            {
                Name = campaignData.DetailsData.CampaignName + DateTime.Now.ToString($"{SHORT_DATE_FORMAT} hh:mm:ss.fff tt"),
                StartDate = string.IsNullOrEmpty(campaignData.DetailsData.StartDate) ? DateTime.Now.ToString(SHORT_DATE_FORMAT) : campaignData.DetailsData.StartDate,
                EndDate = string.IsNullOrEmpty(campaignData.DetailsData.EndDate) ? DateTime.Now.AddDays(3).ToString(SHORT_DATE_FORMAT) : campaignData.DetailsData.EndDate,
                IsDeleted = false,
                Advertiser = new AdvertiserRequestModel
                {
                    Id = campaignData.DetailsData.ClientId
                },
                Brand = new BrandRequestModel
                {
                    Name = campaignData.DetailsData.Product
                },
                AgencyContact = new AgencyContactRequestModel
                {
                    Email = loginUserData.Username
                }
            };

            new ApiExecutor(HttpStatusCode.Created).Execute<CampaignService>(s => s.PostCampaign(model, token));
        }

        public List<string> GetCampaignNames(string token)
        {
            return new ApiExecutor(HttpStatusCode.OK).Execute<CampaignService, List<CampaignResponseModel>>(s => s.GetCampaignIds(token)).Data.Select(x => x.Name).ToList();
        }
    }
}
