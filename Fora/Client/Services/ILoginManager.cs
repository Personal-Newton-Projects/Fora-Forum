namespace Fora.Client.Services
{
    public interface ILoginManager
    {
        Task<bool> LogInWithUser(LoginModel login);
        Task StoreUser(string id);
        Task<bool> IsLoggedIn();
        Task<UserModel> GetLoggedInUser();
        Task<bool> VerifyLogin(LoginModel login);
    }
}