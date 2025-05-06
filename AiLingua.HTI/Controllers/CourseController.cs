using AiLingua.Core.Entities;
using AiLingua.Core.Services.Contract;
using AiLingua.HTI.Dtos;
using AiLingua.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AiLingua.HTI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: api/Course
        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<CourseRDto>>> GetCourses()
        {
            var courses = await _courseService.GetCoursesAsync();

            return Ok(courses);
        }

        // GET: api/Course/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _courseService.GetCourseAsync(id);
            if (course == null)
            {
                return NotFound($"Course with ID {id} not found.");
            }
            return Ok(course);
        }

        // POST: api/Course
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<Course>> AddCourse([FromForm] CourseDto courseDto)
        {
            try
            {
                var teacherId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Create the Course entity
                var course = new Course
                {
                    Title = courseDto.Title,
                    Description = courseDto.Description,
                    CourseType = courseDto.CourseType,
                    CourseLevel= courseDto.CourseLevel,
                    CourseSpeciality=courseDto.CourseSpeciality,
                    Overview = courseDto.Overview,
                    Syllabus= courseDto.Syllabus,
                    WhyTakeThisCourse = courseDto.WhyTakeThisCourse,
                    WhatYouWillBeAbleToDo=courseDto.WhatYouWillBeAbleToDo,
                    Prerequisites=courseDto.Prerequisites,
               //     Slides = courseDto.Slides,
                    CourseVideos = courseDto.CourseVideos,
                    TeacherId = teacherId
                };
                if (courseDto.Slides != null)
                {
                    course.Slides = new List<string>();

                    foreach (var file in courseDto.Slides)
                    {

                        var slideUrl = await UploadSlide(file);
                        course.Slides.Add(slideUrl);
                    }
                }

                var createdCourse = await _courseService.AddCourseAsync(course);

          
                return CreatedAtAction(nameof(GetCourse), new { id = createdCourse.Id }, createdCourse);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"Invalid input: {ex.ParamName}");
            }
        }

        // PUT: api/Course/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<Course>> UpdateCourse(int id, [FromForm] CourseEditDto courseDto)
        {
        
            try
            {
                var teacherId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var getCourseResult = await GetCourse(id);

                // Check if the course exists
                if (getCourseResult.Result is NotFoundObjectResult)
                {
                    return NotFound($"Course with ID {id} not found.");
                }

                // Extract the actual course object
                var existingCourse = (getCourseResult.Result as OkObjectResult)?.Value as Course;
              
                // Check if the logged-in teacher is the owner of the course
                if (existingCourse.TeacherId != teacherId)
                {
                    return Unauthorized("You are not authorized to update this course.");
                }
                if (!string.IsNullOrEmpty(courseDto.Title))
                    existingCourse.Title = courseDto.Title;

                if (!string.IsNullOrEmpty(courseDto.Description))
                    existingCourse.Description = courseDto.Description;

                if (!string.IsNullOrEmpty(courseDto.CourseType))
                    existingCourse.CourseType = courseDto.CourseType;

                if (!string.IsNullOrEmpty(courseDto.CourseLevel))
                    existingCourse.CourseLevel = courseDto.CourseLevel;

                if (!string.IsNullOrEmpty(courseDto.CourseSpeciality))
                    existingCourse.CourseSpeciality = courseDto.CourseSpeciality;

                if (!string.IsNullOrEmpty(courseDto.Overview))
                    existingCourse.Overview = courseDto.Overview;

                if (!string.IsNullOrEmpty(courseDto.Syllabus))
                    existingCourse.Syllabus = courseDto.Syllabus;

                if (!string.IsNullOrEmpty(courseDto.WhyTakeThisCourse))
                    existingCourse.WhyTakeThisCourse = courseDto.WhyTakeThisCourse;

                if (!string.IsNullOrEmpty(courseDto.Prerequisites))
                    existingCourse.Prerequisites = courseDto.Prerequisites;

                if (!string.IsNullOrEmpty(courseDto.WhatYouWillBeAbleToDo))
                    existingCourse.WhatYouWillBeAbleToDo = courseDto.WhatYouWillBeAbleToDo;
                //   existingCourse.Slides = courseDto.Slides;
                //  existingCourse.CourseVideos = courseDto.CourseVideos;
                if (courseDto.CourseVideos != null)
                {

                    if (existingCourse.CourseVideos == null)
                    {
                        existingCourse.CourseVideos= new List<string>(); 
                    }
                    foreach (var video in courseDto.CourseVideos)
                    {

                        existingCourse.CourseVideos.Add(video);
                    }
                }
                if (courseDto.Slides != null)
                {

                    if (existingCourse.Slides == null)
                    {
                        existingCourse.Slides = new List<string>();
                    }
                    foreach (var file in courseDto.Slides)
                    {
                        var slideUrl = await UploadSlide(file);
                        existingCourse.Slides.Add(slideUrl);
                    }
                }
                var updatedCourse = await _courseService.UpdateCourseAsync(existingCourse);
                return Ok(updatedCourse);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"Invalid input: {ex.ParamName}");
            }
        }

        // DELETE: api/Course/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher")] 
        public async Task<ActionResult<Course>> DeleteCourse(int id)
        {
            try
            {
                var teacherId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var getCourseResult = await GetCourse(id);

                // Check if the course exists
                if (getCourseResult.Result is NotFoundObjectResult)
                {
                    return NotFound($"Course with ID {id} not found.");
                }

                // Extract the actual course object
                var existingCourse = (getCourseResult.Result as OkObjectResult)?.Value as Course;

                // Check if the logged-in teacher is the owner of the course
                if (existingCourse.TeacherId != teacherId)
                {
                    return Unauthorized("You are not authorized to update this course.");
                }
                var deletedCourse = await _courseService.DeleteCourseAsync(id);
                return Ok(deletedCourse);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("upload-slide")]
        [Authorize(Roles = "Teacher")]
        public async Task<string> UploadSlide([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return "No file uploaded.";
            }

            var slidesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "slides");

            if (!Directory.Exists(slidesPath))
            {
                Directory.CreateDirectory(slidesPath);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(slidesPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileUrl = $"{Request.Scheme}://{Request.Host}/slides/{uniqueFileName}";

            return  fileUrl ;
        }
        [HttpPost("by-speciality")]
        public async Task<ActionResult<IReadOnlyList<CourseRDto>>> GetCoursesBySpeciality(
       [FromBody] CourseFilterRequest request) // Accepting CourseFilterRequest as input
        {
            if (request.Specialities == null || request.Specialities.Count == 0)
                return BadRequest("At least one speciality must be provided.");

            if (request.Levels == null || request.Levels.Count != request.Specialities.Count)
                return BadRequest("Each speciality must be matched with a corresponding level.");

            var courses = await _courseService.GetCoursesAsync();

            // List to store final result
            var result = new List<Course>();

            // Loop over the specialities and levels to match each speciality to its level
            for (int i = 0; i < request.Specialities.Count; i++)
            {
                var speciality = request.Specialities[i];
                var level = request.Levels[i];

                // 1️⃣ Courses matching both speciality AND level for the current speciality-level pair
                var primary = courses
                    .Where(c => c.CourseSpeciality == speciality && c.CourseLevel == level)
                    .OrderBy(c => request.Specialities.IndexOf(c.CourseSpeciality))
                    .ToList();

                // Add the results to the final list
                result.AddRange(primary);
            }

            // Loop over the specialities and levels to match remaining courses for each speciality
            for (int i = 0; i < request.Specialities.Count; i++)
            {
                var speciality = request.Specialities[i];
                var level = request.Levels[i];

                // 2️⃣ Other courses in the selected speciality (not matching level)
                var secondary = courses
                    .Where(c => c.CourseSpeciality == speciality && c.CourseLevel != level)
                    .OrderBy(c => request.Specialities.IndexOf(c.CourseSpeciality))
                    .ThenByDescending(c => GetLevelPriority(c.CourseLevel))
                    .ToList();

                // Add the results to the final list
                result.AddRange(secondary);
            }

            // 3️⃣ Courses in other specialities (not in the provided specialities list)
            var remaining = courses
                .Where(c => !request.Specialities.Contains(c.CourseSpeciality))
                .OrderBy(c => c.CourseSpeciality)
                .ThenByDescending(c => GetLevelPriority(c.CourseLevel))
                .ToList();

            result.AddRange(remaining);

            return Ok(result);
        }

        private int GetLevelPriority(string level)
        {
            return level switch
            {
                "Advanced" => 3,
                "Intermediate" => 2,
                "Beginner" => 1,
                _ => 0
            };
        }
        /*   [HttpPost("by-speciality")]
        public async Task<ActionResult<IReadOnlyList<CourseRDto>>> GetCoursesBySpeciality(
            [FromBody] List<string> specialities,
            [FromQuery] List<string> levels) // Changed to accept a list of levels
        {
            if (specialities == null || specialities.Count == 0)
                return BadRequest("At least one speciality must be provided.");

            if (levels == null || levels.Count != specialities.Count)
                return BadRequest("Each speciality must be matched with a corresponding level.");

            var courses = await _courseService.GetCoursesAsync();

            // List to store final result
            var result = new List<Course>();

            // Loop over the specialities and levels to match each speciality to its level
            for (int i = 0; i < specialities.Count; i++)
            {
                var speciality = specialities[i];
                var level = levels[i];

                // 1️⃣ Courses matching both speciality AND level for the current speciality-level pair
                var primary = courses
                    .Where(c => c.CourseSpeciality == speciality && c.CourseLevel == level)
                    .OrderBy(c => specialities.IndexOf(c.CourseSpeciality))
                    .ToList();

                // 2️⃣ Other courses in the selected speciality (not matching level)
                var secondary = courses
                    .Where(c => c.CourseSpeciality == speciality && c.CourseLevel != level)
                    .OrderBy(c => specialities.IndexOf(c.CourseSpeciality))
                    .ThenByDescending(c => GetLevelPriority(c.CourseLevel))
                    .ToList();

                // Add the results to the final list
                result.AddRange(primary);
                result.AddRange(secondary);
            }


            // 3️⃣ Courses in other specialities (not in the provided specialities list)
            var remaining = courses
                .Where(c => !specialities.Contains(c.CourseSpeciality))
                .OrderBy(c => c.CourseSpeciality)
                .ThenByDescending(c => GetLevelPriority(c.CourseLevel))
                .ToList();

            result.AddRange(remaining);

            return Ok(result);
        }*/





    }
}
