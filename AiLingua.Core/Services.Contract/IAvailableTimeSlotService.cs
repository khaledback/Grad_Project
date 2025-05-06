using AiLingua.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLingua.Core.Services.Contract
{
    public interface IAvailableTimeSlotService
    {
        Task<IReadOnlyList<AvailableTimeSlot>> GetAllTimeSlotsAsync();
        Task<AvailableTimeSlot> GetTimeSlotByIdAsync(int id);
        Task<AvailableTimeSlot> AddTimeSlotAsync(AvailableTimeSlot timeSlot);
        Task<AvailableTimeSlot> UpdateTimeSlotAsync(AvailableTimeSlot timeSlot);
        Task<IReadOnlyList<AvailableTimeSlot>> GetTimeSlotsByDayAsync(string teacherId, DateTime date);
        Task<AvailableTimeSlot> DeleteTimeSlotAsync(int id);
        Task<IReadOnlyList<AvailableTimeSlot>> GetTimeSlotsForTeacher(string teacherId);
          Task<Boolean> CheckDate(AvailableTimeSlot timeSlot);

    }
}
