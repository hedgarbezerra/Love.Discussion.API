using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Services
{
    public class UserService
    {
        //public UserService(IWebHostEnvironment env)
        //{

        //}
        //public string GenerateToken()
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes("");
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Email, user.Email),
        //            new Claim(ClaimTypes.Name, user.Username),
        //            new Claim(ClaimTypes.Role, user.Role.ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddMinutes(15),
        //        Issuer = "NET Core API",
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}
    }
}
