using Microsoft.AspNetCore.Mvc;

namespace Groceries.Controllers
{

    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository repository;

        public CategoryController(ICategoryRepository _repository)
        {
            repository = _repository;
        }
    }
}