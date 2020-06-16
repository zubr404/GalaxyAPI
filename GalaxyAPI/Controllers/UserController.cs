using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaxyAPI.DTO;
using GalaxyAPI.Models;
using GalaxyAPI.Repositories;
using GalaxyAPI.Resources;
using GalaxyAPI.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GalaxyAPI.Controllers
{
    [Route("api/v1/users/")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserRepository repository;

        public UserController(UserRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Получить авторизованного пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("authuser")]
        public ActionResult Get()
        {
            var response = new
            {
                Name = User.Identity.Name,
                BirthDate = User.Identity.GetUserBirthDate(),
                Amount = User.Identity.GetUserAmount<double>()
            };
            return Ok(response);
        }

        /// <summary>
        /// Получить всех клиентов с возможностью фильтрации по роли
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [Authorize(Roles = RolesResources.Administrator)]
        public ActionResult<GetUsersResponse> Get(GetUsersRequest request)
        {
            var response = repository.Get(request);
            if(response.Users.Count == 0)
            {
                return NotFound();
            }
            return Ok(response);
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost()]
        [Authorize(Roles = RolesResources.Administrator)]
        public ActionResult<UserResponse> Create(CreateUserRequest request)
        {
            if (!repository.Exists(request.Login))
            {
                var user = repository.Create(request);
                return Ok(user);
            }
            else
            {
                return BadRequest("Пользователь с таким login уже существует.");
            }
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut()]
        [Authorize(Roles = RolesResources.Administrator)]
        public ActionResult Update(int id, CreateUserRequest request)
        {
            var response = repository.Update(id, request);
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Authorize(Roles = RolesResources.Administrator)]
        public ActionResult Delete(int id)
        {
            if (repository.Delete(id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}