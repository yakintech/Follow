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
    public class AuthController : ControllerBase
    {
        IGenericRepository<AdminUser> adminUserRepository;

        public AuthController(IGenericRepository<AdminUser> adminUserRepository)
        {
            this.adminUserRepository = adminUserRepository;
        }

        [HttpPost]
        public IActionResult Login(LoginRequestDto model)
        {
            var user = adminUserRepository.GetByQuery(x => x.Email == model.Email && x.Password == model.Password);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var tokenModel = new GarantiTokenHandler().CreateToken(user.Email);
            var response = new TokenResponseModel
            {
                Token = tokenModel.Token,
                RefreshToken = tokenModel.RefreshToken
            };

            return Ok(response);
        }

        //refreshtoken post endpoint
        [HttpPost("refresh")]
        public IActionResult RefreshToken(RefreshTokenRequestDto model)
        {
            GenericRepository<RefreshToken> refreshTokenRepository = new GenericRepository<RefreshToken>();

            var refreshToken = refreshTokenRepository.GetByQuery(x => x.Token == model.RefreshToken);

            if (refreshToken == null)
            {
                return NotFound("Refresh token not found.");
            }

            if (refreshToken.Expiration < System.DateTime.Now)
            {
                return BadRequest("Refresh token expired.");
            }

            var user = adminUserRepository.GetById(refreshToken.UserId);

            var tokenModel = new GarantiTokenHandler().CreateToken(user.Email);
            var response = new TokenResponseModel
            {
                Token = tokenModel.Token,
                RefreshToken = tokenModel.RefreshToken
            };

            return Ok(response);
        }

    }
}
