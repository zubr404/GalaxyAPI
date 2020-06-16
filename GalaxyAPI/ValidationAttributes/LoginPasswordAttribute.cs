using GalaxyAPI.DTO;
using GalaxyAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.ValidationAttributes
{
    /// <summary>
    /// Валидация логина и пароля
    /// </summary>
    public class LoginPasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var user = value as CreateUserRequest;

            if(!string.IsNullOrWhiteSpace(user.Login) && !string.IsNullOrWhiteSpace(user.Password))
            {
                if (user.Login == user.Password)
                {
                    ErrorMessage = "Login и Password не должны быть одинаковыми";
                    return false;
                }
            }
            return true;
        }
    }
}
