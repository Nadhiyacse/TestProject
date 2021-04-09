using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Automation_Framework._3._Pages.Adslot.Common;
using Automation_Framework._3._Pages.Symphony.Common;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using Automation_Framework.DataModels.CommonData;
using Automation_Framework.DataModels.InfrastructureData;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.FeatureToggles;
using Automation_Framework.DataModels.WorkflowTestData;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.NonMediaCosts;
using Automation_Framework.Hooks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps
{
    public class BaseStep
    {
        public WorkflowTestData WorkflowTestData;
        public AgencyInfrastructureData AgencySetupData;
        public GenericInfrastructureData GenericSetupData;
        public NavigationPage NavigationPage;
        public AdslotNavigationPage AdslotNavigationPage;
        protected IWebDriver driver;
        protected FeatureContext FeatureContext;
        protected readonly string InventoryProvider;
        protected readonly TimeZoneInfo AgencyTimeZoneInfo;
        protected readonly List<FeatureTogglesData> FeatureToggles;

        public BaseStep(IWebDriver webDriver, FeatureContext featureContext)
        {
            driver = webDriver;
            FeatureContext = featureContext;
            InventoryProvider = ConfigurationManager.AppSettings["InventoryProvider"];

            if (FeatureContext.FeatureInfo.Tags.Contains("WorkFlowTest"))
            {
                WorkflowTestData = FeatureContext[ContextStrings.WorkflowTestData] as WorkflowTestData;
                WorkflowTestData.NonMediaCostData = WorkflowTestData.NonMediaCostData ?? new List<NonMediaCostData>();
            }

            if (FeatureContext.FeatureInfo.Tags.Contains("Performance"))
            {
                WorkflowTestData = FeatureContext[ContextStrings.PerformanceTestData] as WorkflowTestData;
                WorkflowTestData.NonMediaCostData = WorkflowTestData.NonMediaCostData ?? new List<NonMediaCostData>();
            }

            AgencySetupData = FeatureContext[ContextStrings.AgencySetupData] as AgencyInfrastructureData;

            if (!FeatureContext.FeatureInfo.Title.Equals("Generic_Setup"))
            {
                AgencyTimeZoneInfo = GetTimeZoneInfoFromDisplayName(AgencySetupData.SymphonyAdminData.Agencies.First().TimeZone);
            }

            GenericSetupData = FeatureContext[ContextStrings.GenericSetupData] as GenericInfrastructureData;
            FeatureToggles = GenericSetupData.SymphonyAdminData.FeatureToggles;
            
            NavigationPage = new NavigationPage(driver, featureContext);
            AdslotNavigationPage = new AdslotNavigationPage(driver, featureContext);
        }

        protected bool IsAgencyFeatureEnabled(List<Checkbox> featureList, string featureName)
        {
            if (featureList == null)
                return false;

            return featureList.First(x => x.Name.Equals(featureName)).Enabled;
        }

        private TimeZoneInfo GetTimeZoneInfoFromDisplayName(string timeZoneDisplayName)
        {
            if (string.IsNullOrEmpty(timeZoneDisplayName))
                throw new ArgumentNullException("Time zone is empty");

            var timeZoneId = TimeZoneInfo.GetSystemTimeZones().Where(tz => tz.DisplayName.Equals(timeZoneDisplayName)).First().Id;
            return TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        }

        protected bool IsFeatureToggleEnabled(FeatureToggle featureToggle)
        {
            return FeatureToggles.Any(ft => ft.Feature.Equals(featureToggle));
        }
    }
}