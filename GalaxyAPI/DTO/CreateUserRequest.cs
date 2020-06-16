using GalaxyAPI.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.DTO
{
    [LoginPassword]
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Поле FullName должно быть установлено.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public string FullName { get; set; }

        [BirthDate]
        public string BirthDate { get; set; }

        [Required(ErrorMessage = "Поле Login должно быть установлено.")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Длина Login должна быть в пределах от 3 до 15 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле Password должно быть установлено.")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Длина Password должна быть в пределах от 3 до 15 символов")]
        public string Password { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Значение RoleID должно быть больше нуля.")]
        public int RoleID { get; set; }
    }
}
