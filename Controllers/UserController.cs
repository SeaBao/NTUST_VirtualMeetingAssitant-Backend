using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirturlMeetingAssitant.Backend.Db;

namespace VirturlMeetingAssitant.Backend.DTO
{
    public class UserAddDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int DepartmentID { get; set; }
    }

    public class UserDTO
    {

    }
}

namespace VirturlMeetingAssitant.Backend.Controllers
{
    using VirturlMeetingAssitant.Backend.DTO;
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddDTO dto)
        {
            try
            {
                await _userRepository.AddFromDTOAsync(dto);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return Problem($"Message: {ex.Message}\n InnerException: {ex.InnerException}");
            }
        }

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
    }
}
