using Follow.Business.Repository;
using Follow.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Follow.API.Models
{
    public class GarantiTokenHandler
    {
        public TokenModel CreateToken(string email)
        {
            var claimsData = new[]
            {
                new Claim(ClaimTypes.Email, email)
            };


            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("loremipsumloremipsumloremipsumloremipsumloremipsumloremipsumloremipsum"));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);


            var token = new JwtSecurityToken(
                issuer: "garanti",
                audience: "garanti",
                claims: claimsData,
                expires: DateTime.Now.AddMinutes(40),
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            GenericRepository<RefreshToken> refreshTokenRepository = new GenericRepository<RefreshToken>();
            GenericRepository<AdminUser> userRepository = new GenericRepository<AdminUser>();

            var user =  userRepository.GetByQuery(x => x.Email == email);

            refreshTokenRepository.Create(new RefreshToken
            {
                Token = refreshToken,
                Expiration = DateTime.Now.AddHours(120),
                Revoked = false,
                UserId = user.Id
            });

            return new TokenModel
            {
                Token = tokenString,
                RefreshToken = refreshToken
            };

        }


        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }




    public class TokenModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

    }


}
