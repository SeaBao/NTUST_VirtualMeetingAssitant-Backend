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
