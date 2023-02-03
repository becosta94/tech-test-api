using tech_test_payment_api.Models;

namespace tech_test_payment_api.Interfaces
{
    public interface ISaleFactory
    {
        Sales Create(int sellerId, string sellerName, int sellerCpf, string sellerEmail, string sellerPhone, string itens);
    }
}
