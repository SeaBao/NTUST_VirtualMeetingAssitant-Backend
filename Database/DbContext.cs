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
        public DbSet<OneTimePassword> OneTimePasswords { get; set; }

        public MeetingContext(DbContextOptions<MeetingContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.IsNeededChangePassword)
                .HasDefaultValue(false);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Room>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<Department>()
                .HasIndex(d => d.Name)
                .IsUnique();
        }
    }

    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdateTime { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedTime { get; set; }
    }

    public enum MeetingRepeatType
    {
        None,
        Weekly,
    }

    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Meeting> RelatedMeetings { get; set; }
        public virtual List<User> Users { get; set; }
    }

    public class Meeting : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual Room Location { get; set; }
        public virtual List<Department> Departments { get; set; }
        public virtual List<User> Attendees { get; set; }
        public MeetingRepeatType RepeatType { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
    }

    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public virtual Department Department { get; set; }
        public virtual List<Meeting> AttendMeetings { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public bool IsNeededChangePassword { get; set; }
    }

    public class Room : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Capacity { get; set; }
    }

    public class OneTimePassword
    {
        [Key]
        public string Hash { get; set; }
        [Required]
        public virtual User RelatedUser { get; set; }
        [Required]
        public DateTime Expiration { get; set; }
    }
}