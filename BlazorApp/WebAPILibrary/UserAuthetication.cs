using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPILibrary.Models;

namespace WebAPILibrary
{
    public class UserAuthetication
    {
        private readonly IConfiguration _config;

        public UserAuthetication(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateJSONWebToken(UserModel userinfo)
        {
            var secirityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(secirityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userinfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userinfo.EmailAddress),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }

        public UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = null;
            if (login.UserName == "ashproghelp" && login.Password == "123")
            {
                user = new UserModel { UserName = "AshProgHelp", EmailAddress = "ashproghelp@gmail.com", Password = "123" };
            }
            return user;
        }
    }
}
