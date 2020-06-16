using GalaxyAPI.DTO;
using GalaxyAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.Repositories
{
    public class ProjectUserRepository
    {
        readonly DataBaseContext db;
        public ProjectUserRepository(DataBaseContext dataBase)
        {
            db = dataBase;
        }

        public UsersProjectResponse AttachUsers(UsersProjectRequest item)
        {
            var result = new UsersProjectResponse();

            var usersID = db.ProjectUsers.Where(p => p.ProjectID == item.ProjectID).Select(p => p.UserID).ToList();
            var overlaps = usersID.Intersect(item.UsersID);

            foreach (var id in overlaps)
            {
                item.UsersID.Remove(id);
                result.NotAdded.Add(id);
            }

            var usersAdded = new List<ProjectUser>();
            foreach (var id in item.UsersID)
            {
                usersAdded.Add(new ProjectUser()
                {
                    ProjectID = item.ProjectID,
                    UserID = id
                });
            }

            if (usersAdded.Count > 0)
            {
                db.ProjectUsers.AddRange(usersAdded);
                Save();
                result.Message = "Пользователи успешно прикреплены к проекту.";
            }

            if (result.NotAdded.Count > 0)
            {
                result.Message = "Часть пользователей уже прикреплены к проекту.";
            }
            return result;
        }

        public bool UnpinUser(int userId)
        {
            var projectUser = db.ProjectUsers.AsNoTracking().FirstOrDefault(x => x.UserID == userId);
            if (projectUser != null)
            {
                db.ProjectUsers.Remove(projectUser);
                Save();
                return true;
            }
            return false;
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
