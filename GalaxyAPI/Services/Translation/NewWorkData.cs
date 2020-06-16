using GalaxyAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.Translation
{
    public class NewWorkData
    {
        public int ProjectID { get; set; }
        public List<WorkDto> Works { get; set; }
        public List<TargetDto> Targets { get; set; }
        public List<SourceDto> Sources { get; set; }

        public NewWorkData()
        {
            Works = new List<WorkDto>();
            Targets = new List<TargetDto>();
            Sources = new List<SourceDto>();
        }
    }
}
