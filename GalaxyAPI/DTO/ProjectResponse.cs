using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.DTO
{
    public class ProjectResponse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LangSource { get; set; }
        public string LangTarget { get; set; }
        public string Thematics { get; set; }
    }
}
