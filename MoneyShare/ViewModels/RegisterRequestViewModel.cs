using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyShare.ViewModels
{
    public class RegisterRequestViewModel
    {
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}