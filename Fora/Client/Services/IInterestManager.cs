namespace Fora.Client.Services;

public interface IInterestManager
{
    Task<List<InterestModel>> GetInterests();
    List<UserInterestModel> UserInterest { get; set; }
    Task<List<UserInterestModel>> PostInterest(InterestModel userInterest);
}