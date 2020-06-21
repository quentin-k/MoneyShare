using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoneyShare.Models;
using MoneyShare.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MoneyShare.Controllers
{
    [Route("api/[controller]")]
    public class ResetPasswordController : Controller
    {
        private readonly UserManager<MemberModel> userManager;

        public ResetPasswordController(UserManager<MemberModel> userManager)
        {
            this.userManager = userManager;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ResetPasswordViewModel model)
        {
            if (model.Password == model.ConfirmPassword)
            {
                var user = await userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return new OkResult();
                    }
                }
            }
            return new UnauthorizedResult();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}