using System.Collections.Generic;
using Automation_Framework.DataModels.CommonData;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies.Agency_Access
{
    public class MediaScheduleExportOptionsData
    {
        [JsonProperty("exportType")]
        public string ExportType { get; set; }
        [JsonProperty("includeInExport")]
        public List<Checkbox> IncludeInExport { get; set; }
    }
}
