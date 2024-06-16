namespace TravelInsurance.Infrastructure.Dto.TransactionLogs
{
    public class TransactionLogsResponse
    {
    }

    public class TransactionLogsResponseViewModel
    {
        public Guid Id { get; set; }
        public string merchantID { get; set; }
        public string invoiceNo { get; set; }
        public string cardNo { get; set; }
        public decimal amount { get; set; }
        public string currencyCode { get; set; }
        public string tranRef { get; set; }
        public string referenceNo { get; set; }
        public string approvalCode { get; set; }
        public string eci { get; set; }
        public DateTime? transactionDateTime { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string respCode { get; set; }
        public string respDesc { get; set; }
        public string Remark { get; set; }

        public string paymentID { get; set; }
    }
}
