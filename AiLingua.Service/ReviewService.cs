using AiLingua.Core.Entities;
using AiLingua.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiLingua.Core.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace AiLingua.Service
{
    public class ReviewService :IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Review> AddReviewAsync(Review review)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            await _unitOfWork.Repository<Review>().AddAsync(review);
            await _unitOfWork.CompleteAsync();
            return review;
        }


        public async Task<Review> DeleteReviewAsync(int reviewId)
        {
            var review = await _unitOfWork.Repository<Review>().GetByIdAsync(reviewId);
            if (review == null)
                throw new KeyNotFoundException("Course not found.");

            _unitOfWork.Repository<Review>().DeleteAsync(review);
            await _unitOfWork.CompleteAsync();
            return review;
        }
  
        public async Task<Review> GetSpecificReviewAsync(int reviewId)
        {
            var review = await _unitOfWork.Repository<Review>().GetByIdAsync(reviewId);
            return review;
        }
        public async Task<IReadOnlyList<Review>> GetReviewsAsync()
        {
            return await _unitOfWork.Repository<Review>().GetAllAsync();
        }
        public async Task<IEnumerable<Review>> GetTeacherReviewsAsync(string teacherId)
        {
            
            IEnumerable<Review> review = await _unitOfWork.Repository<Review>().GetByPropertyIncludesAsync(r=>r.TeacherId==teacherId,r=>r.Student);
            return review;
        }
        // to checking in adding review number of reviews user give to teacher and number of session he has take
        public async Task<int> GetStudentTeacherReviewsCount(string teacherId,string studentId)
        {
            IEnumerable<Review> review = await _unitOfWork.Repository<Review>().FindByAsync(r => r.TeacherId == teacherId&&r.StudentId==studentId);
            int reviewCount = review.Count(); 

            return reviewCount;
        }
        public async Task<double> GetTeacherRating(string teacherId)
        {
            var reviews = await _unitOfWork.Repository<Review>().FindByAsync(r => r.TeacherId == teacherId);

            if (!reviews.Any())
                throw new KeyNotFoundException("No reviews found for this course.");

            double totalReviews = reviews.Count();
            double Rating = reviews.Sum(r => r.Rating); // Assuming 'Rating' is a property in Review entity.
            double review = Math.Round(Rating / totalReviews,1);

            return review;
        }
        public async Task<int> PastReviewsCount(string StudentId, string TeacherId)
        {
            var count = await _unitOfWork.Repository<Review>().FindByAsync(l => l.StudentId == StudentId && l.TeacherId == TeacherId);
            return count.Count();

        }

    }

}
