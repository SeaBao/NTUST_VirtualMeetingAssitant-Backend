using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirturlMeetingAssitant.Backend.Db;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace VirturlMeetingAssitant.Backend.DTO
{
    public class MeetingBaseDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Departments { get; set; }
        public List<int> Attendees { get; set; }
        public string Location { get; set; }
        public MeetingRepeatType RepeatType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
    public class MeetingDTO : MeetingBaseDTO
    {
        public int MeetingID { get; set; }
        public int CreatorUid { get; set; }
    }
    public class MeetingAddDTO : MeetingBaseDTO
    {

    }

    public class MeetingUpdateDTO : MeetingBaseDTO
    {
        [Required]
        public int MeetingID { get; set; }
        new public MeetingRepeatType? RepeatType { get; set; }
        new public DateTime? FromDate { get; set; }
        new public DateTime? ToDate { get; set; }
    }
}

namespace VirturlMeetingAssitant.Backend.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using VirturlMeetingAssitant.Backend.DTO;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MeetingController : ControllerBase
    {
        private readonly ILogger<MeetingController> _logger;
        private readonly IMapper _mapper;
        private readonly IMeetingRepository _meetingRepository;

        public MeetingController(IMapper mapper, ILogger<MeetingController> logger, IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("users/{id:int}")]
        [HttpGet]
        public async Task<IEnumerable<MeetingDTO>> GetByUserIdAsync(int id)
        {
            var meetings = await _meetingRepository.Find(m => m.Attendees.Where(u => u.ID == id).Count() > 0).ToListAsync();

            return meetings.Select(x =>
            {
                var dto = new MeetingDTO();
                dto.MeetingID = x.ID;
                dto.Description = x.Description;
                dto.Title = x.Title;
                dto.FromDate = x.FromDate;
                dto.ToDate = x.ToDate;
                dto.Location = x.Location.Name;
                dto.RepeatType = x.RepeatType;
                dto.Attendees = x.Attendees.Select(x => x.ID).ToList();
                dto.Departments = x.Departments.Select(d => d.Name).ToList();

                return dto;
            });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MeetingDTO>> GetMeeting(int id)
        {
            var meeting = await _meetingRepository.Find(x => x.ID == id).FirstOrDefaultAsync();

            if (meeting == null)
            {
                return NotFound();
            }

            var dto = new MeetingDTO()
            {
                MeetingID = meeting.ID,
                Description = meeting.Description,
                Title = meeting.Title,
                FromDate = meeting.FromDate.ToUniversalTime(),
                ToDate = meeting.ToDate.ToUniversalTime(),
                Location = meeting.Location.Name,
                RepeatType = meeting.RepeatType,
                Attendees = meeting.Attendees.Select(x => x.ID).ToList(),
                Departments = meeting.Departments.Select(d => d.Name).ToList()
            };

            return Ok(dto);
        }

        [HttpGet]
        public async Task<IEnumerable<MeetingDTO>> GetAll()
        {
            var meetings = await _meetingRepository.GetAll();

            return meetings.Select(x =>
            {
                var dto = new MeetingDTO();
                dto.MeetingID = x.ID;
                dto.Description = x.Description;
                dto.Title = x.Title;
                dto.FromDate = x.FromDate.ToUniversalTime();
                dto.ToDate = x.ToDate.ToUniversalTime();
                dto.Location = x.Location.Name;
                dto.RepeatType = x.RepeatType;
                dto.Attendees = x.Attendees.Select(x => x.ID).ToList();
                dto.Departments = x.Departments.Select(d => d.Name).ToList();

                return dto;
            });
        }

        [HttpPost]
        public async Task<ActionResult> Add(MeetingAddDTO dto)
        {
            try
            {
                await _meetingRepository.AddFromDTOAsync(dto);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpPatch]
        public async Task<ActionResult> Update(MeetingUpdateDTO dto)
        {
            try
            {
                await _meetingRepository.UpdateFromDTOAsync(dto);
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int meetingUid)
        {
            try
            {
                var meeting = await _meetingRepository.Get(meetingUid);
                await _meetingRepository.Remove(meeting);

                return Ok();
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
