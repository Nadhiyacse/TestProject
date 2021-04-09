using Newtonsoft.Json;

namespace Automation_Framework.DataModels.InfrastructureData.Administrator
{
    public class IOAdminData
    {
        [JsonProperty("billingAddress")]
        public string BillingAddress { get; set; }
        [JsonProperty("invoiceEmail")]
        public string InvoiceEmail { get; set; }
        [JsonProperty("invoiceFax")]
        public string InvoiceFax { get; set; }
        [JsonProperty("termsAndConditionsFileName")]
        public string TermsAndConditionsFileName { get; set; }
        [JsonProperty("paymentTerms")]
        public string PaymentTerms { get; set; }
        [JsonProperty("comments")]
        public string Comments { get; set; }
        [JsonProperty("financeContactName")]
        public string FinanceContactName { get; set; }
        [JsonProperty("financeContactEmail")]
        public string FinanceContactEmail { get; set; }
        [JsonProperty("financeContactPhone")]
        public string FinanceContactPhone { get; set; }
    }
}
