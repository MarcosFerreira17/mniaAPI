using System.Collections.Generic;

namespace mniaAPI.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Technology { get; set; }
        public string Name { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}