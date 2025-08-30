using Burguer404.Application.Arguments.Cliente;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Burguer404.Application.Controllers
{
    public class AutenticacaoController
    {
        private readonly IConfiguration _config;
        public AutenticacaoController(IConfiguration config)
        {
            _config = config;
        }

        public string GerarJwt(ClienteResponse cliente)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim("cpf", cliente.Cpf ?? ""),
                    new Claim("nome", cliente.Nome ?? ""),
                    new Claim("perfilId", cliente.PerfilClienteId.ToString())
                ]),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
