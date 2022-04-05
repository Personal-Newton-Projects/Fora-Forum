
namespace Fora.Client.Services
{
    public class UserManager : IUserManager
    {
        private readonly HttpClient _httpClient;
        public UserManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Create(UserModel user)    
        {
            var result = await _httpClient.PostAsJsonAsync("api/identityuser/", user);
            var id = await result.Content.ReadFromJsonAsync<string>();
            Console.WriteLine(id);
            return id;
        }
        public async Task<List<UserModel>> GetUsers()
        {
           return await _httpClient.GetFromJsonAsync<List<UserModel>>("api/user");
        }

        public async Task<UserModel> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<UserModel>($"api/user/{id}");

        }

        public async Task<UserModel> FindUserByName(string username)
        {
            return GetUsers().Result.Where(u => u.Username == username).FirstOrDefault();
        }

    }

}
