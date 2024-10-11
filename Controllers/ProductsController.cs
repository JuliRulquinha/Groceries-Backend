using Microsoft.AspNetCore.Mvc;
using Groceries;
using System.Data.SqlClient;
using System.Timers;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using Groceries.DTO;
using Mapster;


namespace Groceries.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IGroceriesRepository _repository;
        public ProductsController(IGroceriesRepository repository)
        {
            _repository = repository;           
        }
        [HttpGet("GetAllProducts")]

        public IActionResult GetAllProducts()
        {
            return Ok(_repository.GetAllProducts());
        }

        [HttpGet("{productId}")]

        public IActionResult GetProductById(int productId)
        {
            var product = _repository.GetById(productId);
            return Ok(product);
        }

        [HttpGet("Products/{productName}")]
        public string GetProductByName(string productName)
        { 
            
            var p = _repository.GetProductByName(productName);

            if (p is not null && p.id > 0)
            {
                return $"{p.Name} {p.Description}";
            }
            else
            {
                return "Product not found.";
            }

        }

        [HttpGet("Products/Category/{categoryId}")]
        public async Task<IEnumerable<Product>> GetProductsByCategory(int categoryId)
        {
            SqlConnection connection = new SqlConnection("Server=localhost;Database=Groceries;Trusted_Connection=True;");


            GroceriesRepository productRepository = new GroceriesRepository(connection);

           
            var products = await productRepository.GetProductsByCategoryIdUsingDapperAsync(categoryId);

            return products;

        }

        [HttpPost("Products")]


        public Task CreateProduct([FromBody] ProductDto dto)
        {
            var product = dto.Adapt<Product>();
            return Task.Run(() => _repository.Save(product));
        }

        [HttpPut("Products/Update/{id}")]

        public void UpdateProduct([FromBody]ProductDto dto, int id)
        {
            var updatedProduct = dto.Adapt<Product>();
            _repository.UpdateById(id,updatedProduct);
        }

        [HttpDelete("Products/Delete/{id}")]

        public IActionResult DeleteProduct(int id)
        {
           
           _repository.DeleteById(id);
            return Ok("Product deleted.");
        }

        [HttpPost("Products/Insert/List")]

        public async Task SaveMultipleProducts([FromBody] IEnumerable<Product> Products)
        {
            //List<Product> products = new List<Product>
            //{
            //new Product("Laptop", "https://example.com/laptop.jpg", "High performance laptop", 1200.99m, 5, true, 1),
            //new Product("Smartphone", "https://example.com/smartphone.jpg", "Latest model smartphone", 799.49m, 10, true, 1),
            //new Product("Headphones", null, "Wireless noise-cancelling headphones", 199.99m, 25, true, 1),
            //new Product("Smartwatch", "https://example.com/smartwatch.jpg", "Fitness tracking smartwatch", 149.99m, 15, false, 1),
            //new Product("Tablet", null, "10-inch tablet with stylus support", 499.00m, 8, true, 1)
            //};

            SqlConnection connection = new SqlConnection("Server=localhost;Database=Groceries;Trusted_Connection=True;");

            var repository = new GroceriesRepository(connection);

            await repository.SaveListOfProducts(Products.ToList());
        }

    }

}
