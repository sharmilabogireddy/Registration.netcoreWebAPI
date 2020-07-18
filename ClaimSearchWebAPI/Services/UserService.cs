using System;
using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
using ClaimSearchWebAPI.dto;
using ClaimSearchWebAPI.Models;
using ClaimSearchWebAPI.Helper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;

namespace ClaimSearchWebAPI.Services
{
    public interface IUserService
    {
        UserDto Authenticate(string username, string password);
        IEnumerable<UserDto> GetAll();
        UserDto GetById(int id);
        bool IsEmailExist(string EmailId);
        bool RegisterUser(UserRegistrationDto userDto);
    }

    public class UserService : IUserService
    {
        private UserRegistrationContext dbContext = new UserRegistrationContext();
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        /* private List<UserDto> _users = new List<UserDto>
         {
             new Users { UserId = new Guid(), UserName = "admin", Password = "admin", new Roles {RoleId: 1, RoleName: "Admin"} },
             new Users { UserId = new Guid(), UserName = "TPA", Password = "TPA", Role = Role.TPA },
             new Users { UserId = new Guid(), UserName = "SuperUser", Password="SuperUser", Role = Role.SuperUser }
         };*/

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public UserDto Authenticate(string UserName, string Password)
        {
            var user = dbContext.Users.SingleOrDefault(x => x.UserName == UserName);

            // return null if user not found
            if (user == null)
                return null;

            bool IsValidPass = Hash.VerifyHash(Password, _appSettings.Salt, user.Password);
            if (!IsValidPass)
                return null;

            // Load the related Role
            dbContext.Entry(user).Reference(u => u.Role).Load();

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role.RoleName),
                    new Claim(ClaimTypes.Email, user.EmailId)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            string token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            UserDto userDto = new UserDto
            {
                Id = user.UserId,
                UserName = user.UserName,
                Token = token,
                Role = user.Role.RoleName
            };
            return userDto;
        }

        public IEnumerable<UserDto> GetAll()
        {
            //return _users.WithoutPasswords();
            /*dbContext.Users;
            UserDto userDto = new UserDto
            {
                Id = user.UserId,
                UserName = user.UserName,
                Token = "This is token",
                Role = user.Role.RoleName
            };*/
            return null;
        }

        public UserDto GetById(int id)
        {
            //var user = _users.FirstOrDefault(x => x.Id == id);
            //return user.WithoutPassword();
            return null;
        }

        public bool IsEmailExist(string EmailId)
        {
            var v = dbContext.Users.Where(a => a.EmailId == EmailId).FirstOrDefault();
            return v != null;
        }

        public bool RegisterUser(UserRegistrationDto userDto)
        {
            Users user = new Users
            {
                UserId = Guid.NewGuid(),
                UserName = userDto.UserName,
                EmailId = userDto.EmailId,
                Password = Hash.CreateHash(userDto.Password, _appSettings.Salt),
                RoleId = userDto.RoleId,
                IsActive = "Y",
                CreatedDate = DateTime.Now
            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return true;
        }
    }
}