namespace Fora.Client.Services
{
    public interface ILoginManager
    {
        Task LogInWithUser(string username, string password);
    }
}