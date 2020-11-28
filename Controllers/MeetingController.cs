using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirturlMeetingAssitant.Backend.Db;

namespace VirturlMeetingAssitant.Backend.DTO
{
    public class MeetingDTO
    {
        public int MeetingID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Departments { get; set; }
        public string Location { get; set; }
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
}

namespace VirturlMeetingAssitant.Backend.Controllers
{
    using VirturlMeetingAssitant.Backend.DTO;
    [ApiController]
    [Route("[controller]")]
    public class MeetingController : ControllerBase
    {
        private readonly ILogger<MeetingController> _logger;
        private readonly IMeetingRepository _meetingRepository;

        public MeetingController(ILogger<MeetingController> logger, IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<MeetingDTO>> GetAll()
        {
            var meetings = await _meetingRepository.GetAll();


            return meetings.Select(x =>
            {
                var dto = new MeetingDTO();
                dto.MeetingID = x.MeetingID;
                dto.Description = x.Description;
                dto.Title = x.Title;
                dto.FromDate = x.FromDate;
                dto.ToDate = x.ToDate;
                dto.Location = x.Location.Name;
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
