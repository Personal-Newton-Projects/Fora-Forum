
namespace Fora.Client.Services
{
    public class UserManager : IUserManager
    {
        private readonly HttpClient _httpClient;
        public UserManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserModel> Create(UserModel user, LoginModel login)    
        {
            var createUserResult = await _httpClient.PostAsJsonAsync("api/user/", user); // Create User
            if(createUserResult.IsSuccessStatusCode)
            {
                Console.WriteLine("User has been created");
                var createIdentityResult = await _httpClient.PostAsJsonAsync("api/identityuser/", login); // Create IdentityUser

                if(createIdentityResult.IsSuccessStatusCode)
                {
                    Console.WriteLine("IdentityUser has been created");
                    return user; //Return created user
                }
                else
                {
                    Console.WriteLine($"IdentityUser was NOT created ({createIdentityResult.StatusCode}) and User might be created");
                    Console.WriteLine("RESOLVE THIS!");
                    return user;
                }

            }
            else
            {
                Console.WriteLine($"User was not created ({createUserResult.StatusCode})");
            }
            return null;
            

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
            var Users = await GetUsers();
            return Users.Where(u => u.Username == username).FirstOrDefault();
        }

        public async Task<UserModel> UpdateUser(PostUserUpdateModel postUser)
        {
            // new System.Text.Json.JsonSerializerOptions() { ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles}
            // hackfix

            var updateResult = await _httpClient.PutAsJsonAsync($"api/user/", postUser);
            if(updateResult.IsSuccessStatusCode)
            {
                return await updateResult.Content.ReadFromJsonAsync<UserModel>();
            }
            else
            {
                Console.WriteLine("Update User Failed");
                return null;
            }

        }

        public async Task<List<PostUserInterestsModel>> UpdateUserInterests(List<PostUserInterestsModel> userInterests, int id)
        {
            // JsonSerializerOptions() { ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles }
            // Hackfix
            var updateResult = await _httpClient.PutAsJsonAsync($"api/user/interest/{id}", userInterests);
            new JsonDebug(updateResult);

            if(updateResult.IsSuccessStatusCode)
            {
                return userInterests;
            }
            else
            {
                Console.WriteLine("Update UserInterests failed");
                return null;
            }

        }

    }



}
