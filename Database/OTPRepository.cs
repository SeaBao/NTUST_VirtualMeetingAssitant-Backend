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
        Task<bool> UpdateUserPassword(string otp, string newPassword);
        Task<OneTimePassword> CreateOTP(User user, DateTime expiration);
    }
    public class OneTimePasswordRepository : Repository<OneTimePassword>, IOneTimePasswordRepository
    {
        private readonly IUserRepository _userRepository;
        public OneTimePasswordRepository(MeetingContext dbContext, IUserRepository userRepository) : base(dbContext)
        {
            _userRepository = userRepository;
        }
        /// <summary>
        /// Create an OTP for the given user.
        /// </summary>
        /// <param name="user">The specified user.</param>
        /// <param name="expiration">When will the OTP expired</param>
        public async Task<OneTimePassword> CreateOTP(User user, DateTime expiration)
        {
            var otp = new OneTimePassword()
            {
                Hash = KeyGenerator.GetUniqueKey(20),
                RelatedUser = user,
                Expiration = expiration,
                IsUsed = false,
            };

            return await this.Add(otp);
        }

        /// <summary>
        /// Check if OTP is valid or not.
        /// </summary>
        /// <remarks>
        /// If OTP is valid, then return true, otherwise return false.
        /// </remarks>
        public async Task<(OneTimePassword otp, bool isValid)> CheckOTPValid(string otp)
        {
            var entity = await this.Get(otp);

            if (entity == null || entity.IsUsed) return (entity, false);

            return (entity, true);
        }

        /// <summary>
        /// Update a user password with OTP.
        /// </summary>
        public async Task<bool> UpdateUserPassword(string otp, string newPassword)
        {
            var checkResult = await this.CheckOTPValid(otp);

            if (!checkResult.isValid)
            {
                return false;
            }

            if (checkResult.otp.Expiration < DateTime.UtcNow)
            {
                return false;
            }

            await _userRepository.UpdatePassword(checkResult.otp.RelatedUser, newPassword);
            checkResult.otp.IsUsed = true;
            await this.Update(checkResult.otp);
            return true;
        }
    }
}
