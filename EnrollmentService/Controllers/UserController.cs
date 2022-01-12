using EnrollmentService.Data;
using EnrollmentService.Data.DTO;
using EnrollmentService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnrollmentService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Registration(CreateUserDto user)
        {
            try
            {
                await _user.Registration(user);
                return Ok(new { message = "Berhasil membuat akun" });
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("Role")]
        public async Task<ActionResult> AddRole(CreateRoleDto role)
        {
            try
            {
                await _user.Addrole(role.RoleName);
                return Ok(new { Message = "Berhasil menambah role" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPost("UserInRole")]
        public async Task<ActionResult> AddRoleToUser(string username, string role)
        {
            try
            {
                await _user.AddRoleToUser(username, role);
                return Ok(new { Message = $"Berhasil menambahkan role {role} ke {username}" });
            }
            catch (System.Exception ex)
            {

                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpGet("Role/{username}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<string>>> GetRolesByUser(string username)
        {
            var results = await _user.GetRoleFromUser(username);
            return Ok(results);
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Login(CreateUserDto input)
        {
            try
            {
                var user = await _user.Authenticate(input.Username, input.Password);
                if (user == null)
                {
                    return BadRequest(new { Message = "Username atau password salah" });
                }
                return Ok(user);
            }
            catch (System.Exception ex)
            {

                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
