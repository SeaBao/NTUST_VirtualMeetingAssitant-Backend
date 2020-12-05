using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirturlMeetingAssitant.Backend.Db;
using AutoMapper;

namespace VirturlMeetingAssitant.Backend.DTO
{
    public class MeetingDTO
    {
        public int MeetingID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Departments { get; set; }
        public string Location { get; set; }
        public MeetingRepeatType RepeatType { get; set; }
        public int CreatorUid { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
    public class MeetingAddDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string RoomName { get; set; }
        public List<string> Departments { get; set; }
        public MeetingRepeatType RepeatType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class MeetingUpdateDTO : MeetingAddDTO
    {
        public int ID { get; set; }
    }
}

namespace VirturlMeetingAssitant.Backend.Controllers
{
    using VirturlMeetingAssitant.Backend.DTO;
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
        public IEnumerable<MeetingDTO> GetByUserId(int id)
        {
            var meetings = _meetingRepository.Find(m => m.Attendees.Where(u => u.ID == id).Count() > 0).ToList();

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
                dto.Departments = x.Departments.Select(d => d.Name).ToList();

                return dto;
            });
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
                dto.FromDate = x.FromDate;
                dto.ToDate = x.ToDate;
                dto.Location = x.Location.Name;
                dto.RepeatType = x.RepeatType;
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
