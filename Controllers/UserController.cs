using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using mniaAPI.Data;
using mniaAPI.Models;

namespace mniaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        public UserController(ApplicationDbContext database)
        {
            this.database = database;
        }

        public static string EncriptPassword(string Password)
        {
            try
            {
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(Password);
                byte[] hash = md5.ComputeHash(inputBytes);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString(); // Retorna senha criptografada 
            }
            catch (Exception)
            {
                return null; // Caso encontre erro retorna nulo
            }
        }

        public static bool validaCPF(string cpf)
        {

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserRegisterDTO model)
        {
            try
            {
                var userData = database.Users.ToList();
                User user = new User();

                string EncriptPasswordUser = EncriptPassword(model.Password);

                //Valida se o email já existe no banco de dados.
                foreach (var item in userData)
                {
                    if (item.Email == model.Email)
                    {
                        Response.StatusCode = 401;
                        return new ObjectResult("Este e-mail já existe em nossa base de dados.");
                    }
                }

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.Password = EncriptPasswordUser;

                database.Add(user);
                database.SaveChanges();
                return Ok(new { msg = "Usuário cadastrado com sucesso." });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro ao tentar registrar um novo usuário. Erro: {ex.Message}");
            }
        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginDTO credentials)
        {
            // Busca um usuário por e-mail
            // Verifica se a senha está correta
            // Gerar um token jwt e retornar este token para o usuário
            string EncriptPasswordUser = EncriptPassword(credentials.Password);

            try
            {
                User user = database.Users.First(u => u.Email.Equals(credentials.Email));

                if (User != null)
                {

                    if (user.Password.Equals(EncriptPasswordUser))
                    {
                        // Definindo uma chave de segurança.
                        string securityKey = "mnia_api_rest_projeto_starter";
                        //convertendo a chave de segurança em um array de bytes para conseguir gerar uma chame simé
                        var symmectricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
                        var credentialsForAccess = new SigningCredentials(symmectricKey, SecurityAlgorithms.HmacSha256Signature);

                        var claims = new List<Claim>();
                        claims.Add(new Claim("id", user.Id.ToString()));
                        claims.Add(new Claim("email", user.Email.ToString()));
                        claims.Add(new Claim(ClaimTypes.Role, "Admin"));

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
                else
                {
                    Response.StatusCode = 401;
                    return new ObjectResult("");
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 401;
                return new ObjectResult("");
            }

        }
    }
}
