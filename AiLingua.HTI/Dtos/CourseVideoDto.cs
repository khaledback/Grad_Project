using System.ComponentModel.DataAnnotations;

namespace AiLingua.HTI.Dtos
{
    public class CourseVideoDto
    {
        [Required] 
        public string VideoTitle { get; set; } 

        [Required] 
        [Url] 
        public string VideoPath { get; set; } 

        [Required] 
        public int CourseId { get; set; } 
    }
}
