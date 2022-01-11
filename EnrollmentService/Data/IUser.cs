using EnrollmentService.Data.DTO;
using EnrollmentService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnrollmentService.Data
{
    public interface IUser
    {
        Task Registration(CreateUserDto user);
        Task Addrole(string role);
        Task AddRoleToUser(string username, string user);
        Task<List<string>> GetRoleFromUser (string username);
        Task<User> Authenticate(string username, string password);
    }

}
