using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Common
{
    public class FlightingTabData
    {
        [JsonProperty("flights")]
        public List<FlightData> Flights { get; set; }
        [JsonProperty("goalByHour")]
        public string GoalByHour { get; set; }
    }

    public class FlightData
    {
        [JsonProperty("startDate")]
        public string StartDate { get; set; }
        [JsonProperty("endDate")]
        public string EndDate { get; set; }
        [JsonProperty("goal")]
        public int Goal { get; set; }
    }
}