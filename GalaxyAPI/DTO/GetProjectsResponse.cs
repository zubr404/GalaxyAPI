using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.DTO
{
    public class GetProjectsResponse
    {
        public int CountElements { get; set; }
        public List<ProjectResponse> Projects { get; set; }

        public GetProjectsResponse()
        {
            Projects = new List<ProjectResponse>();
        }
    }
}
