namespace Groceries
{
    public interface IUserRepository
    {
        User Register(User user);
        User Authenticate(User user);
        string GenerateToken();
    }
}
