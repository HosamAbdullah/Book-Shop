using Microsoft.AspNetCore.Identity;

namespace Ecomerce.Models
{
    public class user:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<ShoppingCart> CartList { get; set; } = new List<ShoppingCart>();
		public ICollection<Order> OrderList { get; set; } = new List<Order>();

	}
}
