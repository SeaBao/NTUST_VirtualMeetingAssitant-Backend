using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirturlMeetingAssitant.Backend.Db;

namespace VirturlMeetingAssitant.Backend.DTO
{
    public class RoomAddDTO
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
    }

    public class RoomDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
    }
}

namespace VirturlMeetingAssitant.Backend.Controllers
{
    using VirturlMeetingAssitant.Backend.DTO;
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomRepository _roomRepository;

        public RoomController(ILogger<RoomController> logger, IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<RoomDTO>> GetAll()
        {
            var rooms = await _roomRepository.GetAll();

            return rooms.Select(x =>
            {
                var dto = new RoomDTO();
                dto.ID = x.ID;
                dto.Name = x.Name;
                dto.Capacity = x.Capacity;

                return dto;
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoomAddDTO dto)
        {
            try
            {
                Room toCreatedRoom = new();
                toCreatedRoom.Name = dto.Name;
                toCreatedRoom.Capacity = dto.Capacity;
                await _roomRepository.Add(toCreatedRoom);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return Problem($"Message: {ex.Message}\n InnerException: {ex.InnerException}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string roomName)
        {
            try
            {
                var room = _roomRepository.Find(x => x.Name == roomName).FirstOrDefault();

                if (room == null)
                {
                    return NotFound($"The room ({roomName}) is not found");
                }
                await _roomRepository.Remove(room);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
