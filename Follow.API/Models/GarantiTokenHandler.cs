using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Follow.API.Models
{
    public class GarantiTokenHandler
    {
        public string CreateToken(string email)
        {
            var claimsData  = new[]
            {
                new Claim(ClaimTypes.Email, email)
            };


            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("loremipsumloremipsumloremipsumloremipsumloremipsumloremipsumloremipsum"));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);


            var token = new JwtSecurityToken(
                issuer: "garanti",
                audience: "garanti",
                claims: claimsData,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
