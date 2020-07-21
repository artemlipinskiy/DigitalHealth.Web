using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DigitalHealth.GlobalInterfaces;
using DigitalHealth.Services;
using DigitalHealth.Web.EntitiesDto;

namespace DigitalHealth.Web.Services
{
    

    public class DiagnosticService : IDiagnosticService
    {
        private readonly ILogger _logger;

        public DiagnosticService(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<List<DiagnosticResultDto>> GetResult(List<Guid> SymptomIds)
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    var AllDisease = await db.Diseases.Include(d => d.ICD).Include(d => d.Symptoms).ToListAsync();
                    List<DiagnosticResultDto> result = new List<DiagnosticResultDto>();
                    foreach (var disease in AllDisease)
                    {
                        DiagnosticResultDto currnetresult = null;
                        foreach (var diseaseSymptom in disease.Symptoms)
                        {
                            if (SymptomIds.Contains(diseaseSymptom.Id))
                            {
                                if (currnetresult == null)
                                {
                                    currnetresult = new DiagnosticResultDto();
                                    currnetresult.NumberOfCoincidences = 1;
                                    var diseaseresult = new DiseaseDto
                                    {
                                        Id = disease.Id,
                                        Description = disease.Description,
                                        ICDID = disease.ICDID,
                                        ICDName = disease.ICD.Name,
                                        Name = disease.Name,
                                        SymptomIds = disease.Symptoms.Select(s => s.Id).ToList(),
                                        SymptomNames = disease.Symptoms.Select(s => s.Name).ToList()
                                    };
                                    currnetresult.Disease = diseaseresult;
                                }
                                else
                                {
                                    currnetresult.NumberOfCoincidences++;
                                }
                            }
                        }

                        if (currnetresult != null)
                        {
                            result.Add(currnetresult);
                        }
                    }
                    return result.OrderByDescending(r => r.NumberOfCoincidences).ToList();
                }
            }
            catch (Exception exc)
            {
                _logger.Error($"Failed get result for diagnostic : {exc}");
                throw;
            }
           
        }

        public async Task<List<MethodOfTreatmentDto>> FindMethods(Guid DiseaseId)
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    return await db.MethodOfTreatments.Include(m => m.Disease).Where(m => m.DiseaseId == DiseaseId).Select(m => new MethodOfTreatmentDto
                    {
                        Id = m.Id,
                        Description = m.Description,
                        DiseaseId = m.DiseaseId,
                        DiseaseName = m.Disease.Name,
                        Source = m.Source,
                        Title = m.Title
                    }).ToListAsync();
                }
            }
            catch (Exception exc)
            {
                _logger.Error($"Failed find methods for disease {DiseaseId} :{exc}");
                throw;
            }
           
        }
    }
}