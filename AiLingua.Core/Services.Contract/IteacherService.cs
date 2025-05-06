using AiLingua.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLingua.Core.Services.Contract
{
    public interface IteacherService
    {
        Task<IList<User>> GetTeachersAsync();
     Task<User> GetTeacherAsync(string teacherId);

        Task<IEnumerable<User>> SearchTeachersByNameAsync(string searchText);
      Task<IEnumerable<User>> FilterTeachers(
               DateTime? date,
               string? Speciality,
             string? interest);
        Task<IEnumerable<User>> FilterTeachersBySpeciality(string Speciality);
    }
}
