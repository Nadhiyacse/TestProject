using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation_Framework._3._Pages.Symphony.Common.Enums;

namespace Automation_Framework.Helpers
{
    public static class PurchaseTypeExtensionMethods
    {
        public static bool IsGoalEditableForPurchaseType(this PurchaseType purchaseType)
        {
            if (purchaseType.Equals(PurchaseType.CPD))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
