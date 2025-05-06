using AiLingua.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLingua.Core.Entities
{
    public class AvailableTimeSlot
    {
        public int Id { get; set; } // Unique ID for the time slot
        public string TeacherId { get; set; } // Foreign key to Teacher
        [ForeignKey("TeacherId")]

        public User Teacher { get; set; } // Navigation property
        public DateTime Date { get; set; } // Stores only the date (e.g., 2024-02-09)
        public TimeSpan StartTime { get; set; } // Stores only the start time (e.g., 10:00 AM)
        public TimeSpan EndTime { get; set; } // Stores only the end time (e.g., 11:00 AM)
    //    public bool IsBooked { get; set; }  // True if the slot is booked
        public string ZoomLink { get; set; }

        public decimal price { get; set; }
        public int CountOfStudents {  get; set; }
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>(); // Many Lessons share this slot


    }
}
