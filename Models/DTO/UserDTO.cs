using System.ComponentModel.DataAnnotations;

namespace mniaAPI.Models
{
    public class UserDTO
    {
        [Required(ErrorMessage = "O nome completo é uma informação obrigatória, preencha e tente novamente."), StringLength(50, MinimumLength = 4,
                          ErrorMessage = "Intervalo permitido de 4 a 50 caracteres.")]
        [Display(Name = "Nome completo")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "O username é uma informação obrigatória, preencha e tente novamente.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "O CPF é uma informação obrigatória, preencha e tente novamente.")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "O campo email é obrigatório")]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "É necessário ser um email válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha é uma informação obrigatória, preencha e tente novamente.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "A categoria é uma informação obrigatória, preencha e tente novamente.")]
        public int CategoriesId { get; set; }

        public string Role { get; set; }

    }
}