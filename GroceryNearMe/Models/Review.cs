using System.ComponentModel.DataAnnotations;

namespace GroceryNearMe.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public String? UserName { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }

        [MaxLength(500,ErrorMessage ="Exceed the Length of Comment")]
        public String? Comment { get; set; }


        [Display(Name = "Up vote")]
        [Range(0, 1)]
        public int UpVote
        {
            get; set;
        }
        [Range(0, 1)]
        [Display(Name ="Down vote")]
        public int DownVote
        {
            get; set;
        }

        [Display(Name ="Product")]
        public int ProductID { get; set; }

        public String? ProductName { get; set; }


    }
}
