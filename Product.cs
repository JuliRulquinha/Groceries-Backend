namespace Groceries
{
    public class Product
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string? imgUrl { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public bool? isAvailable { get; set; }
        public int categoryId { get; set; }

        public Product(string name, string? imgUrl, string description, decimal price, int quantity, bool? isAvailable, int categoryId)
        {
            this.name = name;
            this.imgUrl = imgUrl;
            this.description = description;
            this.price = price;
            this.quantity = quantity;
            this.isAvailable = isAvailable;
            this.categoryId = categoryId;
        }
    }
    
}

