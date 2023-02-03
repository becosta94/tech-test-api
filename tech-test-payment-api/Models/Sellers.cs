using System.ComponentModel.DataAnnotations;

namespace tech_test_payment_api.Models
{
    public class Sellers
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cpf { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

    }
}
