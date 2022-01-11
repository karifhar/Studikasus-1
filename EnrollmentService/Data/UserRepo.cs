using EnrollmentService.Data.DTO;
using EnrollmentService.Helpers;
using EnrollmentService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EnrollmentService.Data
{
    public class UserRepo : IUser
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private AppSettings _tokenSettings;

        public UserRepo(UserManager<IdentityUser> user, RoleManager<IdentityRole> roleManager, IOptions<AppSettings> options )
        {
            _userManager = user;
            _roleManager = roleManager;
            _tokenSettings = options.Value;
        }

        public async Task Addrole(string role)
        {
            IdentityResult result;
            try
            {
                var roleExisting = await _roleManager.RoleExistsAsync(role);
                if (!roleExisting)
                {
                    result = await _roleManager.CreateAsync(new IdentityRole(role));
                }
                else
                {
                    throw new System.Exception($"Role {role} sudah ada");
                }
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            } 
           
        }

        public async Task AddRoleToUser(string username, string role)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                await _userManager.AddToRoleAsync(user, role);
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var getUser = await _userManager.FindByNameAsync(username);
            var validUser = await _userManager.CheckPasswordAsync(getUser, password);
            if (!validUser)
            {
                return null;
            }
            var user = new User
            {
                Username = username,
            };
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Username));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return user;

        }

        public async Task<List<string>> GetRoleFromUser(string username)
        {
            List<string> lstRole = new List<string>();
            var user = await _userManager.FindByNameAsync(username);

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var roleItem in roles)
            {
                lstRole.Add(roleItem);
            }
            return lstRole;
        }

       

        public async Task Registration(CreateUserDto user)
        {
            try
            {
                var newUser = new IdentityUser
                {
                    UserName = user.Username,
                    Email=user.Username,
                };
                var result = await _userManager.CreateAsync(newUser, user.Password);
                if (!result.Succeeded)
                    throw new System.Exception($"Gagal menambah user");
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }
        }
    }
}
