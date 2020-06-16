using GalaxyAPI.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.DTO
{
    [LangID]
    public class ProjectRequest
    {
        [Required(ErrorMessage = "Поле Name должно быть установлено.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле LangSource должно быть установлено.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public string LangSource { get; set; }

        [Required(ErrorMessage = "Поле LangTarget должно быть установлено.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public string LangTarget { get; set; }

        [Required(ErrorMessage = "Поле Thematics должно быть установлено.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public string Thematics { get; set; }
    }
}
