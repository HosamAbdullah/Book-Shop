using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecomerce.Models
{
	public class ApplicationDbContext:IdentityDbContext<user>
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<user> users  { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set;}
		public DbSet<Order> orders { get; set; }
		public DbSet<OrderDetails> orderDetails { get; set; }
	}
}
