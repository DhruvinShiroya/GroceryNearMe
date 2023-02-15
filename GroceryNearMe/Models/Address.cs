using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace GroceryNearMe.Models
{
    public class Address
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? StreetName { get; set; }

        [Required]
        [MaxLength(100)]
        public string? City { get; set; } = null;


        [Required]
        [MaxLength(100)]
        public string? state { get; set; } = null;

        [Required]
        [MaxLength(100)]
        public string? PostalCode { get; set;} = null;

    }
}
