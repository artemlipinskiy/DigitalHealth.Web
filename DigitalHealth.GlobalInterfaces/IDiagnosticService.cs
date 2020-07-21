using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalHealth.Web.EntitiesDto;

namespace DigitalHealth.GlobalInterfaces
{
    public interface IDiagnosticService
    {
        Task<List<DiagnosticResultDto>> GetResult(List<Guid> SymptomIds);
        Task<List<MethodOfTreatmentDto>> FindMethods(Guid DiseaseId);
    }
}
