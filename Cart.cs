namespace Groceries
{
    public class Cart
    {
        public int id { get; set; }
        public List<Product> Products;
        public decimal Subtotal { get; set; }
        public string userName { get; set; }
        public Cart()
        {

        }
    }
    
}
