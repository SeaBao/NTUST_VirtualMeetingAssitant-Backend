namespace VirturlMeetingAssitant.Backend.Db
{
    public interface IRoomRepository : IRepository<Room> { }
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(MeetingContext dbContext) : base(dbContext) { }
    }
}
