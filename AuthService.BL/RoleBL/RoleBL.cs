using ECommerce.BL;
using ECommerce.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;
using ECommerce.Common.DTO;
using AuthService.DL;
using AuthService.Common.Entities;
using ECommerce.Common;
using Microsoft.Extensions.Configuration;

namespace AuthService.BL
{
    public class RoleBL : BaseBL, IRoleBL
    {

        public RoleBL(IRoleDL roleDL, IConfiguration configuration) : base(roleDL, configuration)
        {
            this._baseDL = roleDL;
        }

        public async Task<List<Role>> GetRolesByUserID(Guid userID)
        {
            var sql = Utils.GetStringQuery("RoleBL_GetRolesByUserID");
            var res = await QueryAsyncUsingCommandText(typeof(Role), sql, new Dictionary<string, object>() { { "@UserID", userID } });
            var roles = res.Cast<Role>().ToList();
            return roles;

        }
    }
}
