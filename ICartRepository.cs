namespace Groceries
{
    public interface ICartRepository
    {
        Cart GetCartByUserName(string userName);
        void AddToCart(Product product, string userName);
        void RemoveFromCartUsingProductId(int id);
        
        
    }
}
