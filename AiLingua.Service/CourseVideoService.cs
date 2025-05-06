/*using AiLingua.Core.Entities;
using AiLingua.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLingua.Service
{
    public class CourseVideoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseVideoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CourseVideo> AddCourseVideoAsync(CourseVideo coursevideo)
        {
            if (coursevideo == null)
                throw new ArgumentNullException(nameof(coursevideo));

            await _unitOfWork.Repository<CourseVideo>().AddAsync(coursevideo);
            await _unitOfWork.CompleteAsync();
            return coursevideo;
        }


        public async Task<CourseVideo> DeleteCourseAsync(int coursevideoId)
        {
            var courseVideo = await _unitOfWork.Repository<CourseVideo>().GetByIdAsync(coursevideoId);
            if (courseVideo == null)
                throw new KeyNotFoundException("course video not found.");

            _unitOfWork.Repository<CourseVideo>().DeleteAsync(courseVideo);
            await _unitOfWork.CompleteAsync();
            return courseVideo;
        }



        public async Task<CourseVideo?> GetSpecificCourseVideoAsync(int coursevideoId)
        {
            return await _unitOfWork.Repository<CourseVideo>().GetByIdAsync(coursevideoId);

        }
        public async Task<CourseVideo?> GetCourseVideoAsync(int coursevideoId, int courseId)
        {
            var courseVideo = await _unitOfWork.Repository<CourseVideo>().GetAsync(r => r.Id == coursevideoId && r.CourseId == courseId);
            return courseVideo;
        }
        public async Task<CourseVideo?> GetCourseVideosforcourseAsync( int courseId)
        {
            var courseVideo = await _unitOfWork.Repository<CourseVideo>().GetAsync(r => r.CourseId == courseId);
            return courseVideo;
        }
        public async Task<IReadOnlyList<CourseVideo>> GetCourseVideosAsync()
        {
            return await _unitOfWork.Repository<CourseVideo>().GetAllAsync();
        }

        public async Task<CourseVideo> UpdateCourseVideoAsync(CourseVideo courseVideo)
        {
            if (courseVideo == null)
                throw new ArgumentNullException(nameof(courseVideo));

            _unitOfWork.Repository<CourseVideo>().Update(courseVideo);
            await _unitOfWork.CompleteAsync();  
            return courseVideo;
        }

    }
}
*/