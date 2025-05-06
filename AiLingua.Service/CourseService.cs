using AiLingua.Core;
using AiLingua.Core.Entities;
using AiLingua.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLingua.Service
{
    public class CourseService :ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Course> AddCourseAsync(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            await _unitOfWork.Repository<Course>().AddAsync(course);
            await _unitOfWork.CompleteAsync();
            return course;
        }


        public async Task<Course> DeleteCourseAsync(int courseId)
        {
            var course = await _unitOfWork.Repository<Course>().GetByIdAsync(courseId);
            if (course == null)
                throw new KeyNotFoundException("Course not found.");

            _unitOfWork.Repository<Course>().DeleteAsync(course);
            await _unitOfWork.CompleteAsync();
            return course;
        }

     

        public async Task<Course?> GetCourseAsync(int courseId)
        {

            var course = await _unitOfWork.GetByIdIncludesAsync<Course>(
        courseId,
        course => course.Teacher//,  // Eager load Teacher
      //  course => course.CourseVideos //,  // Eager load Students
      //  course => course.Lessons,
        //course => course.Reviews
    );

            return course;
        }

        public async Task<IReadOnlyList<Course>> GetCoursesAsync()
        {
            return await _unitOfWork.Repository<Course>().GetAllAsync();
        }

        public async Task<Course> UpdateCourseAsync(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            _unitOfWork.Repository<Course>().Update(course);
            await _unitOfWork.CompleteAsync();
            return course;
        }

       

        
    }
}
