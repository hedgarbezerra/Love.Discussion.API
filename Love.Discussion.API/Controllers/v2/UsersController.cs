using FluentValidation;
using Love.Discussion.Core.Interfaces;
using Love.Discussion.Core.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Love.Discussion.API.Controllers.v2
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserDto> _validator;

        public UsersController(IUserService userService, IValidator<UserDto> validator)
        {
            _userService = userService;
            _validator = validator;
        }
    }
}
