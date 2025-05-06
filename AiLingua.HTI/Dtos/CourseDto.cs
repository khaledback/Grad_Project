using Microsoft.AspNetCore.Mvc;

namespace AiLingua.HTI.Dtos
{
    public class CourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CourseType { get; set; } // Free, Group, Individual
        public string CourseLevel { get; set; }
        public string CourseSpeciality { get; set; }
        public string Overview { get; set; }

        public string WhyTakeThisCourse { get; set; }
        public string WhatYouWillBeAbleToDo { get; set; }
        public string Prerequisites { get; set; }
        public string Syllabus {get; set;}
        public ICollection<string>? CourseVideos { get; set; }
        [FromForm]

        public ICollection<IFormFile>? Slides { get; set; }

    }
}
