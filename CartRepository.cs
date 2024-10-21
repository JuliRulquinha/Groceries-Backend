namespace Groceries;
using System.Data.SqlClient;


public class CartRepository : ICartRepository
{
    private readonly SqlConnection connection;
    private SqlCommand command;
    public void AddToCart(Product product, string userName)
    {
        try
        {
            connection.Open();

            var cart = GetCartByUserName(userName);

            cart.Products.Add(product);
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            connection.Close();
        }

    }

    public Cart GetCartByUserName(string userName)
    {
        try
        {
            connection.Open();

            string commandText = $"SELECT * FROM Cart WHERE userName=@userName";
            command.CommandText = commandText;
            command.Parameters.AddWithValue("@userName", userName);
            var reader= command.ExecuteReader();
            Cart cart = new Cart();

            while (reader.Read())
            {
                cart.id = Convert.ToInt32(reader["id"]);
                cart.userName = reader["userName"].ToString();
            }

            return cart;
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

    public void RemoveFromCartUsingProductId(int id)
    {
        throw new NotImplementedException();
    }
}

