
namespace Fora.Client.Services
{
    public class UserManager : IUserManager
    {
        private readonly HttpClient _httpClient;
        public UserManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserModel> Create(LoginModel login)    
        {
            var userExist = await FindUserByName(login.Username);
            if (userExist != null)
            {
                return null;
            }
            else
            {
                var createUserResult = await _httpClient.PostAsJsonAsync("api/user/", login); // Create User
                if (createUserResult.IsSuccessStatusCode)
                {
                    Console.WriteLine("User has been created");
                    var createIdentityResult = await _httpClient.PostAsJsonAsync("api/identityuser/", login); // Create IdentityUser

                    if (createIdentityResult.IsSuccessStatusCode)
                    {
                        Console.WriteLine("IdentityUser has been created");
                        return await createUserResult.Content.ReadFromJsonAsync<UserModel>(); //Return created user
                    }
                    else
                    {
                        Console.WriteLine($"IdentityUser was NOT created ({createIdentityResult.StatusCode}) and User might be created");
                        Console.WriteLine("RESOLVE THIS!");
                        return await createUserResult.Content.ReadFromJsonAsync<UserModel>();
                    }

                }
                else
                {
                    Console.WriteLine($"User was not created ({createUserResult.StatusCode})");
                }
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
            return Users.SingleOrDefault(u => u.Username.ToLower() == username.ToLower());
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
        public async Task<UpdateUserInfoModel> UpdateUserPassword(UpdateUserInfoModel updateUserInfo)
        {
            UserModel userModel = await GetById(updateUserInfo.Id);
            string id = await _httpClient.GetFromJsonAsync<string>($"api/identityuser/{userModel.Username}");
            var updateResult = await _httpClient.PutAsJsonAsync($"api/identityuser/{id}/", updateUserInfo);
            new JsonDebug(updateResult);

            if (updateResult.IsSuccessStatusCode)
            {
                return updateUserInfo;
            }
            else
            {
                Console.WriteLine("Update UserInfo Failed!");
                return null;
            }
        }

    }



}
