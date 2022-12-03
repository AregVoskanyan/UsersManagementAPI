using AutoMapper;
using UsersManagementAPI.Data;
using UsersManagementAPI.Models;

namespace UsersManagementAPI.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<User, UserModel>();
        }
    }
}
