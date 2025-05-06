/*using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLingua.Core.Entities
{
    public class CourseVideo 
    {
        [Key] // Marks the property as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Makes it an identity column

        [Column("CourseVideoId")]  

        public int Id { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } // FK to Course
        public string VideoTitle { get; set; }
        public string VideoPath { get; set; } // Path to the video file
    }
}
*/