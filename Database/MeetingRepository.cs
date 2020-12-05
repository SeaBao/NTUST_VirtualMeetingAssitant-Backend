using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirturlMeetingAssitant.Backend.DTO;

namespace VirturlMeetingAssitant.Backend.Db
{
    public interface IMeetingRepository : IRepository<Meeting>
    {
        Task AddFromDTOAsync(MeetingAddDTO dto);
        Task UpdateFromDTOAsync(MeetingUpdateDTO dto);
    }
    public class MeetingRepository : Repository<Meeting>, IMeetingRepository
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IDepartmentRepository _departmentRepository;
        public MeetingRepository(MeetingContext dbContext, IRoomRepository roomRepository, IDepartmentRepository departmentRepository) : base(dbContext)
        {
            _roomRepository = roomRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<bool> ValidateMeetingAsync(Meeting meeting)
        {
            if (meeting.FromDate >= meeting.ToDate)
            {
                throw new Exception("The attribute 'FromDate' must smaller than the attribute 'ToDate'.");
            }

            var meetingsInSameRoomCount = await this.Find(x => x.Location == meeting.Location).
                Where(x => x.ToDate >= meeting.ToDate && x.FromDate <= meeting.FromDate)
                .CountAsync();

            if (meetingsInSameRoomCount != 0)
            {
                throw new Exception("There is already a meeting in the room at the same time.");
            }

            return true;
        }

        public async Task AddFromDTOAsync(MeetingAddDTO dto)
        {
            var departments = _departmentRepository.Find(x => dto.Departments.Any(n => n == x.Name));
            var room = _roomRepository.Find(x => x.Name == dto.Location).First();

            var entity = new Meeting();
            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Location = room;
            entity.Departments = departments.ToList();
            entity.FromDate = dto.FromDate;
            entity.ToDate = dto.ToDate;

            if (await ValidateMeetingAsync(entity))
            {
                await this.Add(entity);
            }
        }

        public async Task UpdateFromDTOAsync(MeetingUpdateDTO dto)
        {
            var meeting = await this.Get(dto.MeetingID);

            meeting.Title = dto.Title ?? meeting.Title;
            meeting.Description = dto.Description ?? meeting.Description;
            meeting.RepeatType = dto.RepeatType ?? meeting.RepeatType;
            meeting.FromDate = dto.FromDate ?? meeting.FromDate;
            meeting.ToDate = dto.ToDate ?? meeting.ToDate;

            if (await ValidateMeetingAsync(meeting))
            {
                await this.Update(meeting);
            }
        }
    }
}
