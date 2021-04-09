using System.Collections.Generic;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Automation_Framework.DataModels.CommonData
{
    public class CustomFieldData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public CustomFieldType Type { get; set; }
        [JsonProperty("values")]
        public List<string> Values { get; set; }
    }
}