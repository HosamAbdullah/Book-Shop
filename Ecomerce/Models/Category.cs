using System.ComponentModel.DataAnnotations;

namespace Ecomerce.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
        [Required]
        public int DisplayOrder { get; set; }
		public ICollection<Product> ProductList { get; set; } = new List<Product>();
	}
}
