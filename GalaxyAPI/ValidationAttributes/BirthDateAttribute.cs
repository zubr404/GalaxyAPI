using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.ValidationAttributes
{
    /// <summary>
    /// Валидация даты рождения пользователя
    /// </summary>
    public class BirthDateAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            const int MINAGE = 0;
            const int MAXAGE = 100;
            const string DATEFORMAT = "yyyy-MM-dd";

            if (base.IsValid(value))
            {
                var birthDateStr = value.ToString();
                if (DateTime.TryParseExact(birthDateStr, DATEFORMAT, new CultureInfo("en-US"), DateTimeStyles.None, out DateTime birthDate))
                {
                    var age = DateTime.Now.Year - birthDate.Year;
                    if (birthDate.AddYears(age) > DateTime.Now)
                    {
                        age--;
                    }
                    if (age < MINAGE || age > MAXAGE)
                    {
                        ErrorMessage = $"Не допустимая дата рождения. Возраст должен быть больше {MINAGE} и меньше {MAXAGE}.";
                        return false;
                    }
                    return true;
                }
                else
                {
                    ErrorMessage = $"Дата рождения не соответствует формату {DATEFORMAT}";
                    return false;
                }
            }
            else
            {
                ErrorMessage = $"Не указана дата рождения.";
                return false;
            }
        }
    }
}
