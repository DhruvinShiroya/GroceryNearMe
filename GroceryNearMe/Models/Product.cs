using MessagePack;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace GroceryNearMe.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }

        [Range(0.01,100000,ErrorMessage ="Invalid Price")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }

        public float? QuntityInKG { get; set; } = null;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int StoreId { get; set; }

        public Store? Store { get; set; }
    
        public List<Review>? Review { get; set; }

        public String? image { get; set; }
         

    }
}
