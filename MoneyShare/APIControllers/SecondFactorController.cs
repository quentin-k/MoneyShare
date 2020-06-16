using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using MoneyShare.Models;
using MoneyShare.Repos;


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
            _signInManager = signInManager;
            _configuration = configuration;
            _userManager = userManager;
            _memberServices = memberServices;
            _roleManager = roleManager;
        }
        [HttpPost]
        public async Task<ActionResult<LoginResponseViewModel>> Post([FromBody] SecondFactorRequestViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.SecondFactorValue))
            {
                return new UnauthorizedResult();
            }

            MemberModel member = await _userManager.FindByNameAsync(model.Username);
            if (member != null)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.CheckPasswordSignInAsync(member, model.Password, false);
                if (result.Succeeded)
                {
                    if (_memberServices.ValidateTwoFactorCodeAsync(member, model.SecondFactorValue))
                    {
                        IList<string> roles = await _userManager.GetRolesAsync(member);
                        string role = "";
                        if (roles.Contains("Admin"))
                            role = "Admin";
                        else if (roles.Contains("Staff"))
                            role = "Staff";
                        else if (roles.Contains("Member"))
                            role = "Member";

                        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtKey"]));
                        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                new Claim(ClaimTypes.Name, member.Id.ToString()),
                                new Claim(ClaimTypes.Role,role)
                            }),
                            Expires = DateTime.UtcNow.AddDays(7),
                            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
                        };
                        SecurityToken securityToken = handler.CreateToken(tokenDescriptor);
                        LoginResponseViewModel responseModel = new LoginResponseViewModel();
                        responseModel.Token = handler.WriteToken(securityToken);
                        return new OkObjectResult(responseModel);
                    }

                }
            }
            return new UnauthorizedResult();
        }
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