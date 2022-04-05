
namespace Fora.Client.Services
{
    public class UserManager : IUserManager
    {
        private readonly HttpClient _httpClient;
        public UserManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> Create(UserModel user)    
        {
            var result = await _httpClient.PostAsJsonAsync("api/identityuser/", user);
            var id = await result.Content.ReadFromJsonAsync<int>();
            Console.WriteLine(id);
            return id;
        }
        public async Task<List<UserModel>> GetUsers()
        {
           return await _httpClient.GetFromJsonAsync<List<UserModel>>("api/user");
        }

    }

}
