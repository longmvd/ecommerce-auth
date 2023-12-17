﻿using ECommerce.BL;
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
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using ECommerce.Common.Extension;
using ECommerce.Common;


namespace AuthService.BL
{
    public class UserBL : BaseBL, IUserBL
    {
        private IRoleBL _roleBL;
        public UserBL(IUserDL userDL, IRoleBL roleBL, IConfiguration configuration) : base(userDL, configuration)
        {
            this._baseDL = userDL;
            this._roleBL = roleBL;
        }

        public override async Task<T> GetByID<T>(Type type, string id)
        {
            var user = await base.GetByID<T>(type, id) as User;
            if (user != null)
            {
                var roles = await _roleBL.GetRolesByUserID(user.UserID);
                user.Roles = roles;
            }
            return user as T;

        }

        public override void BeforeSave(BaseEntity entity)
        {
            base.BeforeSave(entity);
            var user = entity as User;
            user.UserCode = Utils.GenerateSearchParam().Replace("@", "");

            if (user != null && user.Password != null)
            {
                var password = HashPassword(user.Password);
                user.Password = password;
            }
        }

        
        public override async Task ValidateBeforeSaveAsync(BaseEntity entity, List<ValidateResult> validateResults)
        {
            var user = entity as User;
            var sql = Utils.GetStringQuery("UserBL_ValidateDuplicated");
            var result = await ExecuteScalarAsyncUsingCommandText<int>(sql, new Dictionary<string, object>()
            {
                {"@UserName", user.UserName },
                {"@Email", user.Email },
                {"@PhoneNumber", user.PhoneNumber }
            });

            if (result > 0)
            {
                validateResults.Add(new ValidateResult()
                {
                    ErrorCode = ErrorCode.Duplicated
                });
            }
            await base.ValidateBeforeSaveAsync(entity, validateResults);
        }




        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

    }
}
