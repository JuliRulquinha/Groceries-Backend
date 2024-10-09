namespace Groceries
{
    public class CategoryRepositoryUsingEFCore : ICategoryRepository
    {
        private readonly MyFirstContext _context;

        public CategoryRepositoryUsingEFCore(MyFirstContext context)
        {
            _context = context;
        }

        public void DeleteCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryByName(string name)
        {
            throw new NotImplementedException();
        }

        public Category UpdateCategoryById(int id, Category updatedCategory)
        {
            throw new NotImplementedException();
        }
    }
}
