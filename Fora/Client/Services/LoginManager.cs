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
                    await StoreUser(loginToken); //Store user in local storage on sucessfull login
                    Console.WriteLine("Logged in");
                }
            }
        }

        public async Task<bool> VerifyLogin(LoginModel login)
        {
            string loginToken = await _httpClient.GetFromJsonAsync<string>($"api/identityuser/verify/{login.Username}/{login.Password}");
            if (!String.IsNullOrEmpty(loginToken))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Stores user in LocalStorage
        /// </summary>
        /// <param name="id">id of IdentityUser</param>
        /// <returns></returns>
        public async Task StoreUser(string? id)
        {

            if(!String.IsNullOrEmpty(id))
            {
                if (!await DoesUserExist(id))
                {
                    await localStorageService.RemoveItemAsync("user");
                }

                await localStorageService.SetItemAsStringAsync("user", $"{id}");
            }
            else
            {
                await localStorageService.RemoveItemAsync("user");

            }



        }
        
        /// <summary>
        /// Checks if the LocalStorage user id is empty
        /// </summary>
        /// <returns>true if it isn't</returns>
        public async Task<bool> IsLoggedIn()
        {
            string? id = await localStorageService.GetItemAsync<string>("user");

            if (id == null) return false;
            
            if(await DoesUserExist(id) == true)
            {
                if (!String.IsNullOrEmpty(id) || id.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;

        }

        async Task<bool> DoesUserExist(string id)
        {
            var userExistResult = await _httpClient.GetFromJsonAsync<string>($"api/identityuser/ID/{id}");
            if(userExistResult != null)
            {
                return true;
            }
            else
            {
                return false;
            }
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
                if(!String.IsNullOrEmpty(id))
                {
                    string username = await _httpClient.GetFromJsonAsync<string>($"api/identityuser/ID/{id}");
                    if(!String.IsNullOrEmpty(username))
                    {
                        var result = await userManager.FindUserByName(username);
                        if(result != null)
                        {
                            Console.WriteLine($"User is {result.Username}");
                            return result;
                        }
                        else
                        {
                            return null;
                        }
                    }

                }
            }
            return null;
        }
    }
}
