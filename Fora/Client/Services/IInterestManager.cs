namespace Fora.Client.Services;

public interface IInterestManager
{
    Task<List<InterestModel>> GetInterests();
    Task<InterestModel> GetInterest(int id);
    List<UserInterestModel> UserInterest { get; set; }
    Task<List<UserInterestModel>> PostInterest(InterestModel userInterest);
}