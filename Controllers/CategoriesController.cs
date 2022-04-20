using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using mniaAPI.Data;
using mniaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using mniaAPI.HATEOAS;
using System.Collections.Generic;

namespace mniaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        private HATEOAS.HATEOAS HATEOAS;
        public CategoriesController(ApplicationDbContext database)
        {
            this.database = database;
            HATEOAS = new HATEOAS.HATEOAS("localhost:5001/api/v1/Categories");
            HATEOAS.AddAction("GET_INFO", "GET");
            HATEOAS.AddAction("EDIT_CATEGORIE", "PUT");
            HATEOAS.AddAction("DELETE_CATEGORIE", "DELETE");
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var categories = database.Categories.Include(u => u.Users).ToList();

                List<CategoriesContainer> categoriesHATEOAS = new List<CategoriesContainer>();

                var users = database.Users.ToList();

                foreach (var cat in categories)
                {
                    CategoriesContainer categorieHATEOAS = new CategoriesContainer();
                    categorieHATEOAS.categories = cat;
                    categorieHATEOAS.links = HATEOAS.GetActions(cat.Id.ToString());
                    categoriesHATEOAS.Add(categorieHATEOAS);
                }

                //Esconde a senha do usuário.
                foreach (var item in users)
                {
                    item.Password = "********";
                }

                if (categories == null) return NoContent();

                return Ok(categoriesHATEOAS);
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

                CategoriesContainer categoriesHATEOAS = new CategoriesContainer();

                categoriesHATEOAS.categories = categories;
                categoriesHATEOAS.links = HATEOAS.GetActions(categories.Id.ToString());

                if (categories == null) return NoContent();

                if (categories.Id > 0)
                {
                    return Ok(categoriesHATEOAS);
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

                CategoriesContainer categoriesHATEOAS = new CategoriesContainer();

                categoriesHATEOAS.categories = categories;
                categoriesHATEOAS.links = HATEOAS.GetActions(categories.Id.ToString());

                database.Add(categories);
                database.SaveChanges();

                return Ok(new { msg = "Categoria criada com sucesso.", categoriesHATEOAS });


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

                CategoriesContainer categoriesHATEOAS = new CategoriesContainer();

                categoriesHATEOAS.categories = categories;
                categoriesHATEOAS.links = HATEOAS.GetActions(categories.Id.ToString());

                database.Update(categories);
                database.SaveChanges();

                return Ok(new { msg = "Categoria editada com sucesso.", categoriesHATEOAS });

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

                CategoriesContainer categoriesHATEOAS = new CategoriesContainer();

                categoriesHATEOAS.categories = categories;
                categoriesHATEOAS.links = HATEOAS.GetActions(categories.Id.ToString());

                if (categories.Id > 0)
                {
                    database.Remove(categories);
                    database.SaveChanges();
                    return Ok(categoriesHATEOAS);
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

        public class CategoriesContainer
        {
            public Categories categories { get; set; }
            public Link[] links { get; set; }
        }

    }
}