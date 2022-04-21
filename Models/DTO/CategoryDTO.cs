using System.ComponentModel.DataAnnotations;

namespace mniaAPI.Models
{
    public class CategoriesDTO
    {
        [Required(ErrorMessage = "O id é uma informação obrigatória, preencha e tente novamente.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "A Tencnologia é uma informação obrigatória, preencha e tente novamente.")]
        public string Technology { get; set; }
        public string Name { get; set; }

    }
}