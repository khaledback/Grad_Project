using AiLingua.Core.Entities;
using AiLingua.Core.Services.Contract;
using AiLingua.HTI.Dtos;
using AiLingua.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace AiLingua.HTI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailableTimeSlotController : ControllerBase
    {
        private readonly IAvailableTimeSlotService _availableTimeSlotService;

        public AvailableTimeSlotController(IAvailableTimeSlotService availableTimeSlotService)
        {
            _availableTimeSlotService = availableTimeSlotService;
        }
        // GET: api/AvailableTimeSlot
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AvailableTimeSlot>>> GetAvailableTimeSlots()
        {
            var timeSlots = await _availableTimeSlotService.GetAllTimeSlotsAsync();

            var timeSlotDtos = timeSlots.Select(ts => new AvailableTimeSlotRDto
            {
                Id=ts.Id,
                TeacherId = ts.TeacherId,
                Date = ts.Date.ToString("yyyy-MM-dd"), // Convert back to string
                StartTime = ts.StartTime.ToString(@"hh\:mm"),
                EndTime = ts.EndTime.ToString(@"hh\:mm"),
          //      isBooked = ts.IsBooked,
                price = ts.price,
                CountOfStudents = ts.CountOfStudents
            }).ToList();
            return Ok(timeSlots);
        }

        // GET: api/AvailableTimeSlot/{id}
        [HttpGet("wDTO/{id}")]
        public async Task<ActionResult<AvailableTimeSlotDto>> GetAvailableTimeSlotWithoutDto(int id)
        {
            var timeSlot = await _availableTimeSlotService.GetTimeSlotByIdAsync(id);
          
            if (timeSlot == null)
            {
                return NotFound($"Time Slot with ID {id} not found.");
            }
            return Ok(timeSlot);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AvailableTimeSlotDto>> GetAvailableTimeSlot(int id)
        {
            var timeSlot = await _availableTimeSlotService.GetTimeSlotByIdAsync(id);
            var timeSlotDto = new AvailableTimeSlotRDto
            {
                Id = timeSlot.Id,
                TeacherId = timeSlot.TeacherId,
                Date = timeSlot.Date.ToString("yyyy-MM-dd"), // Convert back to string
                StartTime = timeSlot.StartTime.ToString(@"hh\:mm"),
                EndTime = timeSlot.EndTime.ToString(@"hh\:mm"),
        //        isBooked = timeSlot.IsBooked,
                ZoomLink = timeSlot.ZoomLink,
                price = timeSlot.price,
                CountOfStudents = timeSlot.CountOfStudents
            };
            if (timeSlot == null)
            {
                return NotFound($"Time Slot with ID {id} not found.");
            }
            return Ok(timeSlotDto);
        }
        // GET: api/AvailableTimeSlot
        [HttpPost("AvailableTimeSlotsByDay")]
        public async Task<ActionResult<IReadOnlyList<AvailableTimeSlotRDto>>> GetAvailableTimeSlotsByDay(string teacherId, string date)
        {
           DateTime Date = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var timeSlots = await _availableTimeSlotService.GetTimeSlotsByDayAsync(teacherId, Date); 
            if (timeSlots == null || timeSlots.Count == 0)
                return NotFound("No time slots found for this teacher.");
            var timeSlotDtos = timeSlots.Select(ts => new AvailableTimeSlotRDto
            {
                Id=ts.Id,
                TeacherId = ts.TeacherId,
                Date = ts.Date.ToString("yyyy-MM-dd"), // Convert back to string
                StartTime = ts.StartTime.ToString(@"hh\:mm"),
                EndTime = ts.EndTime.ToString(@"hh\:mm"),
            //    isBooked = ts.IsBooked,
                price = ts.price,
                CountOfStudents = ts.CountOfStudents
            }).ToList();

            return Ok(timeSlotDtos);
        }
        [HttpGet("AvailableTimeSlotsForTeacher/{teacherId}")]
        public async Task<ActionResult<IReadOnlyList<AvailableTimeSlot>>> GetTimeSlotsForTeacher(string teacherId)
        {
            var timeSlots = await _availableTimeSlotService.GetTimeSlotsForTeacher(teacherId);
            var timeSlotDtos = timeSlots.Select(ts => new AvailableTimeSlotRDto
            {
                Id = ts.Id,
                TeacherId = ts.TeacherId,
                Date = ts.Date.ToString("yyyy-MM-dd"), // Convert back to string
                StartTime = ts.StartTime.ToString(@"hh\:mm"),
                ZoomLink=ts.ZoomLink,
                EndTime = ts.EndTime.ToString(@"hh\:mm"),
              //  isBooked = ts.IsBooked,
                price=ts.price,
                CountOfStudents=ts.CountOfStudents
            }).ToList();
            if (timeSlots == null || timeSlots.Count == 0)
                return NotFound("No time slots found for this teacher.");

            return Ok(timeSlotDtos);
        }
        // POST: api/Course
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<AvailableTimeSlot>> AddAvailableTimeSlotForTeacher(AvailableTimeSlotDto availableTimeSlotDto)
        {
            try
            {
                //   var teacherId = User.Identity.Name; // Adapt this based on your authentication setup
                var teacherId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var availableTimeSlot = new AvailableTimeSlot
                {
                    Date = DateTime.ParseExact(availableTimeSlotDto.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture),

                    StartTime = TimeSpan.Parse(availableTimeSlotDto.StartTime),
                    EndTime = TimeSpan.Parse(availableTimeSlotDto.EndTime),
                    TeacherId = teacherId,
                    ZoomLink = availableTimeSlotDto.ZoomLink,
           //         IsBooked =false,
                    price=availableTimeSlotDto.price,
                    CountOfStudents=availableTimeSlotDto.CountOfStudents
                };
                bool checkDate = await _availableTimeSlotService.CheckDate(availableTimeSlot);
                if (checkDate)
                {
                    return BadRequest("The selected time slot conflicts with an existing time slot.");
                }
                var createdTimeSlot = await _availableTimeSlotService.AddTimeSlotAsync(availableTimeSlot);




                return CreatedAtAction(nameof(GetAvailableTimeSlot), new { id = createdTimeSlot.Id }, createdTimeSlot);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"Invalid input: {ex.ParamName}");
            }
        }

        // PUT: api/Course/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher")]

        public async Task<ActionResult<Course>> UpdateAvailableTimeSlotForTeacher(int id, AvailableTimeSlotDto availableTimeSlotDto)
        {

            try
            {

                var teacherId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var getTimeSlotResult = await GetAvailableTimeSlotWithoutDto(id);

                // Check if the time slot exists
                if (getTimeSlotResult.Result is NotFoundObjectResult)
                {
                    return NotFound($"Time slot with ID {id} not found.");
                }

                // Extract the actual available time slot object
                var existingTimeSlot = (getTimeSlotResult.Result as OkObjectResult)?.Value as AvailableTimeSlot;

                // Check if the logged-in teacher is the owner of the time slot
                if (existingTimeSlot.TeacherId != teacherId)
                {
                    return Unauthorized("You are not authorized to update this time slot.");
                }

                // Update fields
             //   if (!string.IsNullOrWhiteSpace(availableTimeSlotDto.Date))
                    existingTimeSlot.Date = DateTime.ParseExact(availableTimeSlotDto.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
               // if (!string.IsNullOrWhiteSpace(availableTimeSlotDto.StartTime))
                    existingTimeSlot.StartTime = TimeSpan.Parse(availableTimeSlotDto.StartTime);
               // if (!string.IsNullOrWhiteSpace(availableTimeSlotDto.EndTime))
                    existingTimeSlot.EndTime = TimeSpan.Parse(availableTimeSlotDto.EndTime);
                //if (!string.IsNullOrWhiteSpace(availableTimeSlotDto.ZoomLink))
                    existingTimeSlot.ZoomLink = availableTimeSlotDto.ZoomLink;
                    existingTimeSlot.price = availableTimeSlotDto.price;
                bool checkDate = await _availableTimeSlotService.CheckDate(existingTimeSlot);
                if (checkDate)
                {
                    return BadRequest("The selected time slot conflicts with an existing time slot.");
                }
                var updatedTimeSlot = await _availableTimeSlotService.UpdateTimeSlotAsync(existingTimeSlot);


                return Ok(updatedTimeSlot);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest($"Invalid input: {ex.ParamName}");
            }
        }

        // DELETE: api/Course/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher")]

        public async Task<ActionResult<AvailableTimeSlot>> DeleteAvailableTimeSlot(int id)
        {

            try
            {
                var teacherId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var getTimeSlotResult = await GetAvailableTimeSlotWithoutDto(id);

                // Check if the time slot exists
                if (getTimeSlotResult.Result is NotFoundObjectResult)
                {
                    return NotFound($"Time slot with ID {id} not found.");
                }

                // Extract the actual available time slot object
                var existingTimeSlot = (getTimeSlotResult.Result as OkObjectResult)?.Value as AvailableTimeSlot;

                // Check if the logged-in teacher is the owner of the time slot
                if (existingTimeSlot.TeacherId != teacherId)
                {
                    return Unauthorized("You are not authorized to update this time slot.");
                }
                var deletedavailableTimeSlot = await _availableTimeSlotService.DeleteTimeSlotAsync(id);
                return Ok(deletedavailableTimeSlot);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
