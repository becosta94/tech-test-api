namespace tech_test_payment_api.Models
{
    public enum SaleStatus
    {
        WaitingPayment,
        PaymentAccepted,
        SentToCarrier,
        Delivered,
        Cancelled
    }
}
