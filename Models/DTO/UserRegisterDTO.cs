using System.ComponentModel.DataAnnotations;

namespace mniaAPI.Models
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "O id é uma informação obrigatória, preencha e tente novamente.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome completo é uma informação obrigatória, preencha e tente novamente.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "O username é uma informação obrigatória, preencha e tente novamente.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "O CPF é uma informação obrigatória, preencha e tente novamente.")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "O Email é uma informação obrigatória, preencha e tente novamente.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha é uma informação obrigatória, preencha e tente novamente.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "A categoria é uma informação obrigatória, preencha e tente novamente.")]
        public int CategoriesId { get; set; }

    }
}