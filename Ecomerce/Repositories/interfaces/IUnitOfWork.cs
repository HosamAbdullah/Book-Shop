namespace Ecomerce.Repositories.interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryInterface category { get; set; }
        public IProductInterface product { get; set; }
        public IShopingCartInterface shoppingCart { get; set; }
		public IOrderDetailsInterface orderDetails { get; set; }
		public IOrderInterface order { get; set; }


		void save();
    }
}
