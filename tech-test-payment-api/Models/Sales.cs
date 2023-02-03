using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tech_test_payment_api.Models
{
    public class Sales
    {
        public Sales()
        {
        }

        public Sales(DateTime date, int sellerId, int saleNumber, string itens, SaleStatus status)
        {
            Date=date;
            SellerId=sellerId;
            SaleNumber=saleNumber;
            Itens=itens;
            Status=status;
        }

        public DateTime Date { get; set; }
        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        [Key]
        public int SaleNumber { get; set; }
        public string Itens { get; set; }
        public SaleStatus Status { get; set; }
    }
}
