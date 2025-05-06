using AiLingua.Core.Entities;
using AiLingua.Core.Services.Contract;
using AiLingua.HTI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AiLingua.HTI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        private readonly IAvailableTimeSlotService _availableTimeSlotService;


        public LessonController(ILessonService lessonService, IAvailableTimeSlotService availableTimeSlotService)
        {
            _lessonService = lessonService;
            _availableTimeSlotService = availableTimeSlotService;
        }

        // GET: api/Course
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Lesson>>> GetLessons()
        {
            var lessons = await _lessonService.GetLessonsAsync();
            return Ok(lessons);
        }
        [HttpGet("{lessonId}")]
        public async Task<ActionResult<Lesson>> GetLesson(int lessonId)
        {
            var lesson = await _lessonService.GetSpecificLessonAsync(lessonId);
            if (lesson == null)
            {
                return NotFound($"Lesson with ID {lessonId} not found ");
            }
            return Ok(lesson);
        }

        // GET: api/Lesson/student/{studentId}
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<IReadOnlyList<Lesson>>> GetStudentLessons(string studentId)
        {
            var lessons = await _lessonService.GetStudentLessonsAsync(studentId);
            if (lessons == null || lessons.Count == 0)
                return NotFound("No lessons found for this student.");

            return Ok(lessons);
        }

        // GET: api/Lesson/teacher/{teacherId}
        [HttpGet("teacher/{teacherId}")]
        public async Task<ActionResult<IReadOnlyList<Lesson>>> GetTeacherLessons(string teacherId)
        {
            var lessons = await _lessonService.GetTeacherLessonsAsync(teacherId);
            if (lessons == null || lessons.Count == 0)
                return NotFound("No lessons found for this teacher.");

            return Ok(lessons);
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<Lesson>> AddLesson(LessonDto lessonDto)
        {
            try
            {


                var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var timeSlot = await _availableTimeSlotService.GetTimeSlotByIdAsync(lessonDto.AvailableTimeSlotId);
                if (timeSlot == null)
                {
                    return NotFound("this time slot is not available");

                }
                int BookedSeats = await _lessonService.Bookedseats(lessonDto.AvailableTimeSlotId);
                if (timeSlot.CountOfStudents == BookedSeats)
                {
                    return BadRequest("this time slot is fully booked");
                }
                // Create the Course entity
                var lesson = new Lesson
                {
                    TeacherId = timeSlot.TeacherId,
                    StudentId = studentId,
                    AvailableTimeSlotId = lessonDto.AvailableTimeSlotId

                };
              
            //    timeSlot.IsBooked=true;
              //  var updatedTimeSlot = await _availableTimeSlotService.UpdateTimeSlotAsync(timeSlot);

                var createdLesson = await _lessonService.AddLessonAsync(lesson);
               /* var lessonStudent = new LessonStudent
                {
                    LessonId = createdLesson.Id,
                    StudentId = studentId
                };

                await _lessonService.AddLessonStudentAsync(lessonStudent);*/


                return CreatedAtAction(nameof(GetLesson), new { lessonId = createdLesson.Id }, createdLesson);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"Invalid input: {ex.ParamName}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Student")]

        public async Task<ActionResult<Lesson>> UpdateLesson(int id, LessonDto lessonDto)
        {

            try
            {
                var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var existlesson = await _lessonService.GetSpecificLessonAsync(id);
                if (existlesson == null)
                {
                    return NotFound("No Lesson exists with this id");

                }
                if (existlesson.StudentId != studentId)
                {
                    return Unauthorized("You are not authorized to update this lesson.");

                }
                var timeSlot = await _availableTimeSlotService.GetTimeSlotByIdAsync(lessonDto.AvailableTimeSlotId);

                if (timeSlot == null)
                {
                    return NotFound("this time slot is not available");

                }
                int BookedSeats = await _lessonService.Bookedseats(lessonDto.AvailableTimeSlotId);
                if (timeSlot.CountOfStudents == BookedSeats)
                {
                    return BadRequest("this time slot is fully booked");
                }
                var oldtime = existlesson.AvailableTimeSlotId;
        
                existlesson.AvailableTimeSlotId = lessonDto.AvailableTimeSlotId;
           /*     if (oldtime != lessonDto.AvailableTimeSlotId)
                {
                    var timeSlotbeforUpdate = await _availableTimeSlotService.GetTimeSlotByIdAsync(oldtime);
                    timeSlotbeforUpdate.IsBooked = false;
                    timeSlot.IsBooked = true;
                    var updatedTimeSlotb = await _availableTimeSlotService.UpdateTimeSlotAsync(timeSlotbeforUpdate);

                    var updatedTimeSlot = await _availableTimeSlotService.UpdateTimeSlotAsync(timeSlot);
                }
           */
                    var updatedLesson = await _lessonService.UpdateLessonAsync(existlesson);
                
                return Ok(updatedLesson);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"Invalid input: {ex.ParamName}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<Lesson>> DeleteLesson(int id)
        {
            try
            {
                var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var lesson = await _lessonService.GetSpecificLessonAsync(id);
                if (lesson == null)
                {
                    return NotFound("No Lesson exists with this id");

                }
                var StudentIdOfTable = await _lessonService.GetStudentIdByLessonIdAsync(id);

                if (StudentIdOfTable != studentId)
                {
                    return Unauthorized("You are not authorized to Delete this lesson.");

                }
                var deletedLesson = await _lessonService.DeleteLessonAsync(id);
         //      var timeSlot = await _availableTimeSlotService.GetTimeSlotByIdAsync(lesson.AvailableTimeSlotId);
           //     timeSlot.IsBooked = false;
             //   var updatedTimeSlot = await _availableTimeSlotService.UpdateTimeSlotAsync(timeSlot);

                return Ok(deletedLesson);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("lessonsByDate")]
        public async Task<ActionResult<IReadOnlyList<Lesson>>> GetLessonsByDate([FromQuery] DateTime date)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value; // Get the role from claims
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Get the user ID from claims

            if (userRole == "Teacher")
            {
                // If the user is a teacher, fetch their lessons for the given date
                var lessons = await _lessonService.GetTeacherLessonsByDateAsync(userId, date);
                if (lessons == null || lessons.Count == 0)
                    return NotFound("No lessons found for this teacher on the given date.");
                return Ok(lessons);
            }
            else if (userRole == "Student")
            {
                // If the user is a student, fetch their lessons for the given date
                var lessons = await _lessonService.GetStudentLessonsByDateAsync(userId, date);
                if (lessons == null || lessons.Count == 0)
                    return NotFound("No lessons found for this student on the given date.");
                return Ok(lessons);
            }
            else
            {
                return Unauthorized("You do not have the required role to access this endpoint.");
            }
        }


    }
}

