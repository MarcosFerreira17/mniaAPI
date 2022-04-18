using System.ComponentModel.DataAnnotations;

namespace mniaAPI.Models
{
    public class CategoryDTO
    {

        public int Id { get; set; }
        public string Technology { get; set; }
        public string Name { get; set; }

    }
}