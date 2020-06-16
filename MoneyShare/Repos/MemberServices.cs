using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoneyShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyShare.Repos
{
    public class MemberServices : IMemberServices
    {
        private ApplicationDbContext _dbContext;
        private IEmailService _emailService;
        public static Random _rand = new Random();
        private UserManager<MemberModel> _userManager;
        public MemberServices(ApplicationDbContext dbContext, IEmailService emailService, UserManager<MemberModel> userManager)
        {
            _dbContext = dbContext;
            _emailService = emailService;
            _userManager = userManager;
        }

        public async Task SendTwoFactorCodeAsync(MemberModel member)
        {
            int code = _rand.Next(0, 999999);
            member.TwoFactorCode = code.ToString("000000");
            member.TwoFactorCodeDateTime = DateTime.Now;
            await _userManager.UpdateAsync(member);
            _emailService.EmailTwoFactorCode(member);
        }

        public void SendResetLink(MemberModel member, String link)
        {
            _emailService.EmailForgotPassword(member, link);
        }

        public bool ValidateTwoFactorCodeAsync(MemberModel member, string code)
        {
            if(member.TwoFactorEnabled && member.TwoFactorCodeDateTime!=null && !string.IsNullOrEmpty(member.TwoFactorCode))
            {
                TimeSpan codeTimeSpan = DateTime.Now - member.TwoFactorCodeDateTime;
                if (codeTimeSpan <= TimeSpan.FromMinutes(5))
                {
                    if(code == member.TwoFactorCode)
                    {
                        member.TwoFactorCode = "";
                        _userManager.UpdateAsync(member);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}