using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.DTO
{
    public class GetUsersResponse
    {
        public int CountElements { get; set; }
        public List<UserResponse> Users { get; set; }
    }
}
