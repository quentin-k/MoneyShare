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

namespace MoneyShare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private SignInManager<MemberModel> _signInManager;
        private UserManager<MemberModel> _userManager;
        private IMemberServices _memberServices;
        private IConfiguration _configuration;

        // private static string _LoginFailureMessage = "Login Failed.";

        public ForgotPasswordController(
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
        public async Task<ActionResult> Post([FromBody] ForgotPasswordViewModel model)
        {
            if (
                string.IsNullOrWhiteSpace(model.Username) ||
                string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.FirstName) ||
                string.IsNullOrWhiteSpace(model.LastName))
            {
                return new UnauthorizedResult();
            }
            else
            {
                MemberModel UnameMember = await _userManager.FindByNameAsync(model.Username);
                MemberModel EmailMember = await _userManager.FindByEmailAsync(model.Email);
                if (
                    UnameMember != null &&
                    UnameMember == EmailMember &&
                    UnameMember.FirstName == model.FirstName &&
                    EmailMember.LastName == model.LastName)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(UnameMember);
                    var resetLink = Url.Action("index", "Home",
                        new { UserId = EmailMember.Id, Token = token }, Request.Scheme);
                    resetLink += "#ResetPasswordModal";
                    _memberServices.SendResetLink(UnameMember, resetLink);
                    return new OkResult();
                }
                return new UnauthorizedResult();
            }
        }
    }
}