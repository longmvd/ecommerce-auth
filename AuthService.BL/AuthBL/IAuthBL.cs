using AuthService.Common.Entities;
using AuthService.Common.Records;
using ECommerce.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.BL.AuthBL
{
    public interface IAuthBL
    {
        Task<ServiceResponse> Login(LoginRequest request);

        Task<ServiceResponse> Logout();

        bool ValidateToken(string token);

        object GetInfoFromToken(string token);

        string BuildToken(string key, string issuer, IEnumerable<string> audience, User userName);
    }
}
