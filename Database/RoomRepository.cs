using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace VirturlMeetingAssitant.Backend.Db
{
    public interface IRoomRepository : IRepository<Room> { }
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(MeetingContext dbContext) : base(dbContext) { }
    }
}
