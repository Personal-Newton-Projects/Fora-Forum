
namespace Fora.Client.Services
{
    public class UserManager : IUserManager
    {
        private readonly HttpClient _httpClient;
        public UserManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserModel> AddUser(UserModel user)    
        {
            var result = await _httpClient.PostAsJsonAsync("api/user/", user);
            user = await result.Content.ReadFromJsonAsync<UserModel>();
            return user;
        }
        public async Task<List<UserModel>> GetUsers()
        {
           return await _httpClient.GetFromJsonAsync<List<UserModel>>("api/user");
        }
    }

}
