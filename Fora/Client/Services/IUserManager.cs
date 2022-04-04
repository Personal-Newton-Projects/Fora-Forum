namespace Fora.Client.Services
{
    public interface IUserManager
    {
        Task<UserModel> Post(UserModel user);
        Task<List<UserModel>> GetUsers();
    }
}