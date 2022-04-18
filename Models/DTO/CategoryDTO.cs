using System.ComponentModel.DataAnnotations;

namespace mniaAPI.Models
{
    public class CategoriesDTO
    {

        public int Id { get; set; }
        public string Technology { get; set; }
        public string Name { get; set; }

    }
}