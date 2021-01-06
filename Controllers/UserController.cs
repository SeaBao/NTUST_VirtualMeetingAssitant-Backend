using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using VirturlMeetingAssitant.Backend.Db;
using System.Security.Claims;

namespace VirturlMeetingAssitant.Backend.DTO
{
    public class UserBaseDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string DepartmentName { get; set; }
    }

    public class UserPasswordUpdateDTO
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }

    public class UserUpdateDTO
    {
        [Required]
        public int ID { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string DepartmentName { get; set; }

    }
    public class UserAddDTO : UserBaseDTO
    {
        [Required]
        public string Password { get; set; }
    }

    public class UserDTO : UserBaseDTO
    {
        public int ID { get; set; }
    }

    public class ForgetPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

namespace VirturlMeetingAssitant.Backend.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using VirturlMeetingAssitant.Backend.DTO;

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IOneTimePasswordRepository _otpRepository;
        private readonly IMailService _mailService;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, IOneTimePasswordRepository otpRepository, IMailService mailService)
        {
            _userRepository = userRepository;
            _otpRepository = otpRepository;
            _mailService = mailService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var uesrs = await _userRepository.GetAll();

            return uesrs.Select(u =>
            {
                return new UserDTO()
                {
                    ID = u.ID,
                    Name = u.Name,
                    Email = u.Email,
                    DepartmentName = u.Department == null ? null : u.Department.Name
                };
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddDTO dto)
        {
            try
            {
                await _userRepository.AddFromDTOAsync(dto);

                await _mailService.SendMail(
                    "Hi! You have been added into our meeting service", "Welcome! Feel free to check out website on http://localhost:8080",
                    MailType.NewUser,
                    new string[] { dto.Email }
                );

                return Ok();
            }
            catch (System.Exception ex)
            {
                return Problem($"Message: {ex.Message}\n InnerException: {ex.InnerException}");
            }
        }

        [Authorize]
        [HttpPatch("password")]
        public async Task<IActionResult> UpdatePassword(UserPasswordUpdateDTO dto)
        {
            try
            {
                var result = await _userRepository.UpdatePasswordFromDTO(dto);

                if (result)
                {
                    return Ok();
                }
                return BadRequest("Old password is not match");
            }
            catch (System.Exception ex)
            {
                return Problem($"Message: {ex.Message}\n InnerException: {ex.InnerException}");
            }
        }

        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> UpdateUser(UserUpdateDTO dto)
        {
            try
            {
                await _userRepository.UpdateFromDTOAsync(dto);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return Problem($"Message: {ex.Message}\n InnerException: {ex.InnerException}");
            }
        }

        [Authorize]
        [HttpGet("otp")]
        public async Task<ActionResult<bool>> CheckNeedUpdatePassword()
        {
            string id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var dbuser = await _userRepository.Find(u => u.ID == Convert.ToInt32(id)).FirstOrDefaultAsync();

            return Ok(dbuser.IsNeededChangePassword);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int userId)
        {
            try
            {
                var user = await _userRepository.Get(userId);
                await _userRepository.Remove(user);

                return Ok();
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("forget-pw")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDTO dto)
        {
            try
            {
                var user = await _userRepository.Find(x => x.Email == dto.Email).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound();
                }

                var otp = await _otpRepository.CreateOTP(user, DateTime.UtcNow.AddHours(24));

                var resetUrl = $"http://localhost:8080/#/resetPassword?otp={otp.Hash}";

                await _mailService.SendMail("Reset Password Notification",
                    $"<p>Your reset URL is <a href=\"{resetUrl}\">{resetUrl}</a></p>",
                    MailType.ResetPassword,
                    new string[] { user.Email }
                );

                return Ok();
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
