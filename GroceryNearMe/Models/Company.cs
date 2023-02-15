using System.ComponentModel.DataAnnotations;

namespace GroceryNearMe.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Store> Stores { get; set; }

    }
}
