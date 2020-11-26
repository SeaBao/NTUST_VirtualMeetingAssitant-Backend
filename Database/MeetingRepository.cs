using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace VirturlMeetingAssitant.Backend.Db
{
    public interface IMeetingRepository : IRepository<Meeting> { }
    public class MeetingRepository : Repository<Meeting>, IMeetingRepository
    {
        public MeetingRepository(MeetingContext dbContext) : base(dbContext) { }
    }
}
