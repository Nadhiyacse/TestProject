using Newtonsoft.Json;

namespace Automation_Framework.DataModels.CommonData
{
    public class AccessControlledItem
    {
        [JsonProperty("isDefault")]
        public bool IsDefault { get; set; }
        [JsonProperty("accessItem")]
        public Checkbox AccessItem { get; set; }
    }
}