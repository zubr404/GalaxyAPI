using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaxyAPI.DTO;
using GalaxyAPI.Extension;
using GalaxyAPI.Models;
using GalaxyAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GalaxyAPI.Controllers
{
    [Route("api/v1/test/")]
    [ApiController]
    public class TestController : ControllerBase
    {
        readonly DataBaseContext db;

        public TestController(DataBaseContext dataBase, UserRepository userRepository)
        {
            this.db = dataBase;

            #region Role
            if (db.Roles.Count() == 0)
            {
                var admin = new Role() { Name = "Администратор" };
                var client = new Role() { Name = "Клиент" };
                var editor = new Role() { Name = "Редактор" };
                db.Roles.AddRange(new List<Role>()
                {
                    admin, client, editor
                });
            }
            #endregion

            #region Language
            if (db.Languages.Count() == 0)
            {
                var rus = new Language() { Name = "Русский" };
                var eng = new Language() { Name = "Английский" };
                var fra = new Language() { Name = "Французский" };
                db.Languages.AddRange(new List<Language>()
                {
                    rus, eng, fra
                });
            }
            #endregion
            db.SaveChanges();

            #region Users
            if (db.Users.Count() == 0)
            {
                var newUser1 = new CreateUserRequest()
                {
                    FullName = "Иванов Иван Иванович",
                    BirthDate = "1985-04-01",
                    Login = "iii",
                    Password = "123456",
                    RoleID = 1
                };
                var newUser2 = new CreateUserRequest()
                {
                    FullName = "Петров Петр Петрович",
                    BirthDate = "1977-03-08",
                    Login = "ppp",
                    Password = "123456",
                    RoleID = 2
                };
                var newUser3 = new CreateUserRequest()
                {
                    FullName = "Сидоров Сидр Сидорович",
                    BirthDate = "1993-09-01",
                    Login = "sss",
                    Password = "123456",
                    RoleID = 3
                };

                userRepository.Create(newUser1);
                userRepository.Create(newUser2);
                userRepository.Create(newUser3);
            }
            #endregion
        }

        [HttpGet("get")]
        public async Task<ActionResult> Get()
        {
            //var users  = dataBase.Users;
            //return Ok(users);
            return Ok("ALL OK!");
        }

        [HttpPost("createuser")]
        public async Task<ActionResult> CreateUser(User user)
        {
            UserRepository userRepository = new UserRepository(db);

            /*if (user.BirthDate != "1979-07-24")
            {
                ModelState.AddModelError("BirthDate", $"Error format: {user.BirthDate}");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await userRepository.Create(user);*/
            return Ok();
        }
    }
}