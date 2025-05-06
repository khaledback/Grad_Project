using AiLingua.Core;
using AiLingua.Core.Entities;
using AiLingua.Core.Entities.Identity;
using AiLingua.Core.Services.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLingua.Service
{
    public class TeacherService :IteacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public TeacherService(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<IList<User>> GetTeachersAsync()
        {
            return await _userManager.GetUsersInRoleAsync("Teacher");

        }
        public async Task<User> GetTeacherAsync(string teacherId)
        {
            var t= await _unitOfWork.Repository<User>().GetByIdAsync(teacherId);
            if(t == null)
            {
                return null;
            }
            var userWithIncludes=(await _unitOfWork.Repository<User>()
     .GetByPropertyIncludesAsync(
         t => t.Id == teacherId,
         t => t.ReceivedReviews,
         t => t.ScheduledLessons,
         t => t.AvailableTimeSlots,
         t => t.CreatedCourses
     )).FirstOrDefault();

            if (userWithIncludes == null)
                return null;

            var isTeacher = await _userManager.IsInRoleAsync(userWithIncludes, "Teacher");

            return isTeacher ? userWithIncludes : null;
        }
        public async Task<IEnumerable<User>> FilterTeachersBySpeciality(
            string? Speciality)
        {
            var teacherUsers = await _userManager.GetUsersInRoleAsync("Teacher");

            var teachers = teacherUsers.Where(t =>
                   t.Speciality != null && t.Speciality.Equals(Speciality, StringComparison.OrdinalIgnoreCase));
            return teachers;
        }

        public async Task<IEnumerable<User>> FilterTeachers(
                DateTime? date,
                string? Speciality,
              string? interest)
        {
          
                 var teacherUsers = await _userManager.GetUsersInRoleAsync("Teacher");

            // Extract teacher IDs
            var teacherIds = teacherUsers.Select(t => t.Id).ToList();

            if (!teacherIds.Any())
                return new List<User>();

            // Now fetch teachers with eager-loaded properties
            IEnumerable<User> teachersEnumerable = await _unitOfWork.Repository<User>()
                .GetByPropertyIncludesAsync(
                    u => teacherIds.Contains(u.Id),  // Filter using IDs
                    u => u.AvailableTimeSlots
                );

            if (date.HasValue)
            {
                DateTime startOfDay = date.Value.Date;  // 00:00
                DateTime endOfDay = date.Value.Date.AddDays(1).AddTicks(-1);  // 23:59:59.999
                  
                teachersEnumerable = teachersEnumerable.Where(t =>
                    t.AvailableTimeSlots != null &&  // Ensure it's not null
                    t.AvailableTimeSlots.Any(slot => slot.Date >= startOfDay && slot.Date <= endOfDay));

            }
            if (!string.IsNullOrEmpty(Speciality))
            {
                teachersEnumerable = teachersEnumerable.Where(t =>
                    t.Speciality != null && t.Speciality.Equals(Speciality, StringComparison.OrdinalIgnoreCase));
            }

   
       

            // Filter by Interests
            if (!string.IsNullOrEmpty(interest))
            {
                teachersEnumerable = teachersEnumerable.Where(t =>
                    t.Interests != null && t.Interests.Contains(interest));
            }


            return teachersEnumerable;
        }
        public async Task<IEnumerable<User>> SearchTeachersByNameAsync(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return new List<User>();

            var teacherUsers = await _userManager.GetUsersInRoleAsync("Teacher");

            var matchingTeachers = teacherUsers
                .Where(t => t.FullName != null && t.FullName.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return matchingTeachers;
        }

    }
}
