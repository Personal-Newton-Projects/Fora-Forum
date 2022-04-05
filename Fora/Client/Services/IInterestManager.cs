namespace Fora.Client.Services;

public interface IInterestManager
{
    Task<List<InterestModel>> GetInterests();
}