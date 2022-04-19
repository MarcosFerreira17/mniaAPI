using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using mniaAPI.Data;
using mniaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace mniaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        public CategoriesController(ApplicationDbContext database)
        {
            this.database = database;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var categories = database.Categories.Include(u => u.Users).ToList();
                var users = database.Users.ToList();

                foreach (var item in users)
                {
                    item.Password = "********";
                }

                if (categories == null) return NoContent();

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro ao tentar encontrar categorias. Erro: {ex.Message}");
            }

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var categories = database.Categories.Include(u => u.Users).First(c => c.Id == id);

                if (categories == null) return NoContent();

                if (categories.Id > 0)
                {
                    return Ok(categories);
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar categoria. Erro: {ex.Message}");
            }

            Response.StatusCode = 401;
            return new ObjectResult("");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] CategoriesDTO model)
        {
            try
            {
                Categories categories = new Categories();

                categories.Name = model.Name;
                categories.Technology = model.Technology;

                database.Add(categories);
                database.SaveChanges();

                return Ok(new { msg = "Categoria criada com sucesso." });


            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar criar uma categoria. Erro: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(int id, [FromBody] CategoriesDTO model)
        {
            try
            {
                var categories = database.Categories.First(c => c.Id == id);

                categories.Name = model.Name;
                categories.Technology = model.Technology;

                database.Update(categories);
                database.SaveChanges();

                return Ok(new { msg = "Categoria editada com sucesso." });

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar editar uma categoria. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                var categories = database.Categories.Include(u => u.Users).First(c => c.Id == id);

                if (categories == null) return NoContent();

                if (categories.Id > 0)
                {
                    database.Remove(categories);
                    database.SaveChanges();
                    return this.StatusCode(StatusCodes.Status200OK,
                    $"Categoria deletada com sucesso.");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar categoria. Erro: {ex.Message}");
            }

            return this.StatusCode(StatusCodes.Status400BadRequest,
                    $"Erro ao tentar deletar categoria.");
        }

    }
}