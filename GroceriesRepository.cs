using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Dapper;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;




namespace Groceries
{
    public class GroceriesRepository
    {
        private readonly SqlConnection _connection;
        private SqlCommand _command;

        public GroceriesRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public bool TestConnection()
        {

            try
            {
                _connection.Open();
                Console.WriteLine("It worked");
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Product GetProductByNameUsingDapper(string name)
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
        public Product GetProductByName(string name)
        {
            try
            {
                _connection.Open();

                string searchCommand = $"SELECT * FROM Products WHERE Name like '{name}'";
                SqlCommand searchForProduct = _connection.CreateCommand();
                searchForProduct.CommandText = searchCommand;
                var reader = searchForProduct.ExecuteReader();
                Product p = new Product();

                while (reader.Read())
                {
                    p.name = reader["name"].ToString();
                    p.description = reader["Description"].ToString();
                    p.id = Convert.ToInt32(reader["id"]);
                }

                return p;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;

        }
        public bool Save(Product p)
        {

            try
            {
                _connection.Open();

                //string commandText = $"INSERT INTO Products(Name, imgUrl, Description, Price, Quantity, categoryID) VALUES('{p.name}','{p.imgUrl}','{p.Description}',{p.price},{p.quantity}, {p.categoryId} ) ";
                string commandText = $"INSERT INTO Products(Name, imgUrl, Description, Price, Quantity, categoryID) VALUES(@Name, @imgUrl, @Description, @Price, @Quantity, @categoryID ) ";
                _command = _connection.CreateCommand();
                _command.CommandText = commandText;
                _command.Parameters.AddWithValue("@name", p.name);
                _command.Parameters.AddWithValue("@imgUrl", p.imgUrl);
                _command.Parameters.AddWithValue("@Description", p.description);
                _command.Parameters.AddWithValue("@price", p.price);
                _command.Parameters.AddWithValue("@quantity", p.quantity);
                _command.Parameters.AddWithValue("@categoryID", p.categoryId);
                var result = _command.ExecuteNonQuery();

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

        public async Task<bool> SaveWithDapper(Product p)
        {

            try
            {
                _connection.Open();

                //string commandText = $"INSERT INTO Products(Name, imgUrl, Description, Price, Quantity, categoryID) VALUES('{p.name}','{p.imgUrl}','{p.Description}',{p.price},{p.quantity}, {p.categoryId} ) ";
                var result = await _connection.ExecuteAsync($"INSERT INTO Products(Name, imgUrl, Description, Price, Quantity, categoryID) VALUES(@Name, @imgUrl, @Description, @Price, @Quantity, @categoryID ) ", p);


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

        public Product GetById(int id)
        {

            try
            {
                _connection.Open();

                string commandText = $"SELECT * FROM Products WHERE id={id}";
                _command = _connection.CreateCommand();
                _command.CommandText = commandText;
                var output = _command.ExecuteNonQuery();

                if (output == 0)
                {
                    Console.WriteLine("The request failed.");
                }
                else
                {
                    Console.WriteLine($"Number of rows targeted: {output}");
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
                _command.Parameters.AddWithValue("@name", updatedProduct.name);
                _command.Parameters.AddWithValue("@imgUrl", updatedProduct.imgUrl);
                _command.Parameters.AddWithValue("@Description", updatedProduct.description);
                _command.Parameters.AddWithValue("@price", updatedProduct.price);
                _command.Parameters.AddWithValue("@quantity", updatedProduct.quantity);
                _command.Parameters.AddWithValue("@categoryID", updatedProduct.categoryId);
                _command.Parameters.AddWithValue("@id", id);

                var result = _command.ExecuteNonQuery();

                if (result == 0)
                {
                    Console.WriteLine("The request failed.");
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
        public void DeleteById(int id)
        {

            try
            {
                _connection.Open();

                string commandText = $"DELETE FROM Products WHERE id=@id";
                _command = _connection.CreateCommand();
                _command.CommandText = commandText;
                _command.Parameters.AddWithValue(@"id", id);

                var numberOfRowsAffected = _command.ExecuteNonQuery();

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

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            List<Product> products = new List<Product>();


            try
            {
                _connection.Open();


                var command = new SqlCommand($"SELECT * FROM Products where categoryId = {categoryId}", _connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var p = new Product();
                    var isAvailable = false;

                    p.id = Convert.ToInt32(reader["id"]);
                    p.name = reader["name"].ToString();
                    p.price = Convert.ToDecimal(reader["price"]);
                    p.imgUrl = reader["imgUrl"].ToString();
                    p.description = reader["Description"].ToString();
                    Boolean.TryParse(reader["isAvailable"].ToString(), out isAvailable);
                    p.isAvailable = isAvailable;
                    p.categoryId = Convert.ToInt32(reader["categoryId"]);
                    p.quantity = Convert.ToInt32(reader["quantity"]);


                    products.Add(p);
                }

                return products;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public async Task<IEnumerable<Product>> GetProductsByCategoryIdUsingDapperAsync(int categoryId)
        {
            try
            {
                _connection.Open();

                //return _connection.Query<Product>($"SELECT * FROM Products WHERE categoryID={categoryId}");
                return await _connection.QueryAsync<Product>($"SELECT * FROM Products WHERE categoryID={categoryId}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public void UpdateByIdUsingDapper(int id, Product updatedProduct)
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

        public async Task UpdateByIdUsingDapperAsync<Product>(Product updatedProduct)
        {
            try
            {
                _connection.Open();

                string commandText = $"UPDATE Products SET Name=@Name,imgUrl=@imgUrl,Description= @Description,Price= @Price,Quantity= @Quantity,categoryID= @categoryID WHERE id=@id";
                _command = _connection.CreateCommand();
                _command.CommandText = commandText;


                var result = await _connection.ExecuteAsync(commandText, updatedProduct);

                if (result == 0)
                {
                    Console.WriteLine("The request failed.");
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

        public void DeleteByIdUsingDapper(int id)
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

        public async Task DeleteByIdUsingDapperAsync(int id)
        {

            try
            {
                _connection.Open();

                string commandText = $"DELETE FROM Products WHERE id={id}";
                _command = _connection.CreateCommand();
                _command.CommandText = commandText;


                var numberOfRowsAffected = await _connection.ExecuteAsync(commandText);

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

        public async Task<bool> SaveListOfProductsWithDapperAsync(List<Product> Products)
        {

            try
            {
                _connection.Open();

                //string commandText = $"INSERT INTO Products(Name, imgUrl, Description, Price, Quantity, categoryID) VALUES('{p.name}','{p.imgUrl}','{p.Description}',{p.price},{p.quantity}, {p.categoryId} ) ";
                var result = await _connection.ExecuteAsync($"INSERT INTO Products(Name, imgUrl, Description, Price, Quantity, categoryID) VALUES(@Name, @imgUrl, @Description, @Price, @Quantity, @categoryID )", Products);


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
    }


}
