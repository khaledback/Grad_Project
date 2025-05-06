using AiLingua.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLingua.Core.Services.Contract
{
    public interface IReviewService
    {
        Task<IReadOnlyList<Review>> GetReviewsAsync();
        Task<Review> AddReviewAsync(Review review);
        Task<Review> DeleteReviewAsync(int reviewId);

        Task<IEnumerable<Review>> GetTeacherReviewsAsync(string teacherId);
        Task<int> GetStudentTeacherReviewsCount(string teacherId, string studentId);
        Task<Review> GetSpecificReviewAsync(int reviewId);
        Task<double> GetTeacherRating(string teacherId);
        Task<int> PastReviewsCount(string StudentId, string TeacherId);
    }
}
