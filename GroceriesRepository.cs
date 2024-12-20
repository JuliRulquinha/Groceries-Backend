﻿using System.Data.SqlClient;
using Dapper;
using Groceries.Extensions;


namespace Groceries
{
    public class GroceriesRepository : IGroceriesRepository
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
        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _connection.Open();

                var products = _connection.Query<Product>("SELECT * FROM Products;");

                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return null;
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
                _command.CommandText = searchCommand;
                var reader = _command.ExecuteReader();
                Product p = new Product();

                while (reader.Read())
                {
                    p.Name = reader["name"].ToString();
                    p.Description = reader["Description"].ToString();
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
        public bool Save(Product product)
        {

            try
            {
                _connection.Open();

                //string commandText = $"INSERT INTO Products(Name, imgUrl, Description, Price, Quantity, categoryID) VALUES('{p.name}','{p.imgUrl}','{p.Description}',{p.price},{p.quantity}, {p.categoryId} ) ";
                string commandText = $"INSERT INTO Products(Name, imgUrl, Description, Price, Quantity, categoryID) VALUES(@Name, @imgUrl, @Description, @Price, @Quantity, @categoryID ) ";
                _command = _connection.CreateCommand();
                _command.CommandText = commandText;
                _command.Parameters.AddWithValue("@name", product.Name);
                _command.Parameters.AddWithValue("@imgUrl", product.ImgUrl);
                _command.Parameters.AddWithValue("@Description", product.Description);
                _command.Parameters.AddWithValue("@price", product.Price);
                _command.Parameters.AddWithValue("@quantity", product.Quantity);
                _command.Parameters.AddWithValue("@categoryID", 2);
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
            finally
            {
                _connection.Close();
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
                var reader = _command.ExecuteReader();
                var p = new Product();

                if (reader.Read())
                {
                    p.Name = reader["name"].ToString();
                    p.Description = reader["Description"].ToString();
                    p.id = Convert.ToInt32(reader["id"]);
                    p.ImgUrl = reader["imgUrl"].ToString();
                    p.Quantity = Convert.ToInt32(reader["Quantity"]);
                    p.Price = Convert.ToInt32(reader["Price"]);

                }

                return p;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
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
                _command.Parameters.AddWithValue("@name", updatedProduct.Name);
                _command.Parameters.AddWithValue("@imgUrl", updatedProduct.ImgUrl);
                _command.Parameters.AddWithValue("@Description", updatedProduct.Description);
                _command.Parameters.AddWithValue("@price", updatedProduct.Price);
                _command.Parameters.AddWithValue("@quantity", updatedProduct.Quantity);
                _command.Parameters.AddWithValue("@categoryID", 2);
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
            finally
            {
                _connection.Close();
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
            finally
            {
                _connection.Close();
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
                    p.Name = reader["name"].ToString();
                    p.Price = Convert.ToDecimal(reader["price"]);
                    p.ImgUrl = reader["imgUrl"].ToString();
                    p.Description = reader["Description"].ToString();
                    Boolean.TryParse(reader["isAvailable"].ToString(), out isAvailable);
                    p.IsAvailable = isAvailable;
                    p.CategoryId = Convert.ToInt32(reader["categoryId"]);
                    p.Quantity = Convert.ToInt32(reader["quantity"]);


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

        public Task<bool> SaveListOfProducts(List<Product> Products)
        {

            try
            {
                _connection.Open();

                var validProducts = Products.Where(p => p.IsValid);

                //foreach (var product in Products)
                //{
                //    if (product.IsValid)
                //    {
                //        validProducts.Add(product);
                //    }
                //}

                // which one is better ?




                //string commandText = $"INSERT INTO Products(Name, imgUrl, Description, Price, Quantity, categoryID) VALUES('{p.name}','{p.imgUrl}','{p.Description}',{p.price},{p.quantity}, {p.categoryId} ) ";
                var result =  _connection.Execute($"INSERT INTO Products(Name, imgUrl, Description, Price, Quantity, categoryID) VALUES(@Name, @imgUrl, @Description, @Price, @Quantity, @categoryID )", validProducts);


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
    }


}
