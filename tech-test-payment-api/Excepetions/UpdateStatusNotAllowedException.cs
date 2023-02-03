namespace tech_test_payment_api.Excepetions
{
    public class UpdateStatusNotAllowedException : Exception
    {
        public UpdateStatusNotAllowedException(string? message) : base(message)
        {
        }
    }
}
