using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyShare.Models
{
    public class MemberModel : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string TwoFactorCode { get; set; }
        public DateTime TwoFactorCodeDateTime { get; set; }
    }
}
