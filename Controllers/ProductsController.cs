using Microsoft.AspNetCore.Mvc;
using Groceries;
using System.Data.SqlClient;
using System.Timers;
using System.Diagnostics;

namespace Groceries.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        [HttpGet("SayHello")]

        public string Hello()
        {
            return "Hello from products controller";
        }
        [HttpGet("Products/{productName}")]
        public string GetProductByName(string productName)
        {
            SqlConnection connection = new SqlConnection("Server=localhost;Database=Groceries;Trusted_Connection=True;");

            GroceriesRepository productRepository = new GroceriesRepository(connection);
            var p = productRepository.GetProductByNameUsingDapper(productName);

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

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            var products = await productRepository.GetProductsByCategoryIdUsingDapperAsync(categoryId);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            return products;

        }

        [HttpPost("Products")]

        public async Task CreateProduct([FromBody] Product product)
        {
            SqlConnection connection = new SqlConnection("Server=localhost;Database=Groceries;Trusted_Connection=True;");

            var repository = new GroceriesRepository(connection);


            await repository.SaveWithDapper(product);
        }

        [HttpPut("Products/Update/{id}")]

        public async Task UpdateProduct([FromBody]Product updatedProduct)
        {
            SqlConnection connection = new SqlConnection("Server=localhost;Database=Groceries;Trusted_Connection=True;");

            var repository = new GroceriesRepository(connection);

            await repository.UpdateByIdUsingDapperAsync<Product>(updatedProduct);
        }

        [HttpDelete("Products/Delete/{id}")]

        public async Task DeleteProduct(int id)
        {
            SqlConnection connection = new SqlConnection("Server=localhost;Database=Groceries;Trusted_Connection=True;");

            var repository = new GroceriesRepository(connection);

            await repository.DeleteByIdUsingDapperAsync(id);
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

            await repository.SaveListOfProductsWithDapperAsync(Products.ToList());
        }

    }

}
