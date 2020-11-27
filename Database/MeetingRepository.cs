using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VirturlMeetingAssitant.Backend.Controllers;


namespace VirturlMeetingAssitant.Backend.Db
{
    public interface IMeetingRepository : IRepository<Meeting>
    {
        Task AddFromDTOAsync(MeetingAddDTO dto);
    }
    public class MeetingRepository : Repository<Meeting>, IMeetingRepository
    {
        private readonly IRoomRepository _roomRepository;
        public MeetingRepository(MeetingContext dbContext, IRoomRepository roomRepository) : base(dbContext)
        {
            _roomRepository = roomRepository;
        }

        public async Task AddFromDTOAsync(MeetingAddDTO dto)
        {
            var room = _roomRepository.Find(x => x.Name == dto.RoomName).First();

            var entity = new Meeting();
            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Location = room;
            entity.FromDate = dto.FromDate;
            entity.ToDate = dto.ToDate;

            await this.Add(entity);
        }
    }
}
