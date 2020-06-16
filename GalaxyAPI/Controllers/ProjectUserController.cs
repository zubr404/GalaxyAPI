using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaxyAPI.DTO;
using GalaxyAPI.Repositories;
using GalaxyAPI.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyAPI.Controllers
{
    [Route("api/v1/projectusers/")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class ProjectUserController : ControllerBase
    {
        readonly ProjectUserRepository repository;
        public ProjectUserController(ProjectUserRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Добавление пользователей к проекту
        /// </summary>
        /// <param name="usersId"></param>
        /// <returns></returns>
        [HttpPost("users")]
        [Authorize(Roles = RolesResources.Administrator)]
        public ActionResult AttachUsers(UsersProjectRequest request)
        {
            return Ok(repository.AttachUsers(request));
        }

        /// <summary>
        /// Открепить пользователя от проекта
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("users")]
        [Authorize(Roles = RolesResources.Administrator)]
        public ActionResult UnpinUser(int userId)
        {
            if (repository.UnpinUser(userId))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}