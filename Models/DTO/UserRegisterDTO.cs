using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace mniaAPI.Models
{
    public class UserRegisterDTO
    {

        [Required(ErrorMessage = "O nome completo é uma informação obrigatória, preencha e tente novamente."), StringLength(50, MinimumLength = 4,
                          ErrorMessage = "Intervalo permitido de 3 a 50 caracteres.")]
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
        [Required(ErrorMessage = "O campo imagem é obrigatório.")]
        public IFormFile Files { get; set; }
        [Required(ErrorMessage = "Adicione um nome a sua imagem.")]
        public string FileName { get; set; }

    }
}