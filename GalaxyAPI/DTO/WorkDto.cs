using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.DTO
{
    public class WorkDto
    {
        public Guid ID { get; set; }
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public string UploadDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool Validated { get; set; }
        public string Data { get; set; }
    }
}
