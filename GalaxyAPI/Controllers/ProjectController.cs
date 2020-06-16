using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaxyAPI.DTO;
using GalaxyAPI.Repositories;
using GalaxyAPI.Resources;
using GalaxyAPI.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyAPI.Controllers
{
    [Route("api/v1/projects/")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        readonly ProjectRepository repository;
        public ProjectController(ProjectRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Получить все проекты для авторизованного пользователя с учетом его роли
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult<GetProjectsResponse> Get(GetProjectsRequest request)
        {
            var roleId = User.Identity.GetUserRoleId<int>();
            var userId = User.Identity.GetUserId<int>();

            if (roleId == int.Parse(RolesResources.Administrator))
            {
                return Ok(repository.Get(request));
            }
            else
            {
                return Ok(repository.Get(userId, request));
            }
        }

        /// <summary>
        /// Созданиее нового проекта
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost()]
        [Authorize(Roles = RolesResources.Administrator)]
        public ActionResult Create(ProjectRequest request)
        {
            if (!repository.Exists(request.Name))
            {
                if (!repository.ExistsLanguage(request.LangSource))
                {
                    return BadRequest("LangSource: Данного языка нет в системе.");
                }
                if (!repository.ExistsLanguage(request.LangTarget))
                {
                    return BadRequest("LangTarget: Данного языка нет в системе.");
                }
                var project = repository.Create(request);
                return Ok(project);
            }
            else
            {
                return BadRequest("Проект с таким именем уже существует.");
            }
        }

        /// <summary>
        /// Обновление проекта
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut()]
        [Authorize(Roles = RolesResources.Administrator)]
        public ActionResult<ProjectResponse> Update(int id, ProjectRequest request)
        {
            var response = repository.Update(id, request);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        /// <summary>
        /// Удаляет проект
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Authorize(Roles = RolesResources.Administrator)]
        public ActionResult Delete(int Id)
        {
            if (repository.Delete(Id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}