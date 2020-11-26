using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace VirturlMeetingAssitant.Backend.Db
{
    public interface IUserRepository : IRepository<User> { }
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MeetingContext dbContext) : base(dbContext) { }
    }
}
