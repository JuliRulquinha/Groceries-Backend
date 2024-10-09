using System.Data.SqlClient;
using Dapper;

namespace Groceries
{
    public class CategoryRepositoryUsingDapper : ICategoryRepository
    {
        private readonly SqlConnection _connection;
        private SqlCommand _command;
        public void DeleteCategoryById(int id)
        {
            try
            {
                _connection.Open();

                var result = _connection.Execute($"DELETE * FROM Category WHERE id={id}", id);

                if (result > 0)
                {
                    Console.WriteLine($"Deleted items: {result}.");
                }
                else
                {
                    Console.WriteLine($"Deleted items: {result}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Category GetCategoryById(int id)
        {
            try
            {
                _connection.Open();

                var result = _connection.QuerySingle($"SELECT * FROM Category WHERE id={id}", id);

                if (result > 0)
                {
                    Console.WriteLine($"{result} record was found.");
                }
                else
                {
                    Console.WriteLine("No records were found.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Category GetCategoryByName(string name)
        {
            try
            {
                _connection.Open();

                var result = _connection.QuerySingle($"SELECT * FROM Category WHERE Name='{name}'",name);

                if (result > 0)
                {
                    Console.WriteLine($"{result} record was found.");
                }
                else
                {
                    Console.WriteLine("No records were found.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Category UpdateCategoryById(int id, Category updatedCategory)
        {
            try
            {
                _connection.Open();


                var result = _connection.Execute($"UPDATE Category SET Name=@Name WHERE id=@id", updatedCategory);

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

            return null;
        }
    }
}
