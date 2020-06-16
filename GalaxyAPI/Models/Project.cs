using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.Models
{
    public class Project
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LangSource { get; set; }
        public string LangTarget { get; set; }
        public string Thematics { get; set; }
        public List<ProjectUser> ProjectUsers { get; set; }
        public List<Work> Works { get; set; }
        public List<Source> Sources { get; set; }

        public Project()
        {
            ProjectUsers = new List<ProjectUser>();
            Works = new List<Work>();
            Sources = new List<Source>();
        }
    }
}
