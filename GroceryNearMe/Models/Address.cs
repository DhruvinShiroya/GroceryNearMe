using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace GroceryNearMe.Models
{
    public class Address
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name ="Street Name")]
        public string? StreetName { get; set; }

        [Required]
        [MaxLength(100)]
        public string? City { get; set; } = null;


        [Required]
        [MaxLength(100)]
        public string? state { get; set; } = null;

        [Required]
        [MaxLength(100)]
        [Display(Name ="Postal Code")]
        public string? PostalCode { get; set;} = null;

    }
}
