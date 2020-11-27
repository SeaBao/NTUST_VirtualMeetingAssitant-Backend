using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirturlMeetingAssitant.Backend.Db;

namespace VirturlMeetingAssitant.Backend.Controllers
{
    public class MeetingAddDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string RoomName { get; set; }
        public MeetingRepeatType RepeatType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
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
        public async Task<IEnumerable<Meeting>> GetAll()
        {
            return await _meetingRepository.GetAll();
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
    }
}
