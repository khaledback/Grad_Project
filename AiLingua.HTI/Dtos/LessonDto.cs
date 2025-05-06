using System.ComponentModel.DataAnnotations;

namespace AiLingua.HTI.Dtos
{
    public class LessonDto
    {
      

        //public string TeacherId { get; set; }
       // public string StudentId { get; set; }
        [Required]
        public int AvailableTimeSlotId { get; set; }

    }
}
