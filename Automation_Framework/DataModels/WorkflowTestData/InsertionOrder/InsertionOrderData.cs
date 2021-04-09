using Newtonsoft.Json;

namespace Automation_Framework.DataModels.WorkflowTestData.InsertionOrder
{
    public class InsertionOrderData
    {
        [JsonProperty("ioPublisher")]
        public string IOPublisher { get; set; }
        [JsonProperty("ioName")]
        public string IOName { get; set; }
        [JsonProperty("billingAddress")]
        public string BillingAddress { get; set; }
        [JsonProperty("invoiceEmail")]
        public string InvoiceEmail { get; set; }
        [JsonProperty("invoiceFax")]
        public string InvoiceFax { get; set; }
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
        [JsonProperty("termsAndConditions")]
        public string TermsAndConditions { get; set; }
        [JsonProperty("insertionOrderIssueData")]
        public InsertionOrderIssueData InsertionOrderIssueData { get; set; }
        [JsonProperty("isAutomatedGuaranteedInsertionOrder")]
        public bool IsAutomatedGuaranteedInsertionOrder { get; set; }
    }
}
