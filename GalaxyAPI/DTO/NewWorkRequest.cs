using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.DTO
{
    public class NewWorkRequest
    {
        [Required]
        public int ProjectID { get; set; }
        [Required]
        public IFormFile[] Files { get; set; }
    }
}
