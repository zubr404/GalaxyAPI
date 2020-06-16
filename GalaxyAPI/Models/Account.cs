using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.Models
{
    /// <summary>
    /// Счет пользователя
    /// </summary>
    public class Account
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public double Amount { get; set; }
    }
}
