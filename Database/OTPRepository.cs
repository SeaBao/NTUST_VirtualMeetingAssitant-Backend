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
    public interface IOneTimePasswordRepository : IRepository<OneTimePassword>
    {
        Task<(OneTimePassword otp, bool isValid)> CheckOTPValid(string otp);
        Task<bool> UpdateUserPassword(string otp, User user, string newPassword);
        Task<OneTimePassword> CreateOTP(User user, DateTime expiration);
    }
    public class OneTimePasswordRepository : Repository<OneTimePassword>, IOneTimePasswordRepository
    {
        private readonly IUserRepository _userRepository;
        public OneTimePasswordRepository(MeetingContext dbContext, IUserRepository userRepository) : base(dbContext)
        {
            _userRepository = userRepository;
        }

        public async Task<OneTimePassword> CreateOTP(User user, DateTime expiration)
        {
            var otp = new OneTimePassword()
            {
                Hash = KeyGenerator.GetUniqueKey(12),
                RelatedUser = user,
                Expiration = expiration,
            };

            return await this.Add(otp);
        }

        public async Task<(OneTimePassword otp, bool isValid)> CheckOTPValid(string otp)
        {
            var entity = await this.Get(otp);

            if (entity == null || entity.IsUsed) return (entity, false);

            return (entity, true);
        }

        public async Task<bool> UpdateUserPassword(string otp, User user, string newPassword)
        {
            var checkResult = await this.CheckOTPValid(otp);

            if (!checkResult.isValid)
            {
                return false;
            }

            if (checkResult.otp.RelatedUser != user || checkResult.otp.Expiration < DateTime.UtcNow)
            {
                return false;
            }

            await _userRepository.UpdatePassword(user, newPassword);
            checkResult.otp.IsUsed = true;
            await this.Update(checkResult.otp);
            return true;
        }
    }
}
