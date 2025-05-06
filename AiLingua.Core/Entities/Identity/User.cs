using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLingua.Core.Entities.Identity
{
    public class User:IdentityUser
    {
      

        [Required]
        public string FullName { get; set; }
        
        public string Password { get; set; }
        public string ?AccountNumber { get; set; } 



        // Common fields for both Students & Teachers
        public string ?ProfilePicture { get; set; }
        public string ?About { get; set; }
        //student
        

        public ICollection<Review> ?ReviewsGiven { get; set; }
     //   public ICollection<Course> ?EnrolledCourses { get; set; }
        public ICollection<Lesson> ?BookedLessons { get; set; }
        //teacher
      //  public string ?Accent { get; set; } // Example: "USA Accent"
        public string? Speciality { get; set; }
      //  public int ?ExperienceYears { get; set; }
  //      public List<string> ?PreferredLevels{ get; set; }
       public List<string> ?Interests { get; set; }
        // public List<string>? IndustryFamiliarity { get; set; }
        public ICollection<Review> ?ReceivedReviews { get; set; } // Reviews from students
    //     public ICollection<LessonStudent> LessonStudents { get; set; } = new List<LessonStudent>();

          public ICollection<Lesson> ?ScheduledLessons { get; set; } // Scheduled Lessons
        public ICollection<AvailableTimeSlot> ?AvailableTimeSlots { get; set; }
        public ICollection<Course> ?CreatedCourses { get; set; }


    }
}
