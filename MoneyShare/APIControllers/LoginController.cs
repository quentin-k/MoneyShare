using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using MoneyShare.Models;
using MoneyShare.Repos;
using MoneyShare.ViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace MoneyShare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private SignInManager<MemberModel> _signInManager;
        private UserManager<MemberModel> _userManager;
        private IMemberServices _memberServices;
        private IConfiguration _configuration;

        // private static string _LoginFailureMessage = "Login Failed.";

        public LoginController(
            SignInManager<MemberModel> signInManager,
            UserManager<MemberModel> userManager,
            IMemberServices memberServices,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _memberServices = memberServices;

            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LoginRequestViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
            {
                return new UnauthorizedResult();
            }

            MemberModel member = await _userManager.FindByNameAsync(model.Username);
            if (member != null)
            {
                SignInResult result = await _signInManager.CheckPasswordSignInAsync(member, model.Password, false);
                if (result.Succeeded)
                {
                    // await _signInManager.SignInAsync(member, false);

                    await _memberServices.SendTwoFactorCodeAsync(member);
                    return new OkResult();
                }
            }
            return new UnauthorizedResult();
        }
    }
}