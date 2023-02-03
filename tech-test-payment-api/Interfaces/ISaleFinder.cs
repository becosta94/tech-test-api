using tech_test_payment_api.Models;

namespace tech_test_payment_api.Interfaces
{
    public interface ISaleFinder
    {
        Sales Find(int saleNumber);
    }
}
