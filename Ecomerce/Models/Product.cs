using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomerce.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(1, 1000)]
        [Display(Name = "List Price")]
        public double ListPrice { get; set; }
        [Required]
        [Range(1, 1000)]
        [Display(Name = "Price for 1-50")]
        public double Price { get; set; }
        [Required]
        [Range(1, 1000)]
        [Display(Name = "Price for 50+")]
        public double Price50 { get; set; }
        [Required]
        [Range(1, 1000)]
        [Display(Name = "List Price for 100+")]
        public double Price100 { get; set; }
        public string ImgUrl { get; set; }
        [ForeignKey(nameof(category))]
        public int categoryId { get; set; }
        public Category? category { get; set; }
        public ICollection<ShoppingCart> CartList { get; set; } = new List<ShoppingCart>();
		public ICollection<OrderDetails> OrderDetailsList { get; set; } = new List<OrderDetails>();



	}
}
