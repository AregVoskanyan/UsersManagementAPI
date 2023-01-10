using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersManagementAPI.Data;
using UsersManagementAPI.Models;

namespace UsersManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var usersList = await _context.Users.ToListAsync();
            return _mapper.Map<List<UserModel>>(usersList);
        }

        public async Task<UserModel> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return _mapper.Map<UserModel>(user);
        }

        public async Task<int> AddUserAsync(UserModel userModel)
        {
            var user = new User()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Age = userModel.Age,
                PhoneNumber = userModel.PhoneNumber,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task UpdateUserAsync(int userId, UserModel userModel)
        {
            var user = new User()
            {
                Id = userId,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Age = userModel.Age,
                PhoneNumber = userModel.PhoneNumber,
            };

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserPatchAsync(int userId, JsonPatchDocument userModel)
        {
            var user = await _context.Users.FindAsync(userId);
            if(user != null)
            {
                userModel.ApplyTo(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = new User() { Id = userId };
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}