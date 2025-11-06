using Microsoft.IdentityModel.Tokens;
using PrestacaoServico.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PrestacaoServico.Services
{
    public class TokenService
    {
        public string GerarToken(Usuario usuario)
        {
            var key = Encoding.ASCII.GetBytes("bWluaGFzZW5oYXN1cGVyc2VjcmV0YTEyM0A=");
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.Tipo.ToString())
                ]),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
