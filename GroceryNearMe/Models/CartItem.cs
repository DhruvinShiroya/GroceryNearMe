using Microsoft.Build.Framework;

namespace GroceryNearMe.Models
{
    public class CartItem
    {
        // product ittem, quantity , price , customerID

        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public Product? Product { get; set; }
    }
}
