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

        public bool IsValid = true;

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
            Validate();
        }
        public Product(string name)
        {
            this.Name = name;

            Validate();

        }
        private void Validate()
        {
            if (Name is null)
            {
                IsValid = false;
            }
            else if (Name is not null)
            {
                {
                    IsValid = true;
                }
            }

        }

    }
}

