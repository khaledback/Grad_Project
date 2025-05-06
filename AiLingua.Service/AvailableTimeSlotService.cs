using AiLingua.Core.Entities;
using AiLingua.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiLingua.Core.Services.Contract;

namespace AiLingua.Service
{
    public class AvailableTimeSlotService:IAvailableTimeSlotService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AvailableTimeSlotService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<AvailableTimeSlot>> GetAllTimeSlotsAsync()
        {
            return await _unitOfWork.Repository<AvailableTimeSlot>().GetAllAsync();
        }
        public async Task<IReadOnlyList<AvailableTimeSlot>> GetTimeSlotsForTeacher(string teacherId)
        {
            return await _unitOfWork.Repository<AvailableTimeSlot>().GetListAsync(t=>t.TeacherId==teacherId);
        }

        public async Task<AvailableTimeSlot> GetTimeSlotByIdAsync(int id)
        {
            return await _unitOfWork.Repository<AvailableTimeSlot>().GetByIdAsync(id);
        }
        
        public async Task<AvailableTimeSlot> AddTimeSlotAsync(AvailableTimeSlot timeSlot)
        {
            await _unitOfWork.Repository<AvailableTimeSlot>().AddAsync(timeSlot);
            await _unitOfWork.CompleteAsync();
            return timeSlot;


        }

        public async Task<AvailableTimeSlot> UpdateTimeSlotAsync(AvailableTimeSlot timeSlot)
        {
            _unitOfWork.Repository<AvailableTimeSlot>().Update(timeSlot);
            await _unitOfWork.CompleteAsync();
            return timeSlot;
        }
        public async Task<IReadOnlyList<AvailableTimeSlot>> GetTimeSlotsByDayAsync(string teacherId,DateTime date)
        {
            return await _unitOfWork.Repository<AvailableTimeSlot>().GetListAsync(ts =>
                ts.Date == date && ts.TeacherId==teacherId);
        }
        public async Task<AvailableTimeSlot> DeleteTimeSlotAsync(int id)
        {
            var timeSlot = await _unitOfWork.Repository<AvailableTimeSlot>().GetByIdAsync(id);
            if (timeSlot == null)
                throw new KeyNotFoundException("timeSlot not found.");

            _unitOfWork.Repository<AvailableTimeSlot>().DeleteAsync(timeSlot);
                await _unitOfWork.CompleteAsync();

            
            return timeSlot;
        }
        public async Task<Boolean> CheckDate(AvailableTimeSlot timeSlot)
        {
            var existingSlots = await _unitOfWork.Repository<AvailableTimeSlot>().GetListAsync(ts =>
       ts.TeacherId == timeSlot.TeacherId &&
       ts.Date == timeSlot.Date &&
       ts.Id != timeSlot.Id && // Exclude the current time slot being updated
       ((timeSlot.StartTime >= ts.StartTime && timeSlot.StartTime < ts.EndTime) ||
        (timeSlot.EndTime > ts.StartTime && timeSlot.EndTime <= ts.EndTime) ||
        (timeSlot.StartTime <= ts.StartTime && timeSlot.EndTime >= ts.EndTime))
   );

            if (existingSlots.Any())
            {
                return true;
            }
            return false;
        }

    }
}
