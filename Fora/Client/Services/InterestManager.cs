namespace Fora.Client.Services;

public class InterestManager : IInterestManager
{
    private readonly HttpClient _httpClient;
    public InterestManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public List<UserInterestModel> UserInterest { get; set; }
    public async Task <List<InterestModel>> GetInterests()
    {
        return await _httpClient.GetFromJsonAsync<List<InterestModel>>("api/interest");
    }

    public async Task<InterestModel> GetInterest(int id)
    {
        return await _httpClient.GetFromJsonAsync<InterestModel>($"api/interest/{id}");
    }

    public async Task <List<UserInterestModel>> PostInterest(InterestModel userInterest)
    {
        var result = await _httpClient.PostAsJsonAsync("api/interest/",userInterest);
        UserInterest = await result.Content.ReadFromJsonAsync<List<UserInterestModel>>();
        return UserInterest;
    }
        public async Task UpdateUser(UserModel user)
    {
        await _httpClient.PutAsJsonAsync($"api/interest/", user);
    }
}
}