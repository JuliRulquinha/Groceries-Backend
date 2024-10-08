
namespace Groceries
{
    public class InMemoryGroceriesRepository : IGroceriesRepository
    {
        private List<Product> products;
        public InMemoryGroceriesRepository()
        {
            products = new List<Product>
            {
                new Product
                {
                    id = 1,
                    Name = "Test1"
                },
                new Product
                {
                    id = 2,
                    Name = "Laptop"
                }
            };
        }
        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteByIdUsingDapper(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdUsingDapperAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByName(string name)
        {
            return this.products.FirstOrDefault(p => p.Name == name);
        }

        public Product GetProductByNameUsingDapper(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsByCategoryIdUsingDapperAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public bool Save(Product p)
        {
            this.products.Add(p);
            return true;
        }

        public Task<bool> SaveListOfProductsWithDapperAsync(List<Product> Products)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveWithDapper(Product p)
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

        public void UpdateByIdUsingDapper(int id, Product updatedProduct)
        {
            throw new NotImplementedException();
        }

        public Task UpdateByIdUsingDapperAsync<Product>(Product updatedProduct)
        {
            throw new NotImplementedException();
        }
    }
}
