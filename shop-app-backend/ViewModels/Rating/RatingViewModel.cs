using System.ComponentModel.DataAnnotations;

namespace ViewModels.Rating
{
    public class RatingViewModel
    {
        [Range(1,5)]
        public int Rating { get; set; }
        public int ProductId { get; set; }
    }
}
