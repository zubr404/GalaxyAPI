using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.Models
{
    /// <summary>
    /// Представляет авторизационные данные пользователя
    /// </summary>
    public class UserProfile
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
}
