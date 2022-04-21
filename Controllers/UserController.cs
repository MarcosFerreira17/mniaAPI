using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mniaAPI.Data;
using mniaAPI.HATEOAS;
using mniaAPI.Helpers;
using mniaAPI.Models;

namespace mniaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        private readonly IWebHostEnvironment hostEnvironment;
        private HATEOAS.HATEOAS HATEOAS;
        public UserController(ApplicationDbContext database, IWebHostEnvironment hostEnvironment)
        {
            this.database = database;
            this.hostEnvironment = hostEnvironment;
            HATEOAS = new HATEOAS.HATEOAS("localhost:5001/api/v1/User");
            HATEOAS.AddAction("GET_INFO", "GET");
            HATEOAS.AddAction("EDIT_USER", "PUT");
            HATEOAS.AddAction("DELETE_USER", "DELETE");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                var users = database.Users.ToList();

                List<UserContainer> usersHATEOAS = new List<UserContainer>();

                foreach (var user in users)
                {
                    UserContainer userHATEOAS = new UserContainer();
                    userHATEOAS.Users = user;
                    userHATEOAS.links = HATEOAS.GetActions(user.Id.ToString());
                    usersHATEOAS.Add(userHATEOAS);
                }

                //Esconde senha do user.
                foreach (var item in users)
                {
                    item.Password = "********";
                }

                if (users == null) return NoContent();

                return Ok(usersHATEOAS);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro ao tentar encontrar usuarios. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            try
            {
                var user = database.Users.First(u => u.Id == id);

                //esconde a senha do user.
                user.Password = "********";

                UserContainer usersHATEOAS = new UserContainer();

                usersHATEOAS.Users = user;
                usersHATEOAS.links = HATEOAS.GetActions(user.Id.ToString());

                if (user == null) return NoContent();

                return Ok(usersHATEOAS);
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
                //Valida se a categoria é válida
                if (model.CategoriesId <= 0)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult("Verifique a sua categoria e tente novamente.");
                }

                var modelRole = model.Role.ToLower();
                //Valida a role do user.
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
        public IActionResult Post([FromBody] UserRegisterDTO model)
        {
            try
            {
                var userData = database.Users.ToList();
                var CategoriesData = database.Categories.ToList();

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
                //Verifica se a categoria passsada existe no banco de dados.
                foreach (var item in CategoriesData)
                {
                    if (item.Id != model.CategoriesId)
                    {
                        Response.StatusCode = 401;
                        return new ObjectResult("Esta categoria não existe em nossa base de dados.");
                    }
                }

                var checkEmail = ValidateEmail.IsValidEmail(model.Email);

                if (checkEmail != true)
                {
                    Response.StatusCode = 401;
                    return new ObjectResult("Este e-mail não é válido, verifique e tente novamente.");
                }

                bool checkCPF = ValidateCPF.CPF(model.CPF);
                if (checkCPF != true)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult("Este CPF é inválido, verifique se digitou corretamente e faça uma nova tentativa.");
                }
                //Verifica se a categoria é valida
                if (model.CategoriesId <= 0)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult("Verifique a sua categoria e tente novamente.");
                }

                user.FullName = model.FullName;
                user.Username = model.Username;
                user.CPF = model.CPF;
                user.FourLetters = GenerateFourLetters.Generate(model.FullName);
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
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar usuário. Erro: {ex.Message}");
            }

            return this.StatusCode(StatusCodes.Status400BadRequest,
                    $"Erro ao tentar deletar usuário.");
        }

        public class UserContainer
        {
            public User Users { get; set; }
            public Link[] links { get; set; }
        }

    }
}
