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
        Task AddFromDTOAsync(MeetingAddDTO dto, int creatorID);
        Task UpdateFromDTOAsync(MeetingUpdateDTO dto);
    }
    public class MeetingRepository : Repository<Meeting>, IMeetingRepository
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;
        public MeetingRepository(MeetingContext dbContext, IRoomRepository roomRepository, IDepartmentRepository departmentRepository, IUserRepository userRepository, IMailService mailService) : base(dbContext)
        {
            _roomRepository = roomRepository;
            _departmentRepository = departmentRepository;
            _userRepository = userRepository;
            _mailService = mailService;
        }

        public async Task<bool> ValidateMeetingAsync(Meeting meeting, bool toUpdate)
        {
            if (meeting.FromDate >= meeting.ToDate)
            {
                throw new Exception("The attribute 'FromDate' must smaller than the attribute 'ToDate'.");
            }

            var meetingsInSameRoom = this.Find(x => x.Location == meeting.Location).Where(x => x.ToDate >= meeting.ToDate && x.FromDate <= meeting.FromDate);
            var count = await meetingsInSameRoom.CountAsync();

            if (count == 1)
            {
                var item = await meetingsInSameRoom.FirstAsync();

                if (item.ID == meeting.ID)
                {
                    return true;
                }
            }


            if (count != 0)
            {
                throw new Exception("There is already a meeting in the room at the same time.");
            }

            return true;
        }

        public async Task AddFromDTOAsync(MeetingAddDTO dto, int creatorID)
        {
            var creator = await _userRepository.Get(creatorID);

            var departments = await _departmentRepository.Find(x => dto.Departments.Any(n => n == x.Name)).ToListAsync();
            var room = await _roomRepository.Find(x => x.Name == dto.Location).FirstAsync();
            var attendees = await _userRepository.Find(u => dto.Attendees.Contains(u.ID)).ToListAsync();

            var entity = new Meeting();
            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Attendees = attendees;
            entity.Location = room;
            entity.Departments = departments;
            entity.Creator = creator;
            entity.FromDate = dto.FromDate;
            entity.ToDate = dto.ToDate;

            if (await ValidateMeetingAsync(entity, false))
            {
                await this.Add(entity);

                await _mailService.SendMail(
                    "New meeting!",
                    $"<p>You have been invited to the meeting '{dto.Title}'</p>" +
                    $"<p>Description: { (String.IsNullOrEmpty(dto.Description) ? "Empty" : dto.Description) }</p>" +
                    $"<p>The location is {dto.Location}</p>" +
                    $"<p>The Departments of this meeting is {String.Join(",", departments.Select(x => x.Name))} </p>" +
                    $"<p>The meeting will start on {dto.FromDate}. End on {dto.ToDate} </p>",
                    MailType.MeetingCreated,
                    attendees.Select(x => x.Email)
                );
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

            if (await ValidateMeetingAsync(meeting, true))
            {
                await this.Update(meeting);

                await _mailService.SendMail(
                    $"Your meeting '{meeting.Title}' is updated",
                    $"<p>The meeting '{meeting.Title}' have been updated</p>" +
                    $"<p>Description: { (String.IsNullOrEmpty(meeting.Description) ? "Empty" : meeting.Description) }</p>" +
                    $"<p>The location is {meeting.Location.Name}</p>" +
                    $"<p>The Departments of this meeting is {String.Join(",", meeting.Departments.Select(x => x.Name))} </p>" +
                    $"<p>The meeting will start on {meeting.FromDate}. End on {meeting.ToDate} </p>",
                    MailType.MeetingUpdated,
                    meeting.Attendees.Select(x => x.Email)
                );
            }
        }
    }
}
