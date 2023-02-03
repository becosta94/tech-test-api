using Microsoft.EntityFrameworkCore;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Contexts
{
    public class SellersContext : DbContext
    {
        public SellersContext(DbContextOptions<SellersContext> options) : base(options)
        {

        }
        public DbSet<Sellers> Sellers { get; set; }
    }
}
