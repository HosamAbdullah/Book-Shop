using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomerce.Models
{
	public class OrderDetails
	{
        public int Id { get; set; }
        [ForeignKey("product")]
		public int ProductId { get; set; }
		public Product product { get; set; }
        public int Quantity { get; set; }
		[ForeignKey("order")]
		public int OrderId { get; set; }
        public Order order { get; set; }
		public double Price { get; set; }
    }
}
