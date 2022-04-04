namespace Fora.Client.Services
{
    public interface IUserManager
    {
        Task<UserModel> AddUser(UserModel user);
        Task<List<UserModel>> GetUsers();
    }
}