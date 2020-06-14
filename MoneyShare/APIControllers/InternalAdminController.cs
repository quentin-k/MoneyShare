using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoneyShare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InternalAdminController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        public ActionResult GetAction()
        {
            string name = this.HttpContext.User.Identity.Name;
            return Ok();
        }
    }
}