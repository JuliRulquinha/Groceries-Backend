
using System.Data.SqlClient;
using Dapper;
using Groceries.Extensions;

namespace Groceries
{
    public class GroceriesRepositoryUsingDapper : IGroceriesRepository
    {
        private readonly SqlConnection _connection;
        private SqlCommand _command;
        public void DeleteById(int id)
        {
            try
            {
                _connection.Open();

                string commandText = $"DELETE FROM Products WHERE id=@id";
                _command = _connection.CreateCommand();
                _command.CommandText = commandText;


                var numberOfRowsAffected = _connection.Execute(commandText, id);

                if (numberOfRowsAffected == 0)
                {
                    Console.WriteLine("No records were deleted.");
                }
                else
                {
                    Console.WriteLine($"{numberOfRowsAffected} rows were affected.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            try
            {
                _connection.Open();

                return _connection.QuerySingle<Product>($"SELECT * FROM Products WHERE id={id}");

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
                _connection.Open();

                return _connection.QuerySingle<Product>($"SELECT * FROM Products WHERE Name like '{name}'");

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
                _connection.Open();

                return _connection.Query<Product>($"SELECT * FROM Products WHERE categoryID={categoryId}");
               

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
                _connection.Open();

                //string commandText = $"INSERT INTO Products(Name, imgUrl, Description, Price, Quantity, categoryID) VALUES('{p.name}','{p.imgUrl}','{p.Description}',{p.price},{p.quantity}, {p.categoryId} ) ";
                var result = _connection.Execute($"INSERT INTO Products(Name, imgUrl, Description, Price, Quantity, categoryID) VALUES(@Name, @imgUrl, @Description, @Price, @Quantity, @categoryID ) ", product);


                if (result == 0)
                {
                    Console.WriteLine("The insertion command failed to excute.");

                }
                else
                {
                    Console.WriteLine($"Inserted items: {result}");
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }

        public Task<bool> SaveListOfProducts(List<Product> Products)
        {
            try
            {
                _connection.Open();

                var validProducts = Products.Where(p => p.IsValid);

                var result = _connection.Execute($"INSERT INTO Products(Name, imgUrl, Description, Price, Quantity, categoryID) VALUES(@Name, @imgUrl, @Description, @Price, @Quantity, @categoryID )", validProducts);


                if (result == 0)
                {
                    Console.WriteLine("The insertion command failed to excute.");

                }
                else
                {
                    Console.WriteLine($"Inserted items: {result}");
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public void UpdateById(int id, Product updatedProduct)
        {
            try
            {
                _connection.Open();

                string commandText = $"UPDATE Products SET Name=@Name,imgUrl=@imgUrl,Description= @Description,Price= @Price,Quantity= @Quantity,categoryID= @categoryID WHERE id=@id";
                _command = _connection.CreateCommand();
                _command.CommandText = commandText;


                var result = _connection.Execute(commandText, updatedProduct);

                if (result == 0)
                {
                    Console.WriteLine("The request failed.");
                    Console.WriteLine(updatedProduct.FakeAssMethod());
                }
                else
                {
                    Console.WriteLine($"Updated rows: {result}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
