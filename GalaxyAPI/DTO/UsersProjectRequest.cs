using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.DTO
{
    public class UsersProjectRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Значение ProjectID должно быть больше нуля.")]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Поле UsersID должно быть установлено.")]
        public List<int> UsersID { get; set; }
    }
}
