using AuthService.BL;
using ECommerce.BL;
using ECommerce.Common.Entities;
using ECommerce.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController
    {
        public RolesController(IRoleBL roleBL) : base(roleBL)
        {
            this._baseBL = roleBL;
            this.CurrentType = typeof(Role);
        }
    }
}
