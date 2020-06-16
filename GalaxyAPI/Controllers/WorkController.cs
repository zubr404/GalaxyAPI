using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaxyAPI.DTO;
using GalaxyAPI.Repositories;
using GalaxyAPI.Resources;
using GalaxyAPI.Services.Translation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyAPI.Controllers
{
    [Route("api/v1/works/")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class WorkController : ControllerBase
    {
        readonly WorkRepository repository;
        readonly WorkFile workFile;

        public WorkController(WorkRepository repository, WorkFile workFile)
        {
            this.repository = repository;
            this.workFile = workFile;
        }



        /// <summary>
        /// Загрузить файл xlif
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost()]
        [Authorize(Roles = RolesResources.Administrator)]
        public ActionResult<RequestProcessingResult> UploadFile([FromForm]NewWorkRequest request)
        {
            return Ok(workFile.RequestProcessing(request));
        }
    }
}