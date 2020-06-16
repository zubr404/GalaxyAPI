using GalaxyAPI.DTO;
using GalaxyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.Repositories
{
    public class TargetRepository
    {
        readonly DataBaseContext db;
        public TargetRepository(DataBaseContext dataBase)
        {
            db = dataBase;
        }

        public void Create(IEnumerable<TargetDto> targetsDto)
        {
            if (targetsDto != null)
            {
                var targets = Mapping(targetsDto);
                if (targets.Count() > 0)
                {
                    db.Targets.AddRange(targets);
                    Save();
                }
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private IEnumerable<Target> Mapping(IEnumerable<TargetDto> targets)
        {
            var result = new List<Target>();
            if (targets != null)
            {
                foreach (var target in targets)
                {
                    result.Add(new Target()
                    {
                        ID = target.ID,
                        WorkID = target.WorkID,
                        SourceID = target.SourceID,
                        Text = target.Text,
                        Validated = target.Validated
                    });
                }
            }
            return result;
        }
    }
}
