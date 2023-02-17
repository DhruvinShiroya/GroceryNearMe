using System.ComponentModel.DataAnnotations;

namespace GroceryNearMe.Models
{
    public class Category
    {

        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string? Name { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }


        public string? Image { get; set; }
        public List<Product>? Products { get; set; } 
    }
}
