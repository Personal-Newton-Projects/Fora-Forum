namespace Fora.Client.Services
{
    public interface IUserManager
    {
        Task<int> Create(UserModel user);
        Task<List<UserModel>> GetUsers();

        /// <summary>
        /// Returns the count of registered users
        /// </summary>
        /// <returns>An integer of the count</returns>
        async Task<int> GetUserCount()
        {
            var result = await GetUsers();
            return result.Count;
        }
    }
}