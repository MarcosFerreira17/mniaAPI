using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using mniaAPI.Data;
using mniaAPI.Models;

namespace mniaAPI.Controllers
{
    [ApiController]
    [Route("API/v1/[controller]")]
    public class StarterController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        public StarterController(ApplicationDbContext database)
        {
            this.database = database;
        }


        public static string EncriptPassword(string Senha)
        {
            try
            {
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(Senha);
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

        [HttpPost("Register")]
        public IActionResult Register([FromBody] StarterDTO starterTemp)
        {

            if (ModelState.IsValid)
            {
                Starter Starter = new Starter();
                var StartersData = database.Starters.ToList();

                foreach (var item in StartersData)
                {
                    if (item.Email == starterTemp.Email)
                    {
                        Response.StatusCode = 401;
                        return new ObjectResult("Este e-mail já existe em nossa base de dados.");
                    }
                }
                string EncriptPasswordStarter = EncriptPassword(starterTemp.Password);

                Starter.Name = starterTemp.Name;
                Starter.Email = starterTemp.Email;
                Starter.Password = EncriptPasswordStarter;

                database.Add(Starter);
                database.SaveChanges();
                return Ok(new { msg = "Usuário cadastrado com sucesso." });

            }

            Response.StatusCode = 401;
            return new ObjectResult("");
        }


        // [HttpPost("Login")]
        // public IActionResult Login([FromBody] UserDTO credentials)
        // {
        //     // Buscar um usuário por e-mail
        //     // Verificar se a senha está correta
        //     // Gerar um token jwt e retornar este token para o usuário
        //     try
        //     {
        //         User user = database.Users.First(u => u.Email.Equals(credentials.Email));

        //         if (User != null)
        //         {
        //             if (user.Password.Equals(credentials.Password))
        //             {
        //                 // Definindo uma chave de segurança.
        //                 string securityKey = "mnia_api_rest_projeto_starter";
        //                 //convertendo a chave de segurança em um array de bytes para conseguir gerar uma chame simé
        //                 var symmectricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        //                 var credentialsForAccess = new SigningCredentials(symmectricKey, SecurityAlgorithms.HmacSha256Signature);

        //                 var claims = new List<Claim>();
        //                 claims.Add(new Claim("id", user.Id.ToString()));
        //                 claims.Add(new Claim("email", user.Email.ToString()));
        //                 claims.Add(new Claim(ClaimTypes.Role, "Admin"));

        //                 var JWT = new JwtSecurityToken(
        //                     issuer: "MNIAAPI", // Quem está fornecendo o jwt para o usuário.
        //                     expires: DateTime.Now.AddMinutes(5), // Quando o token expira.
        //                     audience: "usuario_comum", // Pra quem é destinado este token.
        //                     signingCredentials: credentialsForAccess, // Credenciais de acesso.
        //                     claims: claims
        //                 );

        //                 return Ok(new JwtSecurityTokenHandler().WriteToken(JWT));
        //             }
        //             else
        //             {
        //                 Response.StatusCode = 401;
        //                 return new ObjectResult("");
        //             }
        //         }
        //         else
        //         {
        //             Response.StatusCode = 401;
        //             return new ObjectResult("");
        //         }
        //     }
        //     catch (Exception)
        //     {
        //         Response.StatusCode = 401;
        //         return new ObjectResult("");
        //     }

    }
}
