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

    [System.Obsolete("Unused and might not be working!")]
    public async Task <InterestModel> PostInterest(InterestModel interest)
    {
        var result = await _httpClient.PostAsJsonAsync("api/interest/",interest);
    }

}