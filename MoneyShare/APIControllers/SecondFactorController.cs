using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MoneyShare.Repos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using MoneyShare.Models;

namespace MoneyShare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecondFactorController : ControllerBase
    {
        private IConfiguration _configuration;
        private SignInManager<MemberModel> _signInManager;
        private UserManager<MemberModel> _userManager;
        private IMemberServices _memberServices;
        private RoleManager<IdentityRole> _roleManager;
        public SecondFactorController(
            IConfiguration configuration,
            IMemberServices memberServices,
            SignInManager<MemberModel> signInManager,
            UserManager<MemberModel> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _memberServices = memberServices;
            _roleManager = roleManager;
        }
    }

    public class SecondFactorRequestViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string SecondFactorValue { get; set; }
    }

    public class LoginResponseViewModel
    {
        public string Token { get; set; }
    }
}