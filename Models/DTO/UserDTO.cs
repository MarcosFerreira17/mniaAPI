namespace mniaAPI.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string CPF { get; set; }
        public string FourLetters { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CategoriesId { get; set; }
    }
}