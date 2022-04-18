using System.Collections;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using mniaAPI.Data;
using mniaAPI.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace mniaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        public CategoryController(ApplicationDbContext database)
        {
            this.database = database;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var category = database.Categorys.ToList();
                if (category == null) return NoContent();

                return Ok(category);
            }
            catch (System.Exception ex)
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
                var categoria = database.Categorys.First(c => c.Id == id);

                if (categoria == null) return NoContent();

                if (categoria.Id > 0)
                {
                    return Ok(categoria);
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
        public IActionResult Post([FromBody] CategoryDTO CategoryTemp)
        {
            try
            {
                Category category = new Category();

                category.Name = CategoryTemp.Name;
                category.Technology = CategoryTemp.Technology;

                database.Add(category);
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
        public IActionResult Put(int id, [FromBody] CategoryDTO CategoryTemp)
        {
            try
            {
                var category = database.Categorys.First(c => c.Id == id);

                category.Name = CategoryTemp.Name;
                category.Technology = CategoryTemp.Technology;

                database.Update(category);
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
        public IActionResult Delete(int id)
        {
            try
            {
                var categoria = database.Categorys.First(c => c.Id == id);

                if (categoria == null) return NoContent();

                if (categoria.Id > 0)
                {
                    database.Remove(categoria);
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