using Blazored.LocalStorage;

namespace Fora.Client.Services
{
    public class LoginManager : ILoginManager
    {

        private readonly ILocalStorageService localStorageService;
        private readonly HttpClient _httpClient;

        public LoginManager(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            this.localStorageService = localStorageService;
            this._httpClient = httpClient;
        }

        public async Task LogInWithUser(string username, string password)
        {
            Console.WriteLine("Attempted Login");
            if(username != null && password != null)
            { 
                var loginattemptResult = await _httpClient.GetFromJsonAsync<int>($"api/identityuser/verify/{username}/{password}");
                Console.WriteLine("Login attempted");
                if(loginattemptResult != 0)
                {
                    Console.WriteLine("Logged in");
                }
            }
        }
    }
}
