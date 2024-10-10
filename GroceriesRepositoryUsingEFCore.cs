
using Microsoft.EntityFrameworkCore;

namespace Groceries
{
    public class GroceriesRepositoryUsingEFCore : IGroceriesRepository
    {
        private readonly MyFirstContext _context;

        public GroceriesRepositoryUsingEFCore(MyFirstContext context)
        {
            _context = context;
        }
        public void DeleteById(int id)
        {
            try
            {
                var deletedItems = _context.Products.Where(p => p.id == id).ExecuteDelete();

                if (deletedItems == 0)
                {
                    Console.WriteLine($"{deletedItems} records were deleted. Execution failed.");
                }
                else
                {
                    Console.WriteLine($"Rows affected: {deletedItems}.");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Product GetById(int id)
        {
            try
            {
                return _context.Products.FirstOrDefault(p => p.id == id);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);           
            }
            return null;
        }

        public Product GetProductByName(string name)
        {
            try
            {
                return _context.Products.FirstOrDefault(p => p.Name.Equals(name));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            try
            {
                _context.Products.Where(p => p.CategoryId == categoryId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public bool Save(Product product)
        {
            try
            {
                _context.Products.Add(product);
                return _context.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public void UpdateById(int id, Product updatedProduct)
        {
            var productFromDb = GetById(id);
            productFromDb.Name = updatedProduct.Name;
            productFromDb.ImgUrl = updatedProduct.ImgUrl;
            productFromDb.Description = updatedProduct.Description;
            productFromDb.Price = updatedProduct.Price;
            productFromDb.Quantity = updatedProduct.Quantity;

            _context.SaveChanges();
        }

        public Task<bool> SaveListOfProducts(List<Product> Products) 
        {
            try
            {
                _context.Products.AddRange(Products);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
            
        }
        public IEnumerable<Product> GetAllProducts()
        {
            throw new NotImplementedException();
        }
    }
}
