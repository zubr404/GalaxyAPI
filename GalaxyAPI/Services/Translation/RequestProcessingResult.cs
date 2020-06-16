using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.Services.Translation
{
    public class RequestProcessingResult
    {
        public List<string> FilesAdded { get; set; }
        public List<FileNoAdded> FilesNoAdded { get; set; }

        public RequestProcessingResult()
        {
            FilesAdded = new List<string>();
            FilesNoAdded = new List<FileNoAdded>();
        }
    }
}
