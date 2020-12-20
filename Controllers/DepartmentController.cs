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
    public class DepartmentUpdateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IEnumerable<int> Ids { get; set; }
    }

    public class DepartmentAddDTO
    {
        [Required]
        public string Name { get; set; }
    }

    public class DepartmentDTO
    {
        public string Name { get; set; }
        public IEnumerable<int> Attendees { get; set; }
    }
}

namespace VirturlMeetingAssitant.Backend.Controllers
{
    using VirturlMeetingAssitant.Backend.DTO;
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUserRepository _userRepository;

        public DepartmentController(ILogger<DepartmentController> logger, IDepartmentRepository departmentRepository, IUserRepository userRepository)
        {
            _departmentRepository = departmentRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<DepartmentDTO>> GetAll()
        {
            var departments = await _departmentRepository.GetAll();
            return departments.Select(d =>
            {
                return new DepartmentDTO()
                {
                    Name = d.Name,
                    Attendees = d.Users.Select(u =>
                    {
                        return u.ID;
                    }),
                };
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(DepartmentAddDTO dto)
        {
            try
            {
                var department = new Department() { Name = dto.Name };
                await _departmentRepository.Add(department);
                return Ok();
            }
            catch (System.Exception)
            {
                return BadRequest("Duplicate department name");
            }

        }

        [HttpPatch]
        public async Task<IActionResult> Update(DepartmentUpdateDTO dto)
        {
            try
            {
                var department = await _departmentRepository.Get(dto.Name);
                if (department == null)
                {
                    return new NotFoundObjectResult("No such department exists");
                }

                var users = await _userRepository.Find(u => dto.Ids.Contains(u.ID)).ToListAsync();
                department.Users = users;
                await _departmentRepository.Update(department);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string departmentName)
        {
            try
            {
                var department = await _departmentRepository.Get(departmentName);
                await _departmentRepository.Remove(department);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Ok();
        }
    }
}
