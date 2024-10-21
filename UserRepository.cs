namespace Groceries;
using System.Data.SqlClient;


public class UserRepository : IUserRepository
{
    private readonly SqlConnection connection;
    private SqlCommand command;

    public UserRepository(SqlConnection _connection)
    {
        connection = _connection;
    }
    public User Register(User user)
    {
        try
        {
            connection.Open();

            command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Users (Name, Email, Password) VALUES(@Name, @Email, @Password)";
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password);
            var result = command.ExecuteNonQuery();

            if(result == 0)
            {
                Console.WriteLine("Request failed");
            }
            else
            {
                Console.WriteLine("User successfully registered.");
            }

            return user;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return null;
    }

    public User Authenticate(User user)
    {
        try
        {
            connection.Open();

            command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Users WHERE Email='{user.Email}' ";
            var reader = command.ExecuteReader();
            var userFromDb = new User();

            if (reader.Read())
            {
                userFromDb.Name = reader["Name"].ToString();
                userFromDb.Email = reader["Email"].ToString();
                userFromDb.Password = reader["Password"].ToString();

                return userFromDb;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return null;
    }

    public string GenerateToken()
    {
        throw new NotImplementedException();
    }

    
}

