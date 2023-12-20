using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomerce.Models
{
	public class Order
	{
        public int Id { get; set; }
		public DateTime OrderDate { get; set; }
		public DateTime ShippingDate { get; set; }
        public double OrderTotal { get; set; }
		public string? OrderStatus { get; set; }
		[ForeignKey("user")]
		public String UserId { get; set; }
		public user user { get; set; }
		public string Name { get; set; }
		public string postalCode { get; set; }
		public string State { get; set; }
		public string City { get; set; }
		public string address { get; set; }
		public string PhoneNumber { get; set; }
		public ICollection<OrderDetails>? OrderDetailsList { get; set; } = new List<OrderDetails>();

    }
}
