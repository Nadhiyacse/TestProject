using Newtonsoft.Json;

namespace Automation_Framework.DataModels.CommonData
{
    public class FlightDates
    {
        [JsonProperty("startDate")]
        public string StartDate;
        [JsonProperty("endDate")]
        public string EndDate;
    }
}
