using System.Net;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mniaAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace mniaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class StarterController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        public StarterController(ApplicationDbContext database)
        {
            this.database = database;
        }

        [HttpGet]
        [Route("asc")]
        public IActionResult GetByAsc()
        {
            try
            {
                var users = database.Users.ToList();

                if (users == null) return NoContent();

                foreach (var item in users)
                {
                    item.Password = "*********";
                }

                var orderasc = users.OrderBy(n => n.FullName);

                return Ok(orderasc);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro ao tentar encontrar usuario. Erro: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("desc")]
        public IActionResult GetByDesc()
        {
            try
            {
                var users = database.Users.ToList();

                if (users == null) return NoContent();

                foreach (var item in users)
                {
                    item.Password = "*********";
                }

                var orderasc = users.OrderByDescending(n => n.FullName);

                return Ok(orderasc);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro ao tentar encontrar usuario. Erro: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("name/{name}")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var user = database.Users.First(n => n.FullName == name);

                //Esconde senha do user.
                user.Password = "*********";

                if (user == null) return NoContent();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro ao tentar encontrar usuario. Erro: {ex.Message}");
            }
        }


    }
}
