using AiLingua.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLingua.Core.Services.Contract
{
    public interface ILessonService
    {
        Task<Lesson> AddLessonAsync(Lesson lesson);
        Task<Lesson> DeleteLessonAsync(int lessonId);
        Task<Lesson?> GetSpecificLessonAsync(int lessonId);
        Task<IReadOnlyList<Lesson>> GetLessonsAsync();
        Task<Lesson> UpdateLessonAsync(Lesson lesson);
        Task<IReadOnlyList<Lesson>> GetTeacherLessonsAsync(string teacherId);
        Task<IReadOnlyList<Lesson>> GetStudentLessonsAsync(string studentId);
        Task<IReadOnlyList<Lesson>> GetTeacherLessonsByDateAsync(string teacherId, DateTime date);
        Task<IReadOnlyList<Lesson>> GetStudentLessonsByDateAsync(string studentId, DateTime date);
     //   Task<LessonStudent> AddLessonStudentAsync(LessonStudent lessonStudent);
       // Task<LessonStudent> updateLessonStudentAsync(LessonStudent lessonStudent);
        Task<string?> GetStudentIdByLessonIdAsync(int lessonId);
        Task<int> Bookedseats(int timeslotId);
        Task<int> BookedlessonsForReview(string StudentId, string TeacherId);

    }
}
