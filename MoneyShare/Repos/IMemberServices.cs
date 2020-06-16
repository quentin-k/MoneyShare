using MoneyShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyShare.Repos
{
    public interface IMemberServices
    {
        public Task SendTwoFactorCodeAsync(MemberModel member);
        public void SendResetLink(MemberModel member, String link);
        public bool ValidateTwoFactorCodeAsync(MemberModel member, string code);
    }
}