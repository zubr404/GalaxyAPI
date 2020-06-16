using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.DTO
{
    public class UserResponse
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string Login { get; set; }
        public int AccountNumber { get; set; }
        public double Amount { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
