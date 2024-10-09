
namespace Groceries
{
    public class InMemoryGroceriesRepositoryUsingEFCore : IGroceriesRepository
    {
        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public bool Save(Product p)
        {
            throw new NotImplementedException();
        }

        public bool TestConnection()
        {
            throw new NotImplementedException();
        }

        public void UpdateById(int id, Product updatedProduct)
        {
            throw new NotImplementedException();
        }
    }
}
