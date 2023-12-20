using Ecomerce.Models;
using System.ComponentModel.DataAnnotations;

namespace Ecomerce.ViewModels
{
	public class OrderAdminVM
	{
        public int OrderId { get; set; }

        public IEnumerable<OrderDetails>? detailsList { get; set; }

		public string Name { get; set; }
	
		public string postalCode { get; set; }

		public string State { get; set; }

		public string City { get; set; }

		public string address { get; set; }
	
		public string PhoneNumber { get; set; }
		public string OrderStatus { get; set; }

		public double OrderTotal { get; set; }
	}
}
