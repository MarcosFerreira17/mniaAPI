using System.Collections.Generic;

namespace mniaAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Technology { get; set; }
        public string Name { get; set; }
        public IEnumerable<Starter> Starters { get; set; }
    }
}