using AiLingua.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLingua.Core.Entities
{
    public class Lesson 
    {

        [Key] // Marks the property as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Makes it an identity column

        [Column("LessonId")]

        public int Id { get; set; }
        [ForeignKey(nameof(Teacher))] // Specifies TeacherId as FK for Teacher

        public string TeacherId { get; set; }
        public User Teacher { get; set; }
        [ForeignKey(nameof(Student))] 

        public string StudentId { get; set; }
        public User Student { get; set; }
        public int AvailableTimeSlotId { get; set; } // Foreign Key to AvailableTimeSlot
        public AvailableTimeSlot AvailableTimeSlot { get; set; } // Navigation Property
    //        public ICollection<LessonStudent> LessonStudents { get; set; } = new List<LessonStudent>();




    }
}
