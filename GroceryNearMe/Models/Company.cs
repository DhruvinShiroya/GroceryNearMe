using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GroceryNearMe.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string? Name { get; set; }
        public string? Description { get; set; }

        
        public string? Image { get; set; }

        public List<Store>? Stores { get; set; }

    }
}
