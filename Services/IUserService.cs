using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersManagementAPI.Models;

namespace UsersManagementAPI.Services
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByIdAsync(int userId);
        Task<int> AddUserAsync(UserModel user);
        Task UpdateUserAsync(int userId, UserModel user);
        Task UpdateUserPatchAsync(int userId, JsonPatchDocument user);
        Task DeleteUserAsync(int userId);
    }
}
