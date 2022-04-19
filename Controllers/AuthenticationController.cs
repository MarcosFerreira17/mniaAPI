using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using mniaAPI.Data;
using mniaAPI.Models;
using mniaAPI.Services;

namespace mniaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        public AuthenticationController(ApplicationDbContext database)
        {
            this.database = database;
        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginDTO credentials)
        {
            // Busca um usuário por e-mail
            // Verifica se a senha está correta
            // Gerar um token jwt e retornar este token para o usuário
            string EncriptPasswordUser = EncriptPassword.Encripted(credentials.Password);

            try
            {
                User user = database.Users.First(u => u.Username.Equals(credentials.Username));

                if (user == null) { return NoContent(); }

                if (user.Password.Equals(EncriptPasswordUser))
                {
                    // Definindo uma chave de segurança.
                    string securityKey = "mnia_api_rest_projeto_starter";
                    //convertendo a chave de segurança em um array de bytes para conseguir gerar uma chame simé
                    var symmectricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
                    var credentialsForAccess = new SigningCredentials(symmectricKey, SecurityAlgorithms.HmacSha256Signature);

                    var claims = new List<Claim>();
                    claims.Add(new Claim("id", user.Id.ToString()));
                    claims.Add(new Claim("username", user.Username.ToString()));
                    claims.Add(new Claim(ClaimTypes.Role, user.Role));

                    var JWT = new JwtSecurityToken(
                        issuer: "MNIAAPI", // Quem está fornecendo o jwt para o usuário.
                        expires: DateTime.Now.AddMinutes(15), // Quando o token expira.
                        audience: "usuario_comum", // Pra quem é destinado este token.
                        signingCredentials: credentialsForAccess, // Credenciais de acesso.
                        claims: claims
                    );

                    return Ok(new JwtSecurityTokenHandler().WriteToken(JWT));
                }
                else
                {
                    Response.StatusCode = 401;
                    return new ObjectResult("");
                }

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar logar usuário. Erro: {ex.Message}");
            }

        }
    }
}