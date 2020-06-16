using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.Models
{
    public class ProjectUser
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public int UserID { get; set; }
        public Project Project { get; set; }
        public User User { get; set; }
    }
}
