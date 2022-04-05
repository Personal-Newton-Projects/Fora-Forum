﻿namespace Fora.Client.Services
{
    public interface IUserManager
    {
        Task<UserModel> Create(UserModel user, LoginModel login);
        Task<List<UserModel>> GetUsers();
        Task<UserModel> GetById(int id);
        Task<UserModel> FindUserByName(string username);

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