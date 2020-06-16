using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoneyShare.Models;
using MoneyShare.Repos;
using MoneyShare.ViewModels;

namespace MoneyShare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private SignInManager<MemberModel> _signInManager;
        private UserManager<MemberModel> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public RegisterController(
            SignInManager<MemberModel> signInManager,
            UserManager<MemberModel> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPut]
        public async Task<ActionResult<ResponseStatusViewModel>> Put(RegisterRequestViewModel model)
        {
            ResponseStatusViewModel responseModel = new ResponseStatusViewModel();
            responseModel.Result = true;
            if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                responseModel.Result = false;
                responseModel.Messages.Add("First name cannot be blank.");
            }
            if (string.IsNullOrWhiteSpace(model.LastName))
            {
                responseModel.Result = false;
                responseModel.Messages.Add("Last name cannot be blank.");
            }
            if (string.IsNullOrWhiteSpace(model.Username))
            {
                responseModel.Result = false;
                responseModel.Messages.Add("Username cannot be blank.");
            }
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                responseModel.Result = false;
                responseModel.Messages.Add("Password cannot be blank.");
            }
            if (!responseModel.Result)
                return new BadRequestObjectResult(responseModel);

            MemberModel member = new MemberModel()
            {
                Email = model.Email,
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                TwoFactorEnabled = true
            };
            IdentityResult result = await _userManager.CreateAsync(member, model.Password);
            if (result.Succeeded)
            {
                IdentityRole userRole = _roleManager.Roles.FirstOrDefault(r => r.Name == "User");
                if (userRole == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }
                await _userManager.AddToRoleAsync(member, userRole.Name);
                responseModel.Result = true;
                responseModel.Messages.Add("Thank you for registering your account.");
                return new OkObjectResult(responseModel);
            }
            else
            {
                responseModel.Result = false;
                responseModel.Messages.Add("Unable to create your user account.");
                return new BadRequestObjectResult(responseModel);
            }
        }
    }
}