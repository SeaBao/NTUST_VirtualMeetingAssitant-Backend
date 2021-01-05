using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirturlMeetingAssitant.Backend.Db;
using Microsoft.EntityFrameworkCore;

namespace VirturlMeetingAssitant.Backend.DTO
{
    public class UpdateUserPasswordDTO
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }
    }
}

namespace VirturlMeetingAssitant.Backend.Controllers
{
    using VirturlMeetingAssitant.Backend.DTO;

    [ApiController]
    [Route("api/[controller]")]
    public class OTPController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IOneTimePasswordRepository _otpRepository;
        private readonly IUserRepository _userRepository;
        public OTPController(ILogger<RoomController> logger, IOneTimePasswordRepository otpRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _otpRepository = otpRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<bool>> CheckOTPValid(string otp)
        {
            var result = await _otpRepository.CheckOTPValid(otp);
            return Ok(result.isValid);
        }


        [HttpGet("test")]
        public async Task<IActionResult> Create()
        {
            var user = await _userRepository.Get(18);
            var otp = await _otpRepository.CreateOTP(user, DateTime.Now);

            return Ok(otp);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserPassword(UpdateUserPasswordDTO dto, string otp)
        {
            var user = await _userRepository.Find(u => u.ID == dto.UserId).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("The user ID is not found");
            }

            var updateResult = await _otpRepository.UpdateUserPassword(otp, user, dto.NewPassword);

            if (updateResult)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Cannot update user password. OTP chould be invalid");
            }
        }
    }
}
