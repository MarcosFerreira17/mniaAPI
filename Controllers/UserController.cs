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

                bool checkCPF = ValidateCPF.CPF(model.CPF);

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

                var modelRole = model.Role.ToLower();

                if (modelRole != "Admin" || modelRole != "Starter")
                {
                    Response.StatusCode = 400;
                    return new ObjectResult("As roles permitidas são somente Starter e Admin.");
                }

                user.FullName = model.FullName;
                user.Username = model.Username;
                user.CPF = model.CPF;
                user.FourLetters = model.FourLetters;
                user.Email = model.Email;
                user.Password = EncriptPasswordUser;
                user.CategoriesId = model.CategoriesId;
                user.Role = modelRole;

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

                bool checkCPF = ValidateCPF.CPF(model.CPF);
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
