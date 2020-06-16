using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.DTO
{
    public class GetProjectsRequest
    {
        const int MAXLIMIT = 1000;

        private int limit = 10;
        public int Limit
        {
            get { return limit; }
            set
            {
                if (value < 1)
                {
                    limit = 1;
                }
                else if (value > MAXLIMIT)
                {
                    limit = MAXLIMIT;
                }
                else
                {
                    limit = value;
                }

            }
        }

        private int offset = 0;
        public int Offset
        {
            get { return offset; }
            set
            {
                if (value < 0)
                {
                    offset = 0;
                }
            }
        }
    }
}
