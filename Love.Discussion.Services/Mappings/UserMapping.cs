using AutoMapper;
using Love.Discussion.Core.Entities;
using Love.Discussion.Core.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Love.Discussion.Services.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
