using Microsoft.AspNetCore.Identity.UI.Services;
using MoneyShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyShare.Repos
{
    public interface IEmailService : IEmailSender
    {
        public void EmailTwoFactorCode(MemberModel member);
        public void EmailForgotPassword(MemberModel member, String link);
    }
}