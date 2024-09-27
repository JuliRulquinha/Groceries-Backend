using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;




namespace Groceries
{
    public class GroceriesRepository
    {
        SqlConnection Connection = new SqlConnection("Server=localhost;Database=Groceries;Trusted_Connection=True;");
        SqlCommand? command;
        public bool TestConnection()
        {

            try
            {
                Connection.Open();
                Console.WriteLine("It worked");
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Products GetProductByName(string name)
        {
            try
            {
                Connection.Open();

                string searchCommand = $"SELECT * FROM Products WHERE Name like '{name}'";
                SqlCommand searchForProduct = Connection.CreateCommand();
                searchForProduct.CommandText = searchCommand;
                var reader = searchForProduct.ExecuteReader();
                Products p = new Products();

                while (reader.Read())
                {
                    p.name = reader["name"].ToString();
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
        public bool Save(Products p)
        {

            try
            {
                Connection.Open();

                string commandText = $"INSERT INTO Products(Name, imgUrl, Description, Price, Quantity, categoryID) VALUES('{p.name}','{p.imgUrl}','{p.Description}',{p.price},{p.quantity}, {p.categoryId} ) ";
                SqlCommand command = Connection.CreateCommand();
                command.CommandText = commandText;
                var result = command.ExecuteNonQuery();

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

        public Products GetById(int id)
        {

            try
            {
                Connection.Open();

                string commandText = $"SELECT * FROM Products WHERE id={id}";
                command = Connection.CreateCommand();
                command.CommandText = commandText;
                var output = command.ExecuteNonQuery();

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

        public void UpdateById(int id, Products updatedProduct)
        {
            try
            {
                Connection.Open();

                string commandText = $"UPDATE Products SET Name='{updatedProduct.name}',imgUrl='{updatedProduct.imgUrl}',Description='{updatedProduct.Description}',Price={updatedProduct.price},Quantity={updatedProduct.quantity},categoryID={updatedProduct.categoryId} WHERE id={id}";
                command = Connection.CreateCommand();
                command.CommandText = commandText;
                var result = command.ExecuteNonQuery();

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
                Connection.Open();

                string commandText = $"DELETE FROM Products WHERE id={id}";
                command = Connection.CreateCommand();
                command.CommandText = commandText;
                var numberOfRowsAffected = command.ExecuteNonQuery();

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
    }
}
