using System.Collections.Generic;
using Automation_Framework.DataModels.CommonData;
using Automation_Framework.DataModels.WorkflowTestData.Billing;
using Automation_Framework.DataModels.WorkflowTestData.Campaign;
using Automation_Framework.DataModels.WorkflowTestData.InsertionOrder;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.NonMediaCosts;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.PerformancePackage;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.SinglePlacement;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.SponsorshipPackage;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.SummaryTableData;
using Automation_Framework.DataModels.WorkflowTestData.Trafficking;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData
{
    public class WorkflowTestData : ITestData
    {
        [JsonProperty("agencyLoginUserData")]
        public LoginUserData AgencyLoginUserData { get; set; }
        [JsonProperty("publisherLoginUserData")]
        public LoginUserData PublisherLoginUserData { get; set; }
        [JsonProperty("campaignData")]
        public CampaignData CampaignData { get; set; }
        [JsonProperty("singlePlacements")]
        public List<SinglePlacement> SinglePlacements { get; set; }
        [JsonProperty("performancePackages")]
        public List<PerformancePackage> PerformancePackages { get; set; }
        [JsonProperty("sponsorshipPackages")]
        public List<SponsorshipPackage> SponsorshipPackages { get; set; }
        [JsonProperty("billingData")]
        public List<BillingData> BillingData { get; set; }
        [JsonProperty("insertionOrderData")]
        public InsertionOrderData InsertionOrderData { get; set; }
        [JsonProperty("customBillingData")]
        public List<CustomBillingData> CustomBillingData { get; set; }
        [JsonProperty("traffickingData")]
        public List<TraffickingData> TraffickingData { get; set; }
        [JsonProperty("mediaSchedule")]
        public MediaScheduleData MediaScheduleData { get; set; }
        [JsonProperty("nonMediaCostData")]
        public List<NonMediaCostData> NonMediaCostData { get; set; }
        [JsonProperty("mediaScheduleSummaryTableData")]
        public MediaScheduleSummaryTableData MediaScheduleSummaryTableData { get; set; }
        [JsonProperty("columnSettings")]
        public List<string> ColumnSettings { get; set; }
        [JsonProperty("deviceMarketplaceFilters")]
        public List<string> DeviceMarketplaceFilters { get; set; }
        [JsonProperty("buyTypeMarketplaceFilters")]
        public List<string> BuyTypeMarketplaceFilters { get; set; }
    }
}