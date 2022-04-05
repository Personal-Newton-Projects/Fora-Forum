using Blazored.LocalStorage;

namespace Fora.Client.Services
{
    public class LoginManager : ILoginManager
    {

        private readonly ILocalStorageService localStorageService;
        private readonly HttpClient _httpClient;
        private readonly IUserManager userManager;

        public LoginManager(ILocalStorageService localStorageService, HttpClient httpClient, IUserManager UserManager)
        {
            this.localStorageService = localStorageService;
            this._httpClient = httpClient;
            this.userManager = UserManager;
        }

        public async Task LogInWithUser(string username, string password)
        {
            Console.WriteLine("Attempted Login");
            if(username != null && password != null)
            { 
                string loginToken = await _httpClient.GetFromJsonAsync<string>($"api/identityuser/verify/{username}/{password}");
                Console.WriteLine("Login attempted");
                if(!String.IsNullOrEmpty(loginToken))
                {
                    StoreUser(loginToken);
                    Console.WriteLine("Logged in");
                }
            }
        }

        public async Task StoreUser(string id)
        {
            if(id.Length > 1)
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
           string id = await localStorageService.GetItemAsync<string>("user");
            return id != String.Empty;
        }

        [System.Obsolete("Not implemented", true)]
        public async Task<UserModel> GetLoggedInUser()
        {
            if(await IsLoggedIn())
            {
                string id = await localStorageService.GetItemAsync<string>("user");
                string username = await _httpClient.GetFromJsonAsync<string>($"api/identityuser/ID/{id}");
                return await userManager.FindUserByName(username);

            }
            return null;
        }
    }
}
