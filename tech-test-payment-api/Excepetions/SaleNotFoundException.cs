namespace tech_test_payment_api.Excepetions
{
    public class SaleNotFoundException : Exception
    {
        public SaleNotFoundException(string? message) : base(message)
        {
        }
    }
}
