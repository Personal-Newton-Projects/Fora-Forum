namespace Fora.Client.Services
{
    public interface ILoginManager
    {
        Task LogInWithUser(LoginModel login);
        Task StoreUser(string id);
        Task<bool> IsLoggedIn();
        Task<UserModel> GetLoggedInUser();
    }
}