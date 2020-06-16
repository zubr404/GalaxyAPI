using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.DTO
{
    public class SourceDto
    {
        public Guid ID { get; set; }
        public int LocalID { get; set; }
        public int ProjectID { get; set; }
        public string Text { get; set; }
    }
}
