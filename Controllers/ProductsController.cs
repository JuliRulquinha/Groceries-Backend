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
        public string GetProduct(string productName)
        {
            SqlConnection connection = new SqlConnection("Server=localhost;Database=Groceries;Trusted_Connection=True;");

            GroceriesRepository productRepository = new GroceriesRepository(connection);
            var p = productRepository.GetProductByNameUsingDapper(productName);

            if (p is not null && p.id > 0)
            {
                return $"{p.name} {p.Description}";

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

        public async Task CreateProduct([FromBody]Product p)
        {
            SqlConnection connection = new SqlConnection("Server=localhost;Database=Groceries;Trusted_Connection=True;");

            GroceriesRepository repository = new GroceriesRepository(connection);

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            await repository.SaveWithDapper(p);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

    }

}
