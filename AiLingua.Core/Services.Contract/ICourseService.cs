using AiLingua.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLingua.Core.Services.Contract
{
    public interface ICourseService
    {
        Task<Course?>  GetCourseAsync(int courseId);

        Task<IReadOnlyList<Course>> GetCoursesAsync();
        Task<Course>  AddCourseAsync(Course course);
          Task<Course> UpdateCourseAsync(Course course);
        Task<Course> DeleteCourseAsync(int courseid);


    }
}
