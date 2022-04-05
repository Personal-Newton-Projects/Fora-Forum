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

        public async Task StoreUser(int id)
        {
            if(id > 0)
            {
                await localStorageService.SetItemAsStringAsync("user", $"{id}");
            }
            else
            {
                await localStorageService.SetItemAsStringAsync("user", $"{0}");
            }

        }
        
        public async Task<bool> IsLoggedIn()
        {
           int id = await localStorageService.GetItemAsync<int>("user");
            return id > 0;
        }

        [System.Obsolete("Not implemented", true)]
        public async Task<UserModel> GetLoggedInUser()
        {
            if(await IsLoggedIn())
            {
                return null;
            }
            return null;
        }
    }
}
