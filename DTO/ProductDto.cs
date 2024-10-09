﻿namespace Groceries.DTO
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string? ImgUrl { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
    }
}
