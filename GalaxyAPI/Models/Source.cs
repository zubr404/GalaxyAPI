using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.Models
{
    public class Source
    {
        public Guid ID { get; set; }
        public int ProjectID { get; set; }
        public string Text { get; set; }
        public List<Target> Targets { get; set; }
        public Source()
        {
            Targets = new List<Target>();
        }
    }
}
