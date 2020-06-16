using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.DTO
{
    public class UsersProjectResponse
    {
        public string Message { get; set; }
        public List<int> NotAdded { get; set; }

        public UsersProjectResponse()
        {
            NotAdded = new List<int>();
        }
    }
}
