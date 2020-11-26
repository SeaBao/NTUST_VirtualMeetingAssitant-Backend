using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirturlMeetingAssitant.Backend.Db;

namespace VirturlMeetingAssitant.Backend.Controllers
{
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
        public void Add()
        {

        }
    }
}
