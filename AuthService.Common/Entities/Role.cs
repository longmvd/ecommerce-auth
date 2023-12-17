using Common.Entities;
using ECommerce.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Common.Entities
{
    [TableConfig(tableName:"role")]
    public class Role: BaseEntity
    {
        public Guid RoleID { get; set; }

        public string? RoleName { get; set; }

        public string? Description { get; set; }

        public string? RoleCode { get; set; }
    }
}
