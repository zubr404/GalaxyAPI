using GalaxyAPI.DTO;
using GalaxyAPI.Extension;
using GalaxyAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.Repositories
{
    public class UserRepository
    {
        readonly DataBaseContext db;
        public UserRepository(DataBaseContext dataBase)
        {
            db = dataBase;
        }

        public User Get(int id)
        {
            return db.Users.FirstOrDefault(x => x.ID == id);
        }

        public UserAuthResponse Get(string login, string password)
        {
            var user = db.Users
                .Include(u => u.UserProfile)
                .Include(u => u.Account)
                .Where(u => u.Login == login && u.Password == password.SHA())
                .FirstOrDefault();

            if (user != null)
            {
                var result = new UserAuthResponse()
                {
                    ID = user.ID,
                    BirthDate = user.UserProfile.BirthDate,
                    FullName = user.UserProfile.FullName,
                    RoleID = user.UserProfile.RoleID,
                    Amount = user.Account.Amount
                };
                return result;
            }
            return null;
        }

        public GetUsersResponse Get(GetUsersRequest request)
        {
            var result = new GetUsersResponse();
            IQueryable<User> users;

            if (request.RoleID.HasValue)
            {
                users = db.Users.AsNoTracking()
                .Include(u => u.UserProfile)
                    .ThenInclude(p => p.Role)
                .Include(u => u.Account)
                .Where(u => u.UserProfile.RoleID == request.RoleID.Value).OrderBy(u => u.ID)
                .Skip(request.Offset).Take(request.Limit);

                result.CountElements = db.Users
                    .Include(u => u.UserProfile)
                    .Where(u => u.UserProfile.RoleID == request.RoleID.Value).Count();
            }
            else
            {
                users = db.Users.AsNoTracking()
                .Include(u => u.UserProfile)
                    .ThenInclude(p => p.Role)
                .Include(u => u.Account).OrderBy(u => u.ID)
                .Skip(request.Offset).Take(request.Limit);

                result.CountElements = db.Users.Count();
            }

            var usersResult = new List<UserResponse>();
            foreach (var item in users)
            {
                usersResult.Add(new UserResponse()
                {
                    UserID = item.ID,
                    FullName = item.UserProfile.FullName,
                    BirthDate = item.UserProfile.BirthDate,
                    Login = item.Login,
                    AccountNumber = item.Account.ID,
                    Amount = item.Account.Amount,
                    RoleID = item.UserProfile.RoleID,
                    RoleName = item.UserProfile.Role.Name
                });
            }

            result.Users = usersResult;
            return result;
        }

        public bool Exists(string login)
        {
            if (!string.IsNullOrEmpty(login))
            {
                return db.Users.AsNoTracking().Any(x => x.Login == login);
            }
            else
            {
                throw new ArgumentException("login", "Параметр не содержит допустимого значения.");
            }
        }

        public UserResponse Create(CreateUserRequest item)
        {
            var user = db.Users.Add(new User()
            {
                Login = item.Login,
                Password = item.Password.SHA()
            });
            Save();

            user.Entity.UserProfile = new UserProfile()
            {
                UserID = user.Entity.ID,
                FullName = item.FullName,
                BirthDate = item.BirthDate,
                RoleID = item.RoleID
            };

            user.Entity.Account = new Account()
            {
                Amount = 1000
            };
            Save();

            return new UserResponse() 
            {
                UserID = user.Entity.ID,
                FullName = user.Entity.UserProfile.FullName,
                BirthDate = user.Entity.UserProfile.BirthDate,
                Login = user.Entity.Login,
                AccountNumber = user.Entity.Account.ID,
                Amount = user.Entity.Account.Amount,
                RoleID = user.Entity.UserProfile.RoleID,
                RoleName = db.Users.Include(u => u.UserProfile)
                    .ThenInclude(p => p.Role).Where(x => x.ID == user.Entity.ID)
                    .Select(u => new { Name = u.UserProfile.Role.Name }).FirstOrDefault().Name
            };
        }

        public UserResponse Update(int id, CreateUserRequest item)
        {
            var user = db.Users
                .Include(u => u.UserProfile)
                    .ThenInclude(p => p.Role)
                .Include(u => u.Account).FirstOrDefault(x => x.ID == id);
            if (user != null)
            {
                user.UserProfile.FullName = item.FullName;
                user.UserProfile.BirthDate = item.BirthDate;
                user.Login = item.Login;
                user.Password = item.Password.SHA();
                user.UserProfile.RoleID = item.RoleID;
                Save();

                var result = new UserResponse()
                {
                    UserID = user.ID,
                    FullName = user.UserProfile.FullName,
                    BirthDate = user.UserProfile.BirthDate,
                    Login = user.Login,
                    AccountNumber = user.Account.ID,
                    Amount = user.Account.Amount,
                    RoleID = user.UserProfile.RoleID,
                    RoleName = db.Users.Include(u => u.UserProfile)
                    .ThenInclude(p => p.Role).Where(x => x.ID == user.ID)
                    .Select(u => new { Name = u.UserProfile.Role.Name }).FirstOrDefault().Name
                };
                return result;
            }
            return null;
        }

        public bool Delete(int id)
        {
            var user = db.Users.AsNoTracking().FirstOrDefault(x => x.ID == id);
            if(user != null)
            {
                db.Users.Remove(user);
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
