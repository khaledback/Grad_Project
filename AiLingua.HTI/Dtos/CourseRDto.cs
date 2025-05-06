namespace AiLingua.HTI.Dtos
{
    public class CourseRDto
    {
        public string ?Title { get; set; }
        public string? Description { get; set; }
        public string? CourseType { get; set; } // Free, Group, Individual
        public string? CourseLevel { get; set; }
        public string CourseSpeciality { get; set; }
        public string Overview { get; set; }

        public string WhyTakeThisCourse { get; set; }
        public string WhatYouWillBeAbleToDo { get; set; }
        public string Prerequisites { get; set; }
        public string Syllabus { get; set; }
        public string? TeacherId { get; set; } // Only storing the Teacher's ID, not the full object
        public ICollection<string>? CourseVideos { get; set; }
        public ICollection<string>? Slides { get; set; }
    }
}
