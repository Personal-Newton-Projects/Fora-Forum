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

        /// <summary>
        /// Logins with <paramref name="login"/>
        /// </summary>
        /// <param name="login"></param>
        public async Task LogInWithUser(LoginModel login)
        {
            Console.WriteLine("Attempted Login");
            if(login.Username != null && login.Password != null)
            { 
                // This does not work V
                string loginToken = await _httpClient.GetFromJsonAsync<string>($"api/identityuser/verify/{login.Username}/{login.Password}");
                if(!String.IsNullOrEmpty(loginToken))
                {
                    StoreUser(loginToken); //Store user in local storage on sucessfull login
                    Console.WriteLine("Logged in");
                }
            }
        }

        /// <summary>
        /// Stores user in LocalStorage
        /// </summary>
        /// <param name="id">id of IdentityUser</param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Checks if the LocalStorage user id is empty
        /// </summary>
        /// <returns>true if it isn't</returns>
        public async Task<bool> IsLoggedIn()
        {
           string id = await localStorageService.GetItemAsync<string>("user");
            return id != String.Empty;
        }

        /// <summary>
        /// Gets the logged in user through api call and id in LocalStorage
        /// </summary>
        /// <returns>a <see cref="UserModel"/></returns>
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
