using AuthService.Common.Entities;
using ECommerce.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.BL
{
    public interface IRoleBL : IBaseBL
    {
        Task<List<Role>>GetRolesByUserID(Guid userID);
    }
}
