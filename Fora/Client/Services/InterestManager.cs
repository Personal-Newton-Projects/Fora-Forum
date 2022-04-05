namespace Fora.Client.Services;

public class InterestManager : IInterestManager
{
    private readonly HttpClient _httpClient;
    public InterestManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task <List<InterestModel>> GetInterests()
    {
        return await _httpClient.GetFromJsonAsync<List<InterestModel>>("api/interests");
    }
}