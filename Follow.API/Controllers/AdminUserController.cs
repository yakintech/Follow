using Follow.API.DTO.AdminUser;
using Follow.API.Models;
using Follow.Business.Repository;
using Follow.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Follow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        GenericRepository<AdminUser> adminUserRepository;

        public AdminUserController()
        {
            adminUserRepository = new GenericRepository<AdminUser>();
        }

        [HttpPost]
        public IActionResult Login(LoginRequestDto model)
        {
            var user = adminUserRepository.GetByQuery(x => x.Email == model.Email && x.Password == model.Password);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var token = new GarantiTokenHandler().CreateToken(user.Email);

            return Ok(token);
        }
    }
}
