using tech_test_payment_api.Contexts;
using tech_test_payment_api.Interfaces;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Repository
{
    public class SalesRepository : ISaleFactory, ISaleFinder, ISaleUpdateStatus
    {
        private readonly SellersContext _sellerContext;
        private readonly SalesContext _salesContext;

        public SalesRepository()
        {
        }

        public SalesRepository(SalesContext salesContext, SellersContext sellerContext)
        {
            _sellerContext = sellerContext;
            _salesContext = salesContext;
        }

        public Sales Create(int sellerId, string sellerName, int sellerCpf, string sellerEmail, string sellerPhone, string itens)
        {
            Sellers? currentSeller = _sellerContext.Sellers.Find(sellerId);
            if (currentSeller == null)
            {
                currentSeller = new Sellers();
                currentSeller.Phone = sellerPhone;
                currentSeller.Name = sellerName;
                currentSeller.Email = sellerEmail;
                currentSeller.Cpf = sellerCpf;
                _sellerContext.Sellers.Add(currentSeller);
                _sellerContext.SaveChanges();
            }
            if (itens == null)
                return null;
            Sales sale = new Sales();
            sale.Itens = itens;
            sale.Date = DateTime.Now;
            sale.SellerId = sellerId;
            sale.Status = SaleStatus.PaymentAccepted;
            _salesContext.Add(sale);
            _salesContext.SaveChanges();
            return sale;
        }

        public Sales Find(int saleNumber)
        {
            Sales? currentSale = _salesContext.Sales.Find(saleNumber);
            if (currentSale == null)
                return null;
            else
                return currentSale;
        }

        public int Update(int saleNumber, SaleStatus newStatus)
        {
            Sales? currentSale = _salesContext.Sales.Find(saleNumber);
            if (currentSale == null)
                return 1;
            if (currentSale.Status == SaleStatus.WaitingPayment)
            {
                if (newStatus != SaleStatus.PaymentAccepted && newStatus != SaleStatus.Cancelled)
                    return 2;
            }
            else if (currentSale.Status == SaleStatus.PaymentAccepted)
            {
                if (newStatus != SaleStatus.SentToCarrier && newStatus != SaleStatus.Cancelled)
                    return 3; 
            }
            else if (currentSale.Status == SaleStatus.SentToCarrier)
            {
                if (newStatus != SaleStatus.Delivered)
                    return 4;
            }
            currentSale.Status = newStatus;
            _salesContext.SaveChanges();
            return 0;
        }
    }
}
