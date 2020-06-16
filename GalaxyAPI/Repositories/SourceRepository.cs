using GalaxyAPI.DTO;
using GalaxyAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.Repositories
{
    public class SourceRepository
    {
        readonly DataBaseContext db;
        public SourceRepository(DataBaseContext dataBase)
        {
            db = dataBase;
        }

        public bool Exists(int projectID)
        {
            return db.Sources.AsNoTracking().Any();
        }

        public void Create(IEnumerable<SourceDto> sourcesDto)
        {
            if(sourcesDto != null)
            {
                var sources = Mapping(sourcesDto);
                if(sources.Count() > 0)
                {
                    db.Sources.AddRange(sources);
                    Save();
                }
            }
        }

        private IEnumerable<Source> Mapping(IEnumerable<SourceDto> sources)
        {
            var result = new List<Source>();
            if (sources != null)
            {
                foreach (var source in sources)
                {
                    result.Add(new Source()
                    {
                        ID = source.ID,
                        ProjectID = source.ProjectID,
                        Text = source.Text
                    });
                }
            }
            return result;
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
