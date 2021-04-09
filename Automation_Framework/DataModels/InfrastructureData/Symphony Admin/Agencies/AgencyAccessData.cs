using System.Collections.Generic;
using Automation_Framework.DataModels.CommonData;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies.Agency_Access;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies
{
    public class AgencyAccessData
    {
        [JsonProperty("costModel")]
        public string CostModel { get; set; }
        [JsonProperty("placementNamingConvention")]
        public string PlacementNamingConvention { get; set; }
        [JsonProperty("packageNamingConvention")]
        public string PackageNamingConvention { get; set; }
        [JsonProperty("campaignNamingConvention")]
        public string CampaignNamingConvention { get; set; }
        [JsonProperty("mediaScheduleExports")]
        public List<AccessControlledItem> MediaScheduleExports { get; set; }
        [JsonProperty("insertionOrderExports")]
        public List<AccessControlledItem> InsertionOrderExports { get; set; }
        [JsonProperty("costModelImports")]
        public List<AccessControlledItem> CostModelImports { get; set; }
        [JsonProperty("billingExports")]
        public List<AccessControlledItem> BillingExports { get; set; }
        [JsonProperty("traffickingExports")]
        public List<AccessControlledItem> TraffickingExports { get; set; }
        [JsonProperty("publisherDetailExports")]
        public List<AccessControlledItem> PublisherDetailExports { get; set; }
        [JsonProperty("dataMapping")]
        public List<AccessControlledItem> DataMapping { get; set; }
        [JsonProperty("thirdPartyAdServers")]
        public List<AccessControlledItem> ThirdPartyAdServers { get; set; }
        [JsonProperty("fourthPartyAdServers")]
        public List<AccessControlledItem> FourthPartyAdServers { get; set; }
        [JsonProperty("languages")]
        public List<AccessControlledItem> Languages { get; set; }
        [JsonProperty("externalCredentials")]
        public List<AccessControlledItem> ExternalCredentials { get; set; }
        [JsonProperty("mediaScheduleViews")]
        public List<AccessControlledItem> MediaScheduleViews { get; set; }
        [JsonProperty("campaignIdStrategies")]
        public List<AccessControlledItem> CampaignIdStrategies { get; set; }
        [JsonProperty("inventoryProviders")]
        public InventoryProviderData InventoryProviders { get; set; }
        [JsonProperty("costItemAdjustments")]
        public List<Checkbox> CostItemAdjustments { get; set; }
        [JsonProperty("purchaseTypes")]
        public List<Checkbox> PurchaseTypes { get; set; }
        [JsonProperty("scheduleBudgetIncludes")]
        public ScheduleBudgetIncludesData ScheduleBudgetIncludes { get; set; }
        [JsonProperty("creativeTypes")]
        public List<Checkbox> CreativeTypes { get; set; }
        [JsonProperty("otherCostCategories")]
        public List<AccessControlledItem> OtherCostCategories { get; set; }
        [JsonProperty("mandatoryClassifications")]
        public List<Checkbox> MandatoryClassifications { get; set; }
        [JsonProperty("hideStandardImportTemplateColumns")]
        public List<Checkbox> HideStandardImportTemplateColumns { get; set; }
        [JsonProperty("mediaScheduleExportOptions")]
        public MediaScheduleExportOptionsData MediaScheduleExportOptions { get; set; }
        [JsonProperty("billingAllocations")]
        public List<AccessControlledItem> BillingAllocations { get; set; }
    }
}