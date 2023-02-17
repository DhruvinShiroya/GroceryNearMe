using System.ComponentModel.DataAnnotations;

namespace GroceryNearMe.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public String? UserName { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }

        [Range(0, 250)]
        public String? Comment { get; set; }

        public int UpVote
        {
            get; set;
        }

        public int DownVote
        {
            get; set;
        }

        public int ProductID { get; set; }



    }
}
