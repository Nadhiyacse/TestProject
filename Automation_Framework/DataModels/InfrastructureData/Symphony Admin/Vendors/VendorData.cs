using System.Collections.Generic;
using Automation_Framework.DataModels.CommonData;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Vendors
{
    public class VendorData
    {
        [JsonProperty("vendor")]
        public string Vendor { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("taxOtherCosts")]
        public bool TaxOtherCosts { get; set; }
        [JsonProperty("categories")]
        public List<Checkbox> Categories { get; set; }
    }
}