using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.Services.Translation
{
    /// <summary>
    /// Представляет секцию trans-unit xlif-файла
    /// </summary>
    public class TransUnit
    {
        public int ID { get; set; }
        public int LocalID { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
    }
}
