using System.Collections.Generic;
using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.Campaign
{
    public class CampaignStrategyData
    {
        [JsonProperty("objectiveSummary")]
        public string ObjectiveSummary { get; set; }
        [JsonProperty("objectiveOne")]
        public string ObjectiveOne { get; set; }
        [JsonProperty("objectiveTwo")]
        public string ObjectiveTwo { get; set; }
        [JsonProperty("objectiveThree")]
        public string ObjectiveThree { get; set; }
        [JsonProperty("isGenderMale")]
        public bool IsGenderMale { get; set; }
        [JsonProperty("age")]
        public List<string> Age { get; set; }
        [JsonProperty("targetDemographicComment")]
        public string TargetDemographicComment { get; set; }
        [JsonProperty("otherMedia")]
        public List<string> OtherMedia { get; set; }
    }
}