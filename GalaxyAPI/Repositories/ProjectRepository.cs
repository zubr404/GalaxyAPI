using GalaxyAPI.DTO;
using GalaxyAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.Repositories
{
    public class ProjectRepository
    {
        readonly DataBaseContext db;
        public ProjectRepository(DataBaseContext dataBase)
        {
            db = dataBase;
        }

        public bool Exists(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return db.Projects.AsNoTracking().Any(x => x.Name == name);
            }
            else
            {
                throw new ArgumentException("name", "Параметр не содержит допустимого значения.");
            }
        }

        public bool ExistsLanguage(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return db.Languages.AsNoTracking().Any(x => x.Name == name);
            }
            else
            {
                throw new ArgumentException("name", "Параметр не содержит допустимого значения.");
            }
        }

        public GetProjectsResponse Get(GetProjectsRequest item)
        {
            var result = new GetProjectsResponse();
            result.CountElements = db.Projects.Count();

            var projects = db.Projects.Skip(item.Offset).Take(item.Limit);

            foreach (var project in projects)
            {
                result.Projects.Add(Mapping(project));
            }
            return result;
        }

        public GetProjectsResponse Get(int userId, GetProjectsRequest item)
        {
            var result = new GetProjectsResponse();
            result.CountElements = db.Projects.Where(p => p.ID == userId).Count();

            var projects = db.Projects
                .SelectMany(p => p.ProjectUsers, (p, u) => new { Project = p, ProjectUser = u })
                .Where(p => p.ProjectUser.UserID == 1).Select(p => p.Project);

            foreach (var project in projects)
            {
                result.Projects.Add(Mapping(project));
            }
            return result;
        }

        public ProjectResponse Create(ProjectRequest item)
        {
            var project = db.Projects.Add(new Project()
            {
                Name = item.Name,
                LangSource = item.LangSource,
                LangTarget = item.LangTarget,
                Thematics = item.Thematics
            });
            Save();

            return Mapping(project.Entity);
        }

        public ProjectResponse Update(int id, ProjectRequest item)
        {
            var project = db.Projects.FirstOrDefault(p => p.ID == id);
            if (project != null)
            {
                return Mapping(project);
            }
            return null;
        }

        public bool Delete(int id)
        {
            var project = db.Projects.AsNoTracking().FirstOrDefault(x => x.ID == id);
            if (project != null)
            {
                db.Projects.Remove(project);
                Save();
                return true;
            }
            return false;
        }

        private ProjectResponse Mapping(Project project)
        {
            if (project != null)
            {
                return new ProjectResponse()
                {
                    ID = project.ID,
                    Name = project.Name,
                    LangSource = project.LangSource,
                    LangTarget = project.LangTarget,
                    Thematics = project.Thematics
                };
            }
            else
            {
                throw new ArgumentNullException("project", "Параметр равен null.");
            }            
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
