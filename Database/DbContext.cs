using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VirturlMeetingAssitant.Backend.Db
{
    public class MeetingContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Department> Departments { get; set; }

        public MeetingContext(DbContextOptions<MeetingContext> options) : base(options) { }
    }

    public enum MeetingRepeatType
    {
        Weekly,
        None,
    }

    public class Department
    {
        [Key]
        public string Name { get; set; }
        public List<Meeting> RelatedMeetings { get; set; }
        public List<User> Users { get; set; }
    }

    public class Meeting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeetingID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public Room Location { get; set; }
        public List<Department> Departments { get; set; }
        public List<User> Attendees { get; set; }
        public MeetingRepeatType RepeatType { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
    }

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public Department Department { get; set; }
        public List<Meeting> AttendMeetings { get; set; }
        [Required]
        public string Email { get; set; }
    }

    public class Room
    {
        [Key]
        public string Name { get; set; }
        [Required]
        public int Capacity { get; set; }
    }
}