using Microsoft.AspNetCore.Mvc;
using Groceries;

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
            GroceriesRepository productRepository = new GroceriesRepository();
            var p = productRepository.GetProductByName(productName);

            if(p is not null && p.id> 0)
            {
                return $"{p.name} {p.Description}";

            }
            else 
            {
                return "Product not found.";
            }

        }
    }
}
