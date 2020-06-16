using GalaxyAPI.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.ValidationAttributes
{
    /// <summary>
    /// Валидация LangSourceID и LangTargetID
    /// </summary>
    public class LangIDAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var project = value as ProjectRequest;

            if (project.LangSource == project.LangTarget)
            {
                ErrorMessage = "LangSource и LangTarget не должны быть равны";
                return false;
            }
            return true;
        }
    }
}
