using AiLingua.Core.Entities;
using AiLingua.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiLingua.Core.Services.Contract;
using AiLingua.Core.Entities.Identity;

namespace AiLingua.Service
{
    public class LessonService : ILessonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LessonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Lesson> AddLessonAsync(Lesson lesson)
        {
            if (lesson == null)
                throw new ArgumentNullException(nameof(lesson));

            await _unitOfWork.Repository<Lesson>().AddAsync(lesson);
            await _unitOfWork.CompleteAsync();
            return lesson;
        }


        public async Task<Lesson> DeleteLessonAsync(int lessonId)
        {
            var lesson = await _unitOfWork.Repository<Lesson>().GetByIdAsync(lessonId);
            if (lesson == null)
                throw new KeyNotFoundException("lesson not found.");

            _unitOfWork.Repository<Lesson>().DeleteAsync(lesson);
            await _unitOfWork.CompleteAsync();
            return lesson ;
        }



        public async Task<Lesson?> GetSpecificLessonAsync(int lessonId)
        {
            return await _unitOfWork.Repository<Lesson>().GetByIdIncludesAsync(lessonId,l=>l.AvailableTimeSlot);

        }
        public async Task<IReadOnlyList<Lesson>> GetStudentLessonsAsync(string studentId)
        {
            //  var lesson = await _unitOfWork.Repository<Lesson>().GetListAsync(r =>r.StudentId == studentId);
                 var lesson = await _unitOfWork.Repository<Lesson>().GetByPropertyIncludesAsync(l => l.StudentId == studentId, l => l.AvailableTimeSlot);
            //var lesson = await _unitOfWork.Repository<Lesson>().GetByPropertyIncludesAsync(l => l.LessonStudents.Any(ls => ls.StudentId == studentId),
            //l => l.AvailableTimeSlot);
            var lessonDtos = lesson.Select(l => new Lesson
            {
                Id = l.Id,
                TeacherId = l.TeacherId,
                StudentId = l.StudentId,
                AvailableTimeSlot = new AvailableTimeSlot
                {
                    Id = l.AvailableTimeSlot.Id,
                    TeacherId = l.AvailableTimeSlot.TeacherId,
                    Date = l.AvailableTimeSlot.Date,
                    StartTime = l.AvailableTimeSlot.StartTime,
                    EndTime = l.AvailableTimeSlot.EndTime,
                    ZoomLink = l.AvailableTimeSlot.ZoomLink,
                    price = l.AvailableTimeSlot.price,
                    CountOfStudents = l.AvailableTimeSlot.CountOfStudents
                }
            }).ToList();
            return lessonDtos;
        }
       public async Task<IReadOnlyList<Lesson>> GetStudentLessonsByDateAsync(string studentId, DateTime date)
        {
            date = date.Date;
            var lesson = await _unitOfWork.Repository<Lesson>().GetByPropertyIncludesAsync(l => l.StudentId==studentId && l.AvailableTimeSlot.Date.Date == date, l => l.AvailableTimeSlot);
            var lessonDtos = lesson.Select(l => new Lesson
            {
                Id = l.Id,
                TeacherId = l.TeacherId,
                StudentId = l.StudentId,
                AvailableTimeSlot = new AvailableTimeSlot
                {
                    Id = l.AvailableTimeSlot.Id,
                    TeacherId = l.AvailableTimeSlot.TeacherId,
                    Date = l.AvailableTimeSlot.Date,
                    StartTime = l.AvailableTimeSlot.StartTime,
                    EndTime = l.AvailableTimeSlot.EndTime,
                    ZoomLink = l.AvailableTimeSlot.ZoomLink,
                    price = l.AvailableTimeSlot.price,
                    CountOfStudents = l.AvailableTimeSlot.CountOfStudents
                }
            }).ToList();
            return lessonDtos;
        }
        public async Task<string> GetStudentIdByLessonIdAsync(int lessonId)
        {
            var lessonStudent = await _unitOfWork.Repository<Lesson>()
                .GetAsync(ls => ls.Id == lessonId);

            return lessonStudent.StudentId; // Return StudentId or null if not found
        }
        public async Task<IReadOnlyList<Lesson>> GetTeacherLessonsAsync(string teacherId)
        {
            var lesson = await _unitOfWork.Repository<Lesson>().GetByPropertyIncludesAsync(l => l.TeacherId == teacherId, l => l.AvailableTimeSlot);
            var lessonDtos = lesson.Select(l => new Lesson
            {
                Id = l.Id,
                TeacherId = l.TeacherId,
                StudentId = l.StudentId,
                AvailableTimeSlot = new AvailableTimeSlot
                {
                    Id = l.AvailableTimeSlot.Id,
                    TeacherId = l.AvailableTimeSlot.TeacherId,
                    Date = l.AvailableTimeSlot.Date,
                    StartTime = l.AvailableTimeSlot.StartTime,
                    EndTime = l.AvailableTimeSlot.EndTime,
                    ZoomLink = l.AvailableTimeSlot.ZoomLink,
                    price = l.AvailableTimeSlot.price,
                    CountOfStudents = l.AvailableTimeSlot.CountOfStudents
                }
            }).ToList();
            return lessonDtos;
        }
        public async Task<IReadOnlyList<Lesson>> GetTeacherLessonsByDateAsync(string teacherId,DateTime date)
        {
            date = date.Date;
            var lesson = await _unitOfWork.Repository<Lesson>().GetByPropertyIncludesAsync(l => l.TeacherId == teacherId && l.AvailableTimeSlot.Date.Date == date, l=>l.AvailableTimeSlot);
            var lessonDtos = lesson.Select(l => new Lesson
            {
                Id = l.Id,
                TeacherId = l.TeacherId,
                StudentId = l.StudentId,
                AvailableTimeSlot = new AvailableTimeSlot
                {
                    Id = l.AvailableTimeSlot.Id,
                    TeacherId = l.AvailableTimeSlot.TeacherId,
                    Date = l.AvailableTimeSlot.Date,
                    StartTime = l.AvailableTimeSlot.StartTime,
                    EndTime = l.AvailableTimeSlot.EndTime,
                    ZoomLink = l.AvailableTimeSlot.ZoomLink,
                    price = l.AvailableTimeSlot.price,
                    CountOfStudents = l.AvailableTimeSlot.CountOfStudents
                }
            }).ToList();
            return lessonDtos;
        }
        public async Task<IReadOnlyList<Lesson>> GetLessonsAsync()
        {
            return await _unitOfWork.Repository<Lesson>().GetAllAsync();
        }

        public async Task<Lesson> UpdateLessonAsync(Lesson lesson)
        {
            if (lesson == null)
                throw new ArgumentNullException(nameof(lesson));

            _unitOfWork.Repository<Lesson>().Update(lesson);
            await _unitOfWork.CompleteAsync();
            return lesson ;
        }
        /*  public async Task<Lesson> BookLessonAsync(int lessonId)
          {
              var lesson = await _unitOfWork.Repository<Lesson>().GetByIdAsync(lessonId);

              var timeSlot = await _unitOfWork.Repository<AvailableTimeSlot>().GetByIdAsync(lesson.AvailableTimeSlotId);

              if (timeSlot == null)
                  throw new KeyNotFoundException("Time slot not found.");

              if (timeSlot.IsBooked)
                  throw new InvalidOperationException("This time slot is already booked.");

              // Mark lesson and time slot as booked
              lesson.StudentId = "1";
              timeSlot.IsBooked = true;
              timeSlot.StudentId=lesson.StudentId;
              _unitOfWork.Repository<Lesson>().Update(lesson);
              _unitOfWork.Repository<AvailableTimeSlot>().Update(timeSlot);
              await _unitOfWork.CompleteAsync();

              return lesson;
          }*/
 /*      public async Task<LessonStudent> AddLessonStudentAsync(LessonStudent lessonStudent)
        {
            if (lessonStudent == null)
                throw new ArgumentNullException(nameof(lessonStudent));

            _unitOfWork.Repository<LessonStudent>().AddAsync(lessonStudent);
            await _unitOfWork.CompleteAsync();
             return lessonStudent;
        }
        public async Task<LessonStudent> updateLessonStudentAsync(LessonStudent lessonStudent)
        {
            if (lessonStudent == null)
                throw new ArgumentNullException(nameof(lessonStudent));

            _unitOfWork.Repository<LessonStudent>().Update(lessonStudent);
            await _unitOfWork.CompleteAsync();
            return lessonStudent;
        }*/
        public async Task<int> Bookedseats(int timeslotId)
        {
            var count = await _unitOfWork.Repository<Lesson>().FindByAsync(t=>t.AvailableTimeSlotId==timeslotId) ;
            return count.Count();

        }
        public async Task<int> BookedlessonsForReview(string StudentId, string TeacherId)
        {
            var count = await _unitOfWork.Repository<Lesson>().FindByAsync(l => l.StudentId==StudentId && l.TeacherId==TeacherId);
            return count.Count();

        }
    }
}
