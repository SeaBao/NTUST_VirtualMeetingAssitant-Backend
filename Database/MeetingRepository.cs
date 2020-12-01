using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

        public async Task AddMeetingAsync(Meeting meeting)
        {
            if (meeting.FromDate >= meeting.ToDate)
            {
                throw new Exception("The attribute 'FromDate' must smaller than the attribute 'ToDate'.");
            }

            var meetingsInSameRoomCount = this.Find(x => x.Location == meeting.Location).
                Where(x => x.ToDate >= meeting.ToDate && x.FromDate <= meeting.FromDate)
                .Count();

            if (meetingsInSameRoomCount != 0)
            {
                throw new Exception("There is already a meeting in the room at the same time.");
            }

            await this.Add(meeting);
        }

        public async Task AddFromDTOAsync(MeetingAddDTO dto)
        {
            var departments = _departmentRepository.Find(x => dto.Departments.Any(n => n == x.Name));
            var room = _roomRepository.Find(x => x.Name == dto.RoomName).First();

            var entity = new Meeting();
            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Location = room;
            entity.Departments = departments.ToList();
            entity.FromDate = dto.FromDate;
            entity.ToDate = dto.ToDate;

            await this.AddMeetingAsync(entity);
        }

        public async Task UpdateFromDTOAsync(MeetingUpdateDTO dto)
        {
            var meeting = await this.Get(dto.ID);
        }
    }
}
