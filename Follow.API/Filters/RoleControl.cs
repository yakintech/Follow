using Follow.Business.Repository;
using Follow.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Follow.API.Filters
{
    public class RoleControlAttribute : Attribute, IAuthorizationFilter
    {
        public string Role { get; set; }
        public RoleControlAttribute(string role)
        {
            Role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
           //burada rol kontrolü yapılacak

            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            //token icerisimdeki emaili al
            var email = JWTService.GetEmailFromClaim(token);

            GenericRepository<AdminUser> userRepository = new GenericRepository<AdminUser>();


            var roleCheck = userRepository.GetByQuery(x => x.Email == email).Role;

            if (roleCheck != Role)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.JsonResult("You are not authorized to access this resource.")
                {
                    StatusCode = 403
                };
                return;
            }


        }
    }

    public class JWTService
    {
        public static string GetEmailFromClaim(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var email = jsonToken.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            return email;
        }
    }
}
