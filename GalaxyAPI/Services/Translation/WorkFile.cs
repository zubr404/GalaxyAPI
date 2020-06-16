using GalaxyAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaxyAPI.Repositories;
using System.Xml.Linq;
using GalaxyAPI.Extension;
using GalaxyAPI.Translation;
using System.Xml;
using Microsoft.AspNetCore.Http;

namespace GalaxyAPI.Services.Translation
{
    public class WorkFile
    {
        readonly WorkRepository workRepository;
        readonly SourceRepository sourceRepository;
        readonly TargetRepository targetRepository;
        public WorkFile(WorkRepository workRepository, SourceRepository sourceRepository, TargetRepository targetRepository)
        {
            this.workRepository = workRepository;
            this.sourceRepository = sourceRepository;
            this.targetRepository = targetRepository;
        }

        public RequestProcessingResult RequestProcessing(NewWorkRequest request)
        {
            var result = new RequestProcessingResult();
            var newWorkData = new NewWorkData();
            newWorkData.ProjectID = request.ProjectID;

            foreach (var file in request.Files)
            {
                var document = GetXDocument(file, result);

                if(document != null && NoExistsFile(file.FileName, result))
                {
                    // создание нового Work
                    var workId = Guid.NewGuid();
                    var work = new WorkDto()
                    {
                        ID = workId,
                        ProjectID = request.ProjectID,
                        Name = file.FileName,
                        UploadDate = DateTime.Now.ToString(),
                        ModifiedDate = DateTime.Now.ToString(),
                        Validated = false,
                        Data = document.ToString().Replace("'", "''")
                    };

                    newWorkData.Works.Add(work);

                    var sources = newWorkData.Sources;
                    var countSources = sources?.Count() == null ? 0 : sources.Count();
                    var transUnits = document.GetTransUnits();

                    foreach (var transUnit in transUnits)
                    {
                        var clearTarget = transUnit.Target.ReplaceSpace();
                        if (countSources == 0)
                        {
                            var sourceid = Guid.NewGuid();
                            newWorkData.Sources.Add(new SourceDto()
                            {
                                ID = sourceid,
                                LocalID = transUnit.LocalID,
                                ProjectID = request.ProjectID,
                                Text = transUnit.Source
                            });
                            newWorkData.Targets.Add(new TargetDto()
                            {
                                SourceID = sourceid,
                                WorkID = workId,
                                Text = clearTarget,
                                Validated = false
                            });
                        }
                        else if (countSources > 0)
                        {
                            var source = newWorkData.Sources.FirstOrDefault(s => s.Text == transUnit.Source && s.LocalID == transUnit.LocalID);
                            if (source != null)
                            {
                                newWorkData.Targets.Add(new TargetDto()
                                {
                                    SourceID = source.ID,
                                    WorkID = workId,
                                    Text = clearTarget,
                                    Validated = false
                                });
                            }
                        }
                    }
                    result.FilesAdded.Add(file.FileName);
                }
            }
            InsertEntities(newWorkData);
            return result;
        }

        private XDocument GetXDocument(IFormFile file, RequestProcessingResult requestResult)
        {
            XDocument document = null;
            try
            {
                document = XDocument.Load(file.OpenReadStream());
            }
            catch (XmlException ex)
            {
                requestResult.FilesNoAdded.Add(new FileNoAdded() { Name = file.FileName, ErrorMessage = ex.Message });
            }
            return document;
        }

        private bool NoExistsFile(string fileName, RequestProcessingResult requestResult)
        {
            if (workRepository.Exists(fileName))
            {
                requestResult.FilesNoAdded.Add(new FileNoAdded() { Name = fileName, ErrorMessage = "Данный файл уже содержится в системе." });
                return false;
            }
            return true;
        }

        private void InsertEntities(NewWorkData datas)
        {
            workRepository.Create(datas.Works);
            if (!sourceRepository.Exists(datas.ProjectID))
            {
                sourceRepository.Create(datas.Sources);
            }
            targetRepository.Create(datas.Targets);
        }
    }
}
