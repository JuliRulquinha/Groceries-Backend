namespace Groceries
{
    public class Product
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string? imgUrl { get; set; }
        public string Description { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public bool? isAvailable { get; set; }
        public int categoryId { get; set; }

        public Product()
        {
            
        }
    }
    
}

