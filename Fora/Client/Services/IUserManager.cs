namespace Fora.Client.Services
{
    public interface IUserManager
    {
        Task<UserModel> Create(UserModel user);
        Task<List<UserModel>> GetUsers();
    }
}