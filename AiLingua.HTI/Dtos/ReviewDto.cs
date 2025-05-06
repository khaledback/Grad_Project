using System.ComponentModel.DataAnnotations;

namespace AiLingua.HTI.Dtos
{
    public class ReviewDto
    {
 

        [Required]
        public string TeacherId { get; set; } // Teacher who received the review

        public string Comments { get; set; } // Optional feedback

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; } // Star rating (1-5)
    
    }
}
