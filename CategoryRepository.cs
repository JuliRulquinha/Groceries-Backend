namespace Groceries;
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

public class CategoryRepository
{

    private readonly SqlConnection connection;
    private SqlCommand command;

    public CategoryRepository(SqlConnection _connection)
    {
        connection = _connection;
    }

    public Category GetCategoryById(int id)
    {
        connection.Open();

        string commandText = $"SELECT * FROM Category WHERE id=@id";
        command = connection.CreateCommand();
        command.CommandText = commandText;
        command.Parameters.AddWithValue("@id", id);
        var result = command.ExecuteNonQuery();

        if (result != 0)
        {
            Console.WriteLine($"{result} was found.");
        }
        else
        {
            Console.WriteLine($"Failed request");
        }

        return null;
    }

    public Category GetCategoryByName(Category category)
    {
        connection.Open();

        string commandText = $"SELECT * FROM Category WHERE name=@name";
        command.CommandText = commandText;
        command.Parameters.AddWithValue ("@name", category.Name);
        var result = command.ExecuteNonQuery();

        if (result != 0)
        {
            Console.WriteLine($"{result} category was found.");
        }
        else 
        {
            Console.WriteLine($"No categories were found.");
        }

        return null; 
    }

    public void DeleteCategoryById(int id)
    {
        connection.Open();

        string commandText = $"DELETE FROM Categories WHERE id=@id";
        command.CommandText += commandText;
        command.Parameters.AddWithValue("@id",id);
        var result = command.ExecuteNonQuery();

        if (result != 0)
        {
            Console.WriteLine($"{result} item was deleted.");
        }
        else
        {
            Console.WriteLine($"No items where deleted.");
        }
    }

    public Category UpdateCategoryById(int id, Category updatedCategory)
    {
        connection.Open();

        string commandText = "UPDATE Category SET Name=@name WHERE id=@id";
        command.CommandText += commandText;
        command.Parameters.AddWithValue("@name",updatedCategory.Name);
        var result = command.ExecuteNonQuery();

        if (result != 0)
        {
            Console.WriteLine($"Number of rows affected: {result}");
        }
        else 
        {
            Console.WriteLine($"{result} items were updated");
        }
        return updatedCategory;
    }
}
    

