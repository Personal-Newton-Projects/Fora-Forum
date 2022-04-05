namespace Fora.Client.Services
{
    public interface ILoginManager
    {
        Task LogInWithUser(string username, string password);
        Task StoreUser(string id);
        Task<bool> IsLoggedIn();
        Task<UserModel> GetLoggedInUser();
    }
}