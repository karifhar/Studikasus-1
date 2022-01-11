using EnrollmentService.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EnrollmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        /* private IUser _user;

         public RoleController(IUser user)
         {
             _user = user;
         }

         public async Task<ActionResult> AddRole(string role)
         {
             try
             {
                 await _user.Addrole(role);
                 return Ok(new { Message = "Berhasil menambah role" });
             }
             catch (System.Exception ex)
             {
                 return BadRequest(new { Message= ex.Message });
             } 
         }*/
    }
}
