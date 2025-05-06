using AiLingua.Core.Entities;
using AiLingua.Core.Services.Contract;
using AiLingua.HTI.Dtos;
using AiLingua.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

namespace AiLingua.HTI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly ILessonService _lessonService;

        public ReviewController(IReviewService reviewService,ILessonService lessonService)
        {
            _reviewService = reviewService;
            _lessonService=lessonService;
        }

        // GET: api/Course
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Review>>> GetReviews()
        {
            var reviews = await _reviewService.GetReviewsAsync();
            return Ok(reviews);
        }
        [HttpGet("review/{reviewId}")]
        public async Task<ActionResult<Review>> GetSpecificReview(int reviewId)
        {
            var review = await _reviewService.GetSpecificReviewAsync(reviewId);
            if (review == null)
            {
                return NotFound($"No Reviews are associated with this id");
            }
            return Ok(review);
        }
        [HttpGet("teacher/{teacherId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetTeacherReviews(string teacherId)
        {
            IEnumerable<Review> review = await _reviewService.GetTeacherReviewsAsync(teacherId);
            if (review == null)
            {
                return NotFound($"No Reviews are associated with this teacher");
            }
            return Ok(review);
        }
       
        [HttpPost]
        [Authorize(Roles = "Student")]

        public async Task<IActionResult> AddReview([FromBody] ReviewDto reviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //  var existingReview = await _reviewService.GetReviewByUserAndCourseAsync(reviewDto.UserId, reviewDto.CourseId);

            /*   if (existingReview != null)
               {
                   return BadRequest(new { message = "You have already added a review for this course." });
               }*/
            int countoflessons =await  _lessonService.BookedlessonsForReview(studentId, reviewDto.TeacherId);
            int countofreviews = await _reviewService.PastReviewsCount(studentId, reviewDto.TeacherId);
            if (countoflessons <= countofreviews)
            {
                return BadRequest(new { message = "You are not allowed to give review ." });

            }
            var review = new Review
            {
                StudentId = studentId,
                TeacherId = reviewDto.TeacherId,
                Comments = reviewDto.Comments,
                Rating = reviewDto.Rating  
            };

            var createdCourse = await _reviewService.AddReviewAsync(review);


            return Ok(new { message = "Review added successfully!", review });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Student")]

        public async Task<ActionResult<Review>> DeleteReview(int id)
        {
            try
            {
                var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var getReviewResult = await GetSpecificReview(id);
                 
                // Check if the course exists
                if (getReviewResult.Result is NotFoundObjectResult)
                {
                    return NotFound($"Review with ID {id} not found.");
                }

                // Extract the actual course object
                var existingReview = (getReviewResult.Result as OkObjectResult)?.Value as Review;

                // Check if the logged-in teacher is the owner of the course
                if (existingReview.StudentId != studentId)
                {
                    return Unauthorized("You are not authorized to delete this Review.");
                }
                var deletedReview = await _reviewService.DeleteReviewAsync(id);
                return Ok(deletedReview);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("teacherRating/{teacherid}")]
        public async Task<ActionResult<double>> getTeacherRating(string teacherid)
        {
            try
            {
                var rating = await _reviewService.GetTeacherRating(teacherid);
                return Ok(rating);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
