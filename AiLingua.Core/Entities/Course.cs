using AiLingua.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AiLingua.Core.Entities
{
    public class Course 
    {
        [Key] // Marks the property as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Makes it an identity column

        [Column("CourseId")]  // If the column in the database is named "CourseId"

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CourseType { get; set; } // Free, Group, Individual
        public string CourseSpeciality { get; set; }
        public string CourseLevel { get; set; }
        public string Overview { get; set; }

        public string WhyTakeThisCourse { get; set; }
        public string WhatYouWillBeAbleToDo { get; set; }
        public string Prerequisites { get; set; }
        public string Syllabus { get; set; }


        //  public decimal Price { get; set; }
        public string TeacherId { get; set; }
        [ForeignKey("TeacherId")]

        public User Teacher { get; set; } // FK to User (Teacher)
       // public ICollection<Lesson>? Lessons { get; set; } // Live lessons
     //  public string CourseDomain { get; set; }
      //  public ICollection<Review> ?Reviews { get; set; }
        // We’ll use a collection of strings to represent file paths or URLs to the course videos
        public ICollection<string>? CourseVideos { get; set; }

        // Adding a collection for slides, this could also be file paths or URLs
        public ICollection<string>? Slides { get; set; }
    }
}
