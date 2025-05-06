using AiLingua.Core.Entities;
using AiLingua.Core.Entities.Identity;
using AiLingua.Core.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AiLingua.HTI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IteacherService _teacherService;

        public TeacherController(IteacherService teacherService)
        {
            _teacherService = teacherService;
        }
        [HttpGet("{teacherId}")]
        public async Task<ActionResult<User>> GetTeacher(string teacherId)
        {
            var lessons = await _teacherService.GetTeacherAsync(teacherId);
            if (lessons == null) {
                return NotFound("Teacher not found."); 

            }

            return Ok(lessons);
        }
        [HttpGet]

        public async Task<ActionResult<IList<User>>> GetTeachers()
        {
            var lessons = await _teacherService.GetTeachersAsync();


            return Ok(lessons);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> FilterTeachers(
     [FromQuery] DateTime? date,
     [FromQuery] string? Speciality,
     [FromQuery] string? interest)
        {
            var teachers = await _teacherService.FilterTeachers(date, Speciality, interest);
            return Ok(teachers);
        }
        [HttpGet("filter-speciality")]
        public async Task<IActionResult> FilterTeachersBySpeciality(
  [FromQuery] string? Speciality)
        {
            var teachers = await _teacherService.FilterTeachersBySpeciality(Speciality);
            return Ok(teachers);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchTeachers([FromQuery] string name)
        {
            var teachers = await _teacherService.SearchTeachersByNameAsync(name);

            if (!teachers.Any())
                return NotFound("No teachers found.");

            return Ok(teachers);
        }

    }
}
