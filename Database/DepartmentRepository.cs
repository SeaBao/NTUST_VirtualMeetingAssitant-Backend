using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace VirturlMeetingAssitant.Backend.Db
{
    public interface IDepartmentRepository : IRepository<Department> { }
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(MeetingContext dbContext) : base(dbContext) { }
    }
}
