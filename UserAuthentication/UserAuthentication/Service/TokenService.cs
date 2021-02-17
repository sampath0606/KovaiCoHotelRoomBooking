using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UserAuthentication.Service
{
    public class TokenService : ITokenService
    {
        public string GetJWTToken(string id)
        {
            try
            {
                var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.UniqueName,id),
               new Claim(JwtRegisteredClaimNames.Jti,new Guid().ToString())
           };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Kovai_co_Machine_test"));

                var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                        issuer: "HotelManagementSystem",
                        audience: "HotelManagementService",
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(15),
                        signingCredentials: credential
                    );

                var response = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                };

                return JsonConvert.SerializeObject(response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
