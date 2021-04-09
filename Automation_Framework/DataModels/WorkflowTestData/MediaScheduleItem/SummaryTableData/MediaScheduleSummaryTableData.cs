using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.SummaryTableData
{
    public class MediaScheduleSummaryTableData
    {
        [JsonProperty("totalOtherVendorCost")]
        public string TotalOtherVendorCost { get; set; }
    }
}
