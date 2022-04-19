using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using mniaAPI.Data;
using mniaAPI.Models;
using mniaAPI.Services;

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

        public static bool validateCPF(string cpf)
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            try
            {
                var users = database.Users.ToList();

                foreach (var item in users)
                {
                    item.Password = "********";
                }

                if (users == null) return NoContent();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro ao tentar encontrar usuarios. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get(int id)
        {
            try
            {
                var user = database.Users.First(u => u.Id == id);

                user.Password = "********";

                if (user == null) return NoContent();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro ao tentar encontrar usuario. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(int id, [FromBody] UserDTO model)
        {

            try
            {
                var user = database.Users.First(c => c.Id == id);
                var users = database.Users.ToList();
                string EncriptPasswordUser = EncriptPassword.Encripted(model.Password);

                //Valida se o email já existe no banco de dados.

                if (user.Email != model.Email)
                {
                    foreach (var item in users)
                    {
                        if (user.Email == model.Email)
                        {
                            Response.StatusCode = 401;
                            return new ObjectResult("Este e-mail já existe em nossa base de dados.");
                        }
                    }
                }

                bool checkCPF = validateCPF(model.CPF);
                if (checkCPF != true)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult("Este CPF é inválido, verifique se digitou corretamente e faça uma nova tentativa.");
                }

                int checkFourLetters = model.FourLetters.Length;
                if (checkFourLetters != 4)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult("Verifique a quantidade de digito de suas 4 letras e tente novamente.");
                }

                if (model.CategoriesId <= 0)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult("Verifique a sua categoria e tente novamente.");
                }
                user.FullName = model.FullName;
                user.Username = model.Username;
                user.CPF = model.CPF;
                user.FourLetters = model.FourLetters;
                user.Email = model.Email;
                user.Password = EncriptPasswordUser;
                user.CategoriesId = model.CategoriesId;
                user.Role = "Starter";

                database.Update(user);
                database.SaveChanges();

                return Ok(new { msg = "Usuario editado com sucesso." });

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar editar um usuario. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDTO model)
        {
            try
            {
                var userData = database.Users.ToList();
                User user = new User();

                string EncriptPasswordUser = EncriptPassword.Encripted(model.Password);

                //Valida se o email já existe no banco de dados.
                foreach (var item in userData)
                {
                    if (item.Email == model.Email)
                    {
                        Response.StatusCode = 401;
                        return new ObjectResult("Este e-mail já existe em nossa base de dados.");
                    }
                }

                bool checkCPF = validateCPF(model.CPF);
                if (checkCPF != true)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult("Este CPF é inválido, verifique se digitou corretamente e faça uma nova tentativa.");
                }

                int checkFourLetters = model.FourLetters.Length;
                if (checkFourLetters != 4)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult("Verifique a quantidade de digito de suas 4 letras e tente novamente.");
                }

                if (model.CategoriesId <= 0)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult("Verifique a sua categoria e tente novamente.");
                }

                user.FullName = model.FullName;
                user.Username = model.Username;
                user.CPF = model.CPF;
                user.FourLetters = model.FourLetters;
                user.Email = model.Email;
                user.Password = EncriptPasswordUser;
                user.CategoriesId = model.CategoriesId;

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

        // [HttpPost("Login")]
        // public IActionResult Login([FromBody] UserLoginDTO credentials)
        // {
        //     // Busca um usuário por e-mail
        //     // Verifica se a senha está correta
        //     // Gerar um token jwt e retornar este token para o usuário
        //     string EncriptPasswordUser = EncriptPassword(credentials.Password);

        //     try
        //     {
        //         User user = database.Users.First(u => u.Username.Equals(credentials.Username));

        //         if (user == null) { return NoContent(); }

        //         if (user.Password.Equals(EncriptPasswordUser))
        //         {
        //             // Definindo uma chave de segurança.
        //             string securityKey = "mnia_api_rest_projeto_starter";
        //             //convertendo a chave de segurança em um array de bytes para conseguir gerar uma chame simé
        //             var symmectricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        //             var credentialsForAccess = new SigningCredentials(symmectricKey, SecurityAlgorithms.HmacSha256Signature);

        //             var claims = new List<Claim>();
        //             claims.Add(new Claim("id", user.Id.ToString()));
        //             claims.Add(new Claim("username", user.Username.ToString()));
        //             claims.Add(new Claim(ClaimTypes.Role, user.Role));

        //             var JWT = new JwtSecurityToken(
        //                 issuer: "MNIAAPI", // Quem está fornecendo o jwt para o usuário.
        //                 expires: DateTime.Now.AddMinutes(15), // Quando o token expira.
        //                 audience: "usuario_comum", // Pra quem é destinado este token.
        //                 signingCredentials: credentialsForAccess, // Credenciais de acesso.
        //                 claims: claims
        //             );

        //             return Ok(new JwtSecurityTokenHandler().WriteToken(JWT));
        //         }
        //         else
        //         {
        //             Response.StatusCode = 401;
        //             return new ObjectResult("");
        //         }

        //     }
        //     catch (Exception ex)
        //     {
        //         return this.StatusCode(StatusCodes.Status500InternalServerError,
        //             $"Erro ao tentar logar usuário. Erro: {ex.Message}");
        //     }

        // }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = database.Users.First(u => u.Id == id);
                if (user == null) { return NoContent(); }

                if (user.Id > 0)
                {
                    database.Remove(user);
                    database.SaveChanges();
                    return this.StatusCode(StatusCodes.Status200OK,
                    $"Usuário deletado com sucesso.");
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar usuário. Erro: {ex.Message}");
            }

            return this.StatusCode(StatusCodes.Status400BadRequest,
                    $"Erro ao tentar deletar usuário.");
        }

    }
}
