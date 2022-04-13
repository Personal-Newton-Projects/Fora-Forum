namespace Fora.Client.Services;

public interface IInterestManager
{
    Task<List<InterestModel>> GetInterests();
    Task<InterestModel> GetInterest(int id);
    Task<InterestModel> PostInterest(InterestModel interest);
    Task<InterestModel> CreateNewInterest(CreateInterestsModel thisInterest);
    Task<InterestModel> RemoveExistingInterest(int id);
}