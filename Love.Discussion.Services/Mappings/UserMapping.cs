using AutoMapper;
using Love.Discussion.Core.Entities;
using Love.Discussion.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Services.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserDto>();
        }
    }
}
