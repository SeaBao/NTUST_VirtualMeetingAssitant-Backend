using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirturlMeetingAssitant.Backend.Db;

namespace VirturlMeetingAssitant.Backend.DTO
{
    public class DepartmentUpdateDTO
    {
        public string originalName { get; set; }
        public string newName { get; set; }
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

        public DepartmentController(ILogger<DepartmentController> logger, IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetAll()
        {
            var departments = await _departmentRepository.GetAll();
            return departments.Select(x => x.Name);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Department department)
        {
            try
            {
                await _departmentRepository.Add(department);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPatch]
        public async Task<IActionResult> Update(DepartmentUpdateDTO dto)
        {
            try
            {
                var department = await _departmentRepository.Get(dto.originalName);
                if (department == null)
                {
                    return new NotFoundObjectResult("No such department exists");
                }

                department.Name = dto.newName;
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
