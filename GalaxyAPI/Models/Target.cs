using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.Models
{
    public class Target
    {
        public int ID { get; set; }
        public Guid WorkID { get; set; }
        public Guid SourceID { get; set; }
        public string Text { get; set; }
        public bool Validated { get; set; }
    }
}
