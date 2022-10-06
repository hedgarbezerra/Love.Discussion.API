using Love.Discussion.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Core.Interfaces
{
    public interface IUserService
    {
        Task<bool> Create(UserDto user);
    }
}
