namespace Groceries
{
    public class Product
    {
        public int? id { get; set; }
        public string Name { get; set; }
        public string? ImgUrl { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool? IsAvailable { get; set; }
        public int CategoryId { get; set; }

        public bool IsValid { get 
            { 
                return !string.IsNullOrWhiteSpace(this.Name);
            } set { IsValid = value; } } 

        public Product(string name, string? imgUrl, string description, decimal price, int quantity, bool? isAvailable, int categoryId)
        {
            this.Name = name;
            this.ImgUrl = imgUrl;
            this.Description = description;
            this.Price = price;
            this.Quantity = quantity;
            this.IsAvailable = isAvailable;
            this.CategoryId = categoryId;           
        }
       
        public Product()
        {
           
        }               
    }
}

