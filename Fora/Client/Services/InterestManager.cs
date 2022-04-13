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
        if(result.IsSuccessStatusCode)
        {
            return interest;
        }
        else
        {
            return null;
        }
    }
    
    public async Task<InterestModel> CreateNewInterest(CreateInterestsModel thisInterest)
    {
        var result = await _httpClient.PostAsJsonAsync("api/interest/", thisInterest);
        new JsonDebug(result);
        return await result.Content.ReadFromJsonAsync<InterestModel>();
    }

    
    public async Task<InterestModel> RemoveExistingInterest(int id)
    {
        var result = await _httpClient.DeleteAsync($"api/interest/{id}");
        new JsonDebug(result);
        if (result.IsSuccessStatusCode)
        {
            return await result.Content.ReadFromJsonAsync<InterestModel>();
        }
        else
        {
            return null;
        }
    }

}