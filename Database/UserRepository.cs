using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirturlMeetingAssitant.Backend.DTO;
using BC = BCrypt.Net.BCrypt;
namespace VirturlMeetingAssitant.Backend.Db
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> AddFromDTOAsync(UserAddDTO dto);
    }
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IDepartmentRepository _departmentRepository;
        public UserRepository(MeetingContext dbContext, IDepartmentRepository departmentRepository) : base(dbContext)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            var count = await this.Find(x => x.Email == email).CountAsync();

            return count > 0 ? true : false;
        }

        public async Task<User> AddFromDTOAsync(UserAddDTO dto)
        {
            if (await this.IsEmailExistsAsync(dto.Email))
            {
                throw new Exception($"The mail ({dto.Email} is already in use.");
            }

            var user = new User()
            {
                Name = dto.Name,
                Password = BC.HashPassword(dto.Password),
                Email = dto.Email,
                Department = await _departmentRepository.Find(x => x.Name == dto.DepartmentName).FirstAsync(),
            };

            return await this.Add(user);
        }
    }
}
