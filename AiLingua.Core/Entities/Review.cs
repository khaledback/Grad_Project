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
    public class Review 
    {
        [Key] // Marks the property as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Makes it an identity column

        [Column("ReviewId")]

        public int Id { get; set; }
        public string StudentId { get; set; } // Learner who gave the review
        [ForeignKey("StudentId")]

        public User Student { get; set; } // FK to User

        public string TeacherId { get; set; } // Teacher who received the review
        [ForeignKey("TeacherId")]
        public User Teacher { get; set; } // FK to User
       // public int CourseId { get; set; }
      //  public Course Course { get; set; } // FK to Course
        public string Comments { get; set; }
        public int Rating { get; set; } // 1-5 stars
    }
}
