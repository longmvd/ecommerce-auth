using AuthService.BL;
using AuthService.Common.Entities;
using ECommerce.BL;
using ECommerce.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        public UsersController(IUserBL userBL) : base(userBL)
        {
            this._baseBL = userBL;
            this.CurrentType = typeof(User);
        }
    }
}
