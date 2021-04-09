using System;
using Automation_Framework._3._Pages.Symphony.Common.Enums;

namespace Automation_Framework.Helpers
{
    public static class FeatureToggleExtensionMethods
    {
        public static string DisplayName(this FeatureToggle featureToggle)
        {
            var field = featureToggle.GetType().GetField(featureToggle.ToString());
            var attribute = Attribute.GetCustomAttribute(field, typeof(FeatureDisplayNameAttribute)) as FeatureDisplayNameAttribute;
            return attribute == null ? featureToggle.ToString() : attribute.Value;
        }
    }
}
