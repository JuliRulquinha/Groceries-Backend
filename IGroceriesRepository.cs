
namespace Groceries
{
    public interface IGroceriesRepository
    {
        void DeleteById(int id);
        //void DeleteByIdUsingDapper(int id);
        //Task DeleteByIdUsingDapperAsync(int id);
        Product GetById(int id);
        Product GetProductByName(string name);
        //Product GetProductByNameUsingDapper(string name);
        IEnumerable<Product> GetProductsByCategoryId(int categoryId);
        //Task<IEnumerable<Product>> GetProductsByCategoryIdUsingDapperAsync(int categoryId);
        bool Save(Product p);
        //Task<bool> SaveListOfProductsWithDapperAsync(List<Product> Products);
        //Task<bool> SaveWithDapper(Product p);
        //bool TestConnection();
        void UpdateById(int id, Product updatedProduct);
        // void UpdateByIdUsingDapper(int id, Product updatedProduct);
        // Task UpdateByIdUsingDapperAsync<Product>(Product updatedProduct);
        Task<bool> SaveListOfProducts(List<Product> Products);
    }
}