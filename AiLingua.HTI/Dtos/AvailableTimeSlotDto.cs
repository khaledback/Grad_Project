using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiLingua.HTI.Dtos
{
    public class AvailableTimeSlotDto
    {
        public string Date { get; set; } // Stores only the date (e.g., 2024-02-09)
        public string StartTime { get; set; } // Stores only the start time (e.g., 10:00 AM)
        public string EndTime { get; set; } // Stores only the end time (e.g., 11:00 AM)
        

        [Required]
        [Url]
        public string ZoomLink { get; set; }
        public decimal price { get; set; }
        public int CountOfStudents { get; set; }

    }
}
