using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomerce.Models
{
    public class ShoppingCart
    {
        
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Range(1, 1000, ErrorMessage = "please enter count between 1 and 1000")]
        public int Count { get; set; }
        [ForeignKey("product")]
        public int ProductId { get; set; }
        public Product? product { get; set; }
        [ForeignKey("user")]
        public string UserId { get; set; }
        public user? user { get; set; }
        
        public double price { get; set; }
    }
}
