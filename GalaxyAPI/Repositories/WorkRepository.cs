using GalaxyAPI.DTO;
using GalaxyAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GalaxyAPI.Repositories
{
    public class WorkRepository
    {
        readonly DataBaseContext db;
        public WorkRepository(DataBaseContext dataBase)
        {
            db = dataBase;
        }

        public bool Exists(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return db.Works.AsNoTracking().Any(x => x.Name == name);
            }
            else
            {
                throw new ArgumentException("name", "Параметр не содержит допустимого значения.");
            }
        }

        public void Create(IEnumerable<WorkDto> worksDto)
        {
            if(worksDto != null)
            {
                var works = Mapping(worksDto);
                if(works.Count() > 0)
                {
                    db.Works.AddRange(works);
                    Save();
                }
            }
        }

        public bool Delete(Guid id)
        {
            var work = db.Works.AsNoTracking().FirstOrDefault(x => x.ID == id);
            if (work != null)
            {
                db.Works.Remove(work);
                Save();
                return true;
            }
            return false;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private IEnumerable<Work> Mapping(IEnumerable<WorkDto> works)
        {
            var result = new List<Work>();
            if (works != null)
            {
                foreach (var work in works)
                {
                    result.Add(new Work()
                    {
                        ID = work.ID,
                        ProjectID = work.ProjectID,
                        Name = work.Name,
                        UploadDate = work.UploadDate,
                        ModifiedDate = work.ModifiedDate,
                        Data = work.Data,
                        Validated = work.Validated
                    });
                }
            }
            return result;
        }
    }
}
