using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.DTO
{
    public class UserAuthResponse
    {
        public int ID { get; set; }
        public string BirthDate { get; set; }
        public string AuthToken { get; set; }
        public string FullName { get; set; }
        public int RoleID { get; set; }
        public double Amount { get; set; }
    }
}
