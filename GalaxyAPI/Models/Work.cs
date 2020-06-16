using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.Models
{
    public class Work
    {
        public Guid ID { get; set; }
        public int ProjectID { get;set; }
        public string Name { get; set; }
        public string UploadDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool Validated { get; set; }
        public string Data { get; set; }
        public List<Target> Targets { get; set; }
        public Work()
        {
            Targets = new List<Target>();
        }
    }
}
