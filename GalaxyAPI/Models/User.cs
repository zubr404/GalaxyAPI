using GalaxyAPI.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.Models
{
    /// <summary>
    /// Представляет пользователя системы
    /// </summary>
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserProfile UserProfile { get; set; }
        public Account Account { get; set; }
        public List<ProjectUser> ProjectUsers { get; set; }

        public User()
        {
            ProjectUsers = new List<ProjectUser>();
        }
    }
}
