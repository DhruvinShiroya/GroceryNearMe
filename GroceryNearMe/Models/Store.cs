using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;

namespace GroceryNearMe.Models
{
    public class Store
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Display(Name = "Address")]
        public int AddressId { get; set; }

        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        public Company? CompanyName { get; set; }

        public Address? Address { get; set; }

        public List<Product>? Products { get; set; } 


    }
}