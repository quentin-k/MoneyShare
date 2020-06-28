using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoneyShare.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyShare.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoneyShare.Controllers
{
    [Route("api/[controller]")]
    public class AdminCheckController : Controller
    {
        private readonly UserManager<MemberModel> userManager;

        public AdminCheckController(UserManager<MemberModel> userManager)
        {
            this.userManager = userManager;
        }

        // GET: api/<controller>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            MemberModel user = await userManager.FindByIdAsync(User.Identity.Name);
            var roles = await userManager.GetRolesAsync(user);
            if (user.UserName == "Admin")
            {
                AuthorizeCheckViewModel model = new AuthorizeCheckViewModel
                {
                    Username = user.UserName,
                    Role = roles[0]
                };
                return new OkObjectResult(model);
            }
            else
            {
                return new OkResult();
            }
        }
    }
}